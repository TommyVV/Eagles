import { actionType } from "../constants/ActionType/pc";

const info = {};

const scoreReducer = (state = info, action) => {
  switch (action.type) {
    case actionType.memberType.SAVE_INFO:
      return {
        ...state,
        ...action.payload
      };
    case actionType.memberType.CLEAR_INFO:
      return { ...info };
    default:
      return state;
  }
};

export default scoreReducer;
