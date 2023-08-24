using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
namespace LoggingTest
{
    public class Service : IService
    {
        private readonly ILogger _logger;
        public Service(ILogger<Service> logger)
        {
            _logger = logger;
        }

        public void Execute()
        {
            _logger.LogInformation("Service.Execute()");
        }
    }

    public interface IService
    {
        void Execute();
    }
}
