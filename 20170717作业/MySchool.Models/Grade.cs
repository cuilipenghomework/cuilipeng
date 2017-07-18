using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchool.Models
{
    [Serializable]
    public class Grade
    {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
    }
}
