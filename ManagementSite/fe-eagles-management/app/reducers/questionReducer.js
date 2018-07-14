import { actionType } from "../constants/ActionType/pc";

const question = {
  Info: {
    ExercisesId: "",
    ExercisesName: "",
    ExercisesType: "0",
    source: "",
    HtmlDescription: "",
    IsScoreAward: "0",
    PassAwardScore: "",
    SubjectScore: "",
    PassScore: "",
    HasLimitedTime: "0",
    LimitedTime: ""
  },
  Subject: []
};

const questionReducer = (state = question, action) => {
  switch (action.type) {
    case actionType.questionType.SAVE_INFO:
      return {
        ...state,
        ...action.payload
      };
    case actionType.questionType.CLEAR_INFO:
      return { ...question };
    default:
      return state;
  }
};

export default questionReducer;
