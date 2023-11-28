using Godot;
using gouito.Converter;
using gouito.Target;

namespace gouito.Example;

public partial class ClickerBinder: Control
{
    [Export] private Button _button;
    [Export] private Label _clicks;

    private readonly ClickerViewModel _vm = new();

    public override void _Ready()
    {
        _clicks.TextTarget()
            .Wrap(new IntStringConverter())
            .Bind(_vm, x => x.Clicks);

        _button.CommandTarget(ButtonCommandExecutionPolicy.OnDown)
            .Bind(_vm, x => x.ClickCommand);
    }
}