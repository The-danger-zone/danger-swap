namespace DangerSwap.Utilities
{
    using System.Globalization;
    public static class NationalitySelector
    {
        public static IEnumerable<string> GetCountryNames()
        {
            List<string> countryNames = new List<string>();
            CultureInfo[] cultureInfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
            foreach(var culture in cultureInfo)
            {
                countryNames.Add(culture.DisplayName);
            }
            return countryNames;
        }
    }
}
