using Cosmos.System.Network.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PesOS
{
    public class SysInfo
    {
        public void SystemInfo()
        {
            Console.WriteLine("As the world embarks into the era of digitalization, innovation is the new currency. PLM EduSkolars' Operating System, or PesOS, is a personalized operating system developed by Alambra, J., Aragon, P., Banal, D., Beron, A., Bolocon, J., and De Guzman, J. Moreover, PesOS aims to innovate the lives of Filipinos through the aspect of tax calculation.");
            Console.WriteLine("S.Y. 2023-2024\n");
            Console.WriteLine("OS Name   : PesOS");
            Console.WriteLine("Version   : 1.0.0");
            Console.WriteLine(NetworkConfiguration.CurrentAddress?.ToString() ?? "146.168.1.78");
        }

    }
}
