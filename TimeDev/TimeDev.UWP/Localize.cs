using System.Globalization;
using TimeDev.Models;
using Xamarin.Forms;
[assembly: Dependency(typeof(TimeDev.UWP.Localize))]

namespace TimeDev.UWP
{
    public class Localize : ILocalize
    {
        public System.Globalization.CultureInfo GetCurrentCultureInfo()
        {
            return CultureInfo.CurrentUICulture;
        }
    }
}
