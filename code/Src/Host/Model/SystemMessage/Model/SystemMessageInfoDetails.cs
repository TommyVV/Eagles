using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.SystemMessage.Model
{
    public class SystemMessageInfoDetails : SystemMessageInfo
    {
        public Status Status { get; set; }

        public MessageStatus MessageStatus { get; set; }
    }
}