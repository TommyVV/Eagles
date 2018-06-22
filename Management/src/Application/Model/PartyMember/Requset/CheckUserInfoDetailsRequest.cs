namespace Eagles.Application.Model.PartyMember.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckUserInfoDetailsRequest : RequestBase
    {/// <summary>
     /// 上传文件 上传成功返回fileId  前端轮训调用 得到返回状态为完成 则可以解析list 结果
     /// </summary>
        public string FileId { get; set; }

    }
}
