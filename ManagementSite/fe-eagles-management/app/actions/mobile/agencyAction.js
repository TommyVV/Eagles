import { mobileActionType } from "../../constants/ActionType/mobile";

export const saveAgencyList = payload => {
  return {
    payload,
    type: mobileActionType.agencyType.GET_AGENCY_LIST
  }
}
export const saveAgencyInfo = payload => {
  return {
    payload,
    type: mobileActionType.agencyType.SAVE_AGENCY_INFO
  }
}
export const clearAgencyInfo = payload => {
  return {
    payload,
    type: mobileActionType.agencyType.CLEAR_AGENCY_INFO
  }
}
export const saveAgencyMap = payload => {
  return {
    payload,
    type: mobileActionType.agencyType.SAVE_AGENCY_MAP
  }
}

export const saveAgencyTab = payload => {
  return {
    payload,
    type: mobileActionType.agencyType.SAVE_AGENCY_TAB
  }
}
