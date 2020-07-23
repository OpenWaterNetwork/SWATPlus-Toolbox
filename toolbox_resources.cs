using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Shapes;
using Microsoft.Win32;
using YamlDotNet.Serialization;

using static SWAT__Toolbox.home_module;

namespace SWAT__Toolbox
{
    class toolbox_functions
    {

        public static string[] split_string_by_space(string input_string)
        {
            string[] string_parts = input_string.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            return string_parts;
        }
        
        public static string[] read_from(string file_path)
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines(file_path);
            }
            catch (Exception)
            {

                MessageBox.Show($@"Could not read the file,{System.IO.Path.GetFileName(file_path)}{Environment.NewLine}Make sure it exists.");
                return new string[] { };
            }
            return lines;
        }

        public static string get_parameter_change_type_calibration(String change_type)
        {
            string par_change_typ;

            switch (change_type)
            //    Channel, Landscape Unit, Hydrologic Response Unit, All Basin
            {
                case "Percent":
                    par_change_typ = "pctchg";
                    break;
                case "Relative":
                    par_change_typ = "abschg";
                    break;
                case "Replace":
                    par_change_typ = "absval";
                    break;

                default:
                    par_change_typ = "pctchg";
                    break;
            }
            return par_change_typ;
        }


        public static ObservableCollection<recent_project> sort_recents(ObservableCollection<recent_project> recents, project project_obj)
        {

            foreach (var item in recents) {
                if (item.project_name == null)
                {
                    recents.Remove(item);
                }
            }
            if (project_obj == null) { return recents; }

            recent_project recent_instance = new recent_project
            {
                project_name = project_obj.project_name,
                project_path = project_obj.project_path,
                txtinout = project_obj.txtinout,
                last_modified = project_obj.last_modified
            };

            // saving recent projects
            using (StreamWriter streamWriter = new StreamWriter($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\SWAT+ Toolbox\recents.dll"))
            {
                Serializer serializer = new Serializer();
                serializer.Serialize(streamWriter, recents);
            }

            try  // incase a different thread tries to modify source
            {
                recents.Remove(recents.Where(i => i.project_name == recent_instance.project_name).First());
                recents.Remove(recents.Where(i => i.project_name == "").First());
            }
            catch (Exception)
            {

            }

            try  // incase a different thread tries to modify source
            {
                recents.Insert(0, recent_instance);
            }
            catch (Exception)
            {

            }

            return recents;
        }


        public static bool save_project(project project_object, ObservableCollection<recent_project> recents)
        {
            // saving the project
            using (StreamWriter streamWriter = new StreamWriter(project_object.project_path))
            {
                Serializer serializer = new Serializer();
                serializer.Serialize(streamWriter, project_object);
            }


            //if not path - create
            Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\SWAT+ Toolbox");

            //sort recents
            recents = sort_recents(recents, project_object);

            // saving recent projects
            using (StreamWriter streamWriter = new StreamWriter($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\SWAT+ Toolbox\recents.dll"))
            {
                Serializer serializer = new Serializer();
                serializer.Serialize(streamWriter, recents);
            }

            return true;

        }
        public static string pick_file(string filter_extension, string window_heading, string action_button)
        {

            string file_path = "";
            Microsoft.Win32.OpenFileDialog open_dialog = new Microsoft.Win32.OpenFileDialog();
            open_dialog.Filter = filter_extension;
            open_dialog.Title = window_heading;
            open_dialog.RestoreDirectory = true;
            open_dialog.DefaultExt = ".csv";
            Nullable<bool> dialog_result = open_dialog.ShowDialog();

            if (dialog_result == true)
            {
                file_path = open_dialog.FileName;
            }
            return file_path;
        }
        public static string pick_folder(string window_heading, string action_button)
        {
            string folder_path = "";
            FolderBrowserDialog folder_dialog = new FolderBrowserDialog();

            folder_dialog.ShowNewFolderButton = false;
            folder_dialog.Description = window_heading;

            DialogResult result = folder_dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                folder_path = folder_dialog.SelectedPath;
            }
            return folder_path;
        }
        public static string pick_save_path(string filter_extension, string window_heading, string action_button)
        {
            string file_path = "";
            Microsoft.Win32.SaveFileDialog save_dialog = new Microsoft.Win32.SaveFileDialog();
            save_dialog.Filter = filter_extension;
            save_dialog.Title = window_heading;
            save_dialog.RestoreDirectory = true;
            save_dialog.DefaultExt = ".spt";
            Nullable<bool> dialog_result = save_dialog.ShowDialog();

            if (dialog_result == true)
            {
                file_path = save_dialog.FileName;
            }
            return file_path;
        }
    }
}
