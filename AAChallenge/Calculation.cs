using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAChallenge
{
    class Calculation
    {
        public Calculation()
        {
            emailAddress = "b195ex@gmail.com";
            name = "Erick J. Viera";

        }
        public string encodedValue { get; set; }
        public string emailAddress { get; set; }
        public string name { get; set; }
        public string webhookUrl { get; set; }
        public string repoUrl { get; set; }
    }
}
