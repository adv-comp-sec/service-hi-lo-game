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
using W11Service;

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
                GameEngine game = new GameEngine();
                game.StartListening();
            });
        }

        protected override void OnStop()
        {
            Logger.Log("Stopping HiLo Game Service.");
            GameEngine game = new GameEngine();
            game.StopListening();
        }
    }
}
