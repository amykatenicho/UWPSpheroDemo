namespace BigWhiteBall.ViewModels
{
  using BigWhiteBall.Services;

  class MainUIControlViewModel : ViewModelBase
  {
    public MainUIControlViewModel(ISpheroService spheroService)
    {
      this.spheroService = spheroService;
      this.spheroService.SpheroConnectionChanged += OnSpheroConnectionStateChanged;
    }
    public bool HasSphero
    {
      get
      {
        return (this.spheroService.HasConnectedSphero);
      }
    }
    void OnSpheroConnectionStateChanged(object sender, System.EventArgs e)
    {
      base.OnPropertyChanged("HasSphero");
    }
    ISpheroService spheroService;
  }
}