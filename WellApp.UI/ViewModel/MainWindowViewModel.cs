using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.UI;
using WellApp.UI.Services;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;
using WellApp.Data;
using System.Windows.Input;

namespace WellApp.UI.ViewModel
{
    class MainWindowViewModel : BindableBase
    {
        private GmaListViewModel _gmaListViewModel;
        private AquiferListViewModel _aquiferListViewModel;
        private CountyListViewModel _countyListViewModel;
        private WellListViewModel _wellListViewModel;

        private BindableBase _currentViewModel;

        public MainWindowViewModel()
        {
            GroundwaterContext groundwaterContext = new GroundwaterContext();
            WellRepository wellRepository = new WellRepository(groundwaterContext);
            _gmaListViewModel = new GmaListViewModel(wellRepository);
            _aquiferListViewModel = new AquiferListViewModel(wellRepository);
            _countyListViewModel = new CountyListViewModel(wellRepository);
            _wellListViewModel = new WellListViewModel(wellRepository);
            _countyListViewModel.CheckCountyRequested += FilterCounty;
            _countyListViewModel.UncheckCountyRequested += FilterCounty;
            _gmaListViewModel.CheckGmaRequested += FilterGma;
            _gmaListViewModel.UncheckGmaRequested += FilterGma;
            _aquiferListViewModel.CheckAquiferRequested += FilterAquifer;
            _aquiferListViewModel.UncheckAquiferRequested += FilterAquifer;
            CloseCommand = new RelayCommand(OnClose);
            NavCommand = new RelayCommand<BindableItem>(OnNav);
            LoadDataCommand = new RelayCommand(OnLoadData);
            CurrentFilterViewModel = _countyListViewModel;
            ExportToCsvCommand = new RelayCommand(OnExportToCsv);

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
            _wellListViewModel.FilterByAttribute(counties, w => counties.Contains(w.County));            
        }

        private void FilterGma(IEnumerable<string> gmas)
        {
            _wellListViewModel.FilterByAttribute(gmas, w => gmas.Contains(Convert.ToString(w.GMA)));
        }

        private void FilterAquifer(IEnumerable<string> aquifers)
        {
            _wellListViewModel.FilterByAttribute(aquifers, w => aquifers.Contains(w.Aquifer.AquiferName));
        }

        public BindableBase CurrentFilterViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public BindableBase WellListViewModel
        {
            get { return _wellListViewModel; }
        }

        public RelayCommand<BindableItem> NavCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }
        public RelayCommand LoadDataCommand { get; private set; }
        public RelayCommand ExportToCsvCommand { get; private set; }

        private void OnNav(BindableItem bindableItem)
        {
            string destination = bindableItem.Name;
            switch (destination)
            {
                case "GMA":
                    CurrentFilterViewModel = _gmaListViewModel;
                    break;
                case "Aquifer":
                    CurrentFilterViewModel = _aquiferListViewModel;
                    break;
                case "County":
                default:
                    CurrentFilterViewModel = _countyListViewModel;
                    break;
            }
        }        

        private void OnClose()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void OnLoadData()
        {
            var newWindowThread = new Thread(ThreadStartingPoint);
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
        }

        private void OnExportToCsv()
        {
            //TODO
            System.Windows.MessageBox.Show("export to csv clicked");
            var wells = _wellListViewModel.Wells;
            CsvWriter.ToCsv<Domain.Well>("test.csv", ",", wells);            
        }

        private void ThreadStartingPoint()
        {
            var tempWindow = new ProgressBarWindow();
            tempWindow.Show();
            Dispatcher.Run();
        }
    }
}
