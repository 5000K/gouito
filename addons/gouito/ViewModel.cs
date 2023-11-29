// part of 5000K/gouito, licensed under MIT. Get a license under https://github.com/5000K/gouito.

// ReSharper disable CheckNamespace

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
// ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
#pragma warning disable CS8612 // Nullability of reference types in type doesn't match implicitly implemented member.

namespace gouito;

/// <summary>
/// Base class for view models, implements basic INotifyPropertyChanged logic
/// </summary>
public abstract class ViewModel: INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged = null!;

    protected virtual void OnPropertyChanged([CallerMemberName][AllowNull] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName][AllowNull] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}