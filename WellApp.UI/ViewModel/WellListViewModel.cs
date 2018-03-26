using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WellApp.Domain;
using WellApp.UI.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;

namespace WellApp.UI.ViewModel
{
    class WellListViewModel : BindableBase
    {
        private List<Well> _allWells = new List<Well>();
        private IWellRepository _repository;

        private ObservableCollection<Well> _wells;        

        public WellListViewModel(IWellRepository wellRepository)
        {
            _repository = wellRepository;
        }

        public ObservableCollection<Well> Wells
        {
            get { return _wells; }
            set { SetProperty(ref _wells, value); }
        }

        public async void LoadWells()
        {
            if (Wells == null)
            {
                _allWells = await _repository.GetWellsAsync();
                Wells = new ObservableCollection<Well>(_allWells);          
            }
        }

        public void FilterByAttribute(IEnumerable<string> selectedAttributes, Func<Well,bool> predicate)
        {
            if (selectedAttributes.Count() == 0)
            {
                Wells = new ObservableCollection<Well>(_allWells);
                return;
            }
            else
            {
                Wells = new ObservableCollection<Well>(_allWells.Where(predicate));
            }
        }
    }
}
