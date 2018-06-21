/**
 * 应用首页
 */

//应用
import { mobileActionType } from '../../constants/ActionType/mobile';

const initialState = {
    agencyInfo: {
        agencyList: [],
        keyword: '',
    },
    agencyMap: new Map(),
    tab: 0
}

export default function saveAgencyList(state = initialState, action) {
    switch (action.type) {
        case mobileActionType.agencyType.CLEAR_AGENCY_INFO:
            return {
                ...state,
                agencyInfo: action.payload
            };
        case mobileActionType.agencyType.SAVE_AGENCY_INFO:
            return {
                ...state,
                agencyInfo: action.payload.agencyInfo
            }
        case mobileActionType.agencyType.SAVE_AGENCY_MAP:
            return {
                ...state,
                agencyMap: action.payload
            }
        case mobileActionType.agencyType.SAVE_AGENCY_TAB:
            return {
                ...state,
                tab: action.payload
            }
        default:
            return state;
    }
};

