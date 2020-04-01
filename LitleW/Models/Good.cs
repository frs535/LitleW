using LitleW.Models.Base;

namespace LitleW.Models
{
    public class Good : Row
    {
        public Catalog Catalog { get; set; }
        public Catalog Unit { get; set; }
        public double Count { get; set; } = 0.0;

        public Catalog Serie { get; set; }

        private double actualQuantity;
        public double ActualQuantity
        {
            get => actualQuantity;
            set
            {
                actualQuantity = value;
                OnPropertyChanged("ActualQuantity");
            }
        }
    }
}
