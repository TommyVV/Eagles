using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.ScoreSetUp
{
    public class ScoreSetUpRequset:RequestBase
    {
        public OperationType OperationType { get; set; }
    }
}
