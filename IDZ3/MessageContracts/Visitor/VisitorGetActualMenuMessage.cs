namespace IDZ3.MessageContracts.Visitor
{
    public class VisitorGetActualMenuMessage
    {
        public double TimeLimit { get; set; }

        public VisitorGetActualMenuMessage( double timeLimit )
        {
            TimeLimit = timeLimit;
        }
    }
}
