using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class ActivityDetailQueryResponse : ResponseBase
    {
        public string ActivityId { get; set; }

        public string ActivityName { get; set; }

        public string ActivityImageUrl { get; set; }

        public string ActivityContext { get; set; }

        public string ActivityAttachmentName { get; set; }

        public string ActivityAttachmentDownloadUrl { get; set; }

    }
}
