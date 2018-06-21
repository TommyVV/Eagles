import {
  OpActionType
} from "../../constants/ActionType/op";

const shareinfo = {
  list : []
}

const opShareReducer = (state = shareinfo, action) => {
  switch (action.type) {
    case OpActionType.shareType.SAVE_OP_SHARE_LIST:
      return {
        ...state,
        ...action.payload
      }
    default:
      return state
  }
}

export default opShareReducer;