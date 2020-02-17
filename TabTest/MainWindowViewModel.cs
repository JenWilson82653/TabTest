using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabTest
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private int _SelectedTabIndex;
        public int SelectedTabIndex
        {
            get => _SelectedTabIndex;
            set => SetProp(ref _SelectedTabIndex, value);
        }


        private MyControlViewModel _MyControlViewModel1;
        public MyControlViewModel MyControlViewModel1
        {
            get => _MyControlViewModel1;
            set => SetProp(ref _MyControlViewModel1, value);
        }


        private MyControlViewModel _MyControlViewModel2;
        public MyControlViewModel MyControlViewModel2
        {
            get => _MyControlViewModel2;
            set => SetProp(ref _MyControlViewModel2, value);
        }

        private string _SomeString;
        public string SomeString
        {
            get => _SomeString;
            set => SetProp(ref _SomeString, value);
        }

        public MainWindowViewModel()
        {
            MyControlViewModel1 = new MyControlViewModel();
            MyControlViewModel2 = new MyControlViewModel();
            SelectedTabIndex = 0;
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
