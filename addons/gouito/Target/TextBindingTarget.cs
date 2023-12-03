// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.ComponentModel;
using Godot;
// ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.
#pragma warning disable CS0067
#pragma warning disable CS0414

namespace gouito.Target;

/// <summary>
/// Binding that automatically correctly binds to various text-inputs/outputs
/// </summary>
public class TextBindingTarget: IBindingTarget<string>
{
    private readonly IBindingTarget<string> _subBindingTarget;

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
    
    public TextBindingTarget(LineEdit edit)
    {
        _subBindingTarget = new LineEditTextBindingTarget(edit);
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
    
    public class LabelTextBindingTarget: IBindingTarget<string>
    {
        private readonly Label _label;

        public LabelTextBindingTarget(Label label)
        {
            _label = label;
        }
        
        public event PropertyChangedEventHandler PropertyChanged = null!;
        
        public string PropertyName => "text";

        public string Value
        {
            get => _label.Text;
            set => _label.Text = value;
        }
    }

    public class RichTextLabelTextBindingTarget: IBindingTarget<string>
    {
        private readonly RichTextLabel _label;

        public RichTextLabelTextBindingTarget(RichTextLabel label)
        {
            _label = label;
        }
        
        public event PropertyChangedEventHandler PropertyChanged = null!;

        public string PropertyName => "text";

        public string Value
        {
            get => _label.Text;
            set => _label.Text = value;
        }
    }

    public class Label3DTextBindingTarget: IBindingTarget<string>
    {
        private readonly Label3D _label;

        public Label3DTextBindingTarget(Label3D label)
        {
            _label = label;
        }
        
        public event PropertyChangedEventHandler PropertyChanged = null!;
        
        public string PropertyName => "text";

        public string Value
        {
            get => _label.Text;
            set => _label.Text = value;
        }
    }

    public class TextEditTextBindingTarget: IBindingTarget<string>
    {
        private readonly TextEdit _textEdit;

        public TextEditTextBindingTarget(TextEdit textEdit)
        {
            _textEdit = textEdit;
            textEdit.TextSet += OnTextChanged;
        }

        private void OnTextChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        
        public event PropertyChangedEventHandler PropertyChanged = null!;
        
        public string PropertyName => "text";

        public string Value
        {
            get => _textEdit.Text;
            set => _textEdit.Text = value;
        }
    }


    public class LineEditTextBindingTarget: IBindingTarget<string>
    {
        private readonly LineEdit _textEdit;

        public LineEditTextBindingTarget(LineEdit textEdit)
        {
            _textEdit = textEdit;
            textEdit.TextSubmitted += OnTextChanged;
        }

        private void OnTextChanged(string _)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        
        public event PropertyChangedEventHandler PropertyChanged = null!;
        
        public string PropertyName => "text";

        public string Value
        {
            get => _textEdit.Text;
            set => _textEdit.Text = value;
        }
    }


    #endregion
}


public static partial class TargetExtensions
{
    public static IBindingTarget<string> TextTarget(this Label node)
    {
        return new TextBindingTarget.LabelTextBindingTarget(node);
    }
    
    public static IBindingTarget<string> TextTarget(this Label3D node)
    {
        return new TextBindingTarget.Label3DTextBindingTarget(node);
    }
    
    public static IBindingTarget<string> TextTarget(this TextEdit node)
    {
        return new TextBindingTarget.TextEditTextBindingTarget(node);
    }
    
    public static IBindingTarget<string> TextTarget(this LineEdit node)
    {
        return new TextBindingTarget.LineEditTextBindingTarget(node);
    }
    
    public static IBindingTarget<string> TextTarget(this RichTextLabel node)
    {
        return new TextBindingTarget.RichTextLabelTextBindingTarget(node);
    }
}



