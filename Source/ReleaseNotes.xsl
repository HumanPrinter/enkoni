<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="text" indent="no" encoding="UTF-8" omit-xml-declaration="yes"/>
  <xsl:template match="/">
    <xsl:text>###################################################
# Release Notes for Enkoni.Framework assemblies   #
# Author is Oscar Brouwer unless stated otherwise #
# Legend:                                         #
# [*]: Change                                     #
# [+]: Addition                                   #
# [-]: Removal                                    #
###################################################&#10;&#10;</xsl:text>
    <xsl:for-each select="enkoni/releases/release">
      <xsl:text>Version </xsl:text>
      <xsl:value-of select="@version"/>
      <xsl:text>&#10;</xsl:text>
      <xsl:if test="count(remark) = 1">
	      <xsl:text>  </xsl:text>
        <xsl:value-of select="remark"/>
        <xsl:text>&#10;</xsl:text>
        <xsl:text>&#10;</xsl:text>
	    </xsl:if>
	    <xsl:for-each select="projects/project">
	      <xsl:sort select="@name"/>
          <xsl:text xml:space="preserve">  </xsl:text>
          <xsl:value-of select="@name"/>
          <xsl:text> (</xsl:text>
          <xsl:value-of select="@version"/>
          <xsl:if test="@versionPostfix">
            <xsl:text>-</xsl:text>
            <xsl:value-of select="@versionPostfix"/>
          </xsl:if>
          <xsl:text>)&#10;</xsl:text>
          <xsl:for-each select="updates/update">
            <xsl:sort select="@date" order="descending"/>
            <xsl:text xml:space="preserve">  </xsl:text>
            <xsl:choose>
              <xsl:when test="@type = 'addition'"><xsl:text>[+] </xsl:text></xsl:when>
              <xsl:when test="@type = 'change'"><xsl:text>[*] </xsl:text></xsl:when>
              <xsl:when test="@type = 'removal'"><xsl:text>[-] </xsl:text></xsl:when>
            </xsl:choose>
		        <xsl:value-of select="summary"/><xsl:text>&#10;</xsl:text>
		        <xsl:text>      Date:    </xsl:text><xsl:value-of select="@date"/><xsl:text>&#10;</xsl:text>
		        <xsl:text>      Comment: </xsl:text><xsl:value-of select="comment"/><xsl:text>&#10;</xsl:text>
		        <xsl:text>&#10;</xsl:text>
        </xsl:for-each>
      </xsl:for-each>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>