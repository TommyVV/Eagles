import { actionType } from "../constants/ActionType/pc";

export const saveInfo = payload => {
  return {
    payload,
    type: actionType.activityType.SAVE_INFO
  };
};
export const clearInfo = () => {
  return {
    type: actionType.activityType.CLEAR_INFO
  };
};
