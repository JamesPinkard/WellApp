using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.UI.Services;

namespace WellApp.UI.Aquifers
{
    class AquiferListViewModel : BindableBase
    {
        private ObservableCollection<BindableItem> _aquifers;
        private IAquiferCollection _repository = new WellRepository();
        private List<string> _selectedAquifers = new List<string>();

        public AquiferListViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            _aquifers = new ObservableCollection<BindableItem>(_repository.GetAquifersAsync().Result);
            CheckAquiferCommand = new RelayCommand<BindableItem>(OnCheckAquifer);
            UncheckAquiferCommand = new RelayCommand<BindableItem>(OnUncheckAquifer);
        }

        public ObservableCollection<BindableItem> Aquifers
        {
            get
            {
                return _aquifers;
            }
        }

        public RelayCommand<BindableItem> CheckAquiferCommand { get; private set; }
        public RelayCommand<BindableItem> UncheckAquiferCommand { get; private set; }

        public event Action<IEnumerable<String>> CheckAquiferRequested = delegate { };
        public event Action<IEnumerable<String>> UncheckAquiferRequested = delegate { };

        private void OnCheckAquifer(BindableItem aquifer)
        {
            _selectedAquifers.Add(aquifer.Name);
            CheckAquiferRequested(_selectedAquifers);
        }

        private void OnUncheckAquifer(BindableItem aquifer)
        {
            _selectedAquifers.Remove(aquifer.Name);
            UncheckAquiferRequested(_selectedAquifers);
        }
    }
}
