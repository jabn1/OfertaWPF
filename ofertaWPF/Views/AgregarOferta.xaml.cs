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
    /// Interaction logic for AgregarOferta.xaml
    /// </summary>
    public partial class AgregarOferta : Page
    {
        public AgregarOferta()
        {
            InitializeComponent();
        }

        public void ResetFields()
        {
            CBxAño.SelectedIndex = -1;
            CBxTrim.SelectedIndex = -1;
            LbArchivo.Content = "";
        }
    }
}
