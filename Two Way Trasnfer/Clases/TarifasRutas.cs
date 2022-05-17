using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases
{
    class TarifasRutas : INotifyPropertyChanged
    {
        public TarifasRutas()
        {
            Ruta = "";
            IDTarifa = 0;
            Cliente = 0;
            Origen = 0;
            Remitente = "";
            Destino = 0;
            Destinatario = "";
        }
        public string Ruta { get; set; }
        private int _kilometros;

        public int Kilometros
        {
            get { return _kilometros; }
            set
            {
                if (value != this._kilometros)
                {
                    this._kilometros = value;
                    INotifyPropertyChanged("Kilometros");
                }
            }
        }
        
        public int IDTarifa { get; set; }
        public int Cliente { get; set; }
        public string Remitente { get; set; }
        private int _origen;

        public int Origen
        {
            get { return _origen; }
            set
            {
                if (value != this._origen)
                {
                    this._origen = value;
                    INotifyPropertyChanged("Origen");
                }
            }
        }
        private void INotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Destinatario { get; set; }
        private int _destino;
        public int Destino
        {
            get { return _destino; }
            set
            {
                if (value != this._destino)
                {
                    this._destino = value;
                    INotifyPropertyChanged("Destino");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
