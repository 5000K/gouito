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
        set => SetField(ref _clicks, value);
    }

    #endregion

    #region Command

    public ICommand ClickCommand { get; }

    #endregion

    public ClickerViewModel()
    {
        ClickCommand = new RelayCommand(() => Clicks++);
    }
    
}