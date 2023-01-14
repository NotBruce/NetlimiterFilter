using NetLimiter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionNetlimiter.limit
{
    internal class VFilter
    {

        public Filter filter;
        public NLClient client;
        public RuleDir ruleDir;
        public Rule rule;
        public ushort port;
        public uint bytes;

        public VFilter(NLClient client, RuleDir ruleDir, ushort port, uint bytes)
        {
            this.client = client;
            this.ruleDir = ruleDir;
            this.port = port;
            this.bytes = bytes;

            this.rule = new LimitRule(ruleDir, bytes);

            // Checks if filter does not exist, if it doesnt exist create new filter.
            if (client.Filters.Find(x => x.Name == port.ToString() + ruleDir.ToString()) == null)
            {
                this.filter = new Filter(port.ToString() + ruleDir.ToString());
                this.filter.Functions.Add(new FFPathEqual(Constants.appPath));
                this.filter.Functions.Add(new FFRemotePortInRange(new PortRangeFilterValue(port, port))); // Limit port in range

                this.filter = client.AddFilter(this.filter);
                this.rule = client.AddRule(this.filter.Id, new LimitRule(ruleDir, bytes));
                this.rule.IsEnabled = false;
                client.UpdateRule(rule);
                return;
            }

            this.filter = client.Filters.Find(x => x.Name == port.ToString());
            this.rule = client.Rules.Find(x => x.FilterId == this.filter.Id);
            this.rule.IsEnabled = false;
            client.UpdateRule(rule);
        }
    }
}
