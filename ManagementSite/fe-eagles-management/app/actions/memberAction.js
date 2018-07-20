import { actionType } from "../constants/ActionType/pc";

export const saveInfo = payload => {
  return {
    payload,
    type: actionType.memberType.SAVE_INFO
  };
};
export const clearInfo = () => {
  return {
    type: actionType.memberType.CLEAR_INFO
  };
};
