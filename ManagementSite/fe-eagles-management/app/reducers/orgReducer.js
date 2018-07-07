import { actionType } from "../constants/ActionType/pc";

const orgInfo = {};

const orgReducer = (state = orgInfo, action) => {
  switch (action.type) {
    case actionType.orgType.SAVE_ORG_INFO:
      return {
        ...state,
        ...action.payload
      };
    case actionType.orgType.CLEAR_ORG_INFO:
      return { ...orgInfo };
    default:
      return state;
  }
};

export default orgReducer;
