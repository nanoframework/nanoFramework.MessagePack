// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.



using System.Collections;
using System;
using nanoFramework.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace nanoFramework.MessagePack.Benchmark.Data
{
    internal class ComparativeTestObjects
    {
        internal ComparativeTestObjects()
        {
            TestObject = (TestObjectClass)JsonConvert.DeserializeObject(TestJson, typeof(TestObjectClass));
            TestObjectMsgPackBytes = MessagePackSerializer.Serialize(TestObject);
            TestObjectBinaryBytes = BinaryFormatter.Serialize(TestObject);
        }
        internal const string TestJson = @"{
	""statusUpdateTime"": ""0001-01-01T00:00:00Z"",
""lastActivityTime"": ""2021-06-03T05:52:41.4683112Z"",
""web_app"": {
  ""servlet"": [   
    {
      ""servlet_name"": ""cofaxCDS"",
      ""servlet_class"": ""org.cofax.cds.CDSServlet"",
      ""init_param"": {
        ""configGlossaryInstallationAt"": ""Philadelphia, PA"",
        ""configGlossaryAdminEmail"": ""ksm@pobox.com"",
        ""configGlossaryPoweredBy"": ""Cofax"",
        ""configGlossaryPoweredByIcon"": ""/images/cofax.gif"",
        ""configGlossaryPtaticPath"": ""/content/static"",
        ""templateProcessorClass"": ""org.cofax.WysiwygTemplate"",
        ""templateLoaderClass"": ""org.cofax.FilesTemplateLoader"",
        ""templatePath"": ""templates"",
        ""templateOverridePath"": """",
        ""defaultListTemplate"": ""listTemplate.htm"",
        ""defaultFileTemplate"": ""articleTemplate.htm"",
        ""useJSP"": false,
        ""jspListTemplate"": ""listTemplate.jsp"",
        ""jspFileTemplate"": ""articleTemplate.jsp"",
        ""cachePackageTagsTrack"": 200,
        ""cachePackageTagsStore"": 200,
        ""cachePackageTagsRefresh"": 60,
        ""cacheTemplatesTrack"": 100,
        ""cacheTemplatesStore"": 50,
        ""cacheTemplatesRefresh"": 15,
        ""cachePagesTrack"": 200,
        ""cachePagesStore"": 100,
        ""cachePagesRefresh"": 10,
        ""cachePagesDirtyRead"": 10,
        ""searchEngineListTemplate"": ""forSearchEnginesList.htm"",
        ""searchEngineFileTemplate"": ""forSearchEngines.htm"",
        ""searchEngineRobotsDb"": ""WEB_INF/robots.db"",
        ""useDataStore"": true,
        ""dataStoreClass"": ""org.cofax.SqlDataStore"",
        ""redirectionClass"": ""org.cofax.SqlRedirection"",
        ""dataStoreName"": ""cofax"",
        ""dataStoreDriver"": ""com.microsoft.jdbc.sqlserver.SQLServerDriver"",
        ""dataStoreUrl"": ""jdbc:microsoft:sqlserver://LOCALHOST:1433;DatabaseName=goon"",
        ""dataStoreUser"": ""sa"",
        ""dataStorePassword"": ""dataStoreTestQuery"",
        ""dataStoreTestQuery"": ""SET NOCOUNT ON;select test='test';"",
        ""dataStoreLogFile"": ""/usr/local/tomcat/logs/datastore.log"",
        ""dataStoreInitConns"": 10,
        ""dataStoreMaxConns"": 100,
        ""dataStoreConnUsageLimit"": 100,
        ""dataStoreLogLevel"": ""debug"",
        ""maxUrlLength"": 500}},
    {
      ""servlet_name"": ""cofaxEmail"",
      ""servlet_class"": ""org.cofax.cds.EmailServlet"",
      ""init_param"": {
      ""mailHost"": ""mail1"",
      ""mailHostOverride"": ""mail2""}},
    {
      ""servlet_name"": ""cofaxAdmin"",
      ""servlet_class"": ""org.cofax.cds.AdminServlet""},
 
    {
      ""servlet_name"": ""fileServlet"",
      ""servlet_class"": ""org.cofax.cds.FileServlet""},
    {
      ""servlet_name"": ""cofaxTools"",
      ""servlet_class"": ""org.cofax.cms.CofaxToolsServlet"",
      ""init_param"": {
        ""templatePath"": ""toolstemplates/"",
        ""log"": 1,
        ""logLocation"": ""/usr/local/tomcat/logs/CofaxTools.log"",
        ""logMaxSize"": """",
        ""dataLog"": 1,
        ""dataLogLocation"": ""/usr/local/tomcat/logs/dataLog.log"",
        ""dataLogMaxSize"": """",
        ""removePageCache"": ""/content/admin/remove?cache=pages&id="",
        ""removeTemplateCache"": ""/content/admin/remove?cache=templates&id="",
        ""fileTransferFolder"": ""/usr/local/tomcat/webapps/content/fileTransferFolder"",
        ""lookInContext"": 1,
        ""adminGroupID"": 4,
        ""betaServer"": true}}],
  ""servlet_mapping"": {
    ""cofaxCDS"": ""/"",
    ""cofaxEmail"": ""/cofaxutil/aemail/*"",
    ""cofaxAdmin"": ""/admin/*"",
    ""fileServlet"": ""/static/*"",
    ""cofaxTools"": ""/tools/*""},
 
  ""taglib"": {
    ""taglib_uri"": ""cofax.tld"",
    ""taglib_location"": ""/WEB_INF/tlds/cofax.tld""}}}";

        //Init and test MsgPack serialize
        internal static TestObjectClass TestObject { get; set; }

        internal static byte[] TestObjectMsgPackBytes { get; set; }

        internal static byte[] TestObjectBinaryBytes { get; set; }
    }

    [Serializable]
    public class InitParam
    {
        public string configGlossaryInstallationAt { get; set; }
        public string configGlossaryAdminEmail { get; set; }
        public string configGlossaryPoweredBy { get; set; }
        public string configGlossaryPoweredByIcon { get; set; }
        public string configGlossaryPtaticPath { get; set; }
        public string templateProcessorClass { get; set; }
        public string templateLoaderClass { get; set; }
        public string templatePath { get; set; }
        public string templateOverridePath { get; set; }
        public string defaultListTemplate { get; set; }
        public string defaultFileTemplate { get; set; }
        public bool useJSP { get; set; }
        public string jspListTemplate { get; set; }
        public string jspFileTemplate { get; set; }
        public int cachePackageTagsTrack { get; set; }
        public int cachePackageTagsStore { get; set; }
        public int cachePackageTagsRefresh { get; set; }
        public int cacheTemplatesTrack { get; set; }
        public int cacheTemplatesStore { get; set; }
        public int cacheTemplatesRefresh { get; set; }
        public int cachePagesTrack { get; set; }
        public int cachePagesStore { get; set; }
        public int cachePagesRefresh { get; set; }
        public int cachePagesDirtyRead { get; set; }
        public string searchEngineListTemplate { get; set; }
        public string searchEngineFileTemplate { get; set; }
        public string searchEngineRobotsDb { get; set; }
        public bool useDataStore { get; set; }
        public string dataStoreClass { get; set; }
        public string redirectionClass { get; set; }
        public string dataStoreName { get; set; }
        public string dataStoreDriver { get; set; }
        public string dataStoreUrl { get; set; }
        public string dataStoreUser { get; set; }
        public string dataStorePassword { get; set; }
        public string dataStoreTestQuery { get; set; }
        public string dataStoreLogFile { get; set; }
        public int dataStoreInitConns { get; set; }
        public int dataStoreMaxConns { get; set; }
        public int dataStoreConnUsageLimit { get; set; }
        public string dataStoreLogLevel { get; set; }
        public int maxUrlLength { get; set; }
        public string mailHost { get; set; }
        public string mailHostOverride { get; set; }
        public int log { get; set; }
        public string logLocation { get; set; }
        public string logMaxSize { get; set; }
        public int dataLog { get; set; }
        public string dataLogLocation { get; set; }
        public string dataLogMaxSize { get; set; }
        public string removePageCache { get; set; }
        public string removeTemplateCache { get; set; }
        public string fileTransferFolder { get; set; }
        public int lookInContext { get; set; }
        public int adminGroupID { get; set; }
        public bool betaServer { get; set; }
    }

    [Serializable]
    public class TestObjectClass
    {
        public DateTime statusUpdateTime { get; set; }
        public DateTime lastActivityTime { get; set; }
        public WebApp web_app { get; set; }
    }

    [Serializable]
    public class Servlet
    {
        public string servlet_name { get; set; }
        public string servlet_class { get; set; }
        public InitParam init_param { get; set; }
    }

    [Serializable]
    public class ServletMapping
    {
        public string cofaxCDS { get; set; }
        public string cofaxEmail { get; set; }
        public string cofaxAdmin { get; set; }
        public string fileServlet { get; set; }
        public string cofaxTools { get; set; }
    }

    [Serializable]
    public class Taglib
    {
        public string taglib_uri { get; set; }
        public string taglib_location { get; set; }
    }

    [Serializable]
    public class WebApp
    {
        public Servlet[] servlet { get; set; }
        public ServletMapping servlet_mapping { get; set; }
        public Taglib taglib { get; set; }
    }
}
