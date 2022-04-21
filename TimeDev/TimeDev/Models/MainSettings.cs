using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TimeDev.Models
{
    public class MainSettings : INotifyPropertyChanged
    {
        string server;
        string username;
        string password;

        public string Server { get => server; set => SetProperty(ref server, value); }
        public string Username { get => username; set => SetProperty(ref username, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
        #endregion

    }
}
