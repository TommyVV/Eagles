import { mobileActionType } from "../../constants/ActionType/mobile";

export const saveAdvertisementList = payload => {
  return {
    payload,
    type: mobileActionType.indexType.ADVERTISEMENT_LIST
  }
}

