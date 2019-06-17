using System.ComponentModel;

namespace New.Common
{
    /// <summary>
    /// 返回的错误码
    /// </summary>
    public enum MessageCode
    {
        /// <summary>
        /// 系统错误
        /// </summary>
        [Description("系统错误")]
        Exception_SystemError = 10001,
        /// <summary>
        /// 服务暂停
        /// </summary>
        [Description("服务暂停")]
        Exception_ServiceUnavailable = 10002,
        /// <summary>
        /// 远程服务错误
        /// </summary>
        [Description("远程服务错误")]
        Exception_RemoteServiceError = 10003,
        /// <summary>
        /// IP限制不能请求该资源
        /// </summary>
        [Description("IP限制不能请求该资源")]
        Exception_IpLimit = 10004,
        /// <summary>
        /// 该资源需要appkey拥有授权
        /// </summary>
        [Description("该资源需要appkey拥有授权")]
        Exception_PermissionDenied = 10005,
        /// <summary>
        /// 缺少source (appkey) 参数
        /// </summary>
        [Description("缺少source (appkey) 参数")]
        Exception_AppkeyMissing = 10006,
        /// <summary>
        /// 不支持的MediaType (%s)
        /// </summary>
        [Description("不支持的MediaType (%s)")]
        Exception_UnsupportMediatype = 10007,
        /// <summary>
        /// 参数错误，请参考API文档
        /// </summary>
        [Description("参数错误，请参考API文档")]
        Exception_ParamError = 10008,
        /// <summary>
        /// 任务过多，系统繁忙
        /// </summary>
        [Description("任务过多，系统繁忙")]
        Exception_SystemBusy = 10009,
        /// <summary>
        /// 任务超时
        /// </summary>
        [Description("任务超时")]
        Exception_JobExpired = 10010,
        /// <summary>
        /// RPC错误
        /// </summary>
        [Description("RPC错误")]
        Exception_RpcError = 10011,
        /// <summary>
        /// 非法请求
        /// </summary>
        [Description("非法请求")]
        Exception_IllegalRequest = 10012,
        /// <summary>
        /// 不合法的用户
        /// </summary>
        [Description("不合法的用户")]
        Exception_InvalidUser = 10013,
        /// <summary>
        /// 应用的接口访问权限受限
        /// </summary>
        [Description("应用的接口访问权限受限")]
        Exception_InsufficientPermissions = 10014,
        /// <summary>
        /// 缺失必选参数 (%s)，请参考API文档
        /// </summary>
        [Description("缺失必选参数 (%s)，请参考API文档")]
        Exception_MissRequiredParameter = 10016,
        /// <summary>
        /// 参数值非法，需为 (%s)，实际为 (%s)，请参考API文档
        /// </summary>
        [Description("参数值非法，需为 (%s)，实际为 (%s)，请参考API文档")]
        Exception_ParameterInvalid = 10017,
        /// <summary>
        /// 请求长度超过限制
        /// </summary>
        [Description("请求长度超过限制")]
        Exception_BodyLimit = 10018,
        /// <summary>
        /// 接口不存在
        /// </summary>
        [Description("接口不存在")]
        Exception_ApiNotFound = 10020,
        /// <summary>
        /// 请求的HTTP METHOD不支持，请检查是否选择了正确的POST/GET方式
        /// </summary>
        [Description("请求的HTTP METHOD不支持，请检查是否选择了正确的POST/GET方式")]
        Exception_HttpMethodError = 10021,
        /// <summary>
        /// IP请求频次超过上限
        /// </summary>
        [Description("IP请求频次超过上限")]
        Exception_IpRequestLimit = 10022,
        /// <summary>
        /// 用户请求频次超过上限
        /// </summary>
        [Description("用户请求频次超过上限")]
        Exception_UserRequestLimit = 10023,
        /// <summary>
        /// 用户请求特殊接口 (%s) 频次超过上限
        /// </summary>
        [Description("用户请求特殊接口 (%s) 频次超过上限")]
        Exception_UserSpecialRequestLimit = 10024,
        /// <summary>
        /// IDs参数为空
        /// </summary>
        [Description("IDs参数为空")]
        Exception_IDsNull = 20001,
        /// <summary>
        /// Uid参数为空
        /// </summary>
        [Description("Uid参数为空")]
        Exception_UidNull = 20002,
        /// <summary>
        /// 用户不存在
        /// </summary>
        [Description("用户不存在")]
        Exception_UserNotExists = 20003,
        /// <summary>
        /// 不支持的图片类型，仅仅支持JPG、GIF、PNG
        /// </summary>
        [Description("不支持的图片类型，仅仅支持JPG、GIF、PNG")]
        Exception_UnsupportedImageType = 20005,
        /// <summary>
        /// 图片太大
        /// </summary>
        [Description("图片太大")]
        Exception_ImageSizeLimit = 20006,
        /// <summary>
        /// 请确保使用multpart上传图片
        /// </summary>
        [Description("请确保使用multpart上传图片")]
        Exception_MultipartError = 20007,
        /// <summary>
        /// 内容为空
        /// </summary>
        [Description("内容为空")]
        Exception_ContentIsNull = 20008,
        /// <summary>
        /// IDs参数太长了
        /// </summary>
        [Description("IDs参数太长了")]
        Exception_IDsToMany = 20009,
        //[Description("安全检查参数有误，请再调用一次")]
        //Exception_ParamError = 20010,
        /// <summary>
        /// 账号、IP或应用非法，暂时无法完成此操作
        /// </summary>
        [Description("账号、IP或应用非法，暂时无法完成此操作")]
        Exception_AccountOrIpIllgal = 20011,
        /// <summary>
        /// 此IP地址上的行为异常
        /// </summary>
        [Description("此IP地址上的行为异常")]
        Exception_IpBehave = 20012,
        /// <summary>
        /// 认证失败
        /// </summary>
        [Description("认证失败")]
        Exception_AuthFaild = 20013,
        /// <summary>
        /// 用户名或密码不正确
        /// </summary>
        [Description("用户名或密码不正确")]
        Exception_UserOrPwdError = 20014,
        /// <summary>
        /// 用户名密码认证超过请求限制
        /// </summary>
        [Description("用户名密码认证超过请求限制")]
        Exception_UserOrPwdLimit = 20015,
        /// <summary>
        /// 版本号错误
        /// </summary>
        [Description("版本号错误")]
        Exception_VersionRejected = 20016,
        /// <summary>
        /// 缺少必要的参数
        /// </summary>
        [Description("缺少必要的参数")]
        Exception_ParameterAbsent = 20017,
        /// <summary>
        /// 参数被拒绝
        /// </summary>
        [Description("参数被拒绝")]
        Exception_ParameterRejectedOAuth = 20018,
        /// <summary>
        /// 时间戳不正确
        /// </summary>
        [Description("时间戳不正确")]
        Exception_TimestampTefused = 20019,
        /// <summary>
        /// Token已经被使用
        /// </summary>
        [Description("Token已经被使用")]
        Exception_TokenUsed = 20020,
        /// <summary>
        /// Token已经过期
        /// </summary>
        [Description("Token已经过期")]
        Exception_TokenExpired = 20021,
        /// <summary>
        /// Token不合法
        /// </summary>
        [Description("Token不合法")]
        Exception_TokenRejected = 20022,

        [Description("出差申请单不存在！")]
        TripNotExist = 30001,
        [Description("出差申请单已审批！")]
        TripApproved = 30002,
        [Description("出差申请单已审批，状态：不同意！")]
        TripDisagree = 30003,
        [Description("出差申请单已审批，状态：退回！")]
        TripReturn = 30004,
        [Description("出差申请单已审批，状态：撤回！")]
        TripWithdraw = 30005,
        [Description("出差申请单非待审批！")]
        TripNoApprovaling = 30006,
        [Description("出差申请单状态0：退回；1：通过！")]
        StatusError = 30007,
        [Description("出差申请单同步状态失败！")]
        UpdateTripStatuFail = 30008,
        /// <summary>
        /// 其他错误
        /// </summary>
        [Description("其他错误")]
        Exception_Other = 99999
    }
}
