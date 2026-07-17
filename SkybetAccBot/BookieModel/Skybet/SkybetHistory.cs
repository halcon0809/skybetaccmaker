using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkybetAccBot
{
    public class SkybetHistory
    {
        public string RegionIP { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } = new DateTime();
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Mothername { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public string LoginPIN { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; } = new DateTime();
    }
}
