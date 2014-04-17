module Email

    open System
    open System.Net.Mail
    open System.Net
    open System.IO
    open System.Globalization
    open System.Xml
    open System.Xml.Xsl
    
    let readNoUpdatesEmailHtml =
        use sr = new StreamReader("no_updates.htm")
        sr.ReadToEnd()

    let transformXml xml =
        let transform = new XslCompiledTransform()
        transform.Load("blogPosts.xslt")
        
        use inputReader = new StringReader(xml)
        use inputXmlReader = XmlReader.Create(inputReader)

        use writer = new StringWriter()
        transform.Transform(inputXmlReader, new XsltArgumentList(), writer)

        writer.GetStringBuilder().ToString()

    let currentYear =
        DateTime.Now.Year

    let currentWeek =
        let dfi = DateTimeFormatInfo.CurrentInfo
        dfi.Calendar            
            .GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek)

    let getEmailBody =
        let url = sprintf "http://blogs.nhs.uk/choices-internal/%d/feed?w=%d" currentYear currentWeek

        let req = WebRequest.Create(url)
        req.Method <- "GET"

        try
            let reader = new StreamReader(req.GetResponse().GetResponseStream())

            transformXml(reader.ReadToEnd().ToString())
        with
        | :? WebException as webEx when (webEx.Response :? HttpWebResponse) ->
            
            let httpWebResponse = webEx.Response :?> HttpWebResponse

            match httpWebResponse.StatusCode with
            | HttpStatusCode.NotFound ->
                readNoUpdatesEmailHtml
            | otherCode ->
                raise webEx

    let sendEmail = 
        let msg = new MailMessage("blog-notifier@test.com",
                                  "blog-notifier@mailinator.com",
                                  "Test email subject",
                                  getEmailBody)

        let client = new SmtpClient(@"smtp.mailinator.com")
        client.DeliveryMethod <- SmtpDeliveryMethod.Network

        client.Send(msg)