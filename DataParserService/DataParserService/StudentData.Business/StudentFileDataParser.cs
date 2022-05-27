using DataaParserService.Common.Models;
using DataaParserService.Common.ResponseType;
using Newtonsoft.Json;
using StudentData.Business.Interfaces;
using System;
using System.IO;

namespace StudentData.Business
{
    public class StudentFileDataParser : IStudentFileDataParser
    {
        public ResponseDetails ParseStudentDataFromFile(Stream stream)
        {
            ResponseDetails response = new ResponseDetails(); 
            JsonSerializer serializer = new JsonSerializer();
            try
            {
                StudentGradeData studentGrade = new StudentGradeData();
                if(stream == null)
                {
                    response.ResponseStatus = ResponseStatus.BadRequest;
                    response.ResponseMessage = "File was not provided";
                    return response;
                }

                using(StreamReader sr = new StreamReader(stream))
                {
                    using(JsonReader reader = new JsonTextReader(sr))
                    {
                        while (reader.Read())
                        {
                            if(reader.TokenType == JsonToken.StartObject)
                            {
                                studentGrade = serializer.Deserialize<StudentGradeData>(reader);
                                response.ResponseStatus = ResponseStatus.Success;
                                response.ResponseData = studentGrade;
                            }
                        }
                    }
                }

                if(studentGrade != null)
                {

                }


            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
                response.ResponseMessage = "Request could not be completed";
                response.ResponseStatus = ResponseStatus.Error;
            }

            return response;
        }
    }
}
