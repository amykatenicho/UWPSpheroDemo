namespace BigWhiteBall.ViewModels
{
  using System;
  using BigWhiteBall.Services;

  class DriveSpheroControlViewModel : ViewModelBase
  {
    public DriveSpheroControlViewModel(ISpheroService spheroService)
    {
      this.spheroService = spheroService;
    }
    public float SpeedValue
    {
      get
      {
        return (this.speedValue);
      }
      set
      {
        base.SetProperty(ref this.speedValue, value);
        this.DriveSphero();
      }
    }
    public double RotationValue
    {
      get
      {
        return (this.rotationValue);
      }
      set
      {
        base.SetProperty(ref this.rotationValue, value);
        this.RotateSphero();
      }
    }
    void DriveSphero()
    {
      this.spheroService.Drive(this.speedValue / MAX_SLIDER_VALUE);
    }
    void RotateSphero()
    {
      this.spheroService.Rotate((int)this.rotationValue);
    }
    float speedValue;
    static readonly float MAX_SLIDER_VALUE = 100.0f;
    double rotationValue;
    ISpheroService spheroService;
  }
}
