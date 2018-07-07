import { actionType } from "../constants/ActionType/pc";

const newsInfo = {
  
};

const newsReducer = (state = newsInfo, action) => {
  switch (action.type) {
    case actionType.newsType.SAVE_NEWS_INFO:
      return {
        ...state,
        ...action.payload
      };
    case actionType.newsType.CLEAR_NEWS_INFO:
      return { ...newsInfo };
    default:
      return state;
  }
};

export default newsReducer;
