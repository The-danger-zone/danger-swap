namespace DangerSwap.Utilities
{
    using System.Globalization;
    public static class NationalitySelector
    {
        //Mindaugas
        public static IEnumerable<string> GetCountryNames()
        {
            var cultureInfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
            return  cultureInfo.Select(culture => culture.DisplayName).ToList();
        }
    }
}

