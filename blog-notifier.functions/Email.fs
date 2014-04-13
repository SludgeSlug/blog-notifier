module Email

    open System.Net.Mail
    open System.Net
    open System.IO
    
    let getXmlFromBlog =
        let url = "http://blogs.nhs.uk/choices-blog/feed/"

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