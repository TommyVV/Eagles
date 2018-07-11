import { combineReducers } from "redux";

// pc端
import appReducer from "./appReducer";
import userReducer from "./userReducer";
import goodsReducer from "./goodsReducer";
import orgReducer from "./orgReducer";
import newsReducer from "./newsReducer";
import questionReducer from "./questionReducer";
import programaReducer from "./programaReducer";

const reducer = combineReducers({
  appReducer,
  userReducer,
  questionReducer,
  programaReducer,
  goodsReducer,
  orgReducer,
  newsReducer
});

export default reducer;
