using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using Aurora.Install.Bootstrapper.Common.Localizer.Converters;
using Aurora.Install.Bootstrapper.Common.Localizer.DataModels;

namespace Aurora.Install.Bootstrapper.Common.Localizer.Helpers
{
    internal class TranslationHelper
    {
        #region 

        private const string _languageFilename = "Language.dat";
        private const string _stringsFolderPath = @"Aurora.Install.Bootstrapper.Common.Localizer.Strings.";
        private const string _converter = "TranslateParameterConverter";

        private static string _settingsDirectory =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Application.ResourceAssembly.GetName().Name);

        private static string _languagePath = Path.Combine(_settingsDirectory, _languageFilename);

        #endregion
        
        private readonly Assembly _resourcesAssembly;
        private readonly CultureInfo _cultureDefault;
        private List<TranslationData> _translations;

        public TranslationHelper()
        {
            _resourcesAssembly = Assembly.GetExecutingAssembly();
            _cultureDefault = new CultureInfo("en");
        }

        public CultureInfo DefaultCulture => _cultureDefault;

        public void SaveCulture(CultureInfo culture)
        {
            try
            {
                if (!Directory.Exists(_settingsDirectory))
                {
                    Directory.CreateDirectory(_settingsDirectory);
                }
                File.WriteAllText(_languagePath, culture.TwoLetterISOLanguageName);
            }
            catch
            { }
        }

        public CultureInfo LoadCulture()
        {
            CultureInfo culture = null;
            if (!File.Exists(_languagePath)) return null;

            try
            {
                var txt = File.ReadAllText(_languagePath);
                culture = new CultureInfo(txt);
            }
            catch
            { }

            return culture;
        }

        public void InitTranslateConverter()
        {
            if (!Application.Current.Resources.Contains(_converter))
            {
                Application.Current.Resources.Add(_converter, new TranslateParameterConverter());
            }
        }

        public bool IsDefault(CultureInfo culture)
        {
            return culture.Equals(_cultureDefault);
        }

        public List<TranslationData> GetTranslations()
        {
            return _translations ?? (
                       _translations = new List<TranslationData>
                       {
                           new TranslationData(new CultureInfo("en"), string.Empty),
                           new TranslationData(new CultureInfo("ja"), "ja.strings"),
                           new TranslationData(new CultureInfo("de"), "de.strings"),
                           new TranslationData(new CultureInfo("es"), "es.strings")
                       });
        }

        public void InitTranslation(TranslationData translation)
        {
            if (IsDefault(translation.Culture)) return;
            translation.Strings = GetStringsDictionaryByFileName(translation.StringsFile);
        }

        private Dictionary<string, string> GetStringsDictionaryByFileName(string fileName)
        {
            var result = new Dictionary<string, string>();
            var resourceName = _stringsFolderPath + fileName;
            var existingResourceNames = _resourcesAssembly.GetManifestResourceNames();
            if (!existingResourceNames.Contains(resourceName)) return result;

            Stream resourceStream;
            try
            {
                resourceStream = _resourcesAssembly.GetManifestResourceStream(resourceName);
            }
            catch (Exception e)
            {
                return result;
            }
            if (resourceStream == null) return result;

            using (var streamReader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                result = GetStringsDictionary(streamReader);
            }

            return result;
        }

        private static Dictionary<string, string> GetStringsDictionary(StreamReader streamReader)
        {
            var result = new Dictionary<string, string>();
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                var dictionaryItem = GetStringsDictionaryItem(line);
                if (!result.ContainsKey(dictionaryItem.Key))
                {
                    result.Add(dictionaryItem.Key, dictionaryItem.Value);
                }
            }
            return result;
        }

        private static KeyValuePair<string, string> GetStringsDictionaryItem(string line)
        {
            var result = new KeyValuePair<string, string>(string.Empty, string.Empty);
            var parts = line.Trim(';').Split('=');
            if (parts.Length == 0 ||
                parts.Length % 2 != 0)
            {
                return result;
            }

            var stringBuilderKey = new StringBuilder();
            for (var i = 0; i < parts.Length / 2; i++)
            {
                stringBuilderKey.Append(parts[i]);
            }

            var stringBuilderValue = new StringBuilder();
            for (var i = parts.Length / 2; i < parts.Length; i++)
            {
                stringBuilderValue.Append(parts[i]);
            }

            var key = stringBuilderKey.ToString().Trim().Trim('"');
            var value = stringBuilderValue.ToString().Trim().Trim('"');

            result = new KeyValuePair<string, string>(key, value);
            return result;
        }

        #region DEVELOP

        /// <summary>
        /// Usage: in VS perform "Find in files" for _codeUsageEnd and _uiUsageStart 
        /// templates (without escaping symbols '\') and result of both searches put to usagesPath file.
        /// As a result is requiredPath file with all required strings not existing in translatorPath file.
        /// Note: For correct work of this mechanism each usage of translation 
        /// must be single on the line (single quoted text on the line) 
        /// and on the single line (all text without line-brakes in the code/markup line)!
        /// </summary>
        private static void FindRequiredStrings()
        {
            const string _codeUsageStart = "\"";
            const string _codeUsageEnd = "\".Translate()";
            const string _uiUsageStart = "\"{Binding Converter={StaticResource TranslateParameterConverter}, ConverterParameter=\'";
            const string _uiUsageEnd = "\'}\"";

            var translatorPath = Path.Combine(_settingsDirectory, "Translator.strings");
            var usagesPath = Path.Combine(_settingsDirectory, "TranslatorUsages.txt");
            var requiredPath = Path.Combine(_settingsDirectory, "Required.strings");

            if (!File.Exists(translatorPath) || !File.Exists(usagesPath))
            {
                return;
            }

            var streamReaderStrings = File.OpenText(translatorPath);
            var dict = GetStringsDictionary(streamReaderStrings);
            var existing = dict.Keys;

            var required = new List<string>();
            var streamReaderUsages = File.OpenText(usagesPath);
            string line;
            while ((line = streamReaderUsages.ReadLine()) != null)
            {
                line = line.Replace("&amp;", "&");

                string start, end;
                if (line.Contains(_codeUsageStart) &&
                    line.Contains(_codeUsageEnd))
                {
                    start = _codeUsageStart;
                    end = _codeUsageEnd;
                }
                else if (line.Contains(_uiUsageStart) &&
                    line.Contains(_uiUsageEnd))
                {
                    start = _uiUsageStart;
                    end = _uiUsageEnd;
                }
                else
                {
                    continue;
                }

                var startTextIndex = line.IndexOf(start) + start.Length;
                var requiredString = line.Substring(startTextIndex, line.IndexOf(end, startTextIndex) - startTextIndex);

                if (!string.IsNullOrEmpty(requiredString) &&
                    !existing.Contains(requiredString) &&
                    !required.Contains(requiredString))
                {
                    required.Add(requiredString);
                }
            }

            File.WriteAllLines(requiredPath, required.Select(str => $"\"{str}\" = \"{str}\";"));
        }

        #endregion DEVELOP
    }
}
