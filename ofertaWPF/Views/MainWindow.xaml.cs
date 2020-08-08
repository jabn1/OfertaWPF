using ofertaWPF.Views;
using ofertaWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ofertaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        private Resultados resultados;
        private ResultadosModel resultadosModel;
        private Seleccion seleccion;
        private SeleccionModel seleccionModel;
        private Disponibilidad disponibilidad;
        private DisponibilidadModel disponibilidadModel;
        private AgregarOferta agregarOferta;
        private AgregarOfertaModel agregarOfertaModel;
        private Brush selectedBrush;
        private Brush normalBrush;

        public Brush NormalBrush
        {
            get 
            {
                if (normalBrush == null)
                {
                    var bc = new BrushConverter();
                    normalBrush = (Brush)bc.ConvertFrom("#FFDDDDDD");
                }
                return normalBrush; 
            }
            set { normalBrush = value; }
        }

        

        public Brush SelectedBrush
        {
            get 
            { 
                if(selectedBrush == null)
                {
                    var bc = new BrushConverter();
                    selectedBrush = (Brush)bc.ConvertFrom("#FFAAAAAA");
                }
                return selectedBrush; 
            }
            set { selectedBrush = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
            BtSeleccionar.Background = SelectedBrush;

            seleccion = new Seleccion();
            seleccionModel = new SeleccionModel();
            seleccion.DataContext = seleccionModel;
            this.FrMain.Content = seleccion;

            resultados = new Resultados();
            resultadosModel = new ResultadosModel();
            resultados.DataContext = resultadosModel;

            disponibilidad = new Disponibilidad();
            disponibilidadModel = new DisponibilidadModel();
            disponibilidad.DataContext = disponibilidadModel;

            agregarOferta = new AgregarOferta();
            agregarOfertaModel = new AgregarOfertaModel();
            agregarOferta.DataContext = agregarOfertaModel;

            this.DataContext = seleccionModel;
        }

        private Dictionary<Boton,Button> buttons;

        private Dictionary<Boton,Button> Buttons
        {
            get 
            {
                if (buttons == null)
                {
                    buttons = new Dictionary<Boton, Button>();
                    buttons.Add(Boton.Seleccionar,BtSeleccionar);
                    buttons.Add(Boton.VerOferta,BtVerOferta);
                    buttons.Add(Boton.Disponibilidad, BtDisponibilidad);
                    buttons.Add(Boton.AgregarOferta, BtAgregar);
                }
                return buttons; 
            }
            set { buttons = value; }
        }


        private enum Boton
        {
            Seleccionar = 1,
            VerOferta = 2,
            Disponibilidad = 3,
            AgregarOferta = 4
        }

        private void ChangeColor(Boton b)
        {
            foreach (var item in Buttons)
            {
                if (item.Key == b)
                {
                    item.Value.Background = SelectedBrush;
                }
                else
                {
                    if (item.Value.Background == SelectedBrush) item.Value.Background = NormalBrush;
                }
            }
        }

        private void BtSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            this.FrMain.Content = seleccion;
            if (agregarOfertaModel.NewChanges)
            {
                seleccionModel.UpdateTrimestres();
                agregarOfertaModel.NewChanges = false;
            }
            ChangeColor(Boton.Seleccionar);
        }

        private void BtVerOferta_Click(object sender, RoutedEventArgs e)
        {
            if (seleccionModel.SelectedTrim == null) return;
            if(resultadosModel.IdOferta != seleccionModel.SelectedTrim.IdOferta)
            {
                resultadosModel.IdOferta = seleccionModel.SelectedTrim.IdOferta;
                resultados.ResetFields();
            }

            this.FrMain.Content = resultados;
            ChangeColor(Boton.VerOferta);
        }

        private void BtDisponibilidad_Click(object sender, RoutedEventArgs e)
        {
            if (seleccionModel.SelectedTrim == null) return;
            if (disponibilidadModel.IdOferta != seleccionModel.SelectedTrim.IdOferta)
            {
                disponibilidadModel.IdOferta = seleccionModel.SelectedTrim.IdOferta;
                disponibilidad.ResetFields();
            }

            this.FrMain.Content = disponibilidad;
            ChangeColor(Boton.Disponibilidad);
        }

        private void BtAgregar_Click(object sender, RoutedEventArgs e)
        {

            this.FrMain.Content = agregarOferta;
            ChangeColor(Boton.AgregarOferta);
        }
    }
}
