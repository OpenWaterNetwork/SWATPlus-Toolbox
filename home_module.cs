using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static SWAT__Toolbox.model_evaluation_module;
using static SWAT__Toolbox.observations_classes;
using static SWAT__Toolbox.parameters_module;
using static SWAT__Toolbox.run_model_module;

namespace SWAT__Toolbox
{
    class home_module
    {
        //add mechanism to lad existing parameters upon new project
        public class project
        {
            public string project_name { get; set; }
            public string project_path { get; set; }
            public string txtinout { get; set; }
            public ObservableCollection<parameter> current_parameters { get; set; }
            public ObservableCollection<observation> current_observations { get; set; }
            public DateTime created { get; set; }
            public DateTime last_modified { get; set; }
            public DateTime run_date_start { get; set; }
            public DateTime run_date_end { get; set; }
            public int run_warmup { get; set; }
            public bool print_csv { get; set; }
            public print_options print_settings { get; set; }
            public water_balance_results water_balance { get; set; }
            public nutrient_balance_results nutrient_balance { get; set; }
            public plant_results plant_summary { get; set; }
        }
    }
}
