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
using System.Windows.Shell;

namespace SWAT__Toolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<parameter> selected_parameters = new List<parameter>();

        public class parameter
        {
            public string name { get; set; }
            public double min { get; set; }
            public double max { get; set; }
            public double value { get; set; }
            public int change_type { get; set; }
            //public List<int> assigned { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            WindowChrome.SetWindowChrome(this, new WindowChrome());

            selected_parameters.Add(new parameter() { name = "cn2", min = 40.2, max = 89.5, change_type = 1, value = 71.2 });
            selected_parameters.Add(new parameter() { name = "esco", min = 35.2, max = 89.5, change_type = 2, value = 71.2 });

            ui_selected_parameters.ItemsSource = selected_parameters;

            // Python in C# https://pythonnet.github.io/




        }


        private void WindowMinimise(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void RestoreMaximise(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void navigate_homepage(object sender, RoutedEventArgs e)
        {
            pages.SelectedIndex = 0;
        }
        private void navigate_runmodel(object sender, RoutedEventArgs e)
        {
            pages.SelectedIndex = 1;
        }

        private void navigate_parameters(object sender, RoutedEventArgs e)
        {
            pages.SelectedIndex = 2;

        }

        private void navigate_observations(object sender, RoutedEventArgs e)
        {
            pages.SelectedIndex = 3;

        }

        private void navigate_sensitivity(object sender, RoutedEventArgs e)
        {
            pages.SelectedIndex = 4;

        }

        private void navigate_calibration(object sender, RoutedEventArgs e)
        {
            pages.SelectedIndex = 5;

        }

        private void navigate_model_check(object sender, RoutedEventArgs e)
        {
            pages.SelectedIndex = 6;

        }

        //parameters page block
        // mapping info https://github.com/ThinkGeo/Desktop-Maps
        private void list_available_parameters(object sender, EventArgs e)
        {
            ui_parameter_name.Items.Clear();
            if (ui_parameter_group.SelectedIndex == 0)
            {
                ui_parameter_name.Items.Add("cn2");
            }
            else if (ui_parameter_group.SelectedIndex == 1)
            {
                ui_parameter_name.Items.Add("esco");
            }
            else
            {
                ui_parameter_name.Items.Add("cn2");
                ui_parameter_name.Items.Add("esco");


            }
        }
    }
}
