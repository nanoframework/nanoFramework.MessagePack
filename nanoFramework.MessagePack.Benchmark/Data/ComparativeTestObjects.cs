// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace nanoFramework.MessagePack.Benchmark.Data
{
    using System;
    using System.Runtime.Serialization.Formatters.Binary;
    using nanoFramework.Json;

    /// <summary>
    /// Test data.
    /// </summary>
    internal class ComparativeTestObjects
    {
        /// <summary>
        /// Test json text.
        /// </summary>
        internal const string TestJson = @"{
	""StatusUpdateTime"": ""0001-01-01T00:00:00Z"",
""LastActivityTime"": ""2021-06-03T05:52:41.4683112Z"",
""Web_app"": {
  ""Servlet"": [   
    {
      ""Servlet_name"": ""cofaxCDS"",
      ""Servlet_class"": ""org.cofax.cds.CDSServlet"",
      ""Init_param"": {
        ""ConfigGlossaryInstallationAt"": ""Philadelphia, PA"",
        ""ConfigGlossaryAdminEmail"": ""ksm@pobox.com"",
        ""ConfigGlossaryPoweredBy"": ""Cofax"",
        ""ConfigGlossaryPoweredByIcon"": ""/images/cofax.gif"",
        ""ConfigGlossaryPtaticPath"": ""/content/static"",
        ""TemplateProcessorClass"": ""org.cofax.WysiwygTemplate"",
        ""TemplateLoaderClass"": ""org.cofax.FilesTemplateLoader"",
        ""TemplatePath"": ""templates"",
        ""TemplateOverridePath"": """",
        ""DefaultListTemplate"": ""listTemplate.htm"",
        ""DefaultFileTemplate"": ""articleTemplate.htm"",
        ""UseJSP"": false,
        ""JspListTemplate"": ""listTemplate.jsp"",
        ""JspFileTemplate"": ""articleTemplate.jsp"",
        ""CachePackageTagsTrack"": 200,
        ""CachePackageTagsStore"": 200,
        ""CachePackageTagsRefresh"": 60,
        ""CacheTemplatesTrack"": 100,
        ""CacheTemplatesStore"": 50,
        ""CacheTemplatesRefresh"": 15,
        ""CachePagesTrack"": 200,
        ""CachePagesStore"": 100,
        ""CachePagesRefresh"": 10,
        ""CachePagesDirtyRead"": 10,
        ""SearchEngineListTemplate"": ""forSearchEnginesList.htm"",
        ""SearchEngineFileTemplate"": ""forSearchEngines.htm"",
        ""SearchEngineRobotsDb"": ""WEB_INF/robots.db"",
        ""UseDataStore"": true,
        ""DataStoreClass"": ""org.cofax.SqlDataStore"",
        ""RedirectionClass"": ""org.cofax.SqlRedirection"",
        ""DataStoreName"": ""cofax"",
        ""DataStoreDriver"": ""com.microsoft.jdbc.sqlserver.SQLServerDriver"",
        ""DataStoreUrl"": ""jdbc:microsoft:sqlserver://LOCALHOST:1433;DatabaseName=goon"",
        ""DataStoreUser"": ""sa"",
        ""DataStorePassword"": ""dataStoreTestQuery"",
        ""DataStoreTestQuery"": ""SET NOCOUNT ON;select test='test';"",
        ""DataStoreLogFile"": ""/usr/local/tomcat/logs/datastore.log"",
        ""DataStoreInitConns"": 10,
        ""DataStoreMaxConns"": 100,
        ""DataStoreConnUsageLimit"": 100,
        ""DataStoreLogLevel"": ""debug"",
        ""MaxUrlLength"": 500}},
    {
      ""Servlet_name"": ""cofaxEmail"",
      ""Servlet_class"": ""org.cofax.cds.EmailServlet"",
      ""Init_param"": {
      ""MailHost"": ""mail1"",
      ""MailHostOverride"": ""mail2""}},
    {
      ""Servlet_name"": ""cofaxAdmin"",
      ""Servlet_class"": ""org.cofax.cds.AdminServlet""},
 
    {
      ""Servlet_name"": ""fileServlet"",
      ""Servlet_class"": ""org.cofax.cds.FileServlet""},
    {
      ""Servlet_name"": ""cofaxTools"",
      ""Servlet_class"": ""org.cofax.cms.CofaxToolsServlet"",
      ""Snit_param"": {
        ""TemplatePath"": ""toolstemplates/"",
        ""Log"": 1,
        ""LogLocation"": ""/usr/local/tomcat/logs/CofaxTools.log"",
        ""LogMaxSize"": """",
        ""DataLog"": 1,
        ""DataLogLocation"": ""/usr/local/tomcat/logs/dataLog.log"",
        ""DataLogMaxSize"": """",
        ""RemovePageCache"": ""/content/admin/remove?cache=pages&id="",
        ""RemoveTemplateCache"": ""/content/admin/remove?cache=templates&id="",
        ""FileTransferFolder"": ""/usr/local/tomcat/webapps/content/fileTransferFolder"",
        ""LookInContext"": 1,
        ""AdminGroupID"": 4,
        ""BetaServer"": true}}],
  ""Servlet_mapping"": {
    ""CofaxCDS"": ""/"",
    ""CofaxEmail"": ""/cofaxutil/aemail/*"",
    ""CofaxAdmin"": ""/admin/*"",
    ""FileServlet"": ""/static/*"",
    ""CofaxTools"": ""/tools/*""},
 
  ""Taglib"": {
    ""Taglib_uri"": ""cofax.tld"",
    ""Taglib_location"": ""/WEB_INF/tlds/cofax.tld""}}}";

        /// <summary>
        /// Gets json deserialize option.
        /// </summary>
        internal static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparativeTestObjects"/> class.
        /// </summary>
        internal ComparativeTestObjects()
        {
            TestObject = (TestObjectClass)JsonConvert.DeserializeObject(TestJson, typeof(TestObjectClass), JsonSerializerOptions);
            TestObjectMsgPackBytes = MessagePackSerializer.Serialize(TestObject);
            TestObjectBinaryBytes = BinaryFormatter.Serialize(TestObject);
        }

        /// <summary>
        /// Gets test object for deserialize <see cref="TestJson"/>.
        /// </summary>
        internal static TestObjectClass TestObject { get; private set; }

        /// <summary>
        /// Gets byte array for test object used by <see cref="MessagePack"/>.
        /// </summary>
        internal static byte[] TestObjectMsgPackBytes { get; private set; }

        /// <summary>
        /// Gets byte array for test object used by <see cref="BinaryFormatter"/>.
        /// </summary>
        internal static byte[] TestObjectBinaryBytes { get; private set; }

        /// <summary>
        /// Dummy test serialization object sub class.
        /// </summary>
        [Serializable]
        public class InitParam
        {
            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string ConfigGlossaryInstallationAt { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string ConfigGlossaryAdminEmail { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string ConfigGlossaryPoweredBy { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string ConfigGlossaryPoweredByIcon { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string ConfigGlossaryPtaticPath { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string TemplateProcessorClass { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string TemplateLoaderClass { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string TemplatePath { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string TemplateOverridePath { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DefaultListTemplate { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DefaultFileTemplate { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether <see langword="true"/>. Not used.
            /// </summary>
            public bool UseJSP { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string JspListTemplate { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string JspFileTemplate { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int CachePackageTagsTrack { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int CachePackageTagsStore { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int CachePackageTagsRefresh { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int CacheTemplatesTrack { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int CacheTemplatesStore { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int CacheTemplatesRefresh { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int CachePagesTrack { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int CachePagesStore { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int CachePagesRefresh { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int CachePagesDirtyRead { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string SearchEngineListTemplate { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string SearchEngineFileTemplate { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string SearchEngineRobotsDb { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether <see langword="true"/>. Not used.
            /// </summary>
            public bool UseDataStore { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DataStoreClass { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string RedirectionClass { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DataStoreName { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DataStoreDriver { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DataStoreUrl { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DataStoreUser { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DataStorePassword { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DataStoreTestQuery { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DataStoreLogFile { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int DataStoreInitConns { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int DataStoreMaxConns { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int DataStoreConnUsageLimit { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DataStoreLogLevel { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int MaxUrlLength { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string MailHost { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string MailHostOverride { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int Log { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string LogLocation { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string LogMaxSize { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int DataLog { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DataLogLocation { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string DataLogMaxSize { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string RemovePageCache { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string RemoveTemplateCache { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string FileTransferFolder { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int LookInContext { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public int AdminGroupID { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether <see langword="true"/>. Not used.
            /// </summary>
            public bool BetaServer { get; set; }
        }

        /// <summary>
        /// Dummy main test serialization object class.
        /// </summary>
        [Serializable]
        public class TestObjectClass
        {
            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public DateTime StatusUpdateTime { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public DateTime LastActivityTime { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public WebApp Web_app { get; set; }
        }

        /// <summary>
        /// Test serialization object sub class.
        /// </summary>
        [Serializable]
        public class Servlet
        {
            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string Servlet_name { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string Servlet_class { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public InitParam Init_param { get; set; }
        }

        /// <summary>
        /// Dummy test serialization object sub class.
        /// </summary>
        [Serializable]
        public class ServletMapping
        {
            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string CofaxCDS { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string CofaxEmail { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string CofaxAdmin { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string FileServlet { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string CofaxTools { get; set; }
        }

        /// <summary>
        /// Dummy test serialization object sub class.
        /// </summary>
        [Serializable]
        public class Taglib
        {
            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string Taglib_uri { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public string Taglib_location { get; set; }
        }

        /// <summary>
        /// Dummy test serialization object sub class.
        /// </summary>
        [Serializable]
        public class WebApp
        {
            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public Servlet[] Servlet { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public ServletMapping Servlet_mapping { get; set; }

            /// <summary>
            /// Gets or sets test property. Not used.
            /// </summary>
            public Taglib Taglib { get; set; }
        }
    }
}
