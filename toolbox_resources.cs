using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace SWAT__Toolbox
{
    class toolbox_functions
    {
        public static string pick_file(string filter_extension, string window_heading, string action_button)
        {
            string file_path = "";
            OpenFileDialog open_dialog = new OpenFileDialog();
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
    }
}
