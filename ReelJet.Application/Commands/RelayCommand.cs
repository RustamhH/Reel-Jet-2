using System;
using System.Windows.Input;

namespace Reel_Jet.Commands;

public class RelayCommand : ICommand {

    public event EventHandler? CanExecuteChanged {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public Predicate<object?>? _canExcecute;
    public Action<object?>? _excecute;

    public RelayCommand(Action<object?>? excecute, Predicate<object?>? canExcecute = null) {
        _canExcecute = canExcecute;
        _excecute = excecute;
    }

    public bool CanExecute(object? parameter) {
        
        if (_canExcecute == null) return true;
        return _canExcecute(parameter);
    }

    public void Execute(object? parameter) {
        _excecute?.Invoke(parameter);
    }
}