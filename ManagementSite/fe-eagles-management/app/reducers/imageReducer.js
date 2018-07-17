import { actionType } from "../constants/ActionType/pc";

const info = {
  
};

const iamgeReducer = (state = info, action) => {
  switch (action.type) {
    case actionType.imageType.SAVE_INFO:
      return {
        ...state,
        ...action.payload
      };
    case actionType.imageType.CLEAR_INFO:
      return { ...info };
    default:
      return state;
  }
};

export default iamgeReducer;
