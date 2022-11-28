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

namespace W11Service
{
    public partial class MyService : ServiceBase
    {
        public MyService()
        {
            InitializeComponent();
            CanPauseAndContinue = true;
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {

        }
    }
}
