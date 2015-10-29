namespace BigWhiteBall.Utility
{
  using Windows.UI.Core;
  using Windows.UI.Xaml;

  enum AspectRatio
  {
    None,
    Portrait,
    Landscape
  }
  class AspectRatioTrigger : StateTriggerBase
  {
    public static readonly DependencyProperty AspectRatioProperty =
      DependencyProperty.Register(
        "AspectRatio", typeof(AspectRatio), typeof(AspectRatioTrigger),
        new PropertyMetadata(AspectRatio.None, OnAspectRatioChanged));

    public AspectRatioTrigger()
    {

    }
    public AspectRatio AspectRatio
    {
      get
      {
        return (AspectRatio)base.GetValue(AspectRatioProperty);
      }
      set
      {
        base.SetValue(AspectRatioProperty, value);
      }
    }
    static void OnAspectRatioChanged(DependencyObject sender,
      DependencyPropertyChangedEventArgs args)
    {
      AspectRatioTrigger trigger = (AspectRatioTrigger)sender;

      if (trigger.window == null)
      {
        trigger.window = Window.Current;
        trigger.window.SizeChanged += trigger.OnSizeChanged;
      }
    }
    void OnSizeChanged(object sender, 
      WindowSizeChangedEventArgs e)
    {
      base.SetActive(
        ((this.AspectRatio == AspectRatio.Landscape) && (e.Size.Width > e.Size.Height)) ||
        ((this.AspectRatio == AspectRatio.Portrait) && (e.Size.Width <= e.Size.Height)));
    }
    Window window;
  }
}
