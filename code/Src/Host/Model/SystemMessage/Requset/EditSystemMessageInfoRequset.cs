using Eagles.Application.Model.SystemMessage.Model;

namespace Eagles.Application.Model.SystemMessage.Requset
{
    public class EditSystemMessageInfoRequset:RequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        public SystemMessageInfoDetails Info { get; set; }


    }
}
