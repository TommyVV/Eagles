/*
 * @Author: wying 
 * @Date: 2018-04-04 15:37:42 
 * @Last Modified by: mikey.zhaopeng
 * @Last Modified time: 2018-05-08 09:30:12
 */
import { mobileActionType } from '../../constants/ActionType/mobile';

//公共

export const hideSuccess = data => {
    return {
        type: mobileActionType.GLOBAL.HIDE_SUCCESS
    };
}
export const showSuccess = (data,fn) => {
    return {
        type: mobileActionType.GLOBAL.SHOW_SUCCESS,
        text: data,
        fn,
    };
};
export const hideError = data => {
    return {
        type: mobileActionType.GLOBAL.HIDE_ERROR
    };
}
export const showError = data => {
    return {
        type: mobileActionType.GLOBAL.SHOW_ERROR,
        text: data
    };
};

export const hideLoading = () => {
    return {
        type: mobileActionType.GLOBAL.HIDE_LOADING
    };
}

export const showLoading = () => {
    return {
        type: mobileActionType.GLOBAL.SHOW_LOADING
    };
}
// 有白色背景
export const showLoadingWhite = () => {
    return {
        type: mobileActionType.GLOBAL.SHOW_LOADING_WHITE
    };
}
// 有白色背景
export const hideLoadingWhite = () => {
    return {
        type: mobileActionType.GLOBAL.HIDE_LOADING_WHITE
    };
}

//保存tab的值
export const saveTab = data => {
    return {
        type: mobileActionType.GLOBAL.SAVE_TAB,
        tab: data
    };
};