import {
  PcActionType
} from "../../constants/ActionType/pc";

const appinfo = {
  show: false,
}

const appReducer = (state = appinfo, action) => {
  switch (action.type) {
    case PcActionType.appType.SHOW_MODAL_CONTAINER:
      return {
        ...state,
        show: true
      }
    case PcActionType.appType.HIDE_MODAL_CONTAINER:
      return {
        ...state,
        show: false
      }
    default:
      return state
  }
}

export default appReducer;