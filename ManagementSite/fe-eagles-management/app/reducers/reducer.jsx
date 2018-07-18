import { combineReducers } from "redux";

// pc端
import appReducer from "./appReducer";
import userReducer from "./userReducer";
import goodsReducer from "./goodsReducer";
import orgReducer from "./orgReducer";
import newsReducer from "./newsReducer";
import scoreReducer from "./scoreReducer";
import questionReducer from "./questionReducer";
import programaReducer from "./programaReducer";
import exReducer from "./exReducer";
import imageReducer from "./imageReducer";
import memberReducer from "./memberReducer";

const reducer = combineReducers({
  appReducer,
  userReducer,
  questionReducer,
  exReducer,
  programaReducer,
  goodsReducer,
  orgReducer,
  newsReducer,
  scoreReducer,
  imageReducer,
  memberReducer
});

export default reducer;
