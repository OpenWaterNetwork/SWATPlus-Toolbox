using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAT__Toolbox
{
    class observations_classes
    {
        public class observation
        {
            public int id { get; set; }
            public string object_type { get; set; }
            public int number { get; set; }
            public string file { get; set; }
            public string timestep { get; set; }
            public string observed_variable { get; set; }
            public double nse { get; set; }
            public double r2 { get; set; }
            public double pbias { get; set; }


            //public List<int> assigned { get; set; }
        }
    }

    class observations_functions
    {
        public static string get_observation_object_type(int obj_type_id)
        {
            string observation_object_type;

            switch (obj_type_id)
            //    Channel, Landscape Unit, Hydrologic Response Unit, All Basin
            {
                case 1:
                    observation_object_type = "Channel";
                    break;
                case 2:
                    observation_object_type = "Landscape Unit";
                    break;
                case 3:
                    observation_object_type = "Hydrologic Response Unit";
                    break;
                default:
                    observation_object_type = "All Basin";
                    break;
            }
            return observation_object_type;
        }

        public static string get_observation_variable(int obs_variable_id)
        {
            string observation_variable;

            switch (obs_variable_id)
            //  Flow, ET, Sediments, Organic Nitrogen, Nitrate Nitrogen, Amonium Nitrogen, Nitrite Nitrogen, Organic Phosphorus, Mineral Phosphorus
            {
                case 1:
                    observation_variable = "Flow";
                    break;
                case 2:
                    observation_variable = "ET";
                    break;
                case 3:
                    observation_variable = "Sediments";
                    break;
                case 4:
                    observation_variable = "Organic Nitrogen";
                    break;
                case 5:
                    observation_variable = "Nitrate Nitrogen";
                    break;
                case 6:
                    observation_variable = "Amonium Nitrogen";
                    break;
                case 7:
                    observation_variable = "Nitrite Nitrogen";
                    break;
                case 8:
                    observation_variable = "Organic Phosphorus";
                    break;
                case 9:
                    observation_variable = "Mineral Phosphorus";
                    break;
                default:
                    observation_variable = "Mineral Phosphorus";
                    break;
            }
            return observation_variable;
        }
    }
}
