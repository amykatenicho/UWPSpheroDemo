namespace BigWhiteBall.ViewModels
{
  using BigWhiteBall.Services;
  using Utility;
  using System;
  using System.Windows.Input;

  class LookingForSpheroControlViewModel : ViewModelBase
  {
    public LookingForSpheroControlViewModel(ISpheroService spheroService)
    {
      this.spheroService = spheroService;
      this.lookForSpheroCommand = new AlwaysExecuteCommand(this.LookForSphero);
      this.LookForSphero();
    }
    public ICommand LookForSpheroCommand
    {
      get
      {
        return (this.lookForSpheroCommand);
      }
    }
    public bool IsLookingForSphero
    {
      get
      {
        return (this.isLookingForSphero);
      }
      set
      {
        base.SetProperty(ref this.isLookingForSphero, value);
      }
    }
    async void LookForSphero()
    {
      this.IsLookingForSphero = true;

      await this.spheroService.ConnectToFirstSpheroAsync(
        TimeSpan.FromSeconds(10));

      this.IsLookingForSphero = false;
    }
    ISpheroService spheroService;
    ICommand lookForSpheroCommand;
    bool isLookingForSphero;
  }
}
