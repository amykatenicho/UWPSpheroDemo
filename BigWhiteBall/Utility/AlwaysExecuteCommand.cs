namespace BigWhiteBall.Utility
{
  using System;
  using System.Windows.Input;

  class AlwaysExecuteCommand : ICommand
  {
    public event EventHandler CanExecuteChanged;

    public AlwaysExecuteCommand(Action action)
    {
      this._action = action;
    }
    public bool CanExecute(object parameter)
    {
      return (true);
    }
    public void Execute(object parameter)
    {
      if (this._action != null)
      {
        this._action();
      }
    }
    Action _action;
  }
}