namespace Eagles.Application.Model
{
    public class MessageCode
    {
        public static readonly string Success = "00";

        public static readonly string NoData = "10";

        public static readonly string InvalidToken = "11";

        public static readonly string ParametersIsEmpty = "12";

        public static readonly string InvalidParameter = "13";

        public static readonly string UserNotExists = "20";
        
        public static readonly string ActivityNotExists = "30";

        public static readonly string ActivityStatusError = "32";

        public static readonly string JoinActivityExist = "33";

        public static readonly string JoinActivityMax = "34";

        public static readonly string InvalidActivityUser = "31";

        public static readonly string ProductNotExists = "40";

        public static readonly string LimitedCount = "41";

        public static readonly string NoInventory = "42";

        public static readonly string InsufficientScore = "43";

        public static readonly string UpdateScoreFail = "44";

        public static readonly string UserNameOrPasswordError = "50";

        public static readonly string LoginFail = "51";

        public static readonly string ExistsPhone = "52";

        public static readonly string InvalidCode = "53";

        public static readonly string RepeatJoin = "54";

        public static readonly string TestNotExists = "55";

        public static readonly string ExpireValidateCode = "56";

        public static readonly string TaskNotExists = "61";

        public static readonly string TaskStatusError = "62";

        public static readonly string SystemError = "96";
    }
}
