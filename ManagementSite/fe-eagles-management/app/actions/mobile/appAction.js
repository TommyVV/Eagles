import { mobileActionType } from "../../constants/ActionType/mobile";

/**
 * 登陆后保存应用列表
 * @param {*} payload 
 */
export const saveAppList = payload => {
  return {
    payload,
    type: mobileActionType.appType.APP_LIST
  }
}
/**
 * 登录后，保存用户信息
 * @param {*} payload 
 */
export const saveUserInfo = payload => {
  return {
    payload,
    type: mobileActionType.userType.USER_INFO
  }
}

