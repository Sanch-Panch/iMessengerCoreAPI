using System;

namespace iMessengerCoreAPI
{
    public class Startup
    {
        public DateTime Date { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
    }
}
