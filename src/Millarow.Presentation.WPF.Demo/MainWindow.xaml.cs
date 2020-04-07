using Millarow.Presentation.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Millarow.Presentation.WPF.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : CustomWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(CustomWindow));

            var props = typeof(SystemColors).GetProperties(BindingFlags.Static | BindingFlags.Public).Where(x => x.PropertyType == typeof(SolidColorBrush)).ToArray();

            Brushes.ItemsSource = props.Select(x => new
            {
                Brush = x.GetValue(null),
                Name = $"{x.Name} ({x.GetValue(null).ToString()})"
            }).ToArray();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
