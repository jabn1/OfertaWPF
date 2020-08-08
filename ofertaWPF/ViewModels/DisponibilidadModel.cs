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
    public class DisponibilidadModel : INotifyPropertyChanged
    {
		private ICommand cmdUpdateResult;
		private string searchParameter;
		private List<Seccion> secciones;
		private Dictionary<string, Aulas> disp;
		private ObservableCollection<HorarioAula> aulas;
		private ObservableCollection<string> dias;
		private string selectedDia;
		private int idOferta;

		public int IdOferta
		{
			get { return idOferta; }
			set 
			{ 
				idOferta = value;
				secciones = SeccionDB.GetSecciones(idOferta);
				disp = Disponibilidad.GetDisponibilidad(secciones);
				Dias = new ObservableCollection<string>(disp.Keys.AsEnumerable());
				Aulas = new ObservableCollection<HorarioAula>(disp[SelectedDia]);
			}
		}


		public string SelectedDia
		{
			get { return selectedDia; }
			set 
			{ 
				selectedDia = value;
				if(disp != null) Aulas = new ObservableCollection<HorarioAula>(disp[value]);
			}
		}



		public ObservableCollection<string> Dias
		{
			get { return dias; }
			set { dias = value;
				InvokePropertyChanged("Dias");
			}
		}




		public ObservableCollection<HorarioAula> Aulas
		{
			get { return aulas; }
			set 
			{ 
				aulas = value;
				InvokePropertyChanged("Aulas");
			}
		}


		public string SearchParameter
		{
			get { return searchParameter; }
			set { searchParameter = value; }
		}



		public ICommand CmdUpdateResult
		{
			get 
			{
				if (cmdUpdateResult == null) cmdUpdateResult = new Updater(this);
				return cmdUpdateResult; 
			}
		}

		private class Updater : ICommand
		{
			private DisponibilidadModel dm;
			public Updater(DisponibilidadModel disM)
			{
				dm = disM;
			}
			public event EventHandler CanExecuteChanged;

			public bool CanExecute(object parameter)
			{
				return true;
			}

			public void Execute(object parameter)
			{
				string[] data = dm.SearchParameter.ToUpper().Split('&');
				var temp = dm.disp[dm.selectedDia].AsEnumerable();

				
				foreach (var item in data)
				{
					if (item != "") temp = temp.Where(s => s.Aula.Contains(item));
				}
				
				
				dm.Aulas = new ObservableCollection<HorarioAula>(temp);
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
