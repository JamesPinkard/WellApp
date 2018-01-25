using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.UI.Counties;
using WellApp.UI.Wells;
using WellApp.UI.GMA;
using WellApp.UI.Services;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;

namespace WellApp.UI
{
    class MainWindowViewModel : BindableBase
    {
        private GmaListViewModel _gmaListViewModel = new GmaListViewModel();
        private CountyListViewModel _countyListViewModel = new CountyListViewModel();
        private WellListViewModel _wellListViewModel = new WellListViewModel();

        private BindableBase _currentViewModel;

        public MainWindowViewModel()
        {
            _countyListViewModel.CheckCountyRequested += FilterCounty;
            _countyListViewModel.UncheckCountyRequested += FilterCounty;
            _gmaListViewModel.CheckGmaRequested += FilterGma;
            _gmaListViewModel.UncheckGmaRequested += FilterGma;
            NavCommand = new RelayCommand<BindableItem>(OnNav);
            LoadDataCommand = new RelayCommand(OnLoadData);
            CurrentFilterViewModel = _countyListViewModel;

            Criteria = new ObservableCollection<BindableItem>
            {
                new BindableItem("County"),
                new BindableItem("GMA"),
                new BindableItem("Aquifer"),
            };
        }       

        public ObservableCollection<BindableItem> Criteria { get; set; }

        private void FilterCounty(IEnumerable<string> counties)
        {
            _wellListViewModel.FilterCounties(counties);            
        }

        private void FilterGma(IEnumerable<string> gmas)
        {
            _wellListViewModel.FilterGmas(gmas);
        }

        public BindableBase CurrentFilterViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public BindableBase CountyListViewModel
        {
            get { return _countyListViewModel; }            
        }

        public BindableBase WellListViewModel
        {
            get { return _wellListViewModel; }
        }

        public RelayCommand<BindableItem> NavCommand { get; private set; }
        public RelayCommand LoadDataCommand { get; private set; }

        private void OnNav(BindableItem bindableItem)
        {
            string destination = bindableItem.Name;
            switch (destination)
            {
                case "GMA":
                    CurrentFilterViewModel = _gmaListViewModel;
                    break;
                case "County":
                default:
                    CurrentFilterViewModel = _countyListViewModel;
                    break;

            }
        }        

        private void OnLoadData()
        {
            var newWindowThread = new Thread(ThreadStartingPoint);
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
        }

        private void ThreadStartingPoint()
        {
            var tempWindow = new ProgressBarWindow();
            tempWindow.Show();
            Dispatcher.Run();
        }
    }
}
