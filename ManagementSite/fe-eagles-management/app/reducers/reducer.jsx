import { combineReducers } from "redux";

// pc端
import appReducer from "./appReducer";
import userReducer from "./userReducer";
import goodsReducer from "./goodsReducer";
import orgReducer from "./orgReducer";
import newsReducer from "./newsReducer";

const reducer = combineReducers({
  appReducer,
  userReducer,
  goodsReducer,
  orgReducer,
  newsReducer
});

export default reducer;
