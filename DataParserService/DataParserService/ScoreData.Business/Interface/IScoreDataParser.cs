using DataaParserService.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreData.Business.Interface
{
    public interface IScoreDataParser
    {
        List<SubjecScoreData> GetSubjectScoreData(int[] subjectIds);
    }
}
