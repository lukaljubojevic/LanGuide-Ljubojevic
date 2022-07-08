using System;
using System.Collections.Generic;
using System.Text;

namespace LanGuide_Ljubojevic.Model
{
    public class ResultsAPI
    {
        public string id_user { get; set; }
        public string email { get; set; }
        public string create_time { get; set; }
        public string id_exercise { get; set; }
        public int result_percent { get; set; }
        public int result_score { get; set; }
        public string result_max { get; set; }
        public string skill { get; set; }
        public string language {get; set; }
        public string result_date { get; set; }

    }
}
