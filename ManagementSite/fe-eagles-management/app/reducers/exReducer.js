import { actionType } from "../constants/ActionType/pc";

const exercise = {
  Info: {
    // QuestionId: "",
    // Question: "",
    Multiple: "0",
    // MultipleCount: "",
    // Answer: "",
    OptionList: []
  },
};

const exReducer = (state = exercise, action) => {
  switch (action.type) {
    case actionType.exType.SAVE_INFO:
      return {
        ...state,
        ...action.payload
      };
    case actionType.exType.CLEAR_INFO:
      return { ...exercise };
    default:
      return state;
  }
};

export default exReducer;
