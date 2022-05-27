using CsvHelper;
using CsvHelper.Configuration;
using DataaParserService.Common.Models;
using ScoreData.Business.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreData.Business
{
    public class ScoreDataParser : IScoreDataParser
    {
        public List<SubjecScoreData> GetSubjectScoreData(int[] subjectIds)
        {
            List<SubjecScoreData> scores;
            try
            {
                
                var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    HasHeaderRecord = false
                };
                using var streamer = File.OpenText("DataFolder/SubjectScoreData.csv");
                using var csvReader = new CsvReader(streamer, csvConfig);

                scores = csvReader.GetRecords<SubjecScoreData>()
                    .Where(x=>subjectIds.Contains(x.SubjectId)).ToList();
                
            }
            catch (Exception ex)
            {

                throw;
            }
            

            return scores;
        }
    }
}
