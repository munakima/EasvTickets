namespace BLL.WebApi.Gateway.Core
{
    internal static class ApiResources
    {
        public static string CreateEvent { get; private set; } = "api/Event";
        public static string GetFutureEvents { get; private set; } = "api/Event/GetFutureEvents";
        public static string GetEvent { get; private set; } = "api/Event/{id}";
        public static string CanStudentBuyStudentTicket { get; set; } = "api/Event/CanStudentBuyStudentTicket";
    }
}
