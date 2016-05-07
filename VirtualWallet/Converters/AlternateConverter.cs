using System;
using System.Collections;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace VirtualWallet.Converters
{
    public class AlternateConverter : DependencyObject, IValueConverter
    {
        public List<SolidColorBrush> AlternateBrushes
        {
            get { return (List<SolidColorBrush>)GetValue(AlternateBrushesProperty); }
            set { SetValue(AlternateBrushesProperty, value); }
        }

        public static readonly DependencyProperty AlternateBrushesProperty =
            DependencyProperty.Register("AlternateBrushes", typeof(List<SolidColorBrush>),
            typeof(AlternateConverter), new PropertyMetadata(new List<SolidColorBrush>()));

        public object CurrentList
        {
            get { return GetValue(CurrentListProperty); }
            set { SetValue(CurrentListProperty, value); }
        }

        public static readonly DependencyProperty CurrentListProperty =
            DependencyProperty.Register("CurrentList", typeof(object),
            typeof(AlternateConverter), new PropertyMetadata(null));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return AlternateBrushes[(CurrentList as IList).IndexOf(value) % AlternateBrushes.Count];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
