import { combineReducers } from "redux";

// pc端
import appReducer from "./appReducer";
import userReducer from "./userReducer";
import goodsReducer from "./goodsReducer";
import orgReducer from "./orgReducer";
import newsReducer from "./newsReducer";
import questionReducer from "./questionReducer";

const reducer = combineReducers({
  appReducer,
  userReducer,
  questionReducer,
  goodsReducer,
  orgReducer,
  newsReducer
});

export default reducer;
