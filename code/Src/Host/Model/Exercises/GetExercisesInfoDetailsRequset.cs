﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Exercises
{
    public class GetExercisesInfoDetailsRequset
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }


        /// <summary>
        /// 试卷id
        /// </summary>
        public string ExercisesId { get; set; }
    }
}