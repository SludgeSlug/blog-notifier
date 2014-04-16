module Email

    open System
    open System.Net.Mail
    open System.Net
    open System.IO
    open System.Globalization
    
    let currentYear =
        DateTime.Now.Year

    let currentWeek =
        let dfi = DateTimeFormatInfo.CurrentInfo
        dfi.Calendar            
            .GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek)

    let getXmlFromBlog =
        let url = sprintf "http://blogs.nhs.uk/choices-internal/%d/feed?w=%d" currentYear currentWeek

        let req = WebRequest.Create(url)
        req.Method <- "GET"

        try
            let reader = new StreamReader(req.GetResponse().GetResponseStream())

            reader.ReadToEnd().ToString()
        with
        | :? WebException as webEx when (webEx.Response :? HttpWebResponse) ->
            
            let httpWebResponse = webEx.Response :?> HttpWebResponse

            match httpWebResponse.StatusCode with
            | HttpStatusCode.NotFound ->
                "No blog updates this week"
            | otherCode ->
                raise webEx

    let sendEmail = 
        let msg = new MailMessage("blog-notifier@test.com",
                                  "blog-notifier@mailinator.com",
                                  "Test email subject",
                                  getXmlFromBlog)

        let client = new SmtpClient(@"smtp.mailinator.com")
        client.DeliveryMethod <- SmtpDeliveryMethod.Network

        client.Send(msg)