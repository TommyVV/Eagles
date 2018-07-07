using System.Collections.Generic;

namespace Eagles.Application.Model.Upload
{
    public class UploadResponse
    {
        /// <summary>
        /// 上传结果
        /// </summary>
        public List<FileUploadResult> FileUploadResults { get; set; }
    }
}
