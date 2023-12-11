using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace CineMajestic.Views.ProductManagement
{
    public class WatermarkBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(WatermarkBehavior), new PropertyMetadata(string.Empty));

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            SetWatermark();
            AssociatedObject.GotFocus += OnGotFocus;
            AssociatedObject.LostFocus += OnLostFocus;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotFocus -= OnGotFocus;
            AssociatedObject.LostFocus -= OnLostFocus;
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            if (string.Equals(AssociatedObject.Text, Watermark))
            {
                AssociatedObject.Text = string.Empty;
                AssociatedObject.Foreground = new SolidColorBrush(Colors.Black); // or any other color
            }
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            SetWatermark();
        }

        private void SetWatermark()
        {
            if (string.IsNullOrEmpty(AssociatedObject.Text))
            {
                AssociatedObject.Text = Watermark;
                AssociatedObject.Foreground = new SolidColorBrush(Colors.LightGray); // or any other color
            }
        }
    }
}
