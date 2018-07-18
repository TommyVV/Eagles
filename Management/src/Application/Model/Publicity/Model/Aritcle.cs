namespace Eagles.Application.Model.Publicity.Model
{
    public class Aritcle
    {
        /// <summary>
        /// 文章id
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 文章类型 00:文章 01:心得体会 02:会议
        /// </summary>
        //todo 有时间换成枚举
        public int NewsType { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}
