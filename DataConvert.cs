using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Serilog;
using System.Text.Json;


namespace FileScanApp
{
    class DataConvert
    {
        private string InputRow { get; set; }
        private List<Employee> Employees { get; set; }
        private String FilePath { get; set; }
        private System.IO.StreamReader InputFile { get; set; }

        public void DataReader(String Inputfile)
        {
            try
            {
                if (File.Exists(@"" + Inputfile + ""))
                {
                    InputFile = new System.IO.StreamReader(@"" + Inputfile + "");
                    int counter = 0;
                    Employees = new List<Employee>();
                    while ((InputRow = InputFile.ReadLine()) != null)
                    {
                        if (!string.IsNullOrWhiteSpace(InputRow))
                        {
                            string[] words = InputRow.Split('|');
                            Employees.Add(new Employee
                            {
                                id = Int32.Parse(words[0]),
                                name = words[1],
                                surname = words[2],
                                email = words[3],
                                gender = words[4],
                                ipaddress = words[5]
                            });
                            counter++;
                        }
                    }
                    CreatejsonFile(ConvertToJson(Employees));
                }
                else
                {
                    Console.Out.WriteLine("Input File not Found");
                    Log.Error("Input File not Found");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                Console.Out.WriteLine(ex.ToString());
                throw ex;
            }
        }
        private static string ConvertToJson(List<Employee> employee)
        {
                try
                {
                    if (employee != null)
                    {
                        return JsonConvert.SerializeObject(employee);
                    }
                else
                    {
                        Exception ex = new Exception();
                        Log.Error(ex, "Exception thrown on ConvertToJson Object is null ");
                        Console.Out.WriteLine("Object is null");
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                    Console.Out.WriteLine(ex.ToString());
                    throw ex;
                }
        }
        private void CreatejsonFile(string JsonData)
        {
                try
                {
                    if (JsonData != null)
                    {
                        FilePath = @"C:\BDG_Output.json";
                        using (var sw = new StreamWriter(FilePath, false))
                        {
                            sw.WriteLine(JsonData.ToString());
                            sw.Close();
                            Console.WriteLine("Json OutPut File Created");
                        }
                    }
                    else
                    {
                        Exception ex = new Exception();
                        Log.Error(ex, "Exception thrown on CreatejsonFile JsonData is null ");
                        Console.WriteLine("Json data is null");
                        throw ex;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                    throw ex;
                }  
        }
    }
}
