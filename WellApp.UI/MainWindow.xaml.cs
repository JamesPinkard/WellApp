using System.Linq;
using System.Windows;
using System.Windows.Threading;
using System.Threading;
using WellApp.Data;
using System.Collections.Generic;
using WellApp.Repo.Text;

namespace WellApp.UI
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
            NewWindowHandler(sender, e);                   
        }

        private void NewWindowHandler(object sender, RoutedEventArgs e)
        {
            var newWindowThread = new Thread(ThreadStartingPoint);
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
        }

        private void ThreadStartingPoint()
        {
            var tempWindow = new ProgressBarWindow();
            tempWindow.Show();
            Dispatcher.Run();
        }        
    }
}
