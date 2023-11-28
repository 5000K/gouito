﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Godot.Bridge;
using PropertyInfo = System.Reflection.PropertyInfo;

namespace gouito;

[Flags]
public enum BindingDirection
{
    Once = 0,
    OneWay = 1,
    OneWayToSource = 2,
    
    TwoWay = OneWay | OneWayToSource,
}


public sealed class Binding<TBinding,TSource> where TSource: INotifyPropertyChanged
{
    
    private readonly IBindingTarget<TBinding> _target;
    private Expression<Func<TSource, TBinding>> _propertyGetter;
    private IMemberAdapter<TBinding> _adapter;

    public Binding(IBindingTarget<TBinding> target, TSource source, Expression<Func<TSource, TBinding>> propertyGetter, BindingDirection direction)
    {
        _target = target;
        
        // TODO: replace with linq analysis
        _propertyGetter = propertyGetter;

        var expr = (MemberExpression)propertyGetter.Body;

        if (expr.Member is PropertyInfo info)
        {
            _adapter = new PropertyAdapter<TBinding>(info, source);
        }
        else
        {
            throw new GouitoNoPropertyException();
        }

        if (direction.HasFlag(BindingDirection.OneWay))
        {
            source.PropertyChanged += OnSourcePropertyChanged;
        }

        if (direction.HasFlag(BindingDirection.OneWayToSource))
        {
            target.PropertyChanged += OnTargetPropertyChanged;
        }

        // always set one way adapter -> target at initializing the adapter
        _target.Value = _adapter.Value;
    }

    private void OnTargetPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != _target.PropertyName) return;
        
        if (!Equals(_adapter.Value, _target.Value))
        {
            _adapter.Value = _adapter.Value;
        }
    }

    private void OnSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != _adapter.Name) return;
        
        if (!Equals(_adapter.Value, _target.Value))
        {
            _target.Value = _adapter.Value;
        }
    }

    private interface IMemberAdapter<TMember>
    {
        public string Name { get; }
        public TMember Value { get; set; }
    }

    private class PropertyAdapter<TMember>: IMemberAdapter<TMember>
    {
        private readonly PropertyInfo _propertyInfo;
        private readonly object _parentObj;

        public PropertyAdapter(PropertyInfo propertyInfo, object parentObj)
        {
            _propertyInfo = propertyInfo;
            _parentObj = parentObj;
        }

        public string Name => _propertyInfo.Name;

        public TMember Value
        {
            get
            {
                if (_propertyInfo.GetValue(_parentObj) is TMember member)
                {
                    return member;
                }

                return default!;
            }
            set => _propertyInfo.SetValue(_parentObj, value);
        }
    }
    
}