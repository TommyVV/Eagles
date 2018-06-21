import { mobileActionType } from '../../constants/ActionType/mobile';

export const saveUserDetail = detail => {
    return {
        detail,
        type: mobileActionType.USER.SAVE_USER_DETAIL
    }
} 