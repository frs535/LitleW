using System;
namespace LitleW.Models.Base
{
    public class Catalog : BaseItem
    {
        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return Name;
        }
    }
}
