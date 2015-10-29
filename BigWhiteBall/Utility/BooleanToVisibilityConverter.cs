namespace BigWhiteBall.Utility
{
  using System;
  using Windows.UI.Xaml;
  using Windows.UI.Xaml.Data;

  class BooleanToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      bool bValue = (bool)value;
      bool bFlipValue = (parameter != null) && bool.Parse((string)parameter);

      Visibility visibility = bValue ? Visibility.Visible : Visibility.Collapsed;

      if (bFlipValue)
      {
        visibility = 
          visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
      }
      return (visibility);
    }

    public object ConvertBack(
      object value, Type targetType, object parameter, string language)
    {
      throw new NotImplementedException();
    }
  }
}
