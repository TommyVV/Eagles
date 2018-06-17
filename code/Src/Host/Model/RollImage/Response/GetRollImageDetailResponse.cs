using Eagles.Application.Model.RollImage.Model;

namespace Eagles.Application.Model.RollImage.Response
{
    public class GetRollImageDetailsResponse:ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public RollImageInfo Info { get; set; }
    }
}
