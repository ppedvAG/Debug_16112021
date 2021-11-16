using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int c = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Content = $"clicked: {c++}";
        }

        private void KillIt(object sender, RoutedEventArgs e)
        {
            try
            {
                var txt = "3";

                var res = 12 / (c - int.Parse(txt));
            }
            catch (Exception)
            {

                MessageBox.Show("AAAAAA");
            }
        }
    }
}
