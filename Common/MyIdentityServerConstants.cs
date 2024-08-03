namespace Common
{
    public static class MyIdentityServerConstants
    {
        public static string MyAuthenticationScheme => "oidc";
        public static class MyStandardScopes
        {
            public static string PersianProfile => "PersianProfile";
        }
    }

    public static class MyJwtClaimTypes
    {
        public static string Nationalcode => "nationalcode";
        public static string PersianBirthdate => "persianbirthdate";
    }
}
