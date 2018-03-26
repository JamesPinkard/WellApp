using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.Domain;
using WellApp.UI.Services;

namespace WellApp.UI.ViewModel
{
    class GmaListViewModel : BindableBase
    {
        private ObservableCollection<BindableItem> _gmas;
        private IAttributeTable<Well> _repository;
        private List<string> _selectedGmas = new List<string>();

        public GmaListViewModel(IAttributeTable<Well> wellRepository)
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            _repository = wellRepository;            
            CheckGmaCommand = new RelayCommand<BindableItem>(OnCheckGma);
            UncheckGmaCommand = new RelayCommand<BindableItem>(OnUncheckGma);
        }

        public async void LoadAttributes()
        {
            if(Gmas == null)
            {
                var _distinctAttributes = await _repository.GetAttributeValuesAsync(w => w.GMA);
                Gmas = new ObservableCollection<BindableItem>(_distinctAttributes);
            }
        }

        public ObservableCollection<BindableItem> Gmas
        {
            get
            {
                return _gmas;
            }
            private set { SetProperty(ref _gmas, value); }
        }

        public RelayCommand<BindableItem> CheckGmaCommand { get; private set; }
        public RelayCommand<BindableItem> UncheckGmaCommand { get; private set; }

        public event Action<IEnumerable<String>> CheckGmaRequested = delegate { };
        public event Action<IEnumerable<String>> UncheckGmaRequested = delegate { };

        private void OnCheckGma(BindableItem gma)
        {
            _selectedGmas.Add(gma.Name);
            CheckGmaRequested(_selectedGmas);
        }

        private void OnUncheckGma(BindableItem gma)
        {
            _selectedGmas.Remove(gma.Name);
            UncheckGmaRequested(_selectedGmas);
        }
    }
}
