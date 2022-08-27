using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllModel.MyOrm.Result
{
    /// <summary>
    /// 返回成功模型
    /// </summary>
    public class ResultModel<T> : IResultModel<T>
    {
        public bool Success { get; set; }

        public string Msg { get; set; }

        public int Status { get; set; }

        public T Data { get; set; }

        public List<Errors> Errors { get; set; }

        public ResultModel<T> ToSuccess(T data = default, string msg = "success")
        {
            Success = true;
            Msg = msg;
            Status = 200;
            Data = data;
            return this;
        }
    }

    /// <summary>
    /// 返回失败模型
    /// </summary>
    public class FailedResult : IResultModel
    {
        public bool Success { get; set; }

        public string Msg { get; set; }

        public int Status { get; set; }

        public List<Errors> Errors { get; set; }

        public FailedResult ToFailed(string msg = "failed", int code = 200, List<Errors> errors = default)
        {
            Success = false;
            Msg = msg;
            Status = code;
            Errors = errors ?? new List<Errors>();
            return this;
        }
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        public static IResultModel Success<T>(T data = default(T))
        {
            return new ResultModel<T>().ToSuccess(data);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <returns></returns>
        public static IResultModel Success()
        {
            return Success<string>();
        }

        /// <summary>
        /// 失败，返回模型字段错误信息
        /// </summary>
        /// <param name="errors">模型验证失败</param>
        /// <returns></returns>
        public static IResultModel Failed(List<Errors> errors)
        {
            return new FailedResult().ToFailed("failed", 400, errors);
        }

        /// <summary>
        /// 失败，返回模型字段错误信息
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <param name="failedid">错误字段</param>
        /// <returns></returns>
        public static IResultModel Failed(string error, string failedid)
        {
            var errors = new List<Errors>();
            errors.Add(new Errors() { Id = failedid, Msg = error });
            return Failed(errors);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <param name="code">状态码</param>
        /// <returns></returns>
        public static IResultModel Failed(string error, int code)
        {
            return new FailedResult().ToFailed(error, code);
        }

        /// <summary>
        /// 失败，状态码200
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public static IResultModel Failed(string error)
        {
            return Failed(error, 200);
        }

        /// <summary>
        /// 根据布尔值返回结果
        /// </summary>
        /// <param name="success"></param>
        /// <returns></returns>
        public static IResultModel Result(bool success)
        {
            return success ? Success() : Failed("failed");
        }

        /// <summary>
        /// 数据已存在
        /// </summary>
        /// <returns></returns>
        public static IResultModel HasExists => Failed("数据已存在");

        /// <summary>
        /// 数据不存在
        /// </summary>
        public static IResultModel NotExists => Failed("数据不存在");
    }
}
