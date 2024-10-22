using Lab06_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab06_3
{
    public class Subscriber : IClient
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CallMinutesPerMonth { get; set; }
        public int SMSPerMonth { get; set; }
        public double MonthlyFee { get; set; }
        public bool HasRoaming { get; set; }
        public bool HasDataPlan { get; set; }

        public Subscriber(string v) { }

        public Subscriber(string name, string phoneNumber, string address, int callMinutesPerMonth, int smsPerMonth, double monthlyFee, bool hasRoaming, bool hasDataPlan, int v)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            CallMinutesPerMonth = callMinutesPerMonth;
            SMSPerMonth = smsPerMonth;
            MonthlyFee = monthlyFee;
            HasRoaming = hasRoaming;
            HasDataPlan = hasDataPlan;
        }

        public Subscriber(string v1, string v2, string v3, int v4, int v5, int v6, bool v7, bool v8)
        {
        }

        public Subscriber()
        {
        }

        public Subscriber(string name, string phoneNumber, string address, int callMinutesPerMonth, int smsPerMonth, double monthlyFee, bool hasRoaming, bool hasDataPlan, int v, int v1) : this(name, phoneNumber, address, callMinutesPerMonth, smsPerMonth, monthlyFee, hasRoaming, hasDataPlan, v)
        {
        }
    }
}