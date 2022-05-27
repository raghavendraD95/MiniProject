using DataaParserService.Common.ResponseType;
using System.IO;

namespace StudentData.Business.Interfaces
{
    public interface IStudentFileDataParser
    {
        public ResponseDetails ParseStudentDataFromFile(Stream stream);
    }
}
