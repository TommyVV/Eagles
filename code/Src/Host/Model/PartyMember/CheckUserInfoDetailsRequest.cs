using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.PartyMember
{
    public class CheckUserInfoDetailsRequest
    {/// <summary>
     /// 上传文件 上传成功返回fileId  前端轮训调用 得到返回状态为完成 则可以解析list 结果
     /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
