import { combineReducers } from 'redux';

//global
import globalReducer from './mobile/globalReducer';
// pc端
import pcAppReducer from "./PC/appReducer";
import shareReducer from "./PC/shareReducer";
import userReducer from "./PC/userReducer";
import agencyReducer from "./PC/agencyReducer";
import projectReducer from "./PC/projectReducer";

// hci移动端
import appReducer from './mobile/appReducer'; // 应用首页
import mobileIndexReducer from './mobile/indexReducer'; // 应用首页
import mobileDemandReducer from './mobile/demandReducer'; // 需求
import mobileAgencyReducer from './mobile/agencyReducer'; // 咨询机构首页
import mobileShareReducer from './mobile/shareReducer'; // 分享首页
import shareStatisticReducer from './mobile/shareReducer';
import mobileProjectReducer from './mobile/projectReducer';
import mobileSearchReducer from './mobile/searchReducer'; // 搜索
import mobileUserReducer from './mobile/userReducer';  //用户信息


// 运营
import opShareReducer from "./OP/share.reducer";

const reducer = combineReducers({
  pcAppReducer,
  shareReducer,
  userReducer,
  agencyReducer,
  mobileDemandReducer,
  mobileIndexReducer,
  appReducer,
  globalReducer,
  mobileAgencyReducer,
  mobileShareReducer,
  shareStatisticReducer,
  mobileProjectReducer,
  projectReducer,
  mobileSearchReducer,  
  mobileUserReducer,
  opShareReducer
});

export default reducer;
