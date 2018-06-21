import { mobileActionType } from '../../constants/ActionType/mobile';

export function saveShareStatistics(data) {
    console.log(data);
    return {
        type: mobileActionType.SHARE.SAVE_SHARE_STATISTICS,
        statistics: data
    };
}
export const saveShareInfo = payload => {
  return {
    payload,
    type: mobileActionType.shareType.SAVE_SHARE_INFO
  }
}
export const clearShareInfo = payload => {
  return {
    payload,
    type: mobileActionType.shareType.CLEAR_SHARE_INFO
  }
}
export const saveShareMap = payload => {
  return {
    payload,
    type: mobileActionType.shareType.SAVE_SHARE_MAP
  }
}