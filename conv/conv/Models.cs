using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conv
{
    struct Place
    {
        public string country { set; get; }
        public string city { set; get; }
        public string street { set; get; }
        public string house { set; get; }
    }

    struct Programm
    {
        public string title { set; get; }
        public string versoin { set; get; }
        public string dataReilese { set; get; }
    }
}
