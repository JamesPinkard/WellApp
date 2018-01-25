using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.Domain;
using WellApp.UI.Services;

namespace WellApp.UI.Counties
{
    class CountyListViewModel: BindableBase
    {
        private ObservableCollection<BindableItem> _counties;
        private IWellRepository _repository = new WellRepository();
        private List<string> _selectedCounties = new List<string>();

        public CountyListViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            _counties = new ObservableCollection<BindableItem>(_repository.GetCountiesAsync().Result);
            CheckCountyCommand = new RelayCommand<BindableItem>(OnCheckCounty);
            UncheckCountyCommand = new RelayCommand<BindableItem>(OnUncheckCounty);
        }

        public ObservableCollection<BindableItem> Counties
        {
            get
            {
                return _counties;
            }
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
