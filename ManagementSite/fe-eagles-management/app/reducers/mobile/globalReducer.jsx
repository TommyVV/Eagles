import { mobileActionType } from '../../constants/ActionType/mobile';

const initialState = {
    showLoading: false,
    showLoadingWhite: false,
    showSuccess: {
        show: false,
        text: '操作成功',
        fn: () => { }
    },
    showError: {
        show: false,
        text: ''
    },
    tab: 0
};

export default function globalVal(state = initialState, action) {
    switch (action.type) {
        case mobileActionType.GLOBAL.SHOW_SUCCESS:
            return {
                ...state,
                showSuccess: {
                    show: true,
                    text: action.text,
                    fn: action.fn
                }
            };
        case mobileActionType.GLOBAL.HIDE_SUCCESS:
            return {
                ...state,
                showSuccess: {
                    show: false,
                    text: '操作成功'
                }
            };
        case mobileActionType.GLOBAL.SHOW_ERROR:
            return {
                ...state,
                showError: {
                    show: true,
                    text: action.text
                }
            };
        case mobileActionType.GLOBAL.HIDE_ERROR:
            return {
                ...state,
                showError: {
                    show: false,
                    text: ''
                }
            };
        case mobileActionType.GLOBAL.SHOW_LOADING:
            return {
                ...state,
                showLoading: true
            };
        case mobileActionType.GLOBAL.HIDE_LOADING:
            return {
                ...state,
                showLoading: false
            };
        case mobileActionType.GLOBAL.SHOW_LOADING_WHITE:
            return {
                ...state,
                showLoadingWhite: true
            };
        case mobileActionType.GLOBAL.HIDE_LOADING_WHITE:
            return {
                ...state,
                showLoadingWhite: false
            };
        case mobileActionType.GLOBAL.SAVE_TAB:
            console.log(action.tab);
            return {
                ...state,
                tab: action.tab
            }
        default:
            return state;
    }
}

