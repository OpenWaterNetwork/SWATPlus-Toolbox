using System;
using System.IO;
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
using YamlDotNet.Serialization;

using LiveCharts;
using LiveCharts.Wpf;

// Added
using Microsoft.Win32;

// Custom
using static SWAT__Toolbox.observations_classes;
using static SWAT__Toolbox.observations_functions;
using static SWAT__Toolbox.parameters_module;
using static SWAT__Toolbox.toolbox_functions;
using static SWAT__Toolbox.home_module;
using static SWAT__Toolbox.run_model_module;
using static SWAT__Toolbox.model_evaluation_module;
using static SWAT__Toolbox.mannual_calibration_module;


using Path = System.IO.Path;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization.NamingConventions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MaterialDesignThemes.Wpf;
using System.Globalization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Media.Media3D;
using MaterialDesignColors;
using System.ComponentModel;
using System.Windows.Threading;

namespace SWAT__Toolbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<parameter> selected_parameters;
        ObservableCollection<observation> selected_observations;

        project current_project = new project();

        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection_auto { get; set; }
        public string[] Labels { get; set; }
        public string[] Labels_auto { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Observations",
                    Values = new ChartValues<double> { },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Simulations",
                    Values = new ChartValues<double> {},
                    PointGeometry = null
                },
            };
            DataContext = this;

            SeriesCollection_auto = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Observations",
                    Values = new ChartValues<double> { },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Simulations",
                    Values = new ChartValues<double> {},
                    PointGeometry = null
                },
            };
            DataContext = this;

            //ui init
            WindowChrome.SetWindowChrome(this, new WindowChrome());

            var paletteHelper = new PaletteHelper();
            //Retrieve the app's existing theme
            ITheme theme = paletteHelper.GetTheme();

            //Change the base theme to Ligh
            theme.SetBaseTheme(Theme.Light);

            //  //Change all of the primary colors to Red
            //  theme.SetPrimaryColor(System.Windows.Media.Color.FromRgb(21, 101, 192));
            //
            //Change all of the secondary colors to Orange
            theme.SetSecondaryColor(Colors.Orange);

            //  //You can also change a single color on the theme, and optionally set the corresponding foreground color
            //  theme.PrimaryMid = new ColorPair(Colors.Brown, Colors.White);

            //Change the app's current theme
            paletteHelper.SetTheme(theme);


            selected_parameters = new ObservableCollection<parameter>();
            selected_observations = new ObservableCollection<observation>();

            ui_selected_parameters.ItemsSource = selected_parameters;
            ui_selected_observations.ItemsSource = selected_observations;

            ui_calibration_manual_performance.ItemsSource = current_project.current_observations;
            ui_calibration_automatic_performance.ItemsSource = current_project.current_observations;




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
            string timestep = ui_observations_timestep.SelectionBoxItem.ToString();
            int obj_number = int.Parse(ui_observations_object_number.Text);

            selected_observations.Add(new observation()
            {
                file = ui_parameters_file_selection_path.Text,
                id = 1,
                number = obj_number,
                object_type = obj_type,
                observed_variable = obs_variable,
                timestep = timestep,
                chart_name = $@"{obj_type} {obj_number} {timestep} {obs_variable}",
                nse = 0,
                pbias = 0,
                r2 = 0
            });

            ui_observations_object_type.Text = null;
            ui_observations_observed_variable.Text = null;
            ui_observations_object_number.Text = null;
            ui_observations_timestep.Text = null;
            ui_observations_object_number.Text = null;
            ui_parameters_file_selection_path.Text = "Click to select an observation file";

            current_project.current_observations = selected_observations;
            update_project();
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

            ui_parameter_maximum.Text = null;
            ui_parameter_minimum.Text = null;

            ui_parameter_name.Text = null;
            ui_parameter_change_type.Text = null;

            current_project.current_parameters = selected_parameters;
            update_project();
        }

        private void home_set_project_location(object sender, RoutedEventArgs e)
        {
            ui_home_new_project_file_path.Text = pick_save_path("SWAT+ Toolbox Project|*.spt", "Choose Project Save Location", "OK");
        }

        private void home_set_txtinout_directory(object sender, RoutedEventArgs e)
        {
            ui_home_new_project_txtinout_path.Text = pick_folder("Choose a TxtInOut Directory", "OK");
        }

        private void home_new_project_save(object sender, RoutedEventArgs e)
        {
            current_project = new project();
            selected_parameters = new ObservableCollection<parameter>();
            selected_observations = new ObservableCollection<observation>();

            current_project.txtinout = ui_home_new_project_txtinout_path.Text;
            current_project.project_name = Path.GetFileName(ui_home_new_project_file_path.Text);
            current_project.project_path = ui_home_new_project_file_path.Text;
            current_project.current_observations = selected_observations;
            current_project.current_parameters = selected_parameters;

            current_project.created = DateTime.Now;
            current_project.last_modified = DateTime.Now;

            current_project.run_date_start = new DateTime(2000, 1, 1);
            current_project.run_date_end = new DateTime(2010, 1, 1);

            current_project.run_warmup = 2;
            current_project.print_csv = false;

            current_project.print_settings = default_print_settings();
            current_project.water_balance = default_water_balance();
            current_project.nutrient_balance = default_nutrient_balance();
            current_project.plant_summary = default_plant_results();

            current_project.sensivity_settings = default_sensitivity_analysis_settings();

            // saving the project
            save_project(current_project);

            ui_selected_parameters.ItemsSource = selected_parameters;
            ui_selected_observations.ItemsSource = selected_observations;

            ui_calibration_manual_performance.ItemsSource = current_project.current_observations;
            ui_calibration_automatic_performance.ItemsSource = current_project.current_observations;

            //binding checkboxes for printing
            set_binding_contexts();

            //run_model settings
            ui_run_model_start_date.SelectedDate = current_project.run_date_start;
            ui_run_model_end_date.SelectedDate = current_project.run_date_end;

            ui_run_model_warmup_period.Text = current_project.run_warmup.ToString();
        }

        private void home_open_project(object sender, RoutedEventArgs e)
        {
            open_project();
            ui_sensitivity_seed_box.Text = current_project.sensivity_settings.seed.ToString();
            ui_calibration_automatic_seed_box.Text = current_project.sensivity_settings.seed.ToString();
        }

        public void open_project()
        {
            current_project = new project();

            using (StreamReader streamReader = new StreamReader(pick_file("SWAT+ Toolbox Project|*.spt", "Choose Existing SWAT+ Toolbox Project", "OK")))
            {
                var deserializer = new DeserializerBuilder()
                    .Build();
                //Deserializer deserializer = new Deserializer();
                current_project = (project)deserializer.Deserialize(streamReader, typeof(project));
            }

            selected_parameters = current_project.current_parameters;
            selected_observations = current_project.current_observations;

            ui_selected_parameters.ItemsSource = selected_parameters;
            ui_selected_observations.ItemsSource = selected_observations;

            ui_calibration_manual_performance.ItemsSource = current_project.current_observations;
            ui_calibration_automatic_performance.ItemsSource = current_project.current_observations;

            //update ui
            //home

            //run_model
            ui_run_model_start_date.SelectedDate = current_project.run_date_start;
            ui_run_model_end_date.SelectedDate = current_project.run_date_end;

            ui_run_model_warmup_period.Text = current_project.run_warmup.ToString();

            //binding checkboxes for printing
            set_binding_contexts();

            //
        }

        // run model functions
        private void update_project_start_date(object sender, RoutedEventArgs e)
        {
            current_project.run_date_start = (DateTime)ui_run_model_start_date.SelectedDate;
            update_project();
        }
        private void update_project_end_date(object sender, RoutedEventArgs e)
        {
            current_project.run_date_end = (DateTime)ui_run_model_end_date.SelectedDate;
            update_project();
        }

        private void update_project_warmup(object sender, RoutedEventArgs e)
        {
            current_project.run_warmup = int.Parse(ui_run_model_warmup_period.Text);
            update_project();
        }

        private void update_print_settings(object sender, RoutedEventArgs e)
        {
            update_project();
        }

        private void update_parameter_value(object sender, DataGridCellEditEndingEventArgs e)
        {
            update_project();
        }

        private void update_project()
        {
            current_project.last_modified = DateTime.Now;
            save_project(current_project);
        }


        private void set_binding_contexts()
        {
            ui_calibration_manual_performance.ItemsSource = current_project.current_observations;
            ui_calibration_automatic_performance.ItemsSource = current_project.current_observations;

            ui_sensitivity_observation_selection.ItemsSource = current_project.current_observations;
            ui_sensitivity_observation_selection.DisplayMemberPath = "chart_name";
            ui_sensitivity_observation_selection.SelectedValuePath = "chart_name";

            ui_calibration_automatic_observation_selection.ItemsSource = current_project.current_observations;
            ui_calibration_automatic_observation_selection.DisplayMemberPath = "chart_name";
            ui_calibration_automatic_observation_selection.SelectedValuePath = "chart_name";

            ui_calibration_manual_performance.DataContext = current_project.current_observations;
            ui_calibration_automatic_performance.DataContext = current_project.current_observations;

            ui_sensitivity_seed_box.DataContext = current_project.sensivity_settings;

            ui_calibration_manual_available_charts.DataContext = current_project.current_observations;
            ui_calibration_manual_available_charts.ItemsSource = current_project.current_observations;

            ui_calibration_automatic_available_charts.DataContext = current_project.current_observations;
            ui_calibration_automatic_available_charts.ItemsSource = current_project.current_observations;



            ui_run_model_print_csv.DataContext = current_project;

            ui_calibration_mannual_parameters.ItemsSource = current_project.current_parameters;
            ui_calibration_mannual_parameters.DataContext = current_project.current_parameters;

            ui_calibration_automatic_parameters.ItemsSource = current_project.current_parameters;
            ui_calibration_automatic_parameters.DataContext = current_project.current_parameters;

            ui_sensitivity_parameters.ItemsSource = current_project.current_parameters;
            ui_sensitivity_parameters.DataContext = current_project.current_parameters;

            ui_run_model_print_channel_day.DataContext = current_project.print_settings.channel;
            ui_run_model_print_channel_month.DataContext = current_project.print_settings.channel;
            ui_run_model_print_channel_year.DataContext = current_project.print_settings.channel;
            ui_run_model_print_channel_aa.DataContext = current_project.print_settings.channel;

            ui_run_model_print_reservoir_day.DataContext = current_project.print_settings.reservoir;
            ui_run_model_print_reservoir_month.DataContext = current_project.print_settings.reservoir;
            ui_run_model_print_reservoir_year.DataContext = current_project.print_settings.reservoir;
            ui_run_model_print_reservoir_aa.DataContext = current_project.print_settings.reservoir;

            ui_run_model_print_aquifer_day.DataContext = current_project.print_settings.aquifer;
            ui_run_model_print_aquifer_month.DataContext = current_project.print_settings.aquifer;
            ui_run_model_print_aquifer_year.DataContext = current_project.print_settings.aquifer;
            ui_run_model_print_aquifer_aa.DataContext = current_project.print_settings.aquifer;

            ui_run_model_print_nut_basin_day.DataContext = current_project.print_settings.nut_basin;
            ui_run_model_print_nut_basin_month.DataContext = current_project.print_settings.nut_basin;
            ui_run_model_print_nut_basin_year.DataContext = current_project.print_settings.nut_basin;
            ui_run_model_print_nut_basin_aa.DataContext = current_project.print_settings.nut_basin;

            ui_run_model_print_nut_lsu_day.DataContext = current_project.print_settings.nut_lsu;
            ui_run_model_print_nut_lsu_month.DataContext = current_project.print_settings.nut_lsu;
            ui_run_model_print_nut_lsu_year.DataContext = current_project.print_settings.nut_lsu;
            ui_run_model_print_nut_lsu_aa.DataContext = current_project.print_settings.nut_lsu;

            ui_run_model_print_nut_hru_day.DataContext = current_project.print_settings.nut_hru;
            ui_run_model_print_nut_hru_month.DataContext = current_project.print_settings.nut_hru;
            ui_run_model_print_nut_hru_year.DataContext = current_project.print_settings.nut_hru;
            ui_run_model_print_nut_hru_aa.DataContext = current_project.print_settings.nut_hru;

            ui_run_model_print_wb_basin_day.DataContext = current_project.print_settings.wb_basin;
            ui_run_model_print_wb_basin_month.DataContext = current_project.print_settings.wb_basin;
            ui_run_model_print_wb_basin_year.DataContext = current_project.print_settings.wb_basin;
            ui_run_model_print_wb_basin_aa.DataContext = current_project.print_settings.wb_basin;

            ui_run_model_print_wb_lsu_day.DataContext = current_project.print_settings.wb_lsu;
            ui_run_model_print_wb_lsu_month.DataContext = current_project.print_settings.wb_lsu;
            ui_run_model_print_wb_lsu_year.DataContext = current_project.print_settings.wb_lsu;
            ui_run_model_print_wb_lsu_aa.DataContext = current_project.print_settings.wb_lsu;

            ui_run_model_print_wb_hru_day.DataContext = current_project.print_settings.wb_hru;
            ui_run_model_print_wb_hru_month.DataContext = current_project.print_settings.wb_hru;
            ui_run_model_print_wb_hru_year.DataContext = current_project.print_settings.wb_hru;
            ui_run_model_print_wb_hru_aa.DataContext = current_project.print_settings.wb_hru;

            ui_run_model_print_plt_basin_day.DataContext = current_project.print_settings.plt_basin;
            ui_run_model_print_plt_basin_month.DataContext = current_project.print_settings.plt_basin;
            ui_run_model_print_plt_basin_year.DataContext = current_project.print_settings.plt_basin;
            ui_run_model_print_plt_basin_aa.DataContext = current_project.print_settings.plt_basin;

            ui_run_model_print_plt_lsu_day.DataContext = current_project.print_settings.plt_lsu;
            ui_run_model_print_plt_lsu_month.DataContext = current_project.print_settings.plt_lsu;
            ui_run_model_print_plt_lsu_year.DataContext = current_project.print_settings.plt_lsu;
            ui_run_model_print_plt_lsu_aa.DataContext = current_project.print_settings.plt_lsu;

            ui_run_model_print_plt_hru_day.DataContext = current_project.print_settings.plt_hru;
            ui_run_model_print_plt_hru_month.DataContext = current_project.print_settings.plt_hru;
            ui_run_model_print_plt_hru_year.DataContext = current_project.print_settings.plt_hru;
            ui_run_model_print_plt_hru_aa.DataContext = current_project.print_settings.plt_hru;

        }


        public void run_swat()
        {
            //* Create your Process
            Process process = new Process();

            process.StartInfo.FileName = $@"{Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)}\\assets\\rev59_3_64rel.exe";
            //process.StartInfo.Arguments = "/c DIR";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            //* Set your output and error (asynchronous) handlers
            process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
            //* Start process and handlers
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            if (is_sensitivity_run == true)
            {
                process.WaitForExit();
            }
            if (is_calibration_run == true)
            {
                process.WaitForExit();
            }



        }

        public void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {

            //* Do your stuff with the output (write to console/log/StringBuilder)
            Console.WriteLine(outLine.Data);
            int current_year = 0;
            int current_month = 0;
            int current_day = 0;

            string no_space_line = "";
            try
            {
                no_space_line = Regex.Replace(outLine.Data, @"\s+", "");
            }
            catch (Exception)
            {
            }

            if (no_space_line.StartsWith("OriginalSimulation"))
            {
                string[] split = outLine.Data.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                current_year = int.Parse(split[4]);
                current_month = int.Parse(split[2]);
                current_day = int.Parse(split[3]);

                DateTime current_date = new DateTime(current_year, current_month, current_day, 0, 0, 0);

                int total_years = 1 + current_project.run_date_end.Year - current_project.run_date_start.Year;
                int year_count = 1 + current_year - current_project.run_date_start.Year;

                //to-do
                //tell user when in warm up
                //provide stopping mechanism

                this.Dispatcher.Invoke(() =>
                {
                    double all_total_days = Convert.ToDouble(new DateTime(current_project.run_date_end.Year, 12, 31).Subtract(new DateTime(current_project.run_date_start.Year, 1, 1)).TotalDays);
                    double all_past_days = Convert.ToDouble(current_date.Subtract(new DateTime(current_project.run_date_start.Year, 1, 1)).TotalDays);

                    ui_run_model_progress.Value = (double)(all_past_days / all_total_days) * 100;
                    ui_calibration_manual_run_swat_progress.Value = (double)(all_past_days / all_total_days) * 100;

                    double total_days = Convert.ToDouble(new DateTime(current_date.Year, 12, 31).Subtract(new DateTime(current_date.Year, 1, 1)).TotalDays);
                    ui_run_model_annual_progress.Value = (double)(Convert.ToDouble(current_date.DayOfYear) / total_days) * 100;

                    if (is_sensitivity_run == true)
                    {
                        ui_sensitivity_progress.Value = (double)(Convert.ToDouble(par_set_id) / number_of_sens_samples) * 100;
                    }
                    if (is_calibration_run == true)
                    {
                        ui_calibration_automatic_progress.Value = (double)(Convert.ToDouble(par_set_id) / number_of_sens_samples) * 100;
                    }

                    ui_run_model_running_year.Text = current_year.ToString();
                    ui_run_model_total_years.Text = current_project.run_date_end.Year.ToString();
                    ui_run_model_of_label.Text = "/";
                });

            }
        }


        private void run_swat_plus_model(object sender, RoutedEventArgs e)
        {
            Environment.CurrentDirectory = current_project.txtinout;

            File.WriteAllText($"{current_project.txtinout}\\print.prt", print_prt_file(current_project));
            File.WriteAllText($"{current_project.txtinout}\\time.sim", time_sim_file(current_project));

            ui_run_model_progress.DataContext = this;

            ui_run_model_run_icon.Kind = PackIconKind.Stop;
            ui_run_model_run_swat_button_text.Text = "Stop SWAT+";

            run_swat();
            ui_run_model_running_year.Text = "";
            ui_run_model_total_years.Text = "";
            ui_run_model_of_label.Text = "";

            ui_run_model_run_icon.Kind = PackIconKind.Play;
            ui_run_model_run_swat_button_text.Text = "Run SWAT+";


        }

        private string time_sim_file(project current_)
        {
            string time_sim = $@"time.sim: written by SWAT+ Toolbox v0.1
day_start  yrc_start   day_end   yrc_end      step  
        {current_.run_date_start.DayOfYear}      {current_.run_date_start.Year}         {current_.run_date_end.DayOfYear}      {current_.run_date_end.Year}         0  

";
            return time_sim;
        }
        private string print_prt_file(project current_)
        {
            string print_prt = $@"print.prt: written by SWAT+ Toolbox v0.1                                   
nyskip   day_start    yrc_start   day_end   yrc_end   interval                                     
{current_.run_warmup}        0            0           0         0         0
aa_int_cnt                                                                                         
0                                                                                                  
csvout    dbout         cdfout
{(current_.print_csv == true ? "y" : "n")}         n             n
soilout   mgtout     hydcon      fdcout
n         n          n           n
objects           daily     monthly        yearly          avann
basin_wb              {(current_.print_settings.wb_basin.daily == true ? "y" : "n")}           {(current_.print_settings.wb_basin.monthly == true ? "y" : "n")}             {(current_.print_settings.wb_basin.yearly == true ? "y" : "n")}              {(current_.print_settings.wb_basin.annual_average == true ? "y" : "n")}
basin_nb              {(current_.print_settings.nut_basin.daily == true ? "y" : "n")}           {(current_.print_settings.nut_basin.monthly == true ? "y" : "n")}             {(current_.print_settings.nut_basin.yearly == true ? "y" : "n")}              {(current_.print_settings.nut_basin.annual_average == true ? "y" : "n")}
basin_ls              n           n             n              n
basin_pw              {(current_.print_settings.plt_basin.daily == true ? "y" : "n")}           {(current_.print_settings.plt_basin.monthly == true ? "y" : "n")}             {(current_.print_settings.plt_basin.yearly == true ? "y" : "n")}              {(current_.print_settings.plt_basin.annual_average == true ? "y" : "n")}
basin_aqu             {(current_.print_settings.aquifer.daily == true ? "y" : "n")}           {(current_.print_settings.aquifer.monthly == true ? "y" : "n")}             {(current_.print_settings.aquifer.yearly == true ? "y" : "n")}              {(current_.print_settings.aquifer.annual_average == true ? "y" : "n")}
basin_res             n           n             n              n
basin_cha             n           n             n              n
basin_sd_cha          n           n             n              n
basin_psc             n           n             n              n
region_wb             n           n             n              n
region_nb             n           n             n              n
region_ls             n           n             n              n
region_pw             n           n             n              n
region_aqu            n           n             n              n
region_res            n           n             n              n
region_cha            n           n             n              n
region_sd_cha         n           n             n              n
region_psc            n           n             n              n
lsunit_wb             {(current_.print_settings.wb_lsu.daily == true ? "y" : "n")}           {(current_.print_settings.wb_lsu.monthly == true ? "y" : "n")}             {(current_.print_settings.wb_lsu.yearly == true ? "y" : "n")}              {(current_.print_settings.wb_lsu.annual_average == true ? "y" : "n")}
lsunit_nb             {(current_.print_settings.nut_lsu.daily == true ? "y" : "n")}           {(current_.print_settings.nut_lsu.monthly == true ? "y" : "n")}             {(current_.print_settings.nut_lsu.yearly == true ? "y" : "n")}              {(current_.print_settings.nut_lsu.annual_average == true ? "y" : "n")}
lsunit_ls             n           n             n              n
lsunit_pw             {(current_.print_settings.plt_lsu.daily == true ? "y" : "n")}           {(current_.print_settings.plt_lsu.monthly == true ? "y" : "n")}             {(current_.print_settings.plt_lsu.yearly == true ? "y" : "n")}              {(current_.print_settings.plt_lsu.annual_average == true ? "y" : "n")}
hru_wb                {(current_.print_settings.wb_hru.daily == true ? "y" : "n")}           {(current_.print_settings.wb_hru.monthly == true ? "y" : "n")}             {(current_.print_settings.wb_hru.yearly == true ? "y" : "n")}              {(current_.print_settings.wb_hru.annual_average == true ? "y" : "n")}
hru_nb                {(current_.print_settings.nut_hru.daily == true ? "y" : "n")}           {(current_.print_settings.nut_hru.monthly == true ? "y" : "n")}             {(current_.print_settings.nut_hru.yearly == true ? "y" : "n")}              {(current_.print_settings.nut_hru.annual_average == true ? "y" : "n")}
hru_ls                n           n             n              n
hru_pw                {(current_.print_settings.plt_hru.daily == true ? "y" : "n")}           {(current_.print_settings.plt_hru.monthly == true ? "y" : "n")}             {(current_.print_settings.plt_hru.yearly == true ? "y" : "n")}              {(current_.print_settings.plt_hru.annual_average == true ? "y" : "n")}
hru-lte_wb            n           n             n              n
hru-lte_nb            n           n             n              n
hru-lte_ls            n           n             n              n
hru-lte_pw            n           n             n              n
channel               {(current_.print_settings.channel.daily == true ? "y" : "n")}           {(current_.print_settings.channel.monthly == true ? "y" : "n")}             {(current_.print_settings.channel.yearly == true ? "y" : "n")}              {(current_.print_settings.channel.annual_average == true ? "y" : "n")}
channel_sd            {(current_.print_settings.channel.daily == true ? "y" : "n")}           {(current_.print_settings.channel.monthly == true ? "y" : "n")}             {(current_.print_settings.channel.yearly == true ? "y" : "n")}              {(current_.print_settings.channel.annual_average == true ? "y" : "n")}
aquifer               {(current_.print_settings.aquifer.daily == true ? "y" : "n")}           {(current_.print_settings.aquifer.monthly == true ? "y" : "n")}             {(current_.print_settings.aquifer.yearly == true ? "y" : "n")}              {(current_.print_settings.aquifer.annual_average == true ? "y" : "n")}
reservoir             {(current_.print_settings.reservoir.daily == true ? "y" : "n")}           {(current_.print_settings.reservoir.monthly == true ? "y" : "n")}             {(current_.print_settings.reservoir.yearly == true ? "y" : "n")}              {(current_.print_settings.reservoir.annual_average == true ? "y" : "n")}
recall                n           n             n              n
hyd                   n           n             n              n
ru                    n           n             n              n
";
            return print_prt;
        }



        private void analyse_model(object sender, RoutedEventArgs e)
        {
            ui_model_check_hydrology_image.Source = new BitmapImage(new Uri($@"{Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)}\\assets\\analysis_hydrology.dll"));

            results_analyse_wb();
            results_analyse_nb();
            results_analyse_plant_summary();


            int image_font_size = 55;


            //System.Drawing.Image bitmap;

            System.Drawing.Point precipitation_point = new System.Drawing.Point(2025, 520);
            System.Drawing.Point evapotranspiration_point = new System.Drawing.Point(3610, 710);
            System.Drawing.Point surface_runoff_point = new System.Drawing.Point(2820, 1400);
            System.Drawing.Point lateral_flow_point = new System.Drawing.Point(2150, 1270);
            System.Drawing.Point percolation_point = new System.Drawing.Point(2130, 1560);
            System.Drawing.Point revap_point = new System.Drawing.Point(1680, 1480);
            System.Drawing.Point return_flow_point = new System.Drawing.Point(2460, 1840);
            System.Drawing.Point irrigation_point = new System.Drawing.Point(4290, 1130);
            System.Drawing.Point pet_point = new System.Drawing.Point(3160, 540);
            System.Drawing.Point cn_point = new System.Drawing.Point(3140, 870);
            System.Drawing.Point deep_losses_point = new System.Drawing.Point(1670, 2330);
            System.Drawing.Point aquifer_recharge_point = new System.Drawing.Point(1640, 1890);


            System.Drawing.Bitmap bitmap;
            bitmap = (Bitmap)System.Drawing.Image.FromFile($@"{Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)}\\assets\\analysis_hydrology_arrows.dll");

            Graphics image_graphics = Graphics.FromImage(bitmap);
            image_graphics.SmoothingMode = SmoothingMode.AntiAlias; image_graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            StringFormat label_string_format = new StringFormat();
            label_string_format.Alignment = StringAlignment.Near;

            System.Drawing.Color label_string_colour = ColorTranslator.FromHtml("#000000");

            // add recharge
            image_graphics.DrawString($@"Recharge{System.Environment.NewLine}{current_project.water_balance.aqu_rchrg.ToString("0.##")}", new Font("Arial", image_font_size, System.Drawing.FontStyle.Regular),
                new SolidBrush(label_string_colour), aquifer_recharge_point, label_string_format);

            // add cn
            image_graphics.DrawString($@"CN{System.Environment.NewLine}{current_project.water_balance.cn.ToString("0.##")}", new Font("Arial", image_font_size, System.Drawing.FontStyle.Regular),
                new SolidBrush(label_string_colour), cn_point, label_string_format);

            // add pet
            image_graphics.DrawString($@"PET{System.Environment.NewLine}{current_project.water_balance.pet.ToString("0.##")}", new Font("Arial", image_font_size, System.Drawing.FontStyle.Regular),
                new SolidBrush(label_string_colour), pet_point, label_string_format);

            // add irrigation
            image_graphics.DrawString($@"Irrigation{System.Environment.NewLine}{current_project.water_balance.irr.ToString("0.##")}", new Font("Arial", image_font_size, System.Drawing.FontStyle.Regular),
                new SolidBrush(label_string_colour), irrigation_point, label_string_format);

            // add pcp
            image_graphics.DrawString($@"Precipitation{System.Environment.NewLine}{current_project.water_balance.precip.ToString("0.##")}", new Font("Arial", image_font_size, System.Drawing.FontStyle.Regular),
                new SolidBrush(label_string_colour), precipitation_point, label_string_format);

            // add et
            image_graphics.DrawString($@"ET{System.Environment.NewLine}{current_project.water_balance.et.ToString("0.##")}", new Font("Arial", image_font_size, System.Drawing.FontStyle.Regular),
                new SolidBrush(label_string_colour), evapotranspiration_point, label_string_format);

            // add sr
            image_graphics.DrawString($@"Surface Runnof{System.Environment.NewLine}{current_project.water_balance.surq_gen.ToString("0.##")}", new Font("Arial", image_font_size, System.Drawing.FontStyle.Regular),
                new SolidBrush(label_string_colour), surface_runoff_point, label_string_format);

            // add lat_q
            image_graphics.DrawString($@"Lateral Flow{System.Environment.NewLine}{current_project.water_balance.latq.ToString("0.##")}", new Font("Arial", image_font_size, System.Drawing.FontStyle.Regular),
                new SolidBrush(label_string_colour), lateral_flow_point, label_string_format);

            // add perc
            image_graphics.DrawString($@"Perc{System.Environment.NewLine}{current_project.water_balance.perc.ToString("0.##")}", new Font("Arial", image_font_size, System.Drawing.FontStyle.Regular),
                new SolidBrush(label_string_colour), percolation_point, label_string_format);

            // add revap
            image_graphics.DrawString($@"Revap{System.Environment.NewLine}{current_project.water_balance.aqu_revap.ToString("0.##")}", new Font("Arial", image_font_size, System.Drawing.FontStyle.Regular),
                new SolidBrush(label_string_colour), revap_point, label_string_format);

            // add return_flow
            image_graphics.DrawString($@"Retun Flow{System.Environment.NewLine}{current_project.water_balance.aqu_flo_cha.ToString("0.##")}", new Font("Arial", image_font_size, System.Drawing.FontStyle.Regular),
                new SolidBrush(label_string_colour), return_flow_point, label_string_format);

            // https://stackoverflow.com/questions/6588974/get-imagesource-from-memorystream-in-c-sharp-wpf

            ui_model_check_hydrology_image.Source = BitmapToImageSource(bitmap);

        }


        private void results_analyse_wb()
        {
            //wb
            string[] parts = read_from($@"{ current_project.txtinout}\basin_wb_aa.txt");
            string[] water_balance_parts = split_string_by_space(parts[3]);

            current_project.water_balance.precip = double.Parse(water_balance_parts[7]);
            current_project.water_balance.snofall = double.Parse(water_balance_parts[8]);
            current_project.water_balance.snomlt = double.Parse(water_balance_parts[9]);
            current_project.water_balance.surq_gen = double.Parse(water_balance_parts[10]);
            current_project.water_balance.latq = double.Parse(water_balance_parts[11]);
            current_project.water_balance.wateryld = double.Parse(water_balance_parts[12]);
            current_project.water_balance.perc = double.Parse(water_balance_parts[13]);
            current_project.water_balance.et = double.Parse(water_balance_parts[14]);
            current_project.water_balance.tloss = double.Parse(water_balance_parts[15]);
            current_project.water_balance.eplant = double.Parse(water_balance_parts[16]);
            current_project.water_balance.esoil = double.Parse(water_balance_parts[17]);
            current_project.water_balance.surq_cont = double.Parse(water_balance_parts[18]);
            current_project.water_balance.cn = double.Parse(water_balance_parts[19]);
            current_project.water_balance.sw = double.Parse(water_balance_parts[20]);
            current_project.water_balance.sw_300 = double.Parse(water_balance_parts[21]);
            current_project.water_balance.snopack = double.Parse(water_balance_parts[22]);
            current_project.water_balance.pet = double.Parse(water_balance_parts[23]);
            current_project.water_balance.qtile = double.Parse(water_balance_parts[24]);
            current_project.water_balance.irr = double.Parse(water_balance_parts[25]);
            current_project.water_balance.surq_runon = double.Parse(water_balance_parts[26]);
            current_project.water_balance.latq_runon = double.Parse(water_balance_parts[27]);
            current_project.water_balance.overbank = double.Parse(water_balance_parts[28]);
            current_project.water_balance.surq_cha = double.Parse(water_balance_parts[29]);
            current_project.water_balance.surq_res = double.Parse(water_balance_parts[30]);
            current_project.water_balance.surq_ls = double.Parse(water_balance_parts[31]);
            current_project.water_balance.latq_cha = double.Parse(water_balance_parts[32]);
            current_project.water_balance.latq_res = double.Parse(water_balance_parts[33]);
            current_project.water_balance.latq_ls = double.Parse(water_balance_parts[34]);

            //aqu
            parts = read_from($@"{current_project.txtinout}\basin_aqu_aa.txt");
            string[] aquifer_balance_parts = split_string_by_space(parts[3]);

            current_project.water_balance.aqu_flo = double.Parse(aquifer_balance_parts[7]);
            current_project.water_balance.aqu_dep_wt = double.Parse(aquifer_balance_parts[8]);
            current_project.water_balance.aqu_stor = double.Parse(aquifer_balance_parts[9]);
            current_project.water_balance.aqu_rchrg = double.Parse(aquifer_balance_parts[10]);
            current_project.water_balance.aqu_seep = double.Parse(aquifer_balance_parts[11]);
            current_project.water_balance.aqu_revap = double.Parse(aquifer_balance_parts[12]);
            current_project.water_balance.aqu_no3_st = double.Parse(aquifer_balance_parts[13]);
            current_project.water_balance.aqu_min = double.Parse(aquifer_balance_parts[14]);
            current_project.water_balance.aqu_orgn = double.Parse(aquifer_balance_parts[15]);
            current_project.water_balance.aqu_orgp = double.Parse(aquifer_balance_parts[16]);
            current_project.water_balance.aqu_rchrgn = double.Parse(aquifer_balance_parts[17]);
            current_project.water_balance.aqu_nloss = double.Parse(aquifer_balance_parts[18]);
            current_project.water_balance.aqu_no3gw = double.Parse(aquifer_balance_parts[19]);
            current_project.water_balance.aqu_seepno3 = double.Parse(aquifer_balance_parts[20]);
            current_project.water_balance.aqu_flo_cha = double.Parse(aquifer_balance_parts[21]);
            current_project.water_balance.aqu_flo_res = double.Parse(aquifer_balance_parts[22]);
            current_project.water_balance.aqu_flo_ls = double.Parse(aquifer_balance_parts[23]);

            update_project();
        }

        private void results_analyse_nb()
        {
            string[] parts = read_from($@"{current_project.txtinout}\basin_aqu_aa.txt");
            string[] nutrient_balance_parts = split_string_by_space(parts[3]);

            current_project.nutrient_balance.grzn = double.Parse(nutrient_balance_parts[7]);
            current_project.nutrient_balance.grzp = double.Parse(nutrient_balance_parts[8]);
            current_project.nutrient_balance.lab_min_p = double.Parse(nutrient_balance_parts[9]);
            current_project.nutrient_balance.act_sta_p = double.Parse(nutrient_balance_parts[10]);
            current_project.nutrient_balance.fertn = double.Parse(nutrient_balance_parts[11]);
            current_project.nutrient_balance.fertp = double.Parse(nutrient_balance_parts[12]);
            current_project.nutrient_balance.fixn = double.Parse(nutrient_balance_parts[13]);
            current_project.nutrient_balance.denit = double.Parse(nutrient_balance_parts[14]);
            current_project.nutrient_balance.act_nit_n = double.Parse(nutrient_balance_parts[15]);
            current_project.nutrient_balance.act_sta_n = double.Parse(nutrient_balance_parts[16]);
            current_project.nutrient_balance.org_lab_p = double.Parse(nutrient_balance_parts[17]);
            current_project.nutrient_balance.rsd_nitorg_n = double.Parse(nutrient_balance_parts[18]);
            current_project.nutrient_balance.rsd_laborg_p = double.Parse(nutrient_balance_parts[19]);
            current_project.nutrient_balance.no3atmo = double.Parse(nutrient_balance_parts[20]);
            current_project.nutrient_balance.nh4atmo = double.Parse(nutrient_balance_parts[21]);

            update_project();
        }


        private void results_analyse_plant_summary()
        {
            string[] parts = read_from($@"{current_project.txtinout}\basin_pw_aa.txt");
            string[] plant_results_parts = split_string_by_space(parts[3]);

            current_project.plant_summary.lai = double.Parse(plant_results_parts[7]);
            current_project.plant_summary.bioms = double.Parse(plant_results_parts[8]);
            current_project.plant_summary.yield = double.Parse(plant_results_parts[9]);
            current_project.plant_summary.residue = double.Parse(plant_results_parts[10]);
            current_project.plant_summary.sol_tmp = double.Parse(plant_results_parts[11]);
            current_project.plant_summary.strsw = double.Parse(plant_results_parts[12]);
            current_project.plant_summary.strsa = double.Parse(plant_results_parts[13]);
            current_project.plant_summary.strstmp = double.Parse(plant_results_parts[14]);
            current_project.plant_summary.strsn = double.Parse(plant_results_parts[15]);
            current_project.plant_summary.strsp = double.Parse(plant_results_parts[16]);
            current_project.plant_summary.nplt = double.Parse(plant_results_parts[17]);
            current_project.plant_summary.percn = double.Parse(plant_results_parts[18]);
            current_project.plant_summary.pplnt = double.Parse(plant_results_parts[19]);
            current_project.plant_summary.tmx = double.Parse(plant_results_parts[20]);
            current_project.plant_summary.tmn = double.Parse(plant_results_parts[21]);
            current_project.plant_summary.tmpav = double.Parse(plant_results_parts[22]);
            current_project.plant_summary.solarad = double.Parse(plant_results_parts[23]);
            current_project.plant_summary.wndspd = double.Parse(plant_results_parts[24]);
            current_project.plant_summary.rhum = double.Parse(plant_results_parts[25]);
            current_project.plant_summary.phubas0 = double.Parse(plant_results_parts[26]);

            update_project();
        }

        private void run_swat_plus_model_with_new_pars(object sender, RoutedEventArgs e)
        {
            set_new_parameters();

            File.WriteAllText($"{current_project.txtinout}\\print.prt", print_prt_file(current_project));
            File.WriteAllText($"{current_project.txtinout}\\time.sim", time_sim_file(current_project));

            Environment.CurrentDirectory = current_project.txtinout;

            run_swat();
            ui_run_model_running_year.Text = "";
            ui_run_model_total_years.Text = "";
            ui_run_model_of_label.Text = "";
        }

        private void set_new_parameters()
        {
            // modify file.cio to hook parameter files
            string chg_ = chg_line();
            string[] file_cio = read_from($@"{current_project.txtinout}\file.cio");

            string new_file_cio = "";

            for (int i = 0; i < file_cio.Count(); i++)
            {
                if (file_cio[i].StartsWith("chg"))
                {
                    file_cio[i] = chg_;
                }
                new_file_cio = new_file_cio + file_cio[i] + Environment.NewLine;
            }

            File.WriteAllText($@"{current_project.txtinout}\file.cio", new_file_cio);

            // create and write cal_parms
            string new_cal_parms = $@"cal_parms.cal: written by SWAT+ Toolbox" + Environment.NewLine + $@"{ all_parameters().Count()}" + Environment.NewLine + "name                       obj_typ       abs_min       abs_max                    units " + Environment.NewLine;
            for (int i = 0; i < all_parameters().Count(); i++)
            {
                new_cal_parms = new_cal_parms + $@"{String.Format("{0,-27}", all_parameters()[i].name)}{String.Format("{0,7}", all_parameters()[i].object_type)}{String.Format("{0,14}", $@"{String.Format("{0:0.00000}", all_parameters()[i].absolute_minimum)}")}{String.Format("{0,14}", $@"{String.Format("{0:0.00000}", all_parameters()[i].absolute_maximum)}")}{(all_parameters()[i].units == "" ? $@"{String.Format("{0,25}", "null")}" : $@"{String.Format("{0,25}", all_parameters()[i].units)}")}" + Environment.NewLine;
            }
            File.WriteAllText($@"{current_project.txtinout}\cal_parms.cal", new_cal_parms);

            // create calibration.cal

            string new_calibration_cal = $@"calibration.cal: written by SWAT+ Toolbox" + Environment.NewLine + $@"{current_project.current_parameters.Count()}" + Environment.NewLine + "cal_parm               chg_typ       chg_val     conds  soil_lyr1  soil_lyr2       yr1       yr2      day1      day2   obj_tot" + Environment.NewLine;
            for (int i = 0; i < current_project.current_parameters.Count(); i++)
            {
                new_calibration_cal = new_calibration_cal + $@"{String.Format("{0,-17}", current_project.current_parameters[i].name)}{String.Format("{0,13}", get_parameter_change_type_calibration(current_project.current_parameters[i].change_type))}{String.Format("{0,14}", $@"{String.Format("{0:0.00000}", current_project.current_parameters[i].value)}")}         0          0          0         0         0         0         0         0" + Environment.NewLine;
            }
            File.WriteAllText($@"{current_project.txtinout}\calibration.cal", new_calibration_cal);

        }

        private void calibration_manual_refresh_indices(object sender, RoutedEventArgs e)
        {
            (current_project, timeseries_data) = get_performance_indices(current_project);
            ui_calibration_manual_performance.Items.Refresh();
            update_project();
        }

        private void disengage_wb_source(object sender, RoutedEventArgs e)
        {
            ui_model_check_hydrology_image.Source = new BitmapImage(new Uri($@"{Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)}\\assets\\analysis_hydrology.dll"));
        }

        evaluation_time_series timeseries_data;

        private void show_chart_for_selected_observation(object sender, SelectionChangedEventArgs e)
        {
            plot_chart_manual();
        }

        private void show_chart_for_selected_observation_m(object sender, MouseButtonEventArgs e)
        {
            plot_chart_manual();
        }

        public void plot_chart_auto()
        {
            ui_calibration_automatic_chart.Visibility = Visibility.Visible;

            SeriesCollection_auto[0].Values.Clear();
            SeriesCollection_auto[1].Values.Clear();

            if (timeseries_data == null)
            {

                System.Windows.MessageBox.Show("Null Data!");
            }
            else
            {
                List<string> chart_labels = new List<string> { };

                foreach (var series_name in timeseries_data.observed_timeseries.Keys)
                {
                    if (series_name == current_project.current_observations[ui_calibration_automatic_available_charts.SelectedIndex].chart_name)
                    {
                        switch (current_project.current_observations[ui_calibration_automatic_available_charts.SelectedIndex].observed_variable)
                        {
                            case "Flow":
                                break;
                        }

                        foreach (var date_stamp in timeseries_data.observed_timeseries[series_name].Keys)
                        {
                            if (timeseries_data.observed_timeseries[series_name][date_stamp] >= 0)
                            {
                                if (timeseries_data.simulated_timeseries[series_name].ContainsKey(date_stamp))
                                {
                                    Console.WriteLine($@"{date_stamp.ToString()}");
                                    SeriesCollection_auto[0].Values.Add(timeseries_data.observed_timeseries[series_name][date_stamp]);
                                    SeriesCollection_auto[1].Values.Add(timeseries_data.simulated_timeseries[series_name][date_stamp]);
                                    chart_labels.Add($@"{date_stamp.Day}/{date_stamp.Month}/{date_stamp.Year}");
                                }
                            }
                        }
                    }
                }
                Labels_auto = chart_labels.ToArray();
            }
        }

        public void plot_chart_manual()
        {
            ui_calibration_manual_chart.Visibility = Visibility.Visible;
            
            SeriesCollection[0].Values.Clear();
            SeriesCollection[1].Values.Clear();

            if (timeseries_data == null)
            {

                System.Windows.MessageBox.Show("Null Data!");
            }
            else
            {

                List<string> chart_labels = new List<string> { };

                foreach (var series_name in timeseries_data.observed_timeseries.Keys)
                {
                    if (series_name == current_project.current_observations[ui_calibration_manual_available_charts.SelectedIndex].chart_name)
                    {
                        switch (current_project.current_observations[ui_calibration_manual_available_charts.SelectedIndex].observed_variable)
                        {
                            case "Flow":
                                break;
                        }

                        foreach (var date_stamp in timeseries_data.observed_timeseries[series_name].Keys)
                        {
                            if (timeseries_data.observed_timeseries[series_name][date_stamp] >= 0)
                            {
                                if (timeseries_data.simulated_timeseries[series_name].ContainsKey(date_stamp))
                                {
                                    Console.WriteLine($@"{date_stamp.ToString()}");
                                    SeriesCollection[0].Values.Add(timeseries_data.observed_timeseries[series_name][date_stamp]);
                                    SeriesCollection[1].Values.Add(timeseries_data.simulated_timeseries[series_name][date_stamp]);
                                    chart_labels.Add($@"{date_stamp.Day}/{date_stamp.Month}/{date_stamp.Year}");
                                }
                            }
                        }
                    }
                }
                Labels = chart_labels.ToArray();
            }
        }

        private void show_chart_for_selected_observation_d(object sender, MouseButtonEventArgs e)
        {
            plot_chart_manual();
        }

        bool is_sensitivity_run = false;
        bool is_calibration_run = false;
        int number_of_sens_samples = 0;
        int par_set_id = 0;
        bool is_swat_running = false;
        int selected_obs_index = 0;



        private void run_sensitivity(object sender, RoutedEventArgs e)
        {
            // get observation index for comparison
            selected_obs_index = ui_sensitivity_observation_selection.SelectedIndex;

            is_sensitivity_run = true;
            // run loop
            run_sensitivity_loop_async();
        }

        private void run_sample_parameter_calibration(object sender, RoutedEventArgs e)
        {
            // get observation index for comparison
            ui_calibration_automatic_available_charts.SelectedIndex = 0;
            selected_obs_index = ui_calibration_automatic_observation_selection.SelectedIndex;

            // run loop
            is_calibration_run = true;
            run_sensitivity_loop_async();
        }
             


        private void run_sensitivity_loop_async()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += run_loop;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += notify_swat_run_status;
            worker.RunWorkerAsync();
        }


        private void run_loop(object sender, DoWorkEventArgs e)
        {
            
            // save parameter data name,min,max
            string parameter_data = "par_name,min,max" + Environment.NewLine;
            foreach (var sel_parameter in current_project.current_parameters)
            {
                parameter_data = parameter_data + $@"{sel_parameter.name},{sel_parameter.minimum},{sel_parameter.maximum}" + Environment.NewLine;
            }
            File.WriteAllText($@"{current_project.txtinout}\par_data.stb", parameter_data);

            //generate sample
            string sensitivity_api_path = $@"{ Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)}\\assets\\sensitivity_api.exe";

            using (System.Diagnostics.Process p_gen_process = new System.Diagnostics.Process())
            {
                p_gen_process.StartInfo.FileName = sensitivity_api_path;
                p_gen_process.StartInfo.Arguments = $@"generate_sample {current_project.txtinout.Replace(" ", "__space__")} {current_project.sensivity_settings.seed}"; //argument
                p_gen_process.StartInfo.UseShellExecute = false;
                p_gen_process.StartInfo.RedirectStandardOutput = true;
                p_gen_process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p_gen_process.StartInfo.CreateNoWindow = true; //not diplay a windows
                p_gen_process.Start();
                string output = p_gen_process.StandardOutput.ReadToEnd(); //The output result
                p_gen_process.WaitForExit();
            }


            //read generated sample
            string[] parameter_sample = read_from($@"{current_project.txtinout}\par_sample.stb");

            Console.WriteLine(parameter_sample[0]);

            // loop for sensitivity analysis
            string report = "";
            par_set_id = 0;

            foreach (var par_line in parameter_sample)
            {
                par_set_id = par_set_id + 1;

                //set parameters
                string[] par_parts = par_line.Split(',');

                for (int i = 0; i < current_project.current_parameters.Count(); i++)
                {
                    current_project.current_parameters[i].value = double.Parse(par_parts[i]);
                }
                update_project();

                //run the model with new parameters
                set_new_parameters();
                number_of_sens_samples = parameter_sample.Count();

                
                Environment.CurrentDirectory = current_project.txtinout;
                                
                run_swat();
                
                //evaluate
                (current_project, timeseries_data) = get_performance_indices(current_project);
                update_project();

                if (current_project.best_sensitivity_nse < current_project.current_observations[selected_obs_index].nse)
                {
                    current_project.best_sensitivity_nse = current_project.current_observations[selected_obs_index].nse;

                    for (int cj = 0; cj < current_project.current_parameters.Count(); cj++)
                    {
                        current_project.current_parameters[cj].best_sens_parameter = current_project.current_parameters[cj].value;
                    }
                    update_project();

                }

                report = report + $@"{current_project.current_observations[selected_obs_index].nse}" + Environment.NewLine;

                this.Dispatcher.Invoke(() =>
                {
                    ui_calibration_manual_performance.Items.Refresh();
                    ui_calibration_mannual_parameters.Items.Refresh();

                    ui_sensitivity_parameters.Items.Refresh();

                    ui_calibration_automatic_performance.Items.Refresh();
                    ui_calibration_automatic_parameters.Items.Refresh();

                    if (is_calibration_run == true)
                    {
                        plot_chart_auto();
                        ui_calibration_automatic_best_obj_function_label.Text = $@"Best NSE: {current_project.best_sensitivity_nse}";
                    }



                    
                });
            }

            File.WriteAllText($@"{current_project.txtinout}\perf_report.stb", report);

            ((BackgroundWorker)sender).ReportProgress(par_set_id);

            //calculate sensitivity
            using (System.Diagnostics.Process p_gen_process = new System.Diagnostics.Process())
            {
                p_gen_process.StartInfo.FileName = sensitivity_api_path;
                p_gen_process.StartInfo.Arguments = $@"analyse_sensitivity {current_project.txtinout.Replace(" ", "__space__")}"; //argument
                p_gen_process.StartInfo.UseShellExecute = false;
                p_gen_process.StartInfo.RedirectStandardOutput = true;
                p_gen_process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                p_gen_process.StartInfo.CreateNoWindow = true; //not diplay a windows
                p_gen_process.Start();
                string output = p_gen_process.StandardOutput.ReadToEnd(); //The output result
                p_gen_process.WaitForExit();
            }

            string[] sensitivity_content = read_from($@"{current_project.txtinout}\s1_sensitivity.stb");

            for (int i = 0; i < current_project.current_parameters.Count(); i++)
            {
                current_project.current_parameters[i].sensitivity = double.Parse(sensitivity_content[i]);
            }
            update_project();

            this.Dispatcher.Invoke(() =>
            {
                ui_calibration_manual_performance.Items.Refresh();
                ui_calibration_mannual_parameters.Items.Refresh();
                ui_sensitivity_parameters.Items.Refresh();
                
            });
        

        }
        private void notify_swat_run_status(object sender, RunWorkerCompletedEventArgs e)
        {
            File.WriteAllText($@"{current_project.txtinout}\is_model_running.stb", "False");
            Console.WriteLine($@"Notifier is done with {par_set_id}");
            is_swat_running = false;
            is_sensitivity_run = false;
            is_calibration_run = false;


        }

        private void update_sensitivity_seed(object sender, RoutedEventArgs e)
        {
            current_project.sensivity_settings.seed = int.Parse(ui_sensitivity_seed_box.Text);
            ui_calibration_automatic_seed_box.Text = ui_sensitivity_seed_box.Text;
            update_project();
        }
        private void update_sensitivity_seed_auto(object sender, RoutedEventArgs e)
        {
            current_project.sensivity_settings.seed = int.Parse(ui_calibration_automatic_seed_box.Text);
            ui_sensitivity_seed_box.Text = ui_calibration_automatic_seed_box.Text;

            update_project();
        }

        private void navigate_calibration_automatic(object sender, RoutedEventArgs e)
        {
            ui_calibration_tab_pages.SelectedIndex = 1;
        }

        private void navigate_calibration_manual(object sender, RoutedEventArgs e)
        {
            ui_calibration_tab_pages.SelectedIndex = 0;
        }

        private void show_chart_for_selected_observation_m_auto(object sender, MouseButtonEventArgs e)
        {
            plot_chart_auto();
        }

        private void show_chart_for_selected_observation_d_auto(object sender, MouseButtonEventArgs e)
        {
            plot_chart_auto();
        }
        private void show_chart_for_selected_observation_auto(object sender, SelectionChangedEventArgs e)
        {
            plot_chart_auto();
        }

    }
}
