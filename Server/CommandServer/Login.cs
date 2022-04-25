using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    public class Login
    {
        LibProtocol.Models.User data;
        AddDbContext add;
        LibProtocol.Models.User user;
        public Login(LibProtocol.Models.User data)
        {
            this.data = data;
            add = new AddDbContext();
            user = new LibProtocol.Models.User();
        }

        public LibProtocol.Response buildResponce(ref LibProtocol.Response response)
        {
            if (add.DbUsers.Count() == 0)
            {
                response.succces = false;
                response.code = LibProtocol.ResponseCode.Error;
                response.StatusTxt = "Database is Empty";
                return response;

            }

            foreach (var item in add.DbUsers)
            {
                if (item.Login == data.Login && item.Password == data.Password)
                {

                    user = item;
                    response.data = new LibProtocol.Models.User { Id = item.Id, Name = item.Name, Surname = item.Surname, Phpto = item.Phpto };
                    response.succces = true;
                    response.code = LibProtocol.ResponseCode.Ok;
                    response.StatusTxt = "Login Ok";

                    return response;
                }
                else
                {
                    response.succces = false;
                    response.code = LibProtocol.ResponseCode.Error;
                    response.StatusTxt = "Wrong login or password";
                }
            }

            return response;
        }
        public void AddOnline()
        {
            foreach (var item in add.Online)
            {
                if (user.Id == item.IdUser)
                {
                    add.Online.Remove(item);
                }
            }
            add.Online.Add(new LibProtocol.Models.Online { Id = Guid.NewGuid(), Name = user.Name, Surname = user.Surname, IdUser = user.Id });
            add.SaveChanges();
        }
        public void ExitOnline()
        {
            foreach (var item in add.Online)
            {
                if (data.Id == item.IdUser)
                {
                    add.Online.Remove(item);
                }
            }
            add.SaveChanges();
        }
    }
}
