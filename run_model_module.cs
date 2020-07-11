using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using static SWAT__Toolbox.home_module;

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

        
        public static int get_pet_method_index(project current_project)
        {

            switch (current_project.run_options.pet_method)
            //    Channel, Landscape Unit, Hydrologic Response Unit, All Basin
            {
                case "Hargreaves":
                    return 2;
                case "Penman-Monteith":
                    return 1;
                case "Priestly and Taylor":
                    return 0;
                case "Read PET From File":
                    return 3;
                default:
                    return 1;
            }
        }

        public static string get_pet_method_string(int pet_index)
        {

            switch (pet_index)
            //    Channel, Landscape Unit, Hydrologic Response Unit, All Basin
            {
                case 0:
                    return "Priestly and Taylor";
                case 1:
                    return "Penman-Monteith";
                case 2:
                    return "Hargreaves";
                case 3:
                    return "Read PET From File";
                default:
                    return "Penman-Monteith";
            }
        }

        public static int get_routing_method_index(project current_project)
        {
            switch (current_project.run_options.routing_method)
            //    Channel, Landscape Unit, Hydrologic Response Unit, All Basin
            {
           
                case "Variable Storage":
                    return 0;
                case "Muskingum":
                    return 1;
                default:
                    return 1;
            }
        }

        public static string get_routing_method_string(int routing_index)
        {
            switch (routing_index)
            //    Channel, Landscape Unit, Hydrologic Response Unit, All Basin
            {
                case 1:
                    return "Muskingum";
                case 0:
                    return "Variable Storage";
                default:
                    return "Muskingum";
            }
        }


        public static string codes_bsn_file(project current_)
        {
            string codes_bsn = $@"codes.bsn: written by SWAT+ Toolbox
        pet_file           wq_file       pet     event     crack    rtu_wq   sed_det   rte_cha   deg_cha    wq_cha  rte_pest        cn    c_fact    carbon   baseflo      uhyd   sed_cha  tiledrain    wtable    soil_p  abstr_init   atmo_dep  stor_max  headwater  
            null              null         {get_pet_method_index(current_)}         0         0         1         0         {get_routing_method_index(current_)}         0         1         0         0         0         0         0         1         0         0         0         0         0          a         0         0  
";
            return codes_bsn;
        }


        public static string time_sim_file(project current_)
        {
            string time_sim = $@"time.sim: written by SWAT+ Toolbox v0.1
day_start  yrc_start   day_end   yrc_end      step  
        {current_.run_date_start.DayOfYear}      {current_.run_date_start.Year}         {current_.run_date_end.DayOfYear}      {current_.run_date_end.Year}         0  

";
            return time_sim;
        }
        public static string print_prt_file(project current_)
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

    }
}
