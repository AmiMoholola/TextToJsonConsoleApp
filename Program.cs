using Serilog;


namespace FileScanApp
{
 
   class Program
    {
        static void Main(string[] args)
        {
            
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("../../../logs/FileScanApp.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            FileScanApp.DataConvert DataConvert = new DataConvert();

            DataConvert.DataReader("../../../TextInputFile/BDG_Input.txt");
        }
    }
}
