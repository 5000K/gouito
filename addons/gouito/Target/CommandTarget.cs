using System.ComponentModel;
using System.Windows.Input;
using Godot;

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
    public event PropertyChangedEventHandler PropertyChanged;
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
        if (Value != null && Value.CanExecute(this))
        {
            Value.Execute(this);
        }
    }
}