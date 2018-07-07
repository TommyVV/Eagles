/**
 * 应用首页
 */

//应用
import * as actionType from '../../constants/ActionType/mobile';

const initialState = {
    appList: [],
    userInfo: {}
}

export default function saveAppInfo(state = initialState, action) {
    switch (action.type) {
        case actionType.mobileActionType.appType.APP_LIST:
            return {
                ...state,
                appList: action.payload
            };
        case actionType.mobileActionType.userType.USER_INFO:
            return {
                ...state,
                userInfo: action.payload
            };
        default:
            return state;
    }
};

