using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chats
{
    public class ServerConnect
    {
        public IPAddress ip = IPAddress.Parse("127.0.0.1");
        public int port = 3456;

        public event Action<string>? onError;
        TcpClient tpcClient;
        Thread threadClient;
        NetworkStream stream;

        BinaryFormatter bf = new BinaryFormatter();

        public void Connect()
        {
            try
            {
                tpcClient = new TcpClient();
                tpcClient.Connect(new IPEndPoint(ip, port));
                stream = tpcClient.GetStream();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex.Message);
            }
        }
        public void MonOnline()
        {
            LibProtocol.Request request = new LibProtocol.Request();
            request.command = LibProtocol.Command.Online;
            try
            {
                bf.Serialize(stream, request);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex.Message);
            }
        }
        public void SendMessage(string mess, Guid id)
        {
            LibProtocol.Request request = new LibProtocol.Request();
            request.command = LibProtocol.Command.Message;
            request.data = new LibProtocol.Models.Message { Id = Guid.NewGuid(), Messages = mess, dateMess = DateTime.Now, IdUser = id};
            try
            {
                bf.Serialize(stream, request);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex.Message);
            }
        }
        public void newAccaunt(string name, string surname, DateTime date, string login, string password, byte[] photo)
        {
            LibProtocol.Request request = new LibProtocol.Request();
            request.command = LibProtocol.Command.Registration;
            request.data = new LibProtocol.Models.User { Id = Guid.NewGuid(), Name = name, Surname = surname,  DateOfBith = date, Login = login, Password = password, Phpto = photo};
            try
            {
                bf.Serialize(stream, request);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex.Message);
            }
        }
        public void AccExit(Guid id)
        {
            LibProtocol.Request request = new LibProtocol.Request();
            request.command = LibProtocol.Command.Exit;
            request.data = new LibProtocol.Models.User { Id = id};
            try
            {
                bf.Serialize(stream, request);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex.Message);
            }
        }
        public void LoginAccaunt(string log, string pass)
        {
            LibProtocol.Request request = new LibProtocol.Request();
            request.command = LibProtocol.Command.Login;
            request.data = new LibProtocol.Models.User { Login = log, Password = pass };
            try
            {
                bf.Serialize(stream, request);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex.Message);
            }
        }
        public void waitResponse(Action<LibProtocol.Response> onOk)
        {
            LibProtocol.Response response = (LibProtocol.Response)bf.Deserialize(stream);
            if (response.succces)
            {
                onOk(response);
            }
            else
            {
                onError?.Invoke(response.StatusTxt);
            }
        }
    }
}
