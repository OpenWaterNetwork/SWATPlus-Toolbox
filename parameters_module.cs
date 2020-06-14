using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT__Toolbox
{
    class parameters_module
    {
        public class parameter
        {
            public string name { get; set; }
            public double minimum { get; set; }
            public double maximum { get; set; }
            public double absolute_minimum { get; set; }
            public double absolute_maximum { get; set; }
            public double value { get; set; }
            public int change_type { get; set; }
            public string object_type { get; set; }
            public string units { get; set; }
            //public List<int> assigned { get; set; }
        }

        public static List<parameter> hru_parameters()
        {
            List<parameter> all_hru_parameters = new List<parameter>();

            all_hru_parameters.Add(new parameter() { name = "cn2", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 98, absolute_minimum = 25, object_type = "hru", units = "null" });
            all_hru_parameters.Add(new parameter() { name = "cn3_swf", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hru", units = "null" });
            all_hru_parameters.Add(new parameter() { name = "usle_p", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hru", units = "null" });
            all_hru_parameters.Add(new parameter() { name = "ovn", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 30, absolute_minimum = 0.01, object_type = "hru", units = "null" });
            all_hru_parameters.Add(new parameter() { name = "elev", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 5000, absolute_minimum = 0, object_type = "hru", units = "m" });
            all_hru_parameters.Add(new parameter() { name = "slope", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hru", units = "m/m" });
            all_hru_parameters.Add(new parameter() { name = "slope_len", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 150, absolute_minimum = 10, object_type = "hru", units = "m" });
            all_hru_parameters.Add(new parameter() { name = "lat_ttime", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 180, absolute_minimum = 0, object_type = "hru", units = "days" });
            all_hru_parameters.Add(new parameter() { name = "lat_sed", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 5000, absolute_minimum = 0, object_type = "hru", units = "g/L" });
            all_hru_parameters.Add(new parameter() { name = "lat_len", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 150, absolute_minimum = 0, object_type = "hru", units = "m" });
            all_hru_parameters.Add(new parameter() { name = "canmx", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "hru", units = "mm/H20" });
            all_hru_parameters.Add(new parameter() { name = "esco", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hru", units = "null" });
            all_hru_parameters.Add(new parameter() { name = "epco", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hru", units = "null" });
            all_hru_parameters.Add(new parameter() { name = "erorgn", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 5, absolute_minimum = 0, object_type = "hru", units = "null" });
            all_hru_parameters.Add(new parameter() { name = "erorgp", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 5, absolute_minimum = 0, object_type = "hru", units = "null" });
            all_hru_parameters.Add(new parameter() { name = "dis_stream", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100000, absolute_minimum = 0, object_type = "hru", units = "m" });
            all_hru_parameters.Add(new parameter() { name = "biomix", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hru", units = "null" });
            all_hru_parameters.Add(new parameter() { name = "perco", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hru", units = "fraction" });
            all_hru_parameters.Add(new parameter() { name = "lat_orgn", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 200, absolute_minimum = 0, object_type = "hru", units = "mg/l" });
            all_hru_parameters.Add(new parameter() { name = "lat_orgp", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 200, absolute_minimum = 0, object_type = "hru", units = "mg/l" });
            all_hru_parameters.Add(new parameter() { name = "field_len", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 2, absolute_minimum = 0.01, object_type = "hru", units = "km" });
            all_hru_parameters.Add(new parameter() { name = "field_wid", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 2, absolute_minimum = 0.01, object_type = "hru", units = "km" });
            all_hru_parameters.Add(new parameter() { name = "field_ang", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 360, absolute_minimum = 0, object_type = "hru", units = "degrees" });
            all_hru_parameters.Add(new parameter() { name = "snofall_tmp", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 5, absolute_minimum = -5, object_type = "hru", units = "degrees" });
            all_hru_parameters.Add(new parameter() { name = "snomelt_tmp", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 5, absolute_minimum = -5, object_type = "hru", units = "degrees" });
            all_hru_parameters.Add(new parameter() { name = "snomelt_max", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "hru", units = "mm/deg/c/day" });
            all_hru_parameters.Add(new parameter() { name = "snomelt_min", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "hru", units = "mm/deg/c/day" });
            all_hru_parameters.Add(new parameter() { name = "snomelt_lag", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hru", units = "none" });
            all_hru_parameters.Add(new parameter() { name = "tile_dep", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 2000, absolute_minimum = 0, object_type = "hru", units = "mm" });
            all_hru_parameters.Add(new parameter() { name = "tile_dtime", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 72, absolute_minimum = 0, object_type = "hru", units = "hrs" });
            all_hru_parameters.Add(new parameter() { name = "tile_lag", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "hru", units = "hrs" });
            all_hru_parameters.Add(new parameter() { name = "tile_rad", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 40, absolute_minimum = 3, object_type = "hru", units = "mm" });
            all_hru_parameters.Add(new parameter() { name = "tile_dist", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 30000, absolute_minimum = 7600, object_type = "hru", units = "mm" });
            all_hru_parameters.Add(new parameter() { name = "tile_drain", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 51, absolute_minimum = 10, object_type = "hru", units = "mm/day" });
            all_hru_parameters.Add(new parameter() { name = "tile_pump", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "hru", units = "mm/hr" });
            all_hru_parameters.Add(new parameter() { name = "tile_latk", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 4, absolute_minimum = 0.01, object_type = "hru", units = "null" });


            return all_hru_parameters;
        }

        public static ObservableCollection<parameter> sol_parameters()
        {
            ObservableCollection<parameter> all_sol_parameters = new ObservableCollection<parameter>();

            all_sol_parameters.Add(new parameter() { name = "anion_excl", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0.01, object_type = "sol", units = "null" });
            all_sol_parameters.Add(new parameter() { name = "crk", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "sol", units = "null" });
            all_sol_parameters.Add(new parameter() { name = "z", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 3500, absolute_minimum = 0, object_type = "sol", units = "mm" });
            all_sol_parameters.Add(new parameter() { name = "bd", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 2.5, absolute_minimum = 0.9, object_type = "sol", units = "mg/m**3" });
            all_sol_parameters.Add(new parameter() { name = "awc", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0.01, object_type = "sol", units = "mm_H20/mm" });
            all_sol_parameters.Add(new parameter() { name = "k", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 2000, absolute_minimum = 0.0001, object_type = "sol", units = "mm/hr" });
            all_sol_parameters.Add(new parameter() { name = "cbn", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0.05, object_type = "sol", units = "%" });
            all_sol_parameters.Add(new parameter() { name = "clay", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "sol", units = "null" });
            all_sol_parameters.Add(new parameter() { name = "silt", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "sol", units = "%" });
            all_sol_parameters.Add(new parameter() { name = "sand", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "sol", units = "null" });
            all_sol_parameters.Add(new parameter() { name = "rock", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "sol", units = "%" });
            all_sol_parameters.Add(new parameter() { name = "alb", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.25, absolute_minimum = 0, object_type = "sol", units = "null" });
            all_sol_parameters.Add(new parameter() { name = "usle_k", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.65, absolute_minimum = 0, object_type = "sol", units = "null" });
            all_sol_parameters.Add(new parameter() { name = "ec", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "sol", units = "dS/m" });
            all_sol_parameters.Add(new parameter() { name = "cal", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 65, absolute_minimum = 0, object_type = "sol", units = "%" });
            all_sol_parameters.Add(new parameter() { name = "ph", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 3, object_type = "sol", units = "null" });

            return all_sol_parameters;
        }

        public static ObservableCollection<parameter> bsn_parameters()
        {
            ObservableCollection<parameter> all_bsn_parameters = new ObservableCollection<parameter>();

            all_bsn_parameters.Add(new parameter() { name = "surlag", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 24, absolute_minimum = 0.05, object_type = "bsn", units = "days" });
            all_bsn_parameters.Add(new parameter() { name = "adj_pkr", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 2, absolute_minimum = 0.5, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "prf", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 2, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "spcon", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.01, absolute_minimum = 0.0001, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "spexp", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1.5, absolute_minimum = 1, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "evrch", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0.5, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "evlai", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "ffcb", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "cmn", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.003, absolute_minimum = 0.001, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "nperco", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "pperco", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 17.5, absolute_minimum = 10, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "phoskd", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 200, absolute_minimum = 100, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "psp", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.7, absolute_minimum = 0.01, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "rsdco", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.1, absolute_minimum = 0.02, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "percop", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "msk_co1", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "msk_co2", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "msk_x", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.3, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "trnsrch", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "bsn", units = "fraction" });
            all_bsn_parameters.Add(new parameter() { name = "cdn", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 3, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "tb_adj", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "sdnco", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "n_updis", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "p_updis", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "bsn", units = "null" });
            all_bsn_parameters.Add(new parameter() { name = "dorm_hr", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 24, absolute_minimum = 0, object_type = "bsn", units = "hours" });

            return all_bsn_parameters;
        }


        public static ObservableCollection<parameter> swq_parameters()
        {
            ObservableCollection<parameter> all_swq_parameters = new ObservableCollection<parameter>();

            all_swq_parameters.Add(new parameter() { name = "rs1", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1.82, absolute_minimum = 0.15, object_type = "swq", units = "m/hr" });
            all_swq_parameters.Add(new parameter() { name = "rs2", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.1, absolute_minimum = 0.001, object_type = "swq", units = "(mg_disP-P)/((m**2)*hr)" });
            all_swq_parameters.Add(new parameter() { name = "rs3", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.1, absolute_minimum = 0.001, object_type = "swq", units = "(mg_NH4-N)/((m**2)*hr)" });
            all_swq_parameters.Add(new parameter() { name = "rs4", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.1, absolute_minimum = 0.001, object_type = "swq", units = "1/day_or_1/hr" });
            all_swq_parameters.Add(new parameter() { name = "rs5", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.1, absolute_minimum = 0.001, object_type = "swq", units = "1/day_or_1/hr" });
            all_swq_parameters.Add(new parameter() { name = "rs6", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0.01, object_type = "swq", units = "1/day" });
            all_swq_parameters.Add(new parameter() { name = "rs7", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0.01, object_type = "swq", units = "(mg_ANC)" });
            all_swq_parameters.Add(new parameter() { name = "rk1", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 3.4, absolute_minimum = 0.02, object_type = "swq", units = "1/day_or_1/hr" });
            all_swq_parameters.Add(new parameter() { name = "rk2", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "swq", units = "1/day_or_1/hr" });
            all_swq_parameters.Add(new parameter() { name = "rk3", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.36, absolute_minimum = -0.36, object_type = "swq", units = "1/day_or_1/hr" });
            all_swq_parameters.Add(new parameter() { name = "rk4", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "swq", units = "((m**2)*day)" });
            all_swq_parameters.Add(new parameter() { name = "rk5", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 4, absolute_minimum = 0.05, object_type = "swq", units = "1/day" });
            all_swq_parameters.Add(new parameter() { name = "rk6", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "swq", units = "1/day" });
            all_swq_parameters.Add(new parameter() { name = "bc1", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0.1, object_type = "swq", units = "1/hr" });
            all_swq_parameters.Add(new parameter() { name = "bc2", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 2, absolute_minimum = 0.2, object_type = "swq", units = "1/hr" });
            all_swq_parameters.Add(new parameter() { name = "bc3", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.4, absolute_minimum = 0.2, object_type = "swq", units = "1/hr" });
            all_swq_parameters.Add(new parameter() { name = "bc4", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.7, absolute_minimum = 0.01, object_type = "swq", units = "1/hr" });
            all_swq_parameters.Add(new parameter() { name = "rch_dox", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 50, absolute_minimum = 0, object_type = "swq", units = "mg/L" });
            all_swq_parameters.Add(new parameter() { name = "rch_cbod", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1000, absolute_minimum = 0, object_type = "swq", units = "mg/L" });
            all_swq_parameters.Add(new parameter() { name = "algae", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 200, absolute_minimum = 0, object_type = "swq", units = "mg/L" });
            all_swq_parameters.Add(new parameter() { name = "organicn", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "swq", units = "mg/L" });
            all_swq_parameters.Add(new parameter() { name = "ammonian", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 50, absolute_minimum = 0, object_type = "swq", units = "mg/L" });
            all_swq_parameters.Add(new parameter() { name = "nitriten", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "swq", units = "mg/L" });
            all_swq_parameters.Add(new parameter() { name = "organicp", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 25, absolute_minimum = 0, object_type = "swq", units = "mg/L" });
            all_swq_parameters.Add(new parameter() { name = "disolvp", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 25, absolute_minimum = 0, object_type = "swq", units = "mg/L" });

            return all_swq_parameters;
        }
        public static ObservableCollection<parameter> rte_parameters()
        {
            ObservableCollection<parameter> all_rte_parameters = new ObservableCollection<parameter>();

            all_rte_parameters.Add(new parameter() { name = "w", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1000, absolute_minimum = 0, object_type = "rte", units = "m" });
            all_rte_parameters.Add(new parameter() { name = "d", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 30, absolute_minimum = 0, object_type = "rte", units = "m" });
            all_rte_parameters.Add(new parameter() { name = "s", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = -0.001, object_type = "rte", units = "m/m" });
            all_rte_parameters.Add(new parameter() { name = "l", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 500, absolute_minimum = -0.05, object_type = "rte", units = "km" });
            all_rte_parameters.Add(new parameter() { name = "n", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.3, absolute_minimum = -0.01, object_type = "rte", units = "null" });
            all_rte_parameters.Add(new parameter() { name = "k_ch", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 500, absolute_minimum = -0.01, object_type = "rte", units = "mm/hr" });
            all_rte_parameters.Add(new parameter() { name = "cov1", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.6, absolute_minimum = -0.05, object_type = "rte", units = "null" });
            all_rte_parameters.Add(new parameter() { name = "cov2", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = -0.001, object_type = "rte", units = "null" });
            all_rte_parameters.Add(new parameter() { name = "wdr", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10000, absolute_minimum = 0, object_type = "rte", units = "m/m" });
            all_rte_parameters.Add(new parameter() { name = "alpha_bnk", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "rte", units = "days" });
            all_rte_parameters.Add(new parameter() { name = "onco", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "rte", units = "ppm" });
            all_rte_parameters.Add(new parameter() { name = "opco", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 0, object_type = "rte", units = "ppm" });
            all_rte_parameters.Add(new parameter() { name = "side", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 5, absolute_minimum = 0, object_type = "rte", units = "null" });
            all_rte_parameters.Add(new parameter() { name = "bnk_bd", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1.9, absolute_minimum = 1.1, object_type = "rte", units = "(g/cm**3)" });
            all_rte_parameters.Add(new parameter() { name = "bed_bd", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1.9, absolute_minimum = 1.1, object_type = "rte", units = "(g/cm**3)" });
            all_rte_parameters.Add(new parameter() { name = "bnk_kd", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 3.75, absolute_minimum = 0.001, object_type = "rte", units = "cm3/N-s" });
            all_rte_parameters.Add(new parameter() { name = "bed_kd", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 3.75, absolute_minimum = 0.001, object_type = "rte", units = "cm3/N-s" });
            all_rte_parameters.Add(new parameter() { name = "bnk_d50", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10000, absolute_minimum = 1, object_type = "rte", units = "um" });
            all_rte_parameters.Add(new parameter() { name = "bed_d50", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10000, absolute_minimum = 1, object_type = "rte", units = "um" });
            all_rte_parameters.Add(new parameter() { name = "tc_bnk", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 400, absolute_minimum = 0, object_type = "rte", units = "N/m**2" });
            all_rte_parameters.Add(new parameter() { name = "tc_bed", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 400, absolute_minimum = 0, object_type = "rte", units = "N/m**2" });
            all_rte_parameters.Add(new parameter() { name = "erod(1)", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "rte", units = "null" });

            return all_rte_parameters;
        }
        public static ObservableCollection<parameter> res_parameters()
        {
            ObservableCollection<parameter> all_res_parameters = new ObservableCollection<parameter>();

            all_res_parameters.Add(new parameter() { name = "vol", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 10, object_type = "res", units = "m**3" });
            all_res_parameters.Add(new parameter() { name = "sed", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 5000, absolute_minimum = 1, object_type = "res", units = "kg/L" });
            all_res_parameters.Add(new parameter() { name = "orgp", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "res", units = "kg" });
            all_res_parameters.Add(new parameter() { name = "orgn", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "res", units = "kg" });
            all_res_parameters.Add(new parameter() { name = "solp", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "res", units = "kg" });
            all_res_parameters.Add(new parameter() { name = "no3", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "res", units = "kg" });
            all_res_parameters.Add(new parameter() { name = "nh3", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "res", units = "kg" });
            all_res_parameters.Add(new parameter() { name = "no2", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "res", units = "kg" });
            all_res_parameters.Add(new parameter() { name = "esa", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 3000, absolute_minimum = 0, object_type = "res", units = "ha" });
            all_res_parameters.Add(new parameter() { name = "evol", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 3000, absolute_minimum = 15, object_type = "res", units = "10^4_m^3_H20" });
            all_res_parameters.Add(new parameter() { name = "psa", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1000, absolute_minimum = 1, object_type = "res", units = "ha" });
            all_res_parameters.Add(new parameter() { name = "pvol", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100, absolute_minimum = 10, object_type = "res", units = "10^4_m^3_H20" });
            all_res_parameters.Add(new parameter() { name = "k_res", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "res", units = "mm/hr" });
            all_res_parameters.Add(new parameter() { name = "evrsv", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "res", units = "null" });
            all_res_parameters.Add(new parameter() { name = "nsed", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 5000, absolute_minimum = 1, object_type = "res", units = "mg/L" });
            all_res_parameters.Add(new parameter() { name = "psetlr1", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 20, absolute_minimum = 2, object_type = "res", units = "m/day" });
            all_res_parameters.Add(new parameter() { name = "psetlr2", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 20, absolute_minimum = 2, object_type = "res", units = "m/day" });
            all_res_parameters.Add(new parameter() { name = "nsetlr1", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 15, absolute_minimum = 1, object_type = "res", units = "m/day" });
            all_res_parameters.Add(new parameter() { name = "nsetlr2", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 15, absolute_minimum = 1, object_type = "res", units = "m/day" });
            all_res_parameters.Add(new parameter() { name = "chlar", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 2, absolute_minimum = 0.5, object_type = "res", units = "none" });
            all_res_parameters.Add(new parameter() { name = "seccir", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 2, absolute_minimum = 0.5, object_type = "res", units = "none" });

            return all_res_parameters;
        }
        public static ObservableCollection<parameter> aqu_parameters()
        {
            ObservableCollection<parameter> all_aqu_parameters = new ObservableCollection<parameter>();

            all_aqu_parameters.Add(new parameter() { name = "alpha", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "aqu", units = "days" });
            all_aqu_parameters.Add(new parameter() { name = "dep_bot", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "aqu", units = "m" });
            all_aqu_parameters.Add(new parameter() { name = "bf_max", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 2, absolute_minimum = 0, object_type = "aqu", units = "mm" });
            all_aqu_parameters.Add(new parameter() { name = "flo_min", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "aqu", units = "m" });
            all_aqu_parameters.Add(new parameter() { name = "revap_co", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.2, absolute_minimum = 0.02, object_type = "aqu", units = "null" });
            all_aqu_parameters.Add(new parameter() { name = "revap_min", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "aqu", units = "m" });

            return all_aqu_parameters;
        }
        public static ObservableCollection<parameter> hlt_parameters()
        {
            ObservableCollection<parameter> all_hlt_parameters = new ObservableCollection<parameter>();
            all_hlt_parameters.Add(new parameter() { name = "cn2_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 98, absolute_minimum = 25, object_type = "hlt", units = "null" });
            all_hlt_parameters.Add(new parameter() { name = "awc_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0.01, object_type = "hlt", units = "mm_H20/mm" });
            all_hlt_parameters.Add(new parameter() { name = "etco_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hlt", units = "null" });
            all_hlt_parameters.Add(new parameter() { name = "tc_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 86400, absolute_minimum = 1, object_type = "hlt", units = "min" });
            all_hlt_parameters.Add(new parameter() { name = "soildep_lt", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10000, absolute_minimum = 10, object_type = "hlt", units = "mm" });
            all_hlt_parameters.Add(new parameter() { name = "slope_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hlt", units = "m/m" });
            all_hlt_parameters.Add(new parameter() { name = "slopelen_l", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 150, absolute_minimum = 10, object_type = "hlt", units = "m" });
            all_hlt_parameters.Add(new parameter() { name = "sy_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.8, absolute_minimum = 0.001, object_type = "hlt", units = "mm" });
            all_hlt_parameters.Add(new parameter() { name = "abf_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hlt", units = "days" });
            all_hlt_parameters.Add(new parameter() { name = "revapc_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.2, absolute_minimum = 0.02, object_type = "hlt", units = "null" });
            all_hlt_parameters.Add(new parameter() { name = "percc_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.9, absolute_minimum = 0, object_type = "hlt", units = "fraction" });
            all_hlt_parameters.Add(new parameter() { name = "sw_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hlt", units = "fraction" });
            all_hlt_parameters.Add(new parameter() { name = "gw_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10000, absolute_minimum = 0, object_type = "hlt", units = "mm" });
            all_hlt_parameters.Add(new parameter() { name = "gwflow_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "hlt", units = "mm" });
            all_hlt_parameters.Add(new parameter() { name = "gwdeep_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "hlt", units = "mm" });
            all_hlt_parameters.Add(new parameter() { name = "snow_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1000, absolute_minimum = 0, object_type = "hlt", units = "mm" });
            all_hlt_parameters.Add(new parameter() { name = "uslek_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.65, absolute_minimum = 0, object_type = "hlt", units = "null" });
            all_hlt_parameters.Add(new parameter() { name = "uslec_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hlt", units = "null" });
            all_hlt_parameters.Add(new parameter() { name = "uslep_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hlt", units = "null" });
            all_hlt_parameters.Add(new parameter() { name = "uslels_lte", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "hlt", units = "null" });

            return all_hlt_parameters;
        }
        public static ObservableCollection<parameter> pst_parameters()
        {
            ObservableCollection<parameter> all_pst_parameters = new ObservableCollection<parameter>();

            all_pst_parameters.Add(new parameter() { name = "pst_koc", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "pst", units = "m**3/g" });
            all_pst_parameters.Add(new parameter() { name = "pst_washoff", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "pst", units = "null" });
            all_pst_parameters.Add(new parameter() { name = "pst_foliar_hlife", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10000, absolute_minimum = 0, object_type = "pst", units = "days" });
            all_pst_parameters.Add(new parameter() { name = "pst_soil_hlife", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 100000, absolute_minimum = 0, object_type = "pst", units = "days" });
            all_pst_parameters.Add(new parameter() { name = "pst_solub", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 11000000, absolute_minimum = 0, object_type = "pst", units = "mg/L" });
            all_pst_parameters.Add(new parameter() { name = "pst_aq_hlife", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10000, absolute_minimum = 0, object_type = "pst", units = "1/day" });
            all_pst_parameters.Add(new parameter() { name = "pst_aq_volat", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "pst", units = "m/day" });
            all_pst_parameters.Add(new parameter() { name = "pst_aq_settle", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10, absolute_minimum = 0, object_type = "pst", units = "m/day" });
            all_pst_parameters.Add(new parameter() { name = "pst_aq_resus", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "pst", units = "m/day" });
            all_pst_parameters.Add(new parameter() { name = "pst_ben_hlife", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 10000, absolute_minimum = 0, object_type = "pst", units = "1/day" });
            all_pst_parameters.Add(new parameter() { name = "pst_ben_bury", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 0.1, absolute_minimum = 0, object_type = "pst", units = "m/day" });
            all_pst_parameters.Add(new parameter() { name = "pst_ben_act_dep", minimum = 0, maximum = 0, change_type = 1, value = 0, absolute_maximum = 1, absolute_minimum = 0, object_type = "pst", units = "m" });

            return all_pst_parameters;
        }

        public static ObservableCollection<parameter> all_parameters()
        {
            ObservableCollection<parameter> all_the_parameters = (ObservableCollection<parameter>)hru_parameters().Concat(sol_parameters())
                                              .Concat((ObservableCollection<parameter>)bsn_parameters())
                                              .Concat((ObservableCollection<parameter>)swq_parameters())
                                              .Concat((ObservableCollection<parameter>)rte_parameters())
                                              .Concat((ObservableCollection<parameter>)res_parameters())
                                              .Concat((ObservableCollection<parameter>)hlt_parameters())
                                              .Concat((ObservableCollection<parameter>)aqu_parameters())
                                              .Concat((ObservableCollection<parameter>)pst_parameters());

            return all_the_parameters;
        }
    }
}
