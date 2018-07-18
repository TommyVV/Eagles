import { actionType } from "../constants/ActionType/pc";

export const saveInfo = payload => {
  return {
    payload,
    type: actionType.imageType.SAVE_INFO
  };
};
export const clearInfo = () => {
  return {
    type: actionType.imageType.CLEAR_INFO
  };
};
