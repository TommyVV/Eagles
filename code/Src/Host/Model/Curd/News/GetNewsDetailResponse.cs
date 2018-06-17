using Eagles.Application.Model.News.Model;

namespace Eagles.Application.Model.Curd.News
{
    public class GetNewsDetailResponse:ResponseBase
    {
        public NewsDetail NewsDetail { get; set; }
    }
}
