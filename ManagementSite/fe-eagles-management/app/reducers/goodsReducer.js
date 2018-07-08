import { actionType } from "../constants/ActionType/pc";

const info = {
  
};

const goodsReducer = (state = info, action) => {
  switch (action.type) {
    case actionType.goodsType.SAVE_INFO:
      return {
        ...state,
        ...action.payload
      };
    case actionType.goodsType.CLEAR_INFO:
      return { ...info };
    default:
      return state;
  }
};

export default goodsReducer;
