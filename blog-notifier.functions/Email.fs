module Email

    open System
    open System.Net.Mail
    open System.Net
    open System.IO
    open System.Globalization
    
    let currentYear =
        //DateTime.Now.Year
        2013

    let currentWeek =
        let dfi = DateTimeFormatInfo.CurrentInfo
        dfi.Calendar            
            //.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek)
            .GetWeekOfYear(new DateTime(2013,12, 29), dfi.CalendarWeekRule, dfi.FirstDayOfWeek)

    let getXmlFromBlog =
        let url = sprintf "http://blogs.nhs.uk/choices-internal/%d/feed?w=%d" currentYear currentWeek

        let req = WebRequest.Create(url)
        req.Method <- "GET"

        let reader = new StreamReader(req.GetResponse().GetResponseStream())

        reader.ReadToEnd().ToString()

    let sendEmail = 
        let msg = new MailMessage("blog-notifier@test.com",
                                  "blog-notifier@mailinator.com",
                                  "Test email subject",
                                  getXmlFromBlog)

        let client = new SmtpClient(@"smtp.mailinator.com")
        client.DeliveryMethod <- SmtpDeliveryMethod.Network

        client.Send(msg)