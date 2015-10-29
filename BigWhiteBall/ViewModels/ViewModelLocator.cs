namespace BigWhiteBall.ViewModels
{
  using Autofac;
  using System;
  using System.ComponentModel;

  public class ViewModelLocator
  {
    public ViewModelLocator()
    {
    }
    public void Build(
      Action<ContainerBuilder> buildProcessor)
    {
      ContainerBuilder builder = new ContainerBuilder();

      buildProcessor(builder);

      this.container = builder.Build();
    }
    public INotifyPropertyChanged this[string viewName]
    {
      get
      {
        return (this.container.ResolveNamed<INotifyPropertyChanged>(viewName));
      }
    }
    IContainer container;
  }
}
