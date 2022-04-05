namespace System.Globalization
{
    public class CultureInformation
    {
        readonly CultureInfo[] cinfo = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
    }
}






