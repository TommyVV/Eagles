import { PcActionType } from "../../constants/ActionType/pc";

export const saveUserInfo = payload => {
  return {
    payload,
    type: PcActionType.userType.SAVE_USER_INFO
  }
}