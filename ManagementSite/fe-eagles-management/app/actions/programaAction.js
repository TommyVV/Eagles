import { actionType } from "../constants/ActionType/pc";

 // 保存详情
export const saveInfo = payload => {
  return {
    payload,
    type: actionType.programaType.SAVE_INFO
  };
};
// 清除信息
export const clearInfo = () => {
  return {
    type: actionType.programaType.CLEAR_INFO
  };
};
