import { actionType } from "../constants/ActionType/pc";

const info = {
  Keyword:[]
};

const scoreReducer = (state = info, action) => {
  switch (action.type) {
    case actionType.scoreType.SAVE_INFO:
      return {
        ...state,
        ...action.payload
      };
    case actionType.scoreType.CLEAR_INFO:
      return { ...info };
    default:
      return state;
  }
};

export default scoreReducer;
