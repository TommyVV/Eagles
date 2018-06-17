using Eagles.Application.Model.News.Model;

namespace Eagles.Application.Model.Curd.News
{
    public class GetNewsDetailRequest:RequestBase
    {
      public int NewsId { get; set; }
    }
}
