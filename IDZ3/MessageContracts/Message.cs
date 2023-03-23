namespace IDZ3.MessagesContracts
{
    public class Message<T>
    {
        public string AgentFromId { get; set; }
        public T MessageContent { get; set; }

        public Message( string agentFromId, T messageContent )
        {
            AgentFromId = agentFromId;
            MessageContent = messageContent;
        }
    }
}
