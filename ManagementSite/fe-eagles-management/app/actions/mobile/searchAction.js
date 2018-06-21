import { mobileActionType } from "../../constants/ActionType/mobile";

export const clearSearchInfo = payload => {
  return {
    payload,
    type: mobileActionType.searchType.CLEAR_SEARCH_INFO
  }
}
export const saveSearchInfo = payload => {
  return {
    payload,
    type: mobileActionType.searchType.SAVE_SEARCH_INFO
  }
}

