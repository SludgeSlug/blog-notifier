module Email

    open System.Net.Mail

    let sendEmail = 
        let msg = new MailMessage("blog-notifier@test.com",
                                  "blog-notifier@mailinator.com",
                                  "Test email subject",
                                  "Test email body")

        let client = new SmtpClient(@"smtp.mailinator.com")
        client.DeliveryMethod <- SmtpDeliveryMethod.Network

        client.Send(msg)