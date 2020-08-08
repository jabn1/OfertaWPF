using ofertaWPF.Data;
using ofertaWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ofertaWPF.ViewModels
{
    public class ResultadosModel : INotifyPropertyChanged
    {
		private ObservableCollection<Seccion> seccionesRes;

		private List<Seccion> secciones;

		private string searchParameter;

		private ICommand cmdUpdateResult;

		private ObservableCollection<string> filterTypes;

		private string selectedFilter;

		private int idOferta;

		public int IdOferta
		{
			get { return idOferta; }
			set 
			{ 
				idOferta = value;
				Secciones = SeccionDB.GetSecciones(value);
			}
		}


		public ResultadosModel()
		{
			cmdUpdateResult = new Updater(this);
		}

		public string SelectedFilter
		{
			get { return selectedFilter; }
			set { selectedFilter = value; }
		}


		public ObservableCollection<string> FilterTypes
		{
			get 
			{
				if (filterTypes == null) filterTypes = new ObservableCollection<string>() { "Asignatura", "Profesor" , "Aula" };
				return filterTypes; 
			}
			set { filterTypes = value; }
		}


		public string SearchParameter
		{
			get { return searchParameter; }
			set { searchParameter = value.ToUpper(); }
		}


		public List<Seccion> Secciones
		{
			get { return secciones; }
			set 
			{ 
				secciones = value;
				seccionesRes = new ObservableCollection<Seccion>(value);
				InvokePropertyChanged("SeccionesRes");
			}
		}

		public ObservableCollection<Seccion> SeccionesRes
		{
			get { return seccionesRes; }
			set { seccionesRes = value; }
		}


		
		public ICommand CmdUpdateResult
		{
			get{ return cmdUpdateResult; }
		}

		private class Updater : ICommand
		{
			private ResultadosModel rm;
			public Updater(ResultadosModel resM)
			{
				rm = resM;
			}
			public event EventHandler CanExecuteChanged;

			public bool CanExecute(object parameter)
			{
				return true;
			}

			public void Execute(object parameter)
			{
				string[] data = rm.SearchParameter.Split('&');
				var temp = rm.Secciones.AsEnumerable();

				if (rm.SelectedFilter == "Asignatura")
				{
					foreach (var item in data)
					{
						if (item != "") temp = temp.Where(s => s.Asignatura.Contains(item));
					}
				}
				else if(rm.SelectedFilter == "Profesor")
				{
					foreach (var item in data)
					{
						if (item != "") temp = temp.Where(s => s.Profesor.Contains(item));
					}				
				}
				else if(rm.SelectedFilter == "Aula")
				{
					foreach (var item in data)
					{
						if (item != "") temp = temp.Where(s => s.Aula.Contains(item));
					}
				}
				rm.SeccionesRes = new ObservableCollection<Seccion>(temp);
				rm.InvokePropertyChanged("SeccionesRes");
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
