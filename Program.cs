using NetLimiter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionNetlimiter.limit;

namespace VisionLimit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<VFilter> filters = new List<VFilter>();

            var nlClient = new NLClient();
            Console.WriteLine("Ayo");

            try
            {
                nlClient.Connect();
            }
            catch (Exception e)
            {
                Console.WriteLine("Connect exception: {0}", e.Message);
                return;
            }

            Console.WriteLine("Adding filters");
           // filters.Add(new VFilter(client, RuleDir.Out, 3074, 1));

        }
    }
}
