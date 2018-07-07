using Eagles.Application.Model.Upload;
using Eagles.Base;

namespace Eagles.Interface.Core.FileUpload
{
    public interface IFileUploadHandler:IInterfaceBase
    {
        UploadResponse Process();
    }
}
