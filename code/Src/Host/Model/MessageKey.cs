namespace Eagles.Application.Model
{
    public  class MessageKey
    {
        public static readonly string Success = "请求成功";

        public static readonly string NoData = "查询结果为空";

        public static readonly string InvalidToken = "token校验失败";

        public static readonly string ParametersIsEmpty = "参数不允许为空";

        public static readonly string InvalidParameter = "非法参数";

        public static readonly string UserNotExists = "用户不存在";

        public static readonly string ActivityNotExists = "活动不存在";

        public static readonly string InvalidActivityUser = "不符合规则的活动用户";

        public static readonly string ProductNotExists = "产品不存在";

        public static readonly string LimitedCount = "超出购买限制";

        public static readonly string NoInventory = "库存不足";

        public static readonly string InsufficientScore = "积分不足";

        public static readonly string UpdateScoreFail = "更新用户积分失败";

        public static readonly string UserNameOrPasswordError = "用户名密码错误";

        public static readonly string LoginFail = "登录失败";

        public static readonly string ExistsPhone = "手机号已存在";

        public static readonly string InvalidCode = "校验码错误";

        public static readonly string RepeatJoin = "重复参与活动";

        public static readonly string SystemError = "系统错误";
    }
}
