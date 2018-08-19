namespace Aurora.Install.Bootstrapper.Common.Localizer.Helpers
{
    public static class TranslatorExtension
    {
        public static string Translate(this string original)
        {
            return Translator.Instance.GetTranslatedString(original);
        }
    }
}
