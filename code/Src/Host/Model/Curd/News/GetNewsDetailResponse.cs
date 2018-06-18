using Eagles.Application.Model.News.Model;

namespace Eagles.Application.Model.Curd.News
{
    public class GetNewsDetailResponse:ResponseBase
    {
        public NewDetail NewsDetail { get; set; }
    }
}
