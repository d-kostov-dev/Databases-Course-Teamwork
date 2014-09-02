namespace SexStore.Client.Readers.Reporters
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using Newtonsoft.Json;    
    using SexStore.Client.Readers.Helpers;

    public static class JsonReporter
    {
        public static void ExportReportToJsonFiles()
        {
            IList<ProductReport> reports = ProductReportsCreator.CreateReportForEveryProductFromSQLServer();

            var jsonSer = new JsonSerializer();
            
            foreach (var report in reports)
            {
                using (var sw = new StreamWriter(@"..\..\..\Reports\JSONReports\" + report.ProductCode + ".json"))
                using (var writer = new JsonTextWriter(sw))
                {
                    jsonSer.Serialize(writer, report);
                }
            }
        }
    }
}
