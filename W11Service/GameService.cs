using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace A06Service
{
    public partial class A06Service : ServiceBase
    {
        public A06Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Log("Starting HiLo Game Service.");
            Task.Run(() =>
            {
                HiLoGame_Server.SynchronousSocketListenenr ssl = new HiLoGame_Server.SynchronousSocketListenenr();
                ssl.StartListening();
            });
        }

        protected override void OnStop()
        {
            Logger.Log("Stopping HiLo Game Service.");
            HiLoGame_Server.SynchronousSocketListenenr ssl = new HiLoGame_Server.SynchronousSocketListenenr();
            ssl.StopListening();
        }
    }
}
