using ofertaWPF.Data;
using ofertaWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ofertaWPF.ViewModels
{
    public class SeleccionModel : INotifyPropertyChanged
    {

		private ObservableCollection<Trimestre> trimestres;

		private Trimestre selectedTrim;

		private bool isChecked;

		private string selectedTrimText;

		public string SelectedTrimText
		{
			get { return selectedTrimText; }
			set { selectedTrimText = value; }
		}


		public bool IsChecked
		{
			get { return isChecked; }
			set { isChecked = value;
				InvokePropertyChanged("Trimestres");
			}
		}


		public Trimestre SelectedTrim
		{
			get	{ return selectedTrim; }
			set { selectedTrim = value;
				if(value != null) SelectedTrimText = $"Id: {value.IdOferta} | {value.Meses} {value.Año}";
				InvokePropertyChanged("SelectedTrimText");
			}
		}


		public ObservableCollection<Trimestre> Trimestres
		{
			get 
			{
				if (isChecked)
				{
					var temp = new Dictionary<string, Trimestre>();
					string keyt = "";
					foreach (var item in trimestres)
					{
						keyt = item.Año.ToString() + item.Meses;
						if (temp.ContainsKey(keyt))
						{
							if (temp[keyt].IdOferta < item.IdOferta)
							{
								temp[keyt] = item;
							}
						}
						else
						{
							temp.Add(keyt, item);
						}
					}
					return new ObservableCollection<Trimestre>(temp.Values.ToList());
				}
				else
				{
					return trimestres;
				}
			}
			set { 
				trimestres = value;
				InvokePropertyChanged("Trimestres");
			}
		}

		public void UpdateTrimestres()
		{
			Trimestres = new ObservableCollection<Trimestre>(TrimestreDB.GetTrimestres());
		}

		public SeleccionModel()
		{
			trimestres = new ObservableCollection<Trimestre>(TrimestreDB.GetTrimestres());
			SelectedTrimText = "";
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
