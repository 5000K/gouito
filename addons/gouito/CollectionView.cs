// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// no nullability warning (warning on => nullable projects care, warning off => no project cares. implement nullability => standard projects care. ==> turn off warning for now.) 
// ReSharper disable CheckNamespace
#pragma warning disable CS8612

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Godot;
using Container = Godot.Container;

namespace gouito;

/// <summary>
/// Automatically fills a container it's attached to with children ui elements
/// </summary>
/// <typeparam name="TModel"></typeparam>
public partial class CollectionView<TModel>: Container, IBindingTarget<ObservableCollection<TModel>>
{
    public event PropertyChangedEventHandler PropertyChanged;
    string IBindingTarget<ObservableCollection<TModel>>.PropertyName => "list";

    public ObservableCollection<TModel> Value
    {
        get => _value;
        set
        {
            if (Equals(value, _value))
            {
                return;
            }

            if (_value != null)
            {
                _value.CollectionChanged -= OnCollectionChanged;
            }
            
            value.CollectionChanged += OnCollectionChanged;
            
            _value = value;
            RecreateChildren();
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("list"));
        }
    }

    [Export] public PackedScene Scene;
    private ObservableCollection<TModel> _value = new();

    public CollectionView()
    {
        RecreateChildren();
    }

    private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        RecreateChildren();
    }

    private void RecreateChildren()
    {
        this.Fill<Node>(Scene, Value.Count);

        this.ModifyChildren<IBindingTarget<TModel>>((node, i) =>
        {
            node.Value = Value[i];
        });
    }
}