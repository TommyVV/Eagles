import { PcActionType } from "../../constants/ActionType/pc";

export const removeFileUrl = payload => {
  return {
    payload,
    type: PcActionType.shareType.DELETE_SHARE_FILE
  }
}

export const saveFileUrl = payload => {
  return {
    payload,
    type: PcActionType.shareType.SAVE_SHARE_FILE_URL
  }
}

export const saveShareInfo = payload => {
  return {
    payload,
    type: PcActionType.shareType.SAVE_SHARE_INFO
  }
}

export const changeIdentity = payload => {
  return {
    payload,
    type: PcActionType.shareType.CHANGE_IDENTITY
  }
}

export const clearInfo = payload => {
  return {
    type: PcActionType.shareType.CLEAR_SHARE_INFO
  }
}