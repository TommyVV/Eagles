import { mobileActionType } from '../../constants/ActionType/mobile';

const initialState = {
    userDetails: new Map()
}

export default function users(state = initialState, action) {
    switch (action.type) {
        case mobileActionType.USER.SAVE_USER_DETAIL:
            const users = { ...state };
            const userDetails = new Map(users.userDetails);
            userDetails.set(action.detail.userId, action.detail);
            users.userDetails = userDetails;
            return users;
        default:
            return state
    }
}

