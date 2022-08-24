using System;
using System.Collections.Generic;
using System.Text;

namespace AllModel.Code
{
    public class InitializationData
    {
        public List<Config> Config { get; set; }
        public List<Account> Account { get; set; }
        //public List<Depart> Depart { get; set; }
        //public List<StudentInfo> StudentInfo { get; set; }

        public static InitializationData Initialization { get; set; } = new InitializationData();
    }
}
