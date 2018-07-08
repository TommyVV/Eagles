using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Eagles.Application.Model.Upload;
using Eagles.Interface.Configuration;
using Eagles.Interface.Core.FileUpload;

namespace Eagles.DomainService.Core.FileUpload
{
    public class FileUploadHandler: IFileUploadHandler
    {
        private readonly IEaglesConfig configuration;

        public FileUploadHandler(IEaglesConfig configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Process
        /// </summary>
        /// <returns></returns>
        public UploadResponse Process()
        {
            var result= UploadWholeFile(HttpContext.Current);
            return new UploadResponse()
            {
                FileUploadResults = result
            };
        }

        private List<FileUploadResult> UploadWholeFile(HttpContext context)
        {
            if (context.Request.Files.Count<=0)
            {
                return null;
            }
            var root = configuration.EaglesConfiguration.FilePath;
            var url = configuration.EaglesConfiguration.ImageBaseUrl;
            var result=new List<FileUploadResult>();
            for (var i = 0; i < context.Request.Files.Count; i++)
            {
                
                var fileId = Guid.NewGuid().ToString("N");
                var file = context.Request.Files[i];
                var oldFileName = Path.GetFileName(file.FileName);
                var extendName = Path.GetExtension(file.FileName);
                var newFileName = fileId + extendName;
                file.SaveAs(root + newFileName);
                result.Add(new FileUploadResult()
                {
                    FileName = oldFileName,
                    FileSize = file.ContentLength,
                    FileType = extendName,
                    FileUrl = url + newFileName,
                    FileId = fileId
                });
            }
            return result;
        }
    }
}
