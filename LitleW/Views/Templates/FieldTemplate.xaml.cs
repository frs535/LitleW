using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LitleW.Views.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldTemplate : ContentView
    {
        public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(string), string.Empty);
        public static readonly BindableProperty FieldProperty = BindableProperty.Create(nameof(Field), typeof(object), typeof(object), null);

        public FieldTemplate()
        {
            InitializeComponent();
            PropertyChanged += FieldTemplate_PropertyChanged;
        }

        private void FieldTemplate_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Label")
                lblName.Text = Label;
            else if(e.PropertyName == "Field")
            {
                if(Field is null)
                    lblValue.Text = string.Empty;

                if (Field is string)
                    lblValue.Text = ((string)Field).ToString();

                if (Field is DateTime)
                    lblValue.Text = ((DateTime)Field).ToString("dd:MM:yy");

                if (Field is decimal)
                    lblValue.Text = string.Format("{0:C2}", Field);

                if(Field is double | Field is float)
                    lblValue.Text = String.Format("{0:00.0}", Field);

                if (Field is int)
                    lblValue.Text = string.Format("{0:00.0}", Field);
            }
        }

        public string Label
        {
            get => (string)GetValue(LabelProperty);
            set => SetValue(LabelProperty, value);
        }

        public object Field
        {
            get => GetValue(FieldProperty);
            set => SetValue(FieldProperty, value);
        }


    }
}
