using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySchool.Models
{
    [Serializable]
    public class Student
    {
        public int StudentNo { get; set; }
        public string LoginPwd { get; set; }
        public string StudentName { get; set; }
        public string Gender { get; set; }
        public int GradeId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime BornDate { get; set; }
        public string Email { get; set; }
        public string IdentityCard { get; set; }
    }
}
