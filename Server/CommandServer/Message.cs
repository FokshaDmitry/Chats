using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.CommandServer
{
    class Message
    {
        AddDbContext add;
        LibProtocol.Models.Message message;
        public Message(LibProtocol.Models.Message message)
        {
            this.message = message;
            add = new AddDbContext();
        }
        public LibProtocol.Response buildResponce(ref LibProtocol.Response response)
        {
            try
            {
                add.Message.Add(message);
                add.SaveChanges();
                response.succces = true;
                response.code = LibProtocol.ResponseCode.Ok;
            }
            catch (Exception)
            {
                response.succces = false;
                response.code = LibProtocol.ResponseCode.Error;
                response.StatusTxt = "Message dont send";
            }
            return response;
        }
    }
}
