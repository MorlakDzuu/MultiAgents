namespace IDZ3.MessageContracts.Visitor
{
    public class VisitorActualOrderWaitTimeMessage
    {
        public double WaitTime { get; set; }

        public VisitorActualOrderWaitTimeMessage( double waitTime )
        {
            WaitTime = waitTime;
        }
    }
}
