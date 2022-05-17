using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Two_Way_Trasnfer.Clases
{
    public class Tarifas : INotifyPropertyChanged
    {


        public Tarifas()
        {
            IDRemitente = 0;
            Remitente = "";
            ID = 0;
            SubCliente = "";
            ClaveSubcte = 0;
            IDTarifa = 0;
            CiudadRem = "";
            CalleRemitente = "";
            NumeroExteriorRem = "";
            NumeroInteriorRem = "";
            ColoniaRemitente = "";
            Destinatario = "";
            IDDestinatario = 0;
            CiudadDestinatario = "";
            CalleDestinatario = "";
            NumeroInteriorDest = "";
            NumeroExteriorDestinatario = "";
            ColoniaDestinatario = "";
            FechaInicio = DateTime.Now;
            FechaFin = DateTime.Now.AddYears(1);
            Ruta = "";
            Kilometros = 0;
            Tipo = "Exportacion";
            Moneda = "USD";
            Flete = 0;
            Seguro = 0;
            Autopistas = 0;
            Recoleccion = 0;
            Cruce = 0;
            Maniobras = 0;
            Operador = 0;
            Notas = "";
            TransporteInternacional = "";
            CPO = 0;
            PorcionUSA = 0;
            Accesorios = 0;
            Correo = "";
            UsoDeTarifa = "Carretera";

        }

        public int ID { get; set; }
        public int IDTarifa { get; set; }
        private string _subcte;
        public string SubCliente
        {
            get { return _subcte; }
            set
            {
                if (value != this._subcte)
                {
                    this._subcte = value;
                    INotifyPropertyChanged("SubCliente");
                }
            }
        }

        private int _clavesubcte;
        public int ClaveSubcte
        {
            get { return _clavesubcte; }
            set
            {
                if (value != this._clavesubcte)
                {
                    this._clavesubcte = value;
                    INotifyPropertyChanged("ClaveSubcte");
                }
            }
        }

        public string  Remitente { get; set; }

      
        private int _idRemitente;
        public int IDRemitente
        {
            get { return _idRemitente; }
            set
            {
                if (value != this._idRemitente)
                {
                    this._idRemitente = value;
                    INotifyPropertyChanged("IDRemitente");
                }
            }
        }
        private string _ciudadRem;
        public string CiudadRem
        {
            get { return _ciudadRem; }
            set { this._ciudadRem = value;
                INotifyPropertyChanged("CiudadRem");
            }
        }
        private string _calleRem;
        public string CalleRemitente
        {
            get { return _calleRem; }
            set { this._calleRem = value;
                INotifyPropertyChanged("CalleRemitente");
            }
        }
        private string _numeroExteriorRem;
        public string NumeroExteriorRem
        {
            get { return _numeroExteriorRem; }
            set { this._numeroExteriorRem = value;
                INotifyPropertyChanged("NumeroExteriorRem");
            }
        }
        private string _numeroInteriorRem;
        public string NumeroInteriorRem
        {
            get { return _numeroInteriorRem; }
            set { this._numeroInteriorRem = value; 
                INotifyPropertyChanged("NumeroInteriorRem");
            }
        }
        private string _coloniaRem;
        public string ColoniaRemitente
        {
            get { return _coloniaRem; }
            set { _coloniaRem = value;
                INotifyPropertyChanged("ColoniaRemitente");
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
        private int _idDestinatario;
        public int IDDestinatario
        {
            get { return _idDestinatario; }
            set
            {
                if (value != this._idDestinatario)
                {
                    this._idDestinatario = value;
                    INotifyPropertyChanged("IDDestinatario");
                }
            }
        }
        private string _ciudadDest;
        public string CiudadDestinatario
        {
            get { return _ciudadDest; }
            set { this._ciudadDest = value;
                INotifyPropertyChanged("CiudadDestinatario");
            }
        }
        private string _calleDest;
        public string CalleDestinatario
        {
            get { return _calleDest; }
            set { this._calleDest = value;
                INotifyPropertyChanged("CalleDestinatario");
            }
        }
        private string _numerExteriorDest;
        public string NumeroExteriorDestinatario
        {
            get { return _numerExteriorDest; }
            set { this._numerExteriorDest = value;
                INotifyPropertyChanged("NumeroExteriorDestinatario");
            }
        }
        private string _numeroInteriorDest;
        public string NumeroInteriorDest
        {
            get { return _numeroInteriorDest; }
            set { this._numeroInteriorDest = value;
                INotifyPropertyChanged("NumeroInteriorDest");
            }
        }
        private string _coloniaDest;
        public string ColoniaDestinatario
        {
            get { return _coloniaDest; }
            set { _coloniaDest = value;
                INotifyPropertyChanged("ColoniaDestinatario");
            }
        }
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime FechaFin { get; set; } = DateTime.Now.AddDays(30);
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
        public string Tipo { get; set; }
        public string Moneda { get; set; }
        public double Flete { get; set; }
        public double Seguro { get; set; }
        public double Autopistas { get; set; }
        public double Recoleccion { get; set; }
        public double Cruce { get; set; }
        public double Maniobras { get; set; }
        public double Operador { get; set; }
        public string Notas { get; set; } = "";
        public string TransporteInternacional { get; set; }
        public int CPO { get; set; }
        public double PorcionUSA { get; set; }
        public double Accesorios { get; set; }
        public string Correo { get; set; }
        private string _usuario;

        public string Usuario
        {

            get { return _usuario; }
            set
            {
                if (value != this._usuario)
                {
                    this._usuario = value;
                    INotifyPropertyChanged("Usuario");
                }
            }
        }

        private DateTime _fechamod;

        public DateTime FechaMod
        {
            get { return _fechamod; }
            set
            {
                if (value != this._fechamod)
                {
                    this._fechamod = value;
                    INotifyPropertyChanged("FechaMod");
                }
            }
        }

        private string _usoDeTarifa;

        public string UsoDeTarifa
        {

            get { return _usoDeTarifa; }
            set
            {
                if (value != this._usoDeTarifa)
                {
                    this._usoDeTarifa = value;
                    INotifyPropertyChanged("UsoDeTarifa");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


       
    }
  
}
