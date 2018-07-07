import { PcActionType } from "../../constants/ActionType/pc";

export const showModal = payload => {
  return {
    type: PcActionType.appType.SHOW_MODAL_CONTAINER
  }
}


export const hideModal = payload => {
  return {
    type: PcActionType.appType.HIDE_MODAL_CONTAINER
  }
}