namespace BigWhiteBall
{
  using ViewModels;
  using Windows.ApplicationModel.Activation;
  using Windows.UI.Xaml;
  using Autofac;
  using System.ComponentModel;
  using Services;

  sealed partial class App : Application
  {
    public App()
    {
      this.InitializeComponent();
    }
    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
      if (Window.Current.Content == null)
      {
        this.Locator.Build(builder =>
        {
          builder
            .RegisterType<MainUIControlViewModel>()
            .Named<INotifyPropertyChanged>(Constants.Values.MainViewModelName)
            .As<INotifyPropertyChanged>();

          builder.RegisterType<LookingForSpheroControlViewModel>()
            .Named<INotifyPropertyChanged>(Constants.Values.LookingForSpheroViewModelName)
            .As<INotifyPropertyChanged>();

          builder.RegisterType<DriveSpheroControlViewModel>()
            .Named<INotifyPropertyChanged>(Constants.Values.DriveSpheroViewModelName)
            .As<INotifyPropertyChanged>();

          builder
            .RegisterInstance<ISpheroService>(new SpheroService());
        });
        Window.Current.Content = new MainUIControl();
      }
      Window.Current.Activate();
    }
    ViewModelLocator Locator
    {
      get
      {
        return (this.Resources["locator"] as ViewModelLocator);
      }
    }
  }
}
