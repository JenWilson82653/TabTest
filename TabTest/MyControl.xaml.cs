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
    public partial class MyControl : UserControl /* controls don't implement INotifyPropertyChanged, use DependencyProperty instead */
    {

        public MyControlViewModel ViewModel
        {
            get { return (MyControlViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MyControlViewModel), typeof(MyControl), new PropertyMetadata(default(MyControlViewModel)));
        

        public int SelectedTabIndex
        {
            get { return (int)GetValue(SelectedTabIndexProperty); }
            set { SetValue(SelectedTabIndexProperty, value); }
        }

        public static readonly DependencyProperty SelectedTabIndexProperty =
            DependencyProperty.Register("SelectedTabIndex", typeof(int), typeof(MyControl), new FrameworkPropertyMetadata(0) { BindsTwoWayByDefault=true });


        public MyControl()
        {
            InitializeComponent();

            // setting the Source to operate on the control, otherwise we'd operate on the DataContext
            SetBinding(SelectedTabIndexProperty, new Binding("ViewModel.SelectedTabIndex") { Source = this });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetCurrentValue(SelectedTabIndexProperty, SelectedTabIndex == 0 ? 1 : 0);
        }
    }
}
