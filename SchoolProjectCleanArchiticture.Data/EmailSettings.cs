using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Data
{
    public class EmailSettings
    {
        public int portNumber {  get; set; }
        public string Host {  get; set; }
        public string FromAddress { get; set; }
        public string Password { get; set; }
        
    }
}
