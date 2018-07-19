using System.Collections.Generic;
using Eagles.Application.Model.Publicity.Model;

namespace Eagles.Application.Model.Publicity.Response
{
    public class GetPublicAritcleResponse
    {
        /// <summary>
        /// 文章列表
        /// </summary>
        public List<Aritcle> Aritcles { get; set; }
    }
}
