/**
 * 应用首页
 */

//应用
import { mobileActionType } from '../../constants/ActionType/mobile';

const initialState = {
    searchInfo: {
        keyword: '',
        agencyList: [],
        shareList: [],
        demandList: [],
        agencyTotalSize: 0,
        requireTotalSize: 0,
        shareTotalSize: 0,
    }
}

export default function searchInfo(state = initialState, action) {
    switch (action.type) {
        case mobileActionType.searchType.CLEAR_SEARCH_INFO:
            return {
                ...state,
                searchInfo: action.payload
            };
        case mobileActionType.searchType.SAVE_SEARCH_INFO:
            return {
                ...state,
                searchInfo: action.payload.searchInfo
            };
        default:
            return state;
    }
};

