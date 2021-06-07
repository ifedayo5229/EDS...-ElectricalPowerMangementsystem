using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ElectricityDigitalSystem.Constant
{
    public class FileConstant
    {
        public readonly static string DBFOLDER = Path.Combine(Environment.GetFolderPath
                    (Environment.SpecialFolder.MyDocuments), "ElectrictityDigitalSystem");
    }
}
