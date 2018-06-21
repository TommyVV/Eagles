import { mobileActionType } from '../../constants/ActionType/mobile';

const initialState = {
    shareStatics: {},
    shareInfo: {
        shareList: [],
        keyword: '',
    },
    shareMap: new Map(),
};

export default function share(state = initialState, action) {
    switch (action.type) {
        case mobileActionType.shareType.CLEAR_SHARE_INFO:
            return {
                ...state,
                shareInfo: action.payload
            };
        case mobileActionType.SHARE.SAVE_SHARE_STATISTICS:
            return {
                ...state,
                shareStatics: action.statistics
            };
        case mobileActionType.shareType.SAVE_SHARE_INFO:
            return {
                ...state,
                shareInfo: action.payload.shareInfo
            };
        case mobileActionType.shareType.SAVE_SHARE_MAP:
            return {
                ...state,
                shareMap: action.payload
            };
        default:
            return state;
    }
}

