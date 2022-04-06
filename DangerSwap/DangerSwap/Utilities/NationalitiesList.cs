namespace DangerSwap.Utilities
{
    using System.Globalization;
    public class NationalitySelector
    {
        public NationalitySelector()
        {
        }

        public List<string> DisplaySelections()
        {
            List<string> result = new List<string>();
            CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
            for (int i = 0; i < cinfo.Length; i++)
            {
                result.Add(cinfo[i].DisplayName);
            }
            return result;
        }

    }
}
