using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TabTest
{
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
    public partial class MyControl : UserControl, INotifyPropertyChanged
    {

        public MyControlViewModel ViewModel
        {
            get { return (MyControlViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MyControlViewModel), typeof(MyControl), new PropertyMetadata(default(MyControlViewModel), ViewModelChanged));
        

        public int SelectedTabIndex
        {
            get { return (int)GetValue(SelectedTabIndexProperty); }
            set { SetValue(SelectedTabIndexProperty, value); }
        }

        public static readonly DependencyProperty SelectedTabIndexProperty =
            DependencyProperty.Register("SelectedTabIndex", typeof(int), typeof(MyControl), new FrameworkPropertyMetadata(0, SelectedTabIndexChanged) { BindsTwoWayByDefault=true });


        public MyControl()
        {
            InitializeComponent();
        }

        private static void ViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyControl thisControl = d as MyControl;
            thisControl.RaisePropertyChanged(nameof(thisControl.ViewModel));

            if (thisControl.ViewModel != null)
            {
                //Binding b = new Binding();
                //b.Source = thisControl.ViewModel;
                //b.Path = new PropertyPath("SelectedTabIndex");
                //b.Mode = BindingMode.TwoWay;
                //b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                //BindingOperations.SetBinding(thisControl, SelectedTabIndexProperty, b);

                thisControl.SelectedTabIndex = thisControl.ViewModel.SelectedTabIndex;
            }
        }

        private static void SelectedTabIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyControl thisControl = d as MyControl;
            thisControl.RaisePropertyChanged(nameof(thisControl.SelectedTabIndex));

            int newvalue = (int)e.NewValue; // diagnostic
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
