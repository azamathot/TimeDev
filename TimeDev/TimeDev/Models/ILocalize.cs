using System.Globalization;

namespace TimeDev.Models
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
