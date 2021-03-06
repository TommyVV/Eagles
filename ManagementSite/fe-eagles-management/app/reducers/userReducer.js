import {
  actionType
} from "../constants/ActionType/pc";

// avatar	头像	string
// mobile	手机号	string
// openId		string
// userId	用户id	string
// userName	用户姓名
const userinfo = {
  avatar: '',
  mobile: '',
  openId: '',
  userId: '',
  userName: '',
}

const userReducer = (state = userinfo, action) => {
  switch (action.type) {
    case actionType.userType.SAVE_USER_INFO:
      return {
        ...state,
        ...action.payload
      }
    default:
      return state
  }
}

export default userReducer;