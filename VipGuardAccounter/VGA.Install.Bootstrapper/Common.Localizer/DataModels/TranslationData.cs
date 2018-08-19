using System.Collections.Generic;
using System.Globalization;

namespace Aurora.Install.Bootstrapper.Common.Localizer.DataModels
{
    internal class TranslationData
    {
        public CultureInfo Culture { get; }
        public string StringsFile { get; }
        
        public Dictionary<string, string> Strings { get; set; }

        public TranslationData(CultureInfo culture, string stringsFile)
        {
            Culture = culture;
            StringsFile = stringsFile;
            
            Strings = new Dictionary<string, string>();
        }
    }
}
