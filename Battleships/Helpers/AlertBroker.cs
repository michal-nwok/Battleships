using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Helpers
{
    public static class AlertBroker
    {
        public static List<string> Alerts { get; set; } = new();

        public static void PrintAlerts()
        {
            foreach(string alert in Alerts)
            {
                Console.WriteLine(alert);
            }
        }

        public static void AddAlert(string message)
        {
            Alerts.Add(message);
        }

        public static void ClearAlerts()
        {
            Alerts.Clear();
        }
    }
}
