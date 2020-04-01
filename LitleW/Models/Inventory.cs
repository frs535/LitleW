using System.Collections.ObjectModel;
using LitleW.Models.Base;

namespace LitleW.Models
{
    public class Inventory: Document
    {
        public Catalog Warehause { get; set; }
        public Catalog Company { get; set; }
        public ObservableCollection<Good> Goods { get; set; } = new ObservableCollection<Good>();
    }
}
