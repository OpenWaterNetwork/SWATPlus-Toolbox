using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SWAT__Toolbox.home_module;

namespace SWAT__Toolbox
{
    class sensitivity_analysis_module
    {
        public static string get_sensitivity_analysis_method(int sensitivity_index)
        {
            switch (sensitivity_index)
            {
                case 0:
                    return "Sobol";
                case 1:
                    return "Fourier Amplitude";
                case 2:
                    return "Random Balance Designs Fourier Amplitude";
                case 3:
                    return "Delta Moment-Independent Measure";
                default:
                    return "Sobol";
            }
        }

        public static string get_sensitivity_analysis_method_code(project current_project)
        {
            int sensitivity_index = get_sensitivity_analysis_index(current_project);
            switch (sensitivity_index)
            {
                case 0:
                    return "sobol";
                case 1:
                    return "FAST";
                case 2:
                    return "RBD_FAST";
                case 3:
                    return "DMIM";
                default:
                    return "sobol";
            }
        }


        public static int get_sensitivity_analysis_index(project current_project)
        {
            switch (current_project.sensivity_settings.sensitivity_method)
            {
                case "Sobol":
                    return 0;
                case "Fourier Amplitude":
                    return 1;
                case "Random Balance Designs Fourier Amplitude":
                    return 2;
                case "Delta Moment-Independent Measure":
                    return 3;
                default:
                    return 0;
            }
        }

    }
}
