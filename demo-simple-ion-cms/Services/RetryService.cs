using System;
using System.Threading.Tasks;
using demo_simple_ion_cms.IServices;
using Serilog;

namespace demo_simple_ion_cms.Services
{
    public class RetryService : IRetryService
    {
        public int RetryCount = 0;
        
        public void DemoRetry()
        {
            Log.Information($"Method is running {RetryCount} times at {DateTime.UtcNow}");

            RetryCount++;

            if (RetryCount <= 4)
            {
                throw new Exception();
            }
        }
    }
}