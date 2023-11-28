using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace gouito.Example;

public class ClickerViewModel: ViewModel
{
    #region Fields

    
    private int _clicks;
    
    #endregion

    #region Properties
    
    public int Clicks
    {
        get => _clicks;
        private set
        {
            if (_clicks < value)
            {
                Timestamps.Add(DateTime.Now.ToString("hh:mm:ss"));
            }
            
            SetField(ref _clicks, value);
        }
    }

    public ObservableCollection<string> Timestamps { get; } = new();

    #endregion

    #region Command

    public ICommand ClickCommand { get; }

    #endregion

    public ClickerViewModel()
    {
        ClickCommand = new RelayCommand(() => Clicks++);
    }
    
}