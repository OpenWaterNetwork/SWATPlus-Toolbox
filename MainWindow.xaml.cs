using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// Added
using Microsoft.Win32;

// Custom
using static SWAT__Toolbox.observations_classes;
using static SWAT__Toolbox.observations_functions;
using static SWAT__Toolbox.parameters_module;
using static SWAT__Toolbox.toolbox_functions;

namespace SWAT__Toolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<parameter> selected_parameters = new ObservableCollection<parameter>();
        ObservableCollection<observation> selected_observations = new ObservableCollection<observation>();

        public MainWindow()
        {
            InitializeComponent();
            WindowChrome.SetWindowChrome(this, new WindowChrome());

            ui_selected_parameters.ItemsSource = selected_parameters;
            ui_selected_observations.ItemsSource = selected_observations;

            // Python in C# https://pythonnet.github.io/

            this.DataContext = this;

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
            // All, Hydrologic Response Unit, Aquifer, Routing, HRU LTE, Soil, Reservoir, SWQ
            ui_parameter_name.ItemsSource = null;
            ui_parameter_name.Items.Clear();
            ui_parameter_name.DisplayMemberPath = "name";
            ui_parameter_name.SelectedValuePath = "name";

            if (ui_parameter_group.SelectedIndex == 0)
            {
                ui_parameter_name.ItemsSource = all_parameters();
            }
            else if (ui_parameter_group.SelectedIndex == 1)
            {
                ui_parameter_name.ItemsSource = hru_parameters();
            }
            else if (ui_parameter_group.SelectedIndex == 2)
            {
                ui_parameter_name.ItemsSource = aqu_parameters();
            }
            else if (ui_parameter_group.SelectedIndex == 3)
            {
                ui_parameter_name.ItemsSource = rte_parameters();
            }
            else if (ui_parameter_group.SelectedIndex == 4)
            {
                ui_parameter_name.ItemsSource = hlt_parameters();
            }
            else if (ui_parameter_group.SelectedIndex == 5)
            {
                ui_parameter_name.ItemsSource = sol_parameters();
            }
            else if (ui_parameter_group.SelectedIndex == 6)
            {
                ui_parameter_name.ItemsSource = res_parameters();
            }
            else if (ui_parameter_group.SelectedIndex == 7)
            {
                ui_parameter_name.ItemsSource = swq_parameters();
            }
            else
            {

            }
        }

        private void pick_observation_file(object sender, RoutedEventArgs e)
        {
            string file_name = pick_file("Comma Separated|*.csv", "Select and Observation File", "Select");
            ui_parameters_file_selection_path.Text = file_name;
        }
        private void clear_observation_file(object sender, RoutedEventArgs e)
        {
            ui_parameters_file_selection_path.Text = "Click to select an observation file";
        }

        private void add_observation(object sender, RoutedEventArgs e)
        {
            //get objects values from user

            string obj_type = get_observation_object_type(ui_observations_object_type.SelectedIndex + 1);
            string obs_variable = get_observation_variable(ui_observations_observed_variable.SelectedIndex + 1);
            int obj_number = int.Parse(ui_observations_object_number.Text);

            selected_observations.Add(new observation() { file = ui_parameters_file_selection_path.Text, id = 1, number = obj_number, object_type = obj_type, observed_variable = obs_variable });
        }

        private void add_parameter(object sender, RoutedEventArgs e)
        {
            string par_obj = (string)ui_parameter_name.SelectedValue;

            foreach (var par_item in all_parameters())
            {
                if (par_obj == par_item.name)
                {
                    parameter additional_parameter = par_item;
                    additional_parameter.change_type = get_parameter_change_type(ui_parameter_change_type.SelectedIndex + 1);
                    additional_parameter.maximum = double.Parse(ui_parameter_maximum.Text);
                    additional_parameter.minimum = double.Parse(ui_parameter_minimum.Text);
                    selected_parameters.Add(additional_parameter);

                }
                else
                {
                    
                }
            }
            //MessageBox.Show(par_obj);
        }
    }
}
