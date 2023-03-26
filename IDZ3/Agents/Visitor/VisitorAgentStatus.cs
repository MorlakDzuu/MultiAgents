namespace IDZ3.Agents.Visitor
{
    public enum VisitorAgentStatus
    {
        NONE = 0,
        INIT = 1,
        ASK_ADMIN_TO_CREATE_ORDER = 2,
        CANCELL_ORDER = 3,
        GET_ORDER_WAITING_TIME = 4
    }
}
