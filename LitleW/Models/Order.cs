using LitleW.Models.Base;
using System.Collections.ObjectModel;

namespace LitleW.Models
{
    public class Order : Document
    {
        public string Type { get; set; } = string.Empty;
        public Catalog Warehause { get; set; }
        public Catalog Customer { get; set; }
        public Catalog Department { get; set; }
        public ObservableCollection<Good> Goods { get; set; } = new ObservableCollection<Good>();
    }
}
