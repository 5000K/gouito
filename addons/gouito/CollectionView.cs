﻿// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Godot;
using Container = Godot.Container;
// ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.

namespace gouito;

/// <summary>
/// Automatically fills a container it's attached to with children ui elements
/// </summary>
/// <typeparam name="TModel"></typeparam>
public partial class CollectionView<TModel>: Container, IBindingTarget<ObservableCollection<TModel>>
{
    public event PropertyChangedEventHandler PropertyChanged = null!;
    
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

            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (value != null)
            {
                value.CollectionChanged += OnCollectionChanged;
            }
            
            _value = value;
            RecreateChildren();
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("list"));
        }
    }

    [Export] public PackedScene Scene = null!;
    [MaybeNull] private ObservableCollection<TModel> _value = new();

    public CollectionView()
    {
        RecreateChildren();
        Value = new();
    }

    public override void _ExitTree()
    {
        Value = null;
        ManagedNodeDisposed?.Invoke(this);
    }

    public event IBindingTarget<ObservableCollection<TModel>>.ManagedNodeDisposedHandler ManagedNodeDisposed;

    private void OnCollectionChanged([AllowNull]object sender, NotifyCollectionChangedEventArgs e)
    {
        RecreateChildren();
    }

    private void RecreateChildren()
    {
        if (Value == null)
        {
            return;
        }
        
        this.Fill<Node>(Scene, Value.Count);

        this.ModifyChildren<IBindingTarget<TModel>>((node, i) =>
        {
            node.Value = Value[i];
        });
    }
}