using System.ComponentModel;
using Godot;

namespace gouito.Target;

/// <summary>
/// Binding that automatically binds to various text-outputs
/// </summary>
public class TextBindingTarget: IBindingTarget<string>
{
    private IBindingTarget<string> _subBindingTarget;

    public event PropertyChangedEventHandler PropertyChanged
    {
        add => _subBindingTarget.PropertyChanged += value;
        remove => _subBindingTarget.PropertyChanged -= value;
    }

    public TextBindingTarget(Label label)
    {
        _subBindingTarget = new LabelTextBindingTarget(label);
    }
    
    public TextBindingTarget(Label3D label)
    {
        _subBindingTarget = new Label3DTextBindingTarget(label);
    }

    public TextBindingTarget(TextEdit edit)
    {
        _subBindingTarget = new TextEditTextBindingTarget(edit);
    }

    public TextBindingTarget(RichTextLabel edit)
    {
        _subBindingTarget = new RichTextLabelTextBindingTarget(edit);
    }

    public string PropertyName => _subBindingTarget.PropertyName;

    public string Value
    {
        get => _subBindingTarget.Value;
        set => _subBindingTarget.Value = value;
    }

    #region Implementations
    
    private class LabelTextBindingTarget: IBindingTarget<string>
    {
        private Label _label;

        public LabelTextBindingTarget(Label label)
        {
            _label = label;
        }

        private void DummyPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string PropertyName { get; } = "text";

        public string Value
        {
            get => _label.Text;
            set => _label.Text = value;
        }
    }

    private class RichTextLabelTextBindingTarget: IBindingTarget<string>
    {
        private RichTextLabel _label;

        public RichTextLabelTextBindingTarget(RichTextLabel label)
        {
            _label = label;
        }

        private void DummyPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string PropertyName { get; } = "text";

        public string Value
        {
            get => _label.Text;
            set => _label.Text = value;
        }
    }

    private class Label3DTextBindingTarget: IBindingTarget<string>
    {
        private Label3D _label;

        public Label3DTextBindingTarget(Label3D label)
        {
            _label = label;
        }

        private void DummyPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string PropertyName { get; } = "text";

        public string Value
        {
            get => _label.Text;
            set => _label.Text = value;
        }
    }

    private class TextEditTextBindingTarget: IBindingTarget<string>
    {
        private TextEdit _textEdit;

        public TextEditTextBindingTarget(TextEdit textEdit)
        {
            _textEdit = textEdit;
            textEdit.TextSet += OnTextChanged;
        }

        private void OnTextChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string PropertyName { get; } = "text";

        public string Value
        {
            get => _textEdit.Text;
            set => _textEdit.Text = value;
        }
    }
    
    #endregion
}




