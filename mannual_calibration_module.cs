using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Navigation;

using static SWAT__Toolbox.home_module;
using static SWAT__Toolbox.observations_classes;
using static SWAT__Toolbox.toolbox_functions;


namespace SWAT__Toolbox
{
    class mannual_calibration_module
    {
        public static string chg_line()
        {
            string chg_content = "chg               cal_parms.cal     calibration.cal   null              null              null              null              null             null               null              ";
            return chg_content;
        }
        public static string cal_parms_file()
        {
            string cal_parms_content = "";
            return cal_parms_content;
        }

        public class evaluation_time_series
        {
            public Dictionary<string, Dictionary<DateTime, double>> observed_timeseries { get; set; }
            public Dictionary<string, Dictionary<DateTime, double>> simulated_timeseries { get; set; }
        }


        public static (project, evaluation_time_series) get_performance_indices(project project_object)
        {

            project eval_project = project_object;

            //initialise ts object
            evaluation_time_series time_series_data = new evaluation_time_series();

            time_series_data = new evaluation_time_series { };
            time_series_data.simulated_timeseries = new Dictionary<string, Dictionary<DateTime, double>> { };
            time_series_data.observed_timeseries = new Dictionary<string, Dictionary<DateTime, double>> { };


            for (int i = 0; i < eval_project.current_observations.Count(); i++)
            {
                time_series_data.observed_timeseries.Add(eval_project.current_observations[i].chart_name, new Dictionary<DateTime, double> { });
                time_series_data.simulated_timeseries.Add(eval_project.current_observations[i].chart_name, new Dictionary<DateTime, double> { });

                // read observation file
                string[] observations_content = read_from(eval_project.current_observations[i].file);

                //create dictionary to keep observations
                var obs_dictionary = new Dictionary<DateTime, double>();
                var sim_dictionary = new Dictionary<DateTime, double>();
                double[] valid_observations = Array.Empty<double>();
                double NSE_numerator = 0;
                double NSE_denominator = 0;
                double PBIAS_denominator = 0;

                //populate observations dictionary
                for (int j = 1; j < observations_content.Count(); j++)
                {
                    DateTime date_value;
                    string[] parts = observations_content[j].Split(',');

                    if (DateTime.TryParse(parts[0], out date_value))
                    {
                        //date is valid format
                        date_value = date_value.Date;
                        switch (eval_project.current_observations[i].timestep)
                        {
                            case "Monthly":
                                date_value = new DateTime(date_value.Year, date_value.Month, 1);
                                break;

                            case "Yearly":
                                date_value = new DateTime(date_value.Year, 1, 1);
                                break;
                        }

                        //todo check if there is already a date and raise awareness if more than 1 day in the same month or
                        //more than than 1 month in a year is supplied for monthly and yearly timesteps respectively

                        obs_dictionary.Add(date_value, double.Parse(parts[1]));

                        if (obs_dictionary[date_value] >= 0)
                        {
                            Array.Resize(ref valid_observations, valid_observations.Length + 1);
                            valid_observations[valid_observations.Length - 1] = obs_dictionary[date_value];
                        }

                    }
                    else
                    {
                        //date is bad format
                    }
                }

                //read model results
                switch (eval_project.current_observations[i].observed_variable)
                //Flow //ET //Sediments //Organic Nitrogen //Nitrate Nitrogen //Amonium Nitrogen //Nitrite Nitrogen //Organic Phosphorus //Mineral Phosphorus
                {
                    case "Flow":
                        //populate simulations dictionary
                        switch (eval_project.current_observations[i].object_type)
                        {
                            case "Channel":
                                switch (eval_project.current_observations[i].timestep)
                                {
                                    case "Daily":

                                        string[] simulations_content_day = read_from($@"{eval_project.txtinout}\channel_sd_day.txt");

                                        for (int k = 3; k < simulations_content_day.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_day[k]);
                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), int.Parse(parts[1]), int.Parse(parts[2]));
                                                sim_dictionary.Add(date_value, double.Parse(parts[47]));
                                            }
                                        }
                                        break;

                                    case "Monthly":

                                        string[] simulations_content_month = read_from($@"{eval_project.txtinout}\channel_sd_mon.txt");

                                        //populate simulations dictionary
                                        for (int k = 3; k < simulations_content_month.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_month[k]);

                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), int.Parse(parts[1]), 1);
                                                sim_dictionary.Add(date_value, double.Parse(parts[47]));
                                            }

                                        }
                                        break;

                                    case "Yearly":

                                        string[] simulations_content_year = read_from($@"{eval_project.txtinout}\channel_sd_yr.txt");

                                        //populate simulations dictionary
                                        for (int k = 3; k < simulations_content_year.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_year[k]);

                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), 1, 1);
                                                sim_dictionary.Add(date_value, double.Parse(parts[47]));
                                            }
                                        }

                                        break;

                                    default:
                                        break;
                                }

                                break;
                        }
                        break;


                    case "ET":
                        //populate simulations dictionary
                        switch (eval_project.current_observations[i].object_type)
                        {
                            case "Hydrologic Response Unit":
                                switch (eval_project.current_observations[i].timestep)
                                {
                                    case "Daily":

                                        string[] simulations_content_day = read_from($@"{eval_project.txtinout}\hru_wb_day.txt");

                                        for (int k = 3; k < simulations_content_day.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_day[k]);
                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), int.Parse(parts[1]), int.Parse(parts[2]));
                                                sim_dictionary.Add(date_value, double.Parse(parts[14]));
                                            }
                                        }
                                        break;

                                    case "Monthly":

                                        string[] simulations_content_month = read_from($@"{eval_project.txtinout}\hru_wb_mon.txt");

                                        //populate simulations dictionary
                                        for (int k = 3; k < simulations_content_month.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_month[k]);

                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), int.Parse(parts[1]), 1);
                                                sim_dictionary.Add(date_value, double.Parse(parts[14]));
                                            }

                                        }
                                        break;

                                    case "Yearly":

                                        string[] simulations_content_year = read_from($@"{eval_project.txtinout}\hru_wb_yr.txt");

                                        //populate simulations dictionary
                                        for (int k = 3; k < simulations_content_year.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_year[k]);

                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), 1, 1);
                                                sim_dictionary.Add(date_value, double.Parse(parts[14]));
                                            }
                                        }

                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case "Landscape Unit":
                                switch (eval_project.current_observations[i].timestep)
                                {
                                    case "Daily":

                                        string[] simulations_content_day = read_from($@"{eval_project.txtinout}\lsunit_wb_day.txt");

                                        for (int k = 3; k < simulations_content_day.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_day[k]);
                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), int.Parse(parts[1]), int.Parse(parts[2]));
                                                sim_dictionary.Add(date_value, double.Parse(parts[14]));
                                            }
                                        }
                                        break;

                                    case "Monthly":

                                        string[] simulations_content_month = read_from($@"{eval_project.txtinout}\lsunit_wb_mon.txt");

                                        //populate simulations dictionary
                                        for (int k = 3; k < simulations_content_month.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_month[k]);

                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), int.Parse(parts[1]), 1);
                                                sim_dictionary.Add(date_value, double.Parse(parts[14]));
                                            }

                                        }
                                        break;

                                    case "Yearly":

                                        string[] simulations_content_year = read_from($@"{eval_project.txtinout}\lsunit_wb_yr.txt");

                                        //populate simulations dictionary
                                        for (int k = 3; k < simulations_content_year.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_year[k]);

                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), 1, 1);
                                                sim_dictionary.Add(date_value, double.Parse(parts[14]));
                                            }
                                        }

                                        break;

                                    default:
                                        break;
                                }
                                break;
                            case "All Basin":
                                switch (eval_project.current_observations[i].timestep)
                                {
                                    case "Daily":

                                        string[] simulations_content_day = read_from($@"{eval_project.txtinout}\basin_wb_day.txt");

                                        for (int k = 3; k < simulations_content_day.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_day[k]);
                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), int.Parse(parts[1]), int.Parse(parts[2]));
                                                sim_dictionary.Add(date_value, double.Parse(parts[14]));
                                            }
                                        }
                                        break;

                                    case "Monthly":

                                        string[] simulations_content_month = read_from($@"{eval_project.txtinout}\basin_wb_mon.txt");

                                        //populate simulations dictionary
                                        for (int k = 3; k < simulations_content_month.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_month[k]);

                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), int.Parse(parts[1]), 1);
                                                sim_dictionary.Add(date_value, double.Parse(parts[14]));
                                            }

                                        }
                                        break;

                                    case "Yearly":

                                        string[] simulations_content_year = read_from($@"{eval_project.txtinout}\basin_wb_yr.txt");

                                        //populate simulations dictionary
                                        for (int k = 3; k < simulations_content_year.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_year[k]);

                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), 1, 1);
                                                sim_dictionary.Add(date_value, double.Parse(parts[14]));
                                            }
                                        }

                                        break;

                                    default:
                                        break;
                                }
                                break;
                        }
                        break;

                    case "Sediments":
                        //populate simulations dictionary
                        switch (eval_project.current_observations[i].object_type)
                        {
                            case "Channel":
                                switch (eval_project.current_observations[i].timestep)
                                {
                                    case "Daily":

                                        string[] simulations_content_day = read_from($@"{eval_project.txtinout}\channel_sd_day.txt");

                                        for (int k = 3; k < simulations_content_day.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_day[k]);
                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), int.Parse(parts[1]), int.Parse(parts[2]));
                                                sim_dictionary.Add(date_value, double.Parse(parts[48]));
                                            }
                                        }
                                        break;

                                    case "Monthly":

                                        string[] simulations_content_month = read_from($@"{eval_project.txtinout}\channel_sd_mon.txt");

                                        //populate simulations dictionary
                                        for (int k = 3; k < simulations_content_month.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_month[k]);

                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), int.Parse(parts[1]), 1);
                                                sim_dictionary.Add(date_value, double.Parse(parts[48]));
                                            }

                                        }
                                        break;

                                    case "Yearly":

                                        string[] simulations_content_year = read_from($@"{eval_project.txtinout}\channel_sd_yr.txt");

                                        //populate simulations dictionary
                                        for (int k = 3; k < simulations_content_year.Count(); k++)
                                        {
                                            DateTime date_value;
                                            string[] parts = split_string_by_space(simulations_content_year[k]);

                                            if (int.Parse(parts[4]) == eval_project.current_observations[i].number)
                                            {
                                                date_value = new DateTime(int.Parse(parts[3]), 1, 1);
                                                sim_dictionary.Add(date_value, double.Parse(parts[48]));
                                            }
                                        }

                                        break;

                                    default:
                                        break;
                                }

                                break;
                        }
                        break;
                }

                // calculate indices

                //get observation average
                foreach (var date_stamp_obs in obs_dictionary.Keys)
                {
                    if (obs_dictionary[date_stamp_obs] >= 0)
                    {
                        if (sim_dictionary.ContainsKey(date_stamp_obs))
                        {
                            // For NSE
                            NSE_numerator = NSE_numerator + ((sim_dictionary[date_stamp_obs] - obs_dictionary[date_stamp_obs]) * (sim_dictionary[date_stamp_obs] - obs_dictionary[date_stamp_obs]));
                            NSE_denominator = NSE_denominator + ((obs_dictionary[date_stamp_obs] - valid_observations.Average()) * (obs_dictionary[date_stamp_obs] - valid_observations.Average()));

                            PBIAS_denominator = PBIAS_denominator + obs_dictionary[date_stamp_obs];

                        }
                    }
                }

                // this will be sent over for graphing
                time_series_data.observed_timeseries[eval_project.current_observations[i].chart_name] = obs_dictionary;
                time_series_data.simulated_timeseries[eval_project.current_observations[i].chart_name] = sim_dictionary;

                eval_project.current_observations[i].nse = Math.Round((1 - (NSE_numerator / NSE_denominator)), 2);
                eval_project.current_observations[i].pbias = Math.Round((1 - (NSE_numerator * 100 / PBIAS_denominator)), 2);

            }
            return (eval_project, time_series_data);
        }

    }

}
