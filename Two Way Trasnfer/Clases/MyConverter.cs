using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Two_Way_Trasnfer.Clases
{

    class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (null == value)
                return "null";

            ReadOnlyObservableCollection<object> items = (ReadOnlyObservableCollection<object>)value;

            List<Tarifas> myEntities = (from i in items select (Tarifas)i).ToList();

            foreach (Tarifas entity in myEntities)
            {
                if (entity.Remitente != null && entity.Destinatario != null && entity.Ruta != null)
                {
                    return true;
                }
            }
            return false;
        }    

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
