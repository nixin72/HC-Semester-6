using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAC.RACModels
{
    public class SelfEvaluationModel
    {
        public int competencyID { get; set; }
        public int elementID { get; set; }
        public SelfEvaluationAnswer answer { get; set; }
    }
}