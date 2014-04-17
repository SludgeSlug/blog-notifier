## Blog notifier ##

### Introduction ###

The blog notifier will query the NHS Choices developer blog and construct an email detailing any new blog posts from the past week.

### Current functionality ###

- To run the code, compile the code in Visual studio 2012. You can then either run the code through visual studio by pressing F5 or run the executable located at blog-notifier\blog-notifier\bin\Debug\blog-notifier.exe.
- When running the code an email will be sent to the email address blog-notifier@mailinator.com. This can be accessed by going to the URL blog-notifier.mailinator.com
- If no updates are found then the content of the email will contain a message informing that there are no new blog updates. If some updates are found then the email will contain a list of blog entries from the past week