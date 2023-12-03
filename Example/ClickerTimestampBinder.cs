using System.ComponentModel;
using Godot;
using gouito.Target;

namespace gouito.Example;

public partial class ClickerTimestampBinder: Control, IBindingTarget<string>
{
	[Export] private Label _label;
	
	private IBindingTarget<string> _bindingTargetImplementation;

	public override void _Ready()
	{
		_bindingTargetImplementation = new TextBindingTarget(_label);
	}

	public event PropertyChangedEventHandler PropertyChanged
	{
		add => _bindingTargetImplementation.PropertyChanged += value;
		remove => _bindingTargetImplementation.PropertyChanged -= value;
	}

	public event IBindingTarget<string>.ManagedNodeDisposedHandler ManagedNodeDisposed;

	public override void _ExitTree()
	{
		ManagedNodeDisposed?.Invoke(this);
	}

	string IBindingTarget<string>.PropertyName => _bindingTargetImplementation.PropertyName;

	public string Value
	{
		get => _bindingTargetImplementation.Value;
		set => _bindingTargetImplementation.Value = value;
	}
}
