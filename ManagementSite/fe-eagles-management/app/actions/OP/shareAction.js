import { OpActionType } from "../../constants/ActionType/op";

export const saveShareList = payload => {
  return {
    payload,
    type: OpActionType.shareType.SAVE_OP_SHARE_LIST
  }
}

// export const clearInfo = payload => {
//   return {
//     type: OpActionType.shareType.CLEAR_SHARE_INFO
//   }
// }