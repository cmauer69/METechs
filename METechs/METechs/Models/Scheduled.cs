using System;
using System.Collections.Generic;
using System.Text;

namespace METechs.Models
{
    class Scheduled : BaseDataObject
    {
        public int Id_project { get; set; }
        public int client_id { get; set; }

        public new DateTime start_time { get; set; }

        public new DateTime end_time { get; set; }

        public new decimal hours_worked { get; set; }

        public new decimal pay_rate { get; set; }

        public new string sow { get; set; }

        public new string text_notes { get; set; }
    }
}
