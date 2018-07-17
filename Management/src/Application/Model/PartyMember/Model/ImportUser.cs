namespace Eagles.Application.Model.PartyMember.Model
{
    public class ImportUser
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public int UserName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 党员类型 0:党员; 1:预备党员
        /// </summary>
        public int MemberType { get; set; }

        /// <summary>
        /// 导入是否成功（导入时不传递，导入后结果返回）
        /// </summary>
        public bool ImportStatus { get; set; }

        /// <summary>
        /// 导入失败原因（导入时不传递，导入后结果返回）
        /// </summary>
        public string ErrorReason { get; set; }
    }
}
