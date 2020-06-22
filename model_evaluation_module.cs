using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SWAT__Toolbox
{
    class model_evaluation_module
    {
        // water balance resources
        public class water_balance_results
        {
            //wb_basin
            public double precip { get; set; }
            public double snofall { get; set; }
            public double snomlt { get; set; }
            public double surq_gen { get; set; }
            public double latq { get; set; }
            public double wateryld { get; set; }
            public double perc { get; set; }
            public double et { get; set; }
            public double tloss { get; set; }
            public double eplant { get; set; }
            public double esoil { get; set; }
            public double surq_cont { get; set; }
            public double cn { get; set; }
            public double sw { get; set; }
            public double sw_300 { get; set; }
            public double snopack { get; set; }
            public double pet { get; set; }
            public double qtile { get; set; }
            public double irr { get; set; }
            public double surq_runon { get; set; }
            public double latq_runon { get; set; }
            public double overbank { get; set; }
            public double surq_cha { get; set; }
            public double surq_res { get; set; }
            public double surq_ls { get; set; }
            public double latq_cha { get; set; }
            public double latq_res { get; set; }
            public double latq_ls { get; set; }


            //aqu_basin
            public double aqu_flo { get; set; }
            public double aqu_dep_wt { get; set; }
            public double aqu_stor { get; set; }
            public double aqu_rchrg { get; set; }
            public double aqu_seep { get; set; }
            public double aqu_revap { get; set; }
            public double aqu_no3_st { get; set; }
            public double aqu_min { get; set; }
            public double aqu_orgn { get; set; }
            public double aqu_orgp { get; set; }
            public double aqu_rchrgn { get; set; }
            public double aqu_nloss { get; set; }
            public double aqu_no3gw { get; set; }
            public double aqu_seepno3 { get; set; }
            public double aqu_flo_cha { get; set; }
            public double aqu_flo_res { get; set; }
            public double aqu_flo_ls { get; set; }

        }

        public static water_balance_results default_water_balance()
        {
            water_balance_results defaults = new water_balance_results();

            // wb
            defaults.precip = 0;
            defaults.snofall = 0;
            defaults.snomlt = 0;
            defaults.surq_gen = 0;
            defaults.latq = 0;
            defaults.wateryld = 0;
            defaults.perc = 0;
            defaults.et = 0;
            defaults.tloss = 0;
            defaults.eplant = 0;
            defaults.esoil = 0;
            defaults.surq_cont = 0;
            defaults.cn = 0;
            defaults.sw = 0;
            defaults.sw_300 = 0;
            defaults.snopack = 0;
            defaults.pet = 0;
            defaults.qtile = 0;
            defaults.irr = 0;
            defaults.surq_runon = 0;
            defaults.latq_runon = 0;
            defaults.overbank = 0;
            defaults.surq_cha = 0;
            defaults.surq_res = 0;
            defaults.surq_ls = 0;
            defaults.latq_cha = 0;
            defaults.latq_res = 0;
            defaults.latq_ls = 0;

            // aqu
            defaults.aqu_flo = 0;
            defaults.aqu_dep_wt = 0;
            defaults.aqu_stor = 0;
            defaults.aqu_rchrg = 0;
            defaults.aqu_seep = 0;
            defaults.aqu_revap = 0;
            defaults.aqu_no3_st = 0;
            defaults.aqu_min = 0;
            defaults.aqu_orgn = 0;
            defaults.aqu_orgp = 0;
            defaults.aqu_rchrgn = 0;
            defaults.aqu_nloss = 0;
            defaults.aqu_no3gw = 0;
            defaults.aqu_seepno3 = 0;
            defaults.aqu_flo_cha = 0;
            defaults.aqu_flo_res = 0;
            defaults.aqu_flo_ls = 0;

            return defaults;
        }


        // nutrient balance resources
        public class nutrient_balance_results
        {
            public double grzn { get; set; }
            public double grzp { get; set; }
            public double lab_min_p { get; set; }
            public double act_sta_p { get; set; }
            public double fertn { get; set; }
            public double fertp { get; set; }
            public double fixn { get; set; }
            public double denit { get; set; }
            public double act_nit_n { get; set; }
            public double act_sta_n { get; set; }
            public double org_lab_p { get; set; }
            public double rsd_nitorg_n { get; set; }
            public double rsd_laborg_p { get; set; }
            public double no3atmo { get; set; }
            public double nh4atmo { get; set; }

        }

        public static nutrient_balance_results default_nutrient_balance()
        {
            nutrient_balance_results defaults = new nutrient_balance_results();

            defaults.grzn = 0;
            defaults.grzp = 0;
            defaults.lab_min_p = 0;
            defaults.act_sta_p = 0;
            defaults.fertn = 0;
            defaults.fertp = 0;
            defaults.fixn = 0;
            defaults.denit = 0;
            defaults.act_nit_n = 0;
            defaults.act_sta_n = 0;
            defaults.org_lab_p = 0;
            defaults.rsd_nitorg_n = 0;
            defaults.rsd_laborg_p = 0;
            defaults.no3atmo = 0;
            defaults.nh4atmo = 0;

            return defaults;
        }

        public static BitmapImage BitmapToImageSource(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }


        // plant resources
        public class plant_results
        {
            public double lai { get; set; }
            public double bioms { get; set; }
            public double yield { get; set; }
            public double residue { get; set; }
            public double sol_tmp { get; set; }
            public double strsw { get; set; }
            public double strsa { get; set; }
            public double strstmp { get; set; }
            public double strsn { get; set; }
            public double strsp { get; set; }
            public double nplt { get; set; }
            public double percn { get; set; }
            public double pplnt { get; set; }
            public double tmx { get; set; }
            public double tmn { get; set; }
            public double tmpav { get; set; }
            public double solarad { get; set; }
            public double wndspd { get; set; }
            public double rhum { get; set; }
            public double phubas0 { get; set; }

        }

        public static plant_results default_plant_results()
        {
            plant_results defaults = new plant_results();

            defaults.lai = 0;
            defaults.bioms = 0;
            defaults.yield = 0;
            defaults.residue = 0;
            defaults.sol_tmp = 0;
            defaults.strsw = 0;
            defaults.strsa = 0;
            defaults.strstmp = 0;
            defaults.strsn = 0;
            defaults.strsp = 0;
            defaults.nplt = 0;
            defaults.percn = 0;
            defaults.pplnt = 0;
            defaults.tmx = 0;
            defaults.tmn = 0;
            defaults.tmpav = 0;
            defaults.solarad = 0;
            defaults.wndspd = 0;
            defaults.rhum = 0;
            defaults.phubas0 = 0;

            return defaults;
        }

    }
}
