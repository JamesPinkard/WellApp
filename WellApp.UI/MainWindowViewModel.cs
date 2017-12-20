using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.UI.Counties;
using WellApp.UI.Wells;


namespace WellApp.UI
{
    class MainWindowViewModel : BindableBase
    {
        private CountyListViewModel _countyListViewModel;
        private WellListViewModel _wellListViewModel;

        public MainWindowViewModel()
        {
            _countyListViewModel = new CountyListViewModel();
            _wellListViewModel = new WellListViewModel();
            _countyListViewModel.CheckCountyRequested += FilterCounty;
            _countyListViewModel.UncheckCountyRequested += FilterCounty;
        }

        

        private void FilterCounty(IEnumerable<string> counties)
        {
            _wellListViewModel.FilterCounties(counties);            
        }

        public BindableBase CountyListViewModel
        {
            get { return _countyListViewModel; }            
        }

        public BindableBase WellListViewModel
        {
            get { return _wellListViewModel; }
        }
    }
}
