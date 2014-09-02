﻿namespace SexStore.Client.Readers.Helpers
{
    using System;
    using System.IO;
    using System.Linq;

    public class NamingFactory
    {
        public static string BuildName(string methodName, string fileType)
        {
            // Removed const since you cannot assign a constant with DateTime - Georgi
            // string DateTimeFormatType = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            string dateTimeFormatType = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

            string tempFolder = string.Format("../../../Reports/{0}Reports/", fileType.ToUpper());
            string tempFileName = string.Format("{0}-{1}.{2}", methodName, dateTimeFormatType, fileType);
            bool ifExists = Directory.Exists(tempFolder);

            if (!ifExists)
            {
                Directory.CreateDirectory(tempFolder);
            }

            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Report exported to {0}", fileType.ToUpper());
            Console.WriteLine("-------------------------------------------------");
            return tempFolder + tempFileName;
        }
    }
}
