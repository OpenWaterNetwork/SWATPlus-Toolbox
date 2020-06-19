using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT__Toolbox
{
    class run_model_module
    {
        public class print_timesteps
        {
            public bool daily { get; set; }
            public bool monthly { get; set; }
            public bool yearly { get; set; }
            public bool annual_average { get; set; }

        }
        public class print_options
        {
            public print_timesteps channel { get; set; }
            public print_timesteps reservoir { get; set; }
            public print_timesteps aquifer { get; set; }
            public print_timesteps nut_basin { get; set; }
            public print_timesteps nut_lsu { get; set; }
            public print_timesteps nut_hru { get; set; }
            public print_timesteps wb_basin { get; set; }
            public print_timesteps wb_lsu { get; set; }
            public print_timesteps wb_hru { get; set; }
            public print_timesteps plt_basin { get; set; }
            public print_timesteps plt_lsu { get; set; }
            public print_timesteps plt_hru { get; set; }
        }
        public static print_options default_print_settings()
        {
            print_options defaults = new print_options();

            defaults.channel    = new print_timesteps { daily =  true, annual_average = false, monthly = false, yearly = false };
            defaults.reservoir  = new print_timesteps { daily = false, annual_average = false, monthly = false, yearly = false };
            defaults.aquifer    = new print_timesteps { daily = false, annual_average =  true, monthly = false, yearly = false };
            defaults.nut_basin  = new print_timesteps { daily = false, annual_average =  true, monthly = false, yearly =  true };
            defaults.nut_lsu    = new print_timesteps { daily = false, annual_average = false, monthly = false, yearly = false };
            defaults.nut_hru    = new print_timesteps { daily = false, annual_average = false, monthly = false, yearly = false };
            defaults.wb_basin   = new print_timesteps { daily = false, annual_average =  true, monthly = false, yearly =  true };
            defaults.wb_lsu     = new print_timesteps { daily = false, annual_average = false, monthly = false, yearly = false };
            defaults.wb_hru     = new print_timesteps { daily = false, annual_average =  true, monthly = false, yearly = false };
            defaults.plt_basin  = new print_timesteps { daily = false, annual_average =  true, monthly = false, yearly =  true };
            defaults.plt_lsu    = new print_timesteps { daily = false, annual_average = false, monthly = false, yearly = false };
            defaults.plt_hru    = new print_timesteps { daily = false, annual_average =  true, monthly = false, yearly = false };

            return defaults;
        }

    }
}
