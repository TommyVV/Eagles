import { mobileActionType } from "../../constants/ActionType/mobile";

export const saveDemandList = payload => {
  return {
    payload,
    type: mobileActionType.demandType.GET_DEMAND_LIST
  }
}

export const saveDemandInfo = payload => {
  return {
    payload,
    type: mobileActionType.demandType.SAVE_DEMAND_INFO
  }
}
export const clearDemandInfo = payload => {
  return {
    payload,
    type: mobileActionType.demandType.CLEAR_DEMAND_INFO
  }
}
export const saveDemandMap = payload => {
  return {
    payload,
    type: mobileActionType.demandType.SAVE_DEMAND_MAP
  }
}

