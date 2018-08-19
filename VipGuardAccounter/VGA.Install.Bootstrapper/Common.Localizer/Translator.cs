using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Aurora.Install.Bootstrapper.Common.Localizer.DataModels;
using Aurora.Install.Bootstrapper.Common.Localizer.Helpers;

namespace Aurora.Install.Bootstrapper.Common.Localizer
{
    public class Translator
    {
        private static readonly object syncRoot = new object();
        private static Translator _instance;
        private readonly TranslationHelper _helper;

        private CultureInfo _culture;

        private TranslationData _translationDefault;
        private TranslationData _translation;

        private Translator()
        {
            _helper = new TranslationHelper();

            var translations = _helper.GetTranslations();

            //var currentCulture = _helper.LoadCulture();
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var existingCulture = translations.FirstOrDefault(tr => tr.Culture.Equals(currentCulture))?.Culture;
            _culture = existingCulture ?? _helper.DefaultCulture;

            _translation = translations.FirstOrDefault(tr => tr.Culture.Equals(_culture));
            _translationDefault = translations.FirstOrDefault(tr => _helper.IsDefault(tr.Culture));

            _helper.InitTranslation(_translation);

            if (!_helper.IsDefault(_culture))
            {
                _helper.InitTranslation(_translationDefault);
            }
        }

        public static Translator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Translator();
                        }
                    }
                }
                return _instance;
            }
        }

        public CultureInfo GetCurrentCulture()
        {
            return _culture;
        }

        public void SaveCulture(CultureInfo culture)
        {
            _helper.SaveCulture(culture);
        }


        public IEnumerable<CultureInfo> GetCultureInfos()
        {
            return _helper.GetTranslations().Select(loc => loc.Culture);
        }

        public void InitTranslateConverter()
        {
            _helper.InitTranslateConverter();
        }

        internal string GetTranslatedString(string original)
        {
            var result = _translation.Strings.ContainsKey(original) ? _translation.Strings[original] : original;
            return result;
        }
    }
}
