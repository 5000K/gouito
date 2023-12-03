// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.ComponentModel;
using System.Windows.Input;
using Godot;
// ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.

namespace gouito.Target;

public enum ButtonCommandExecutionPolicy
{
    
    OnDown,
    OnUp,
}

public class CommandTarget: IBindingTarget<ICommand>
{
    private readonly BaseButton _button;
    private readonly ButtonCommandExecutionPolicy _policy;
    private ICommand _value;
    public event PropertyChangedEventHandler PropertyChanged = null!;
    public string PropertyName => "command";

    public ICommand Value
    {
        get => _value;
        set
        {
            _value = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

    public CommandTarget(BaseButton button, ButtonCommandExecutionPolicy policy = ButtonCommandExecutionPolicy.OnDown)
    {
        _button = button;
        _policy = policy;

        _value = RelayCommand.NullCommand;

        if (_policy == ButtonCommandExecutionPolicy.OnDown)
        {
            button.ButtonDown += OnButtonPressed;
        }

        if (_policy == ButtonCommandExecutionPolicy.OnUp)
        {
            button.ButtonUp += OnButtonPressed;
        }
    }

    ~CommandTarget()
    {
        if (GodotObject.IsInstanceValid(_button))
        {
            if (_policy == ButtonCommandExecutionPolicy.OnDown)
            {
                _button.ButtonDown -= OnButtonPressed;
            }

            if (_policy == ButtonCommandExecutionPolicy.OnUp)
            {
                _button.ButtonUp -= OnButtonPressed;
            }
        }
    }

    private void OnButtonPressed()
    {
        if (Value.CanExecute(this))
        {
            Value.Execute(this);
        }
    }
}

public static partial class TargetExtensions
{
    public static IBindingTarget<ICommand> CommandTarget(this BaseButton button, ButtonCommandExecutionPolicy policy = ButtonCommandExecutionPolicy.OnDown)
    {
        return new CommandTarget(button, policy);
    }
}