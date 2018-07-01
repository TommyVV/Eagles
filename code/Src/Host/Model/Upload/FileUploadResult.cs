namespace Eagles.Application.Model.Upload
{
    public class FileUploadResult
    {
        /// <summary>
        /// 文件唯一id
        /// </summary>
        public string FileId { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

       /// <summary>
       /// 文件类型
       /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// 访问路径
        /// </summary>
        public string FileUrl { get; set; }
    }
}
