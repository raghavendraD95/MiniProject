using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataaParserService.Common.Models
{
    public class Grade
    {
        public int GradeNumber { get; set; }
        public List<SubjectData> Subjects { get; set; }
    }
}
