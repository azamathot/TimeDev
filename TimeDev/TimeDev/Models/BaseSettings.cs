using System.Collections.ObjectModel;

namespace TimeDev.Models
{
    public class BaseSettings
    {
        public BaseSettings()
        {
            TrackerTypeList = new ObservableCollection<ITrackerType>();
        }

        public ObservableCollection<ITrackerType> TrackerTypeList { get; set; }
        public ObservableCollection<string> GroupByList { get; set; }
        public ObservableCollection<string> SortByList { get; set; }
        public ObservableCollection<string> Themes { get; set; }
        public ObservableCollection<string> Languages { get; set; }
    }
}
