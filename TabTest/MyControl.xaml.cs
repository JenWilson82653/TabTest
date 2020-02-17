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
    public partial class MyControl : UserControl
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
            DependencyProperty.Register("SelectedTabIndex", typeof(int), typeof(MyControl), new FrameworkPropertyMetadata(0, SelectedTabIndexChanged) { BindsTwoWayByDefault = true });


        public MyControl()
        {
            InitializeComponent();
        }

        private static void ViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyControl thisControl = d as MyControl;

            if (e.OldValue is INotifyPropertyChanged oldViewModel)
                oldViewModel.PropertyChanged -= thisControl.ViewModel_PropertyChanged;
            if (e.NewValue is INotifyPropertyChanged newViewModel)
                newViewModel.PropertyChanged += thisControl.ViewModel_PropertyChanged;

            thisControl.SetCurrentValue(SelectedTabIndexProperty, thisControl.ViewModel.SelectedTabIndex);
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SetCurrentValue(SelectedTabIndexProperty, ViewModel.SelectedTabIndex);
        }

        private static void SelectedTabIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MyControl thisControl = d as MyControl;
            if (thisControl.ViewModel != null)
                thisControl.ViewModel.SelectedTabIndex = thisControl.SelectedTabIndex;
        }
    }
}
