using Eagles.Application.Model.News.Model;

namespace Eagles.Application.Model.News.Response
{
   /// <summary>
   /// 
   /// </summary>
   public  class GetNewDetailResponse : ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public NewDetail Info { get; set; }
    }
}
