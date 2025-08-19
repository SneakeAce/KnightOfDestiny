public class CommandInvoker : ICommandInvoker
{
    private ICommand _command;

    public void AddCommand(ICommand command)
    {
        _command = command;
    }

    public void ExecuteCommand()
    {
        _command.Execute();
    }
}
