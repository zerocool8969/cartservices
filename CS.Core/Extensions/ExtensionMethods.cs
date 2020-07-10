namespace CS.Core.Extensions
{
    public static class ExtensionMethods
    {
        public static string NullPassword(this string password)
        {
            password = "#########";
            return password;
        }
    }
}
