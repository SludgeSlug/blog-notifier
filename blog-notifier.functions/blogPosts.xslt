<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html"/>
	<xsl:template match="rss/channel">
    <html>
      <head>
        <link rel='stylesheet' id='style-css'  href='http://blogs.nhs.uk/choices-internal/wp-content/themes/cleanr/style.css?ver=3.8.3' type='text/css' media='all' />
        <title>NHS Choices internal blog update</title>
      </head>
      <body>
        <div class="container_16">
          <div id="header" class="grid_16">
            <h1 class="site-title">NHS Choices internal blog update</h1>
          </div>
          <hr class="grid_16" />
            <div class="grid_16">
              <br />
              <h2>New posts this week</h2>
              <ul>
                <xsl:apply-templates select="item"/>
              </ul>
            </div>
            <hr class="grid_16" />
              <div id="footer" class="grid_16">
                You can access the blog at <a href='http://blogs.nhs.uk/choices-internal/'>http://blogs.nhs.uk/choices-internal/</a>. Feel free to post any updates which you think may be of interest to the choices team.
              </div>
            </div>
      </body>
    </html>
	</xsl:template>
	<xsl:template match="item">
		<li>
      <xsl:element name ="a">
        <xsl:attribute name="href">
          <xsl:value-of select="link"/>
        </xsl:attribute>
        <xsl:value-of select="title"/>
      </xsl:element>
    </li>
	</xsl:template>
</xsl:stylesheet>