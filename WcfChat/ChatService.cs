using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single )]
    public class ChatService : IChatService
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                Id = nextId++,
                Name = name,
                OperationCtx = OperationContext.Current
            };

            nextId++;

            SendMessage($"{user.Name} connected to the chat!");

            users.Add(user);

            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                users.Remove(user);
                SendMessage($"{user.Name} disconnected from chat!");
            }
        }

        public void SendMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
