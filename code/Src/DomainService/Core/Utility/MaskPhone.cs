namespace Eagles.DomainService.Core.Utility
{
    public static class UtilityHelper
    {
        public static string MaskPhone(this string phone)
        {
            if (phone.Length > 6)
            {
                return phone.Substring(0, 3) + "****" + phone.Substring(phone.Length-3, 3);
            }

            return phone;
        }
    }
}
