namespace CityTransport
{
    internal class UserData
    {
        public enum UserType
        {
            Unknown, Operator, Admin, SuperAdmin
        };

        public static UserType userType = UserType.Unknown;

        public static DB_Objects.Account Account = null;
        
    }
}
