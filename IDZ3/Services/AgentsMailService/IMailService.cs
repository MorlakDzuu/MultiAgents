using IDZ3.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ3.Services.AgentsMailService
{
    public interface IMailService
    {
        void RegisterAgentMailbox( string agentId );
        void RemoveAgentMailbox( string agentId );
        Message<T> GetNextMessage<T>( string agentId );
        void SendMessageToAgent<T>( T messageContent, string agentFromId, string agentId );
    }
}
