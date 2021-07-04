using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SatoImsV1._1.Common
{
    public class Statics
    {
        public static ObservableCollection<T> ToObservableCollection<T>(IEnumerable<T> enumeration)
        {
            return new ObservableCollection<T>(enumeration);
        }
    }
}
