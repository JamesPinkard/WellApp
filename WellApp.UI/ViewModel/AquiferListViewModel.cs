using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WellApp.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.UI.Services;

namespace WellApp.UI.ViewModel
{
    class AquiferListViewModel : BindableBase
    {
        private ObservableCollection<BindableItem> _aquifers;
        private IAttributeTable<Well> _repository;
        private List<string> _selectedAquifers = new List<string>();

        public AquiferListViewModel(IAttributeTable<Well> wellRepository)
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            _repository = wellRepository;            
            CheckAquiferCommand = new RelayCommand<BindableItem>(OnCheckAquifer);
            UncheckAquiferCommand = new RelayCommand<BindableItem>(OnUncheckAquifer);
        }

        public async void LoadAttributes()
        {
            if (Aquifers == null)
            {
                var _distinctAttributes = await _repository.GetAttributeValuesAsync(w => w.Aquifer.AquiferName);
                Aquifers = new ObservableCollection<BindableItem>(_distinctAttributes);
            }
        }

        public ObservableCollection<BindableItem> Aquifers
        {
            get
            {
                return _aquifers;
            }
            private set { SetProperty(ref _aquifers, value); }
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
