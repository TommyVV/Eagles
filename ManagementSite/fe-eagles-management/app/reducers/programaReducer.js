import { actionType } from "../constants/ActionType/pc";

const info = {
  
};

const goodsReducer = (state = info, action) => {
  switch (action.type) {
    case actionType.programaType.SAVE_INFO:
      return {
        ...state,
        ...action.payload
      };
    case actionType.programaType.CLEAR_INFO:
      return { ...info };
    default:
      return state;
  }
};

export default goodsReducer;
