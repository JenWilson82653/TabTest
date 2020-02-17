using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TabTest
{
    public class MyControlViewModel: INotifyPropertyChanged
    {
        private int _SelectedTabIndex;
        public int SelectedTabIndex
        {
            get => _SelectedTabIndex;
            set => SetProp(ref _SelectedTabIndex, value);
        }

        public ICommand ChangeTabCommand { get; set; }

        public MyControlViewModel() => ChangeTabCommand = new DelegateCommand<int>(ChangeTabCommandHandler);

        public void ChangeTabCommandHandler(int value)
        {
            // The problem starts here.  We want to be able to set SelectedTabIndex in code and have
            // the value propagate through the bindings to MainWindowViewModel.

            SelectedTabIndex = SelectedTabIndex == 0 ? 1 : 0;
        }


        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberNameAttribute] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void SetProp<T>(ref T prop, T value, [CallerMemberNameAttribute] string propertyName = "")
        {
            if (!Object.Equals(prop, value))
            {
                prop = value;
                RaisePropertyChanged(propertyName);
            }
        }
        #endregion
    }
}
