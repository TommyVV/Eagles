import { actionType } from "../constants/ActionType/pc";

export const saveInfo = payload => {
  return {
    payload,
    type: actionType.scoreType.SAVE_INFO
  };
};
export const clearInfo = () => {
  return {
    type: actionType.scoreType.CLEAR_INFO
  };
};
