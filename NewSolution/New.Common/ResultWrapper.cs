using New.Common.Util;
using System;
using System.Threading.Tasks;

namespace New.Common
{
    public class ResultWrapper
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        public static ResultWrapper Fail(int resultCode, string msg)
        {
            return new ResultWrapper { Code = resultCode, Message = msg, Success = false };
        }

        public static ResultWrapper Fail(MessageCode messageCode)
        {
            return Fail((int)messageCode, EnumUtil.GetDescription(messageCode));
        }

        public static Task<ResultWrapper> FailAsync(MessageCode messageCode)
        {
            return Task.FromResult<ResultWrapper>(Fail(messageCode));
        }

        public static ResultWrapper Succeed(string msg = "成功")
        {
            return new ResultWrapper() { Code = 200, Success = true, Message = msg };
        }

        public static Task<ResultWrapper> SucceedAsync(string msg = "成功")
        {
            return Task.FromResult<ResultWrapper>(Succeed(msg));
        }


        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ResultWrapper<T> Fail<T>(int resultCode, string msg)
        {
            return new ResultWrapper<T>(resultCode, msg);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        public static ResultWrapper<T> Fail<T>(MessageCode messageCode)
        {
            return Fail<T>((int)messageCode, EnumUtil.GetDescription(messageCode));
        }


        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ResultWrapper<T> Succeed<T>(T data)
        {
            return Succeed<T>(data, string.Empty);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ResultWrapper<T> Succeed<T>(T data, string msg)
        {
            return new ResultWrapper<T>(data, msg);
        }



    }
    public sealed class ResultWrapper<T> : ResultWrapper
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        public ResultWrapper()
        {
        }

        public ResultWrapper(int resultCode, string msg)
        {
            if (Code == 200)
                throw new Exception("接口返回值错误的情况下Code不能为200");
            if (string.IsNullOrEmpty(Message))
                throw new Exception("接口返回值错误的情况下Message不能为空");
            Code = resultCode;
            Success = false;
            Message = msg;
        }
        
        public ResultWrapper(T result)
        {
            if (Data == null)
                throw new Exception("接口正常的情况下Data不能为空");

            Code = 200;
            Success = true;
            Message = string.Empty;
            Data = result;
        }

        public ResultWrapper(T result, string msg)
        {
            if (Data == null)
                throw new Exception("接口正常的情况下Data不能为空");

            Code = 200;
            Success = true;
            Message = msg;
            Data = result;
        }
        


    }
}
