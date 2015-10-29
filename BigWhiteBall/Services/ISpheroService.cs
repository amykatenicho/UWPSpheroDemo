namespace BigWhiteBall.Services
{
  using System;
  using System.Threading.Tasks;

  interface ISpheroService
  {
    bool HasConnectedSphero { get; }

    event EventHandler SpheroConnectionChanged;

    Task<bool> ConnectToFirstSpheroAsync(TimeSpan timeout);

    void Rotate(int angle);
    void Drive(float speed);
  }
}