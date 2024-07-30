using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkin.DTO
{
    public class userDTO
    {
        public String qrcode { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public String phone { get; set; }
        public String company { get; set; }
        public Int32? type { get; set; }
        public DateTime? scan_time { get; set; }
    }
}
