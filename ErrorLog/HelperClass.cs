using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment_IRCTC_Revervation.ErrorLog
{
    public class Helper
    {
        public static void WriteLog(string message)
        {
            
            string ErrorLogDir = " C: \\Users\\win10\\Desktop\\Assessment_IRCTC_Revervation\\ErrorLog\\";


            if (!Directory.Exists(ErrorLogDir))
                Directory.CreateDirectory(ErrorLogDir);

            ErrorLogDir += "\\error" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt";
            using (StreamWriter sr = new StreamWriter(ErrorLogDir, true))
            {
                sr.WriteLine(DateTime.Now.ToString("DD-MM-yyyy HH-mm-ss") + message);
            }

        }
    }
}
