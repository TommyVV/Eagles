/**
 * 应用首页
 */

//应用
import * as actionType from '../../constants/ActionType/mobile';

const initialState = {
    advertisementList: [],
}

export default function saveAppInfo(state = initialState, action) {
    switch (action.type) {
        case actionType.mobileActionType.indexType.ADVERTISEMENT_LIST:
            return {
                ...state,
                advertisementList: action.payload
            };
        default:
            return state;
    }
};

