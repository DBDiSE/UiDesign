using System;
using System.Collections.Generic;
using System.Text;

namespace UiDesign
{
    public class ParsingClass
    {

        public class Rootobject
        {
            public Chatters chatters { get; set; }
        }

        public class Chatters
        {
            public string[] broadcaster { get; set; }
            public string[] vips { get; set; }
            public string[] moderators { get; set; }
            public object[] staff { get; set; }
            public object[] admins { get; set; }
            public object[] global_mods { get; set; }
            public string[] viewers { get; set; }
        }

    }






}
