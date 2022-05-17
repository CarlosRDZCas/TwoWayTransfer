using NServiceBus.Testing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases
{
    public class RemitenteCB 
    {
        public RemitenteCB()
        {
            
        }
        private string _remitente;

        public string NomRemitente
        {
            get { return _remitente; }
            set { _remitente = value; }
        }
        private int _idRemitente;

        public int RemitenteID
        {
            get { return _idRemitente; }
            set
            {
                if (value != this._idRemitente)
                {
                    this._idRemitente = value;
                    INotifyPropertyChanged();
                }
            }
        }
        private string _calle;

        public string Calle
        {
            get { return _calle; }
            set {this._calle = value;
                INotifyPropertyChanged();
            }
        }

        private string _numExterior;

        public string NumeroExterior
        {
            get { return _numExterior; }
            set
            {
                this._numExterior = value;
                INotifyPropertyChanged();
            }
        }

        private string _numeroInterior;

        public string NumeroInterior
        {
            get { return _numeroInterior; }
            set { this._numeroInterior = value;
                INotifyPropertyChanged();
            }
        }


        private string _colonia;

        public string Colonia
        {
            get { return _colonia; }
            set { this._colonia = value;
                INotifyPropertyChanged();
            }
        }       

        private string _ciudadRem;

        public string CiudadRemitente
        {
            get { return _ciudadRem; }
            set { this._ciudadRem = value;
                INotifyPropertyChanged();
            }
        }



        private void INotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Referencia { get; set; }
        public string Estado { get; set; }
        public string  Municipio { get; set; }


        private string _nombreCompleto;

        public string NombreCompleto
        {
            get { return _nombreCompleto; }
            set {
                _nombreCompleto = NomRemitente.Trim()+" - "+Referencia.Trim()+" - "+Estado.Trim()+" - "+Municipio.Trim();
                INotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
