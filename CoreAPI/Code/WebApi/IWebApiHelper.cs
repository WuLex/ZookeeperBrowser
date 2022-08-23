using AllDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using yrjw.ORM.Chimp.Result;

namespace CoreAPI.Code.WebApi
{
    [JsonReturn]
    public interface IWebApiHelper: IHttpApi
    {
        [HttpGet("api/StudentInfo")]
        ITask<StudentInfoListResultModel> GetStudentInfoListAsync();
    }

    /// <summary>
    /// 接口返回模型
    /// </summary>
    public class StudentInfoListResultModel: IResultModel
    {
        public bool Success { get; set; }

        public string Msg { get; set; }

        public int Code { get; set; }
        public string FailedId { get; set; }

        public List<StudentInfoDTO> Data { get; set; }

        public int Status => throw new NotImplementedException();

        public List<Errors> Errors => throw new NotImplementedException();
    }
}
