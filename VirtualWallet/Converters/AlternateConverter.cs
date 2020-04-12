using BL.Metadata;
using System;
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
            get => (List<SolidColorBrush>)GetValue(AlternateBrushesProperty);
            set => SetValue(AlternateBrushesProperty, value);
        }

        public static readonly DependencyProperty AlternateBrushesProperty =
            DependencyProperty.Register("AlternateBrushes", typeof(List<SolidColorBrush>),
            typeof(AlternateConverter), new PropertyMetadata(new List<SolidColorBrush>()));

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return AlternateBrushes[((value as IIndexable)?.Index ?? 0) % AlternateBrushes.Count];
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
