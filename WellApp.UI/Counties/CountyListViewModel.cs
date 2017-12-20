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
        private ObservableCollection<County> _counties;
        private IWellRepository _repository = new WellRepository();
        private List<string> _selectedCounties = new List<string>();

        public CountyListViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            _counties = new ObservableCollection<County>(_repository.GetCountiesAsync().Result);
            CheckCountyCommand = new RelayCommand<County>(OnCheckCounty);
            UncheckCountyCommand = new RelayCommand<County>(OnUncheckCounty);
        }

        public ObservableCollection<County> Counties
        {
            get
            {
                return _counties;
            }
        }

        public RelayCommand<County> CheckCountyCommand { get; private set; }
        public RelayCommand<County> UncheckCountyCommand { get; private set; }

        public event Action<IEnumerable<String>> CheckCountyRequested = delegate { };
        public event Action<IEnumerable<String>> UncheckCountyRequested = delegate { };

        private void OnCheckCounty(County county)
        {
            _selectedCounties.Add(county.Name);
            CheckCountyRequested(_selectedCounties);
        }        

        private void OnUncheckCounty(County county)
        {
            _selectedCounties.Remove(county.Name);
            UncheckCountyRequested(_selectedCounties);
        }
    }
}
