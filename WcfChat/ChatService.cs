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

            SendMessage($": {user.Name} connected to the chat!", 0);

            users.Add(user);

            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                users.Remove(user);
                SendMessage($": {user.Name} disconnected from chat!", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach(var item in users) 
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(x => x.Id == id);

                if (user != null)
                {
                    answer += $": {user.Name} ";
                }

                answer += message;

                var callback = item.OperationCtx?.GetCallbackChannel<IChatServiceCallback>();
                callback?.MessageCallback(answer);
            }
        }
    }
}
