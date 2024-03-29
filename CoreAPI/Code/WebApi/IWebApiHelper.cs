﻿using AllDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using AllModel.MyOrm.Result;

namespace CoreAPI.Code.WebApi
{
    [JsonReturn]
    public interface IWebApiHelper : IHttpApi
    {
        [HttpGet("api/EmployeeInfo")]
        ITask<EmployeeInfoListResultModel> GetEmployeeInfoListAsync();
    }

    /// <summary>
    /// 接口返回模型
    /// </summary>
    public class EmployeeInfoListResultModel : IResultModel
    {
        public bool Success { get; set; }

        public string Msg { get; set; }

        public int Code { get; set; }
        public string FailedId { get; set; }


        public int Status => throw new NotImplementedException();

        public List<Errors> Errors => throw new NotImplementedException();
    }
}