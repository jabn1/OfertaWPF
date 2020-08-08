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

namespace ofertaWPF.Views
{
    /// <summary>
    /// Interaction logic for Disponibilidad.xaml
    /// </summary>
    public partial class Disponibilidad : Page
    {
        public Disponibilidad()
        {
            InitializeComponent();
        }

        public void ResetFields()
        {
            TbSearchP.Text = "";
            CBxFiltro.SelectedIndex = 0;
        }
        private void TbSearchP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtBuscar.Focus();

                BtBuscar.Command.Execute(null);
            }
        }
    }
}
