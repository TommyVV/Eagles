/**
 * 应用首页
 */

//应用
import { mobileActionType } from '../../constants/ActionType/mobile';

const initialState = {
    demandInfo: {
        demandList: [],
        totalSize: 0,
        keyword: ''
    },
    demandMap: new Map(),
}

export default function saveDemandList(state = initialState, action) {
    switch (action.type) {
        case mobileActionType.demandType.CLEAR_DEMAND_INFO:
            return {
                ...state,
                demandInfo: action.payload
            };
        case mobileActionType.demandType.SAVE_DEMAND_INFO:
            return {
                ...state,
                demandInfo: action.payload.demandInfo
            };
        case mobileActionType.demandType.SAVE_DEMAND_MAP:
            return {
                ...state,
                demandMap: action.payload
            };
        default:
            return state;
    }
};

