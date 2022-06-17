namespace DucVuSport.Common
{
    public static class Constans
    {
       public static class Session
        {
            public const string LOGIN_SESSION = "user";
            public const string CART_SESSION = "cart";
            public const string ADMIN_SESSION = "admin";
        }
        public static class Status
        {
            public const string WaitingConfirm = "Chờ xác nhận";
            public const string Cancel = "Hủy";
            public const string Approve = "Đã xác nhận";
            public const string Delivering = "Đang giao";
            public const string Success = "Giao thành công";
        }
        public static class Role
        {
            public const string ADMIN = "admin";
            public const string EMPLOYEE = "employee";
            public const string CUSTOMER = "customer";
        }
    }
}