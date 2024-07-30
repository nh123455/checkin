using Checkin.DAL;
using Checkin.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkin.BUS
{
    public class userBUS
    {
        public bool InsertUser(List<userDTO> list, string connectstring)
        {
            userDAL usDAL = new userDAL();
            return usDAL.InsertUser(list, connectstring);
        }

        public bool ResetCheckin(string connectstring)
        {
            userDAL usDAL = new userDAL();
            return usDAL.ResetCheckin(connectstring);
        }

        public bool DeleteData(string connectstring)
        {
            userDAL usDAL = new userDAL();
            return usDAL.DeleteData(connectstring);
        }

        public List<userDTO> GetUser(string connectstring)
        {
            userDAL usDAL = new userDAL();
            return usDAL.GetUser(connectstring);
        }

        public List<userDTO> GetUserByQrcode(string qrcode, string connectstring)
        {
            userDAL usDAL = new userDAL();
            return usDAL.GetUserByQrcode(qrcode, connectstring);
        }

        public List<userDTO> GetUserDetail(string connectstring)
        {
            userDAL usDAL = new userDAL();
            return usDAL.GetUserDetail(connectstring);
        }

        public bool Checkin(string qrcode, string connectstring)
        {
            userDAL usDAL = new userDAL();
            return usDAL.Checkin(qrcode, connectstring);
        }
    }
}
