using System.Collections.ObjectModel;
using LitleW.Models.Base;

namespace LitleW.Models
{
    public class StorageCells : Document
    {
        public string Type { get; set; }
        public Catalog Warehause { get; set; }
        public ObservableCollection<Good> GoodsSelection { get; set; } = new ObservableCollection<Good>();
        public ObservableCollection<Good> GoodsPlacing { get; set; } = new ObservableCollection<Good>();
    }
}
