namespace BigWhiteBall.Services
{
  using Utility;
  using RobotKit;
  using System;
  using System.Threading;
  using System.Threading.Tasks;


  class SpheroService : ISpheroService
  {
    public event EventHandler SpheroConnectionChanged;

    public SpheroService()
    {
    }
    public void Drive(float speed)
    {
      this.speed = speed;
      this.Roll();
    }
    public void Rotate(int angle)
    {
      this.angle = angle;
      this.Roll();
    }
    void Roll()
    {
      // NB: using roll for both rotate and drive as SetHeading doesn't
      // seem to work for me.
      this.sphero.Roll(this.angle, this.speed);
    }
    bool HasSphero
    {
      get
      {
        return (this.sphero != null);
      }
    }
    public bool HasConnectedSphero
    {
      get
      {
        bool connected = (this.HasSphero &&
          (this.sphero.ConnectionState == ConnectionState.Connected));

        return (connected);
      }
    }
    RobotProvider Provider
    {
      get
      {
        return (RobotProvider.GetSharedProvider());
      }
    }
    bool HasProvider
    {
      get
      {
        return (this.Provider != null);
      }
    }
    public async Task<bool> ConnectToFirstSpheroAsync(TimeSpan timeout)
    {
      // I'm not being correct with respect to the timeout here as
      // worst case scenario both of my async functions below could
      // wait for 'timeout' so it could be a factor of 2 out.
      if (!this.HasConnectedSphero)
      {
        if (!this.HasSphero)
        {
          await this.DiscoverFirstRobotAsync(timeout);
        }
        if (this.HasSphero)
        {
          await this.ConnectDiscoveredSpheroAsync(timeout);
        }
      }
      return (this.HasConnectedSphero);
    }
    async Task DiscoverFirstRobotAsync(TimeSpan timeout)
    {
      TaskCompletionSource<bool> completionSource =
        new TaskCompletionSource<bool>();

      if (this.HasProvider && !this.HasSphero)
      {
        EventHandler<Robot> robotsHandler = null;

        robotsHandler = (s, robot) =>
        {
          this.sphero = robot as Sphero;

          this.sphero.OnConnectionStateChanged =
            this.OnSpheroConnectionStateChanged;

          completionSource.SetResult(true);
        };
        this.Provider.DiscoveredRobotEvent += robotsHandler;

        this.Provider.FindRobots();

        await Task.WhenAny(completionSource.Task, Task.Delay(timeout));
        this.Provider.DiscoveredRobotEvent -= robotsHandler;
      }
    }
    void OnSpheroConnectionStateChanged(Robot sender, ConnectionState state)
    {
      var handlers = this.SpheroConnectionChanged;

      if ((state == ConnectionState.Disconnected) ||
        (state == ConnectionState.Connected))
      {
        handlers(this, EventArgs.Empty);
      }
      if (state == ConnectionState.Connected)
      {
        this.sphero.SetBackLED(1.0f);
      }
    }
    async Task ConnectDiscoveredSpheroAsync(TimeSpan timeout)
    {
      TaskCompletionSource<bool> completionSource =
        new TaskCompletionSource<bool>();

      if (this.HasProvider && this.HasSphero &&
        !this.HasConnectedSphero)
      {
        EventHandler<Robot> robotsHandler = null;

        robotsHandler = (s, robot) =>
        {
          completionSource.SetResult(true);
        };
        this.Provider.ConnectedRobotEvent += robotsHandler;

        AsyncExceptionSwallowingContext ctx = new AsyncExceptionSwallowingContext();

        ctx.Replace();
        this.Provider.ConnectRobot(this.sphero);
        ctx.Replace();

        await Task.WhenAny(completionSource.Task, Task.Delay(timeout));
        this.Provider.ConnectedRobotEvent -= robotsHandler;
      }
    }
    float speed;
    int angle;
    Sphero sphero;
  }
}