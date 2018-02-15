using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.Domain;
using WellApp.UI.Services;
using Prism.Events;

namespace WellApp.UI.ViewModel
{
    public class CountyListViewModel: BindableBase
    {
        private ObservableCollection<BindableItem> _counties;
        private IAttributeTable<Well> _repository;
        private List<string> _selectedCounties = new List<string>();        

        public CountyListViewModel(IAttributeTable<Well> wellRepository)
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            _repository = wellRepository;
            CheckCountyCommand = new RelayCommand<BindableItem>(OnCheckCounty);
            UncheckCountyCommand = new RelayCommand<BindableItem>(OnUncheckCounty);
        }

        public async void LoadAttributes()
        {
            var _distinctCounties = await _repository.GetAttributeValuesAsync(w => w.County);
            Counties = new ObservableCollection<BindableItem>(_distinctCounties);
        }

        public ObservableCollection<BindableItem> Counties
        {
            get
            {
                return _counties;
            }
            private set { SetProperty(ref _counties, value); }
        }

        public RelayCommand<BindableItem> CheckCountyCommand { get; private set; }
        public RelayCommand<BindableItem> UncheckCountyCommand { get; private set; }

        public event Action<IEnumerable<String>> CheckCountyRequested = delegate { };
        public event Action<IEnumerable<String>> UncheckCountyRequested = delegate { };

        private void OnCheckCounty(BindableItem county)
        {
            _selectedCounties.Add(county.Name);
            CheckCountyRequested(_selectedCounties);
        }        

        private void OnUncheckCounty(BindableItem county)
        {
            _selectedCounties.Remove(county.Name);
            UncheckCountyRequested(_selectedCounties);
        }
    }
}
