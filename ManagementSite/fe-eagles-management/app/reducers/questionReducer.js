import { actionType } from "../constants/ActionType/pc";

const info = {
  ExercisesType: 0
};

const questionReducer = (state = info, action) => {
  switch (action.type) {
    case actionType.questionType.SAVE_INFO:
      return {
        ...state,
        ...action.payload
      };
    case actionType.questionType.CLEAR_INFO:
      return { ...info };
    default:
      return state;
  }
};

export default questionReducer;
