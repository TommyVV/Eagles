/**
 * 以下为默认配置
 */
const ROOT_ID = 'root'; // 组织机构根节点id
const IS_PRODUCT = false; // 是否为生产环境
/**
 * 权限的信息
 */
const MANAGE_AREA = 'roomArea'; // 可以管理区域
const MANAGE_ROOM = 'room'; // 可以管理会议室，但不能管理区域

export const appConfig = {
  ROOT_ID,
  IS_PRODUCT,
  MANAGE_AREA,
  MANAGE_ROOM
};
