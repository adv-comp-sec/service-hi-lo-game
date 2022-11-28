using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W11Service
{
    

    public class FileDataEntity
    {
        const string Create = "Create";
        const string Delete = "Delete";
        const string Change = "Change";
        const string Rename = "Rename";
        const int Error = -1;
        public int ID { get; set; }

        public string Action
        {
            get { return Action; }
            set { 
                if (value == Create || value == Delete || value == Change || value == Rename)
                {
                    Action = value;
                }
                else
                {
                    Action = Error.ToString();
                }
            }
        }
        public string PathName { get; set; }
        public string OldPathName { get; set; }
        
        public DateTime TimeAffected { get; set; }
    }
}
