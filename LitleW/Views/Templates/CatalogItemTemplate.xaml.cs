using LitleW.Models.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LitleW.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatalogItemTemplate : ContentView
    {
        public static readonly BindableProperty CatalogProperty = BindableProperty.Create(nameof(Catalog), typeof(Catalog), typeof(Catalog), null);
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(string), string.Empty);
        public static readonly BindableProperty CatalogFontProperty = BindableProperty.Create(nameof(CatalogFont), typeof(FontAttributes), typeof(FontAttributes), FontAttributes.None);

        public Catalog Catalog
        {
            get => (Catalog)GetValue(CatalogProperty);
            set => SetValue(CatalogProperty, value);
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public FontAttributes CatalogFont
        {
            get => (FontAttributes)GetValue(CatalogFontProperty);
            set => SetValue(CatalogFontProperty, value);
        }

        public CatalogItemTemplate()
        {
            InitializeComponent();
            PropertyChanged += CatalogItemTemplate_PropertyChanged;
        }

        private void CatalogItemTemplate_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Catalog")
                lblValue.Text = Catalog.ToString();
            else if (e.PropertyName == "Label")
                lblName.Text = Label;
            else if (e.PropertyName == "CatalogFont")
                lblValue.FontAttributes = CatalogFont;
            
        }
    }
}
