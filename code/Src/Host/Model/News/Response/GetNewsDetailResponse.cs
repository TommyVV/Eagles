using Eagles.Application.Model.News.Model;

namespace Eagles.Application.Model.News.Response
{
   /// <summary>
   /// 
   /// </summary>
   public  class GetNewsDetailResponse : ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public NewsDetail Info { get; set; }
    }
}
