using ofertaWPF.Data;
using ofertaWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ofertaWPF.ViewModels
{
    public class AgregarOfertaModel : INotifyPropertyChanged
    {
		private ICommand cmdBuscarArchivo;

		private string archivo;

		private ObservableCollection<int> años;

		private int selectedAño;

		private ObservableCollection<string> trimestres;

		private Dictionary<string, int> trimTable;

		private bool newChanges;

		public bool NewChanges
		{
			get { return newChanges; }
			set { newChanges = value; }
		}


		public AgregarOfertaModel()
		{
			trimTable = new Dictionary<string, int>();
			trimTable.Add("Febrero - Abril", 1);
			trimTable.Add("Mayo - Julio", 2);
			trimTable.Add("Agosto - Octubre", 3);
			trimTable.Add("Noviembre - Enero", 4);

			Trimestres = new ObservableCollection<string>(trimTable.Keys.AsEnumerable());

			años = new ObservableCollection<int>();
			int current = DateTime.Now.Year;
			for (int i = 2019; i <= current; i++)
			{
				años.Add(i);
			}
			NewChanges = false;
		}

		private string selectedTrimestre;

		public string SelectedTrimestre
		{
			get { return selectedTrimestre; }
			set { selectedTrimestre = value; }
		}

		public ObservableCollection<string> Trimestres
		{
			get { return trimestres; }
			set { trimestres = value; }
		}


		public int SelectedAño
		{
			get { return selectedAño; }
			set { selectedAño = value; }
		}


		public ObservableCollection<int> Años
		{
			get { return años; }
			set { años = value; }
		}


		public string Archivo
		{
			get { return archivo; }
			set 
			{ 
				archivo = value;
				InvokePropertyChanged("Archivo");
			}
		}


		public ICommand CmdBuscarArchivo
		{
			get
			{
				if (cmdBuscarArchivo == null) cmdBuscarArchivo = new Buscador(this);
				return cmdBuscarArchivo;
			}
		}

		private class Buscador : ICommand
		{
			private AgregarOfertaModel am;
			public Buscador(AgregarOfertaModel aoM)
			{
				am = aoM;
			}
			public event EventHandler CanExecuteChanged;

			public bool CanExecute(object parameter)
			{
				return true;
			}

			public void Execute(object parameter)
			{
				// Create OpenFileDialog 
				Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



				// Set filter for file extension and default file extension 
				dlg.DefaultExt = ".html";
				dlg.Filter = "HTML Files (*.html)|*.html";


				// Display OpenFileDialog by calling ShowDialog method 
				Nullable<bool> result = dlg.ShowDialog();


				// Get the selected file name and display in a TextBox 
				if (result == true)
				{
					// Open document 
					string filename = dlg.FileName;
					am.Archivo = filename;
					MessageBox.Show(am.Archivo);
				}
			}
		}

		private ICommand cmdAgregar;

		public ICommand CmdAgregar
		{
			get
			{
				if (cmdAgregar == null) cmdAgregar = new Agregador(this);
				return cmdAgregar;
			}
		}

		private class Agregador : ICommand
		{
			private AgregarOfertaModel am;
			public Agregador(AgregarOfertaModel aoM)
			{
				am = aoM;
			}
			public event EventHandler CanExecuteChanged;

			public bool CanExecute(object parameter)
			{
				return true;
			}

			public void Execute(object parameter)
			{
				if (am.Archivo == null) 
				{
					MessageBox.Show("Debe elegir un archivo de oferta acemica.");
					return;
				}
				var secciones = HtmlParser.HtmlParse(am.Archivo);
				var trim = new Trimestre();
				trim.Año = am.SelectedAño;
				trim.IdTrimestre = am.trimTable[am.SelectedTrimestre];

				if(secciones == null)
				{
					MessageBox.Show("No se pudo agregar.");
				}
				else
				{
					if (!SeccionDB.SaveSecciones(secciones,trim))
					{
						MessageBox.Show("No se pudo agregar.");
					}
					else
					{
						MessageBox.Show("La oferta se agregó exitosamente.");
						am.NewChanges = true;
					}
				}
				


			}
		}


		public event PropertyChangedEventHandler PropertyChanged;

		private void InvokePropertyChanged(string propertyName)
		{
			var e = new PropertyChangedEventArgs(propertyName);
			PropertyChangedEventHandler changed = PropertyChanged;
			if (changed != null) changed(this, e);
		}
	}
}
