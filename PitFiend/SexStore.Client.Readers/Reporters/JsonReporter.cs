namespace SexStore.Client.Readers.Reporters
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using SexStore.Client.Readers.Helpers;
    using Newtonsoft.Json;    

    public static class JsonReporter
    {
        public static void ExportReportToJsonFiles(IList<ProductReport> reports)
        {
            var jsonSer = new JsonSerializer();
            
            foreach (var report in reports)
            {
                using (var sw = new StreamWriter(@"..\..\..\Reports\" + report.ProductCode + ".json"))
                using (var writer = new JsonTextWriter(sw))
                {
                    jsonSer.Serialize(writer, report);
                }
            }
        }
    }
}
