import { actionType } from "../constants/ActionType/pc";

const info = {
  
};

const activityReducer = (state = info, action) => {
  switch (action.type) {
    case actionType.activityType.SAVE_INFO:
      return {
        ...state,
        ...action.payload
      };
    case actionType.activityType.CLEAR_INFO:
      return { ...info };
    default:
      return state;
  }
};

export default activityReducer;
