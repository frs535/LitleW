using System;
using LitleW.ViewModels;

namespace LitleW.Models.Base
{
    public class Document : BaseItem
    {
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.MinValue;

        public override string ToString()
        {
            return string.Format("Order {0} from {1}", Number, Date); ;
        }
    }
}
