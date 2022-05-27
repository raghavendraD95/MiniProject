using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataaParserService.Common.Models
{
    public class StudentGradeData
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public List<Grade> Grades { get; set; }
    }
}
