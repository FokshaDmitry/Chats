using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.CommandServer
{
    public class Online
    {
        AddDbContext add;
        List<string> mess;
        List<string> online;
        public Online()
        {
            mess = new List<string>();
            online = new List<string>();
            add = new AddDbContext();
        }
        public LibProtocol.Response buildResponce(ref LibProtocol.Response response)
        {
            try
            {
                if (add.Online.Count() != 0)
                {
                    foreach (var item in add.Online)
                    {
                        online.Add($"{item.Name} {item.Surname}");
                    }
                }

                if (add.Message.Count() != 0)
                {
                    foreach (var item in add.Message.Join(add.DbUsers, m => m.IdUser, u => u.Id, (m, u) => new {Mess = m, Use = u} ).OrderByDescending(m => m.Mess.dateMess).Take(50).Reverse())
                    {
                        mess.Add($"{item.Mess.dateMess} {item.Use.Name} {item.Use.Surname}: {item.Mess.Messages}");
                    }
                }
                response.data = new LibProtocol.Online { OnlineUser = online, Message = mess};
                response.succces = true;
                response.code = LibProtocol.ResponseCode.Ok;
            }
            catch (Exception)
            {
                response.succces = false;
                response.code = LibProtocol.ResponseCode.Error;
                response.StatusTxt = "Online False";
            }
            return response;
        }
    }
}
