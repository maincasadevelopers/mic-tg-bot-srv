using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MIC_TG_BOT_SRV
{
    internal static class Program
    {
        
        static void Main()
        {
            
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new TelegramService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
