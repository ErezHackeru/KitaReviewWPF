using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfAppSafeInvoke
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int maxNum = Convert.ToInt32(mytxtblock.Text);
            Task.Run(() =>
            {
                for (int i = maxNum; i >= 0; i--)
                {
                    Action uiwork = () => { mytxtblock.Text = i.ToString(); };                    
                    SafeInvoke(uiwork);
                    Thread.Sleep(250);
                }
            });
        }

        private void SafeInvoke(Action work)
        {
            if (Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                work.Invoke();
                return;
            }
            this.Dispatcher.BeginInvoke(work);
        }
    }
}
