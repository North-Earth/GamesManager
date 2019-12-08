using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace GamesManager.Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string name = "GamesManager.Launcher";

            if (GetProcesses(name).Count > 1)
            {
                MessageBox.Show("Application is already running.");
                Application.Current.Shutdown();
            }

            InitializeComponent();
        }

        public static IReadOnlyList<Process> GetProcesses(string name) 
            => Process.GetProcesses().Where(p => p.ProcessName.Contains(name)).ToList();
    }
}
