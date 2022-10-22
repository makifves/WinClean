﻿using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Scover.WinClean.Presentation.Converters;

internal class ToSolidColorBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => new SolidColorBrush((Color)value) { Opacity = System.Convert.ToDouble(parameter, culture) };

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ((SolidColorBrush)value).Color;
}