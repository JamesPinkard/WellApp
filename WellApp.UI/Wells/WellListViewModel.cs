using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WellApp.Domain;
using WellApp.UI.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;

namespace WellApp.UI.Wells
{
    class WellListViewModel : BindableBase
    {
        private List<Well> _allWells = new List<Well>();
        private IWellRepository _repository = new WellRepository();

        private ObservableCollection<Well> _wells;        

        public WellListViewModel()
        {

        }

        public ObservableCollection<Well> Wells
        {
            get { return _wells; }
            set { SetProperty(ref _wells, value); }
        }

        public async void LoadWells()
        {
            _allWells = await _repository.GetWellsAsync();
            Wells = new ObservableCollection<Well>(_allWells);          
        }

        //public void FilterCounties(IEnumerable<string> counties)
        //{
        //    if (counties.Count() != 0)
        //    {
        //        WellCollectionView.Filter = (w) => { return (counties.Contains<string>(((Well)w).County)); };
        //    }
        //}

        public void FilterCounties(IEnumerable<string> counties)
        {
            if (counties.Count() == 0)
            {
                Wells = new ObservableCollection<Well>(_allWells);
                return;
            }
            else
            {
                Wells = new ObservableCollection<Well>(_allWells.Where(w => counties.Contains(w.County)));
            }
        }

        internal void FilterGmas(IEnumerable<string> gmas)
        {
            if (gmas.Count() == 0)
            {
                Wells = new ObservableCollection<Well>(_allWells);
                return;
            }
            else
            {
                Wells = new ObservableCollection<Well>(_allWells.Where(w => gmas.Contains(Convert.ToString(w.GMA))));
            }
        }
    }
}
