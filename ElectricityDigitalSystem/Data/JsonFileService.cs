using ElectricityDigitalSystem.ClientServices;
using ElectricityDigitalSystem.Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace ElectricityDigitalSystem.Data
{
    public class JsonFileService
    {
        private readonly string JsonFileName = "ElectricalDigitalSystem.json";
        public DbContext Database { get; set; }

        public JsonFileService()
        {
            this.OnInit();
        }

        private void OnInit()
        {
            if(!Directory.Exists(FileConstant.DBFOLDER))
            {
                Directory.CreateDirectory(FileConstant.DBFOLDER);  
            }
            if (!File.Exists(Path.Combine(FileConstant.DBFOLDER, JsonFileName)))
            {
                File.Create(Path.Combine(FileConstant.DBFOLDER, JsonFileName)).Close();
                this.Database = new DbContext();
                
                
            }

            else
            {
                //if file exist then read the file from the directory
                using StreamReader reader = new StreamReader(Path.Combine(FileConstant.DBFOLDER, JsonFileName));
                {
                    try
                    {
                        //Read all filecontent from the json file
                        string jsonFileContent = reader.ReadToEnd();
                        this.Database = JsonSerializer.Deserialize<DbContext>(jsonFileContent);
                    }
                    catch(Exception )
                    {
                        //If the databse is empty instead of throwing an error just create an instance of the database
                        Database = new DbContext();             
                    }
                }
            }
        }
        public void SaveChanges()
        {
            
            //This will covert the database object in a json and write to a file
            
            string newJson = JsonSerializer.Serialize(Database);
            File.WriteAllText(Path.Combine(FileConstant.DBFOLDER, JsonFileName), newJson);
        }
    }
}
