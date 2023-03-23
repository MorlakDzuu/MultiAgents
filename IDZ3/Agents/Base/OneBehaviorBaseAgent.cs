namespace IDZ3.Agents.Base
{
    /// <summary>
    /// Всегда выполняет поведение агента 1 раз
    /// </summary>
    public class OneBehaviorBaseAgent : BaseAgent
    {
        public OneBehaviorBaseAgent( string serviceType, string ownerId ) : base( serviceType, ownerId )
        {
        }

        public new bool Done()
        {
            return true;
        }
    }
}
