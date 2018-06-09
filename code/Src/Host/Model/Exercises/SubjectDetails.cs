using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Exercises
{
    public class SubjectDetails : Subject
    {
        public List<Option> OptionList { get; set; }
    }
}
