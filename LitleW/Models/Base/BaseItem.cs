using System;
namespace LitleW.Models.Base
{
    public class BaseItem
    {
        public BaseItem()
        {
            var refItem = this;
            GlobalParameters.Register(ref refItem);
        }

        ~BaseItem()
        {
            var refItem = this;
            GlobalParameters.UnRegister(ref refItem);
        }

        public Guid Id { get; set; } = Guid.Empty;
        public string Barcode { get; set; } = string.Empty;

       public override bool Equals(object obj)
       {
           if (obj is BaseItem)
                return ((BaseItem)obj).Id == Id;

            return base.Equals(obj);
        }
    }
}
