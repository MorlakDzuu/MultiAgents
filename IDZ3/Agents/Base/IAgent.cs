using IDZ3.Message;

namespace IDZ3.Agents.Base
{
    public interface IAgent
    {
        /// <summary>
        /// Уничтожить агента
        /// </summary>
        void SelfDestruct();

        /// <summary>
        /// Приостановить выпонения агента
        /// </summary>
        void StopWorking();

        /// <summary>
        /// Отправить сообщение другому агенту
        /// </summary>
        void SendMessageToAgent<T>( T message, string agentId );

        /// <summary>
        /// Получить сообщение от другого агента
        /// </summary>
        Message<T> GetMessage<T>();

        /// <summary>
        /// Выполнить действие
        /// </summary>
        void Action();

        /// <summary>
        /// Обозначает выполнено ли действие
        /// </summary>
        bool Done();

        /// <summary>
        /// Получить id агента
        /// </summary>
        string GetId();
    }
}
