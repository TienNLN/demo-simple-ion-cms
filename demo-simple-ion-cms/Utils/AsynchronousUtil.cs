using System.IO;
using System.Threading;
using System.Threading.Tasks;
using demo_simple_ion_cms.Constants;
using Serilog;

namespace demo_simple_ion_cms.Utils
{
    public static class AsynchronousUtil
    {
        private static async Task<int> ProcessReadFile(string filePath)
        {
            var length = 0;
            
            Log.Information("Starting read file...");

            using (var reader = new StreamReader(filePath))
            {
                var content = await reader.ReadToEndAsync();

                length = content.Length;
            }

            Log.Information("File reading is done.");
            
            return length;
        }

        public static async void Execute()
        {
            var taskReadFile = ProcessReadFile(GlobalConstants.SKILLS_FILE_PATH);
            
            Log.Information("Other work 1");
            
            Log.Information("Other work 2");
            
            Log.Information("Other work 3");

            var length = await taskReadFile;
            Log.Information($"Total length of file is {length}");
            
            Log.Information("After work 1");
            Log.Information("After work 2");
            Log.Information("After work 3");
        }
    }
}