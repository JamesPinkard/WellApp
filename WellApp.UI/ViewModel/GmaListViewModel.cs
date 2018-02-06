using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellApp.UI.Services;

namespace WellApp.UI.ViewModel
{
    class GmaListViewModel : BindableBase
    {
        private ObservableCollection<BindableItem> _gmas;
        private IGmaCollection _repository;
        private List<string> _selectedGmas = new List<string>();

        public GmaListViewModel(IGmaCollection wellRepository)
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            _repository = wellRepository;
            _gmas = new ObservableCollection<BindableItem>(_repository.GetGmasAsync().Result);
            CheckGmaCommand = new RelayCommand<BindableItem>(OnCheckGma);
            UncheckGmaCommand = new RelayCommand<BindableItem>(OnUncheckGma);
        }

        public ObservableCollection<BindableItem> Gmas
        {
            get
            {
                return _gmas;
            }
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
