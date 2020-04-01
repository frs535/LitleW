using System.Linq;
using System.Collections.Generic;
using LitleW.Models.Base;
using System.Threading.Tasks;
using LitleW.Models;
using LitleW.ViewModels;

namespace LitleW
{
    public static class GlobalParameters
    {
        private static readonly List<BaseItem> _catalogs = new List<BaseItem>();

        public static void Register(ref BaseItem item)
        {
            _catalogs.Add(item);
        }

        public static void UnRegister(ref BaseItem item)
        {
            _catalogs.Remove(item);
        }

        public static BaseItem GetItemByBarcode(string barcode)
        {
            return _catalogs.Where(p=>p.Barcode == barcode | p.Id.ToString() == barcode).Select(p=>p).FirstOrDefault();
        }
    }
}
