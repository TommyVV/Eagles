/**
 * 返回状态码及信息
 *
 */
export const ErrorCode = {
    /**
     * 成功状态码
     */
    SUCCESS_CODE : 0,

    /**
     * 系统内部异常状态码
     */
    SYSTEM_ERROR : -1,
    /**
     * token相关错误状态码
     */
    TOKEN_EXPIRED : 40001,
    TOKEN_ILLEGAL : 40002,
    TOKEN_IS_EMPTY : 40003,
    REFRESH_TOKEN_ILLEGAL : 40004,
    REFRESH_TOKEN_IS_EMPTY : 40005,
    TOKEN_RESOLVE_EXCEPTION : 40006,
    TOKEN_RESOLVE_USERID_IS_EMPTY : 40007,
    TOKEN_CREATE_FAILED : 40008,
    REFRESH_TOKEN_CREATE_FAILED : 40009,
    /**
     * 参数验证异常状态码
     */
    PARAM_IS_EMPTY : 10001,
    PARAM_ILLEGAL : 10002,
    QTCODE_IS_EMPTY : 10003,
    APPID_IS_ILLEGAL : 10004,
    USER_IS_ILLEGAL : 10005,
    PROJECT_IS_DELETE: 10006,

    /**
     * 数据库操作异常状态码
     */
    SAVE_FAILED : 20001,
    UPDATE_FAILED : 20002,
    DELETE_FAILED : 20003,
    SELECT_FAILED : 20004,
    DATA_PERSISTENCE_EXCEPTION : 20005,
    /**
     * 轻推接口调用异常状态码
     */
    USER_INFO_IS_EMPTY : 30001,
    GET_QT_USER_INFO_FAILED : 30002,
    GET_QT_TOKEN_FAILED : 30003,
    GET_QT_JSAPI_TICKET : 30004,
    QT_TOKEN_DUE : 30005,
    /**
     * 文件操作异常状态码
     */
    FILE_UPLOAD_FAILED : 50001,
    FILE_STREAM_EXCEPTION : 50002,
    FILE_DOWNLOAD_FAILED : 50003,
    DOWNLOAD_PROGRESS_EXCEPTION : 50004,
    FILE_DELETE_EXCEPTION : 50005,
    FILE_EXCEED_SIZE_LIMIT : 50006,
    FILE_COMPRESS_MULTIPLE_EXCEPTION : 50007,
    /**
     * 非法调用接口异常状态码
     */
    ILLEGAL_OPERATION : 60001,


    /**
     * 我的信息
     */
    FOCUS_ALREADY_EXIST : 70000, //已关注
    COLLECTION_ALREADY_EXIST : 70001, //已收藏
    COLLECTION_NOT_EXIST : 70002, //未收藏

    /**
     * 支付宝提示信息
     */
    ORDER_PRODUCTION_FAILED : 80000,//支付订单生产失败
}

const ErrorCodeMessage ={};
/**
     * 系统内部异常状态码
     */
ErrorCodeMessage[ErrorCode.SYSTEM_ERROR] = '系统异常';
/**
     * token相关错误状态码
     */
ErrorCodeMessage[ErrorCode.TOKEN_EXPIRED] = 'TOKEN过期';
ErrorCodeMessage[ErrorCode.TOKEN_ILLEGAL] = 'TOKEN不合法';
ErrorCodeMessage[ErrorCode.TOKEN_IS_EMPTY] = 'TOKEN为空';
ErrorCodeMessage[ErrorCode.REFRESH_TOKEN_ILLEGAL] = 'REFRESH_TOKEN不合法';
ErrorCodeMessage[ErrorCode.REFRESH_TOKEN_IS_EMPTY] = 'REFRESH_TOKEN为空';
ErrorCodeMessage[ErrorCode.TOKEN_RESOLVE_EXCEPTION] = 'TOKEN异常';
ErrorCodeMessage[ErrorCode.TOKEN_RESOLVE_USERID_IS_EMPTY] = 'TOKEN的USERID为空';
ErrorCodeMessage[ErrorCode.TOKEN_CREATE_FAILED] = 'TOKEN创建失败';
ErrorCodeMessage[ErrorCode.REFRESH_TOKEN_CREATE_FAILED] = 'REFRESH_TOKEN创建失败';

 /**
     * 参数验证异常状态码
     */

ErrorCodeMessage[ErrorCode.PARAM_IS_EMPTY] = '参数为空';
ErrorCodeMessage[ErrorCode.PARAM_ILLEGAL] = '参数不合法';
ErrorCodeMessage[ErrorCode.QTCODE_IS_EMPTY] = 'QTCODE为空';
ErrorCodeMessage[ErrorCode.APPID_IS_ILLEGAL] = 'APPID不合法';
ErrorCodeMessage[ErrorCode.USER_IS_ILLEGAL] = '用户不合法';
ErrorCodeMessage[ErrorCode.PROJECT_IS_DELETE] = '项目已被删除';

/**
     * 数据库操作异常状态码
     */
ErrorCodeMessage[ErrorCode.SAVE_FAILED] = '保存失败';
ErrorCodeMessage[ErrorCode.UPDATE_FAILED] = '更新失败';
ErrorCodeMessage[ErrorCode.DELETE_FAILED] = '删除失败';
ErrorCodeMessage[ErrorCode.SELECT_FAILED] = '查询失败';
ErrorCodeMessage[ErrorCode.DATA_PERSISTENCE_EXCEPTION] = '数据异常';

/**
     * 轻推接口调用异常状态码
     */

ErrorCodeMessage[ErrorCode.USER_INFO_IS_EMPTY] = '用户信息为空';
ErrorCodeMessage[ErrorCode.GET_QT_USER_INFO_FAILED] = '获取轻推用户失败';
ErrorCodeMessage[ErrorCode.GET_QT_TOKEN_FAILED] = '获取QT_TOKEN失败';
ErrorCodeMessage[ErrorCode.GET_QT_JSAPI_TICKET] = '获取QT_JSAPI_TICKET失败';
ErrorCodeMessage[ErrorCode.QT_TOKEN_DUE] = 'QT_TOKEN_DUE失败';

 /**
     * 文件操作异常状态码
     */

ErrorCodeMessage[ErrorCode.FILE_UPLOAD_FAILED] = '文件上传失败';
ErrorCodeMessage[ErrorCode.FILE_STREAM_EXCEPTION] = '文件流异常';
ErrorCodeMessage[ErrorCode.FILE_DOWNLOAD_FAILED] = '文件下载失败';
ErrorCodeMessage[ErrorCode.DOWNLOAD_PROGRESS_EXCEPTION] = '下载进度异常';
ErrorCodeMessage[ErrorCode.FILE_DELETE_EXCEPTION] = '当前文件已被别人删除';
ErrorCodeMessage[ErrorCode.FILE_EXCEED_SIZE_LIMIT] = '系统异常';
ErrorCodeMessage[ErrorCode.FILE_COMPRESS_MULTIPLE_EXCEPTION] = '文件压缩异常';

 /**
     * 非法调用接口异常状态码
     */

ErrorCodeMessage[ErrorCode.ILLEGAL_OPERATION] = '非法调用接口';

/**
     * 我的信息
     */
ErrorCodeMessage[ErrorCode.FOCUS_ALREADY_EXIST] = '已关注';
ErrorCodeMessage[ErrorCode.COLLECTION_ALREADY_EXIST] = '已收藏';
ErrorCodeMessage[ErrorCode.COLLECTION_NOT_EXIST] = '未收藏';

/**
     * 支付宝提示信息
     */
ErrorCodeMessage[ErrorCode.ORDER_PRODUCTION_FAILED] = '订单失败';   
 
export { ErrorCodeMessage}