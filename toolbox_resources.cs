using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using YamlDotNet.Serialization;

using static SWAT__Toolbox.home_module;

namespace SWAT__Toolbox
{
    class toolbox_functions
    {

        public static bool save_project(project project_object)
        {
            // saving the project
            using (StreamWriter streamWriter = new StreamWriter(project_object.project_path))
            {
                Serializer serializer = new Serializer();
                serializer.Serialize(streamWriter, project_object);
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
