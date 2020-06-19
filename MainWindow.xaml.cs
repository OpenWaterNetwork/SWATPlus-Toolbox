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

// Added
using Microsoft.Win32;

// Custom
using static SWAT__Toolbox.observations_classes;
using static SWAT__Toolbox.observations_functions;
using static SWAT__Toolbox.parameters_module;
using static SWAT__Toolbox.toolbox_functions;
using static SWAT__Toolbox.home_module;
using static SWAT__Toolbox.run_model_module;


using Path = System.IO.Path;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization.NamingConventions;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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

        public MainWindow()
        {
            InitializeComponent();

            WindowChrome.SetWindowChrome(this, new WindowChrome());

            selected_parameters = new ObservableCollection<parameter>();
            selected_observations = new ObservableCollection<observation>();

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


            ui_observations_object_type.Text = null;
            ui_observations_observed_variable.Text = null;
            ui_observations_object_number.Text = null;
            ui_observations_object_number.Text = null;
            ui_parameters_file_selection_path.Text = "Click to select an observation file";

            current_project.current_observations = selected_observations;
            current_project.last_modified = DateTime.Now;

            save_project(current_project);
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
            current_project.last_modified = DateTime.Now;

            save_project(current_project);
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

            // saving the project
            save_project(current_project);

            ui_selected_parameters.ItemsSource = selected_parameters;
            ui_selected_observations.ItemsSource = selected_observations;

            //binding checkboxes for printing
            set_binding_context_for_print();
        }

        private void home_open_project(object sender, RoutedEventArgs e)
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

            //update ui
            //home

            //run_model
            ui_run_model_start_date.SelectedDate = current_project.run_date_start;
            ui_run_model_end_date.SelectedDate = current_project.run_date_end;

            ui_run_model_warmup_period.Text = current_project.run_warmup.ToString();

            //binding checkboxes for printing
            set_binding_context_for_print();

            //
        }


        // run model functions
        private void update_project_start_date(object sender, RoutedEventArgs e)
        {
            current_project.run_date_start = (DateTime)ui_run_model_start_date.SelectedDate;
            current_project.last_modified = DateTime.Now;
            save_project(current_project);
        }
        private void update_project_end_date(object sender, RoutedEventArgs e)
        {
            current_project.run_date_end = (DateTime)ui_run_model_end_date.SelectedDate;
            current_project.last_modified = DateTime.Now;
            save_project(current_project);
        }

        private void update_project_warmup(object sender, RoutedEventArgs e)
        {
            current_project.run_warmup = int.Parse(ui_run_model_warmup_period.Text);
            current_project.last_modified = DateTime.Now;
            save_project(current_project);
        }

        private void update_project_print_csv(object sender, RoutedEventArgs e)
        {
            if ((bool)ui_run_model_print_csv.IsChecked)
            {
                current_project.print_csv = true;
                current_project.last_modified = DateTime.Now;
                save_project(current_project);
            }
            else
            {
                current_project.print_csv = false;
                current_project.last_modified = DateTime.Now;
                save_project(current_project);
            }
        }

        private void update_print_settings(object sender, RoutedEventArgs e)
        {
            current_project.last_modified = DateTime.Now;
            save_project(current_project);
        }

        private void set_binding_context_for_print()
        {
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

            process.StartInfo.FileName = "C:\\SWAT\\JAMES+\\model_files\\TxtInOut\\rev59_3_64rel.exe";
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
            //process.WaitForExit();
        }

        public void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {

            //* Do your stuff with the output (write to console/log/StringBuilder)
            int current_year = 0;
            int current_month= 0;
            int current_day= 0;

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
                    ui_run_model_progress.Value = (double)(Convert.ToDouble(year_count) / Convert.ToDouble(total_years)) * 100;

                    double total_days = Convert.ToDouble(new DateTime(current_date.Year, 12, 31).Subtract(new DateTime(current_date.Year, 1, 1)).TotalDays);

                    ui_run_model_annual_progress.Value = (double)(Convert.ToDouble(current_date.DayOfYear) / total_days) * 100;
                    ui_run_model_running_year.Text = current_year.ToString();
                    ui_run_model_total_years.Text = current_project.run_date_end.Year.ToString();
                    ui_run_model_of_label.Text = "of";
                });

            }
        }


        private void run_swat_plus_model(object sender, RoutedEventArgs e)
        {
            Environment.CurrentDirectory = current_project.txtinout;

            File.WriteAllText($"{current_project.txtinout}\\print.prt", print_prt_file(current_project));
            File.WriteAllText($"{current_project.txtinout}\\time.sim", time_sim_file(current_project));

            ui_run_model_progress.DataContext = this;

            run_swat();
            ui_run_model_running_year.Text = "";
            ui_run_model_total_years.Text = "";
            ui_run_model_of_label.Text = "";
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
basin_aqu             n           n             n              n
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


    }
}
