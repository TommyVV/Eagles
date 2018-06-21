/**
 * 我的
 */
//获取某人信息
const userType = {
    USER_INFO: 'USER_INFO'
};

// 搜索
const searchType = {
    CLEAR_SEARCH_INFO: 'CLEAR_SEARCH_INFO', // 清空搜索信息(keyword list totalSize)
    SAVE_SEARCH_INFO: 'SAVE_SEARCH_INFO', // 保存搜索信息
};
// app首页
const appType = {
    APP_LIST: 'APP_LIST', // app列表
};
// hci首页
const indexType = {
    ADVERTISEMENT_LIST: 'ADVERTISEMENT_LIST', // hci首页广告列表
};
// 需求
const demandType = {
    SAVE_DEMAND_INFO: 'SAVE_DEMAND_INFO', // 咨询需求列表(keyword list totalSize)
    CLEAR_DEMAND_INFO: 'CLEAR_DEMAND_INFO', // 清空需求列表信息
    SAVE_DEMAND_MAP: 'SAVE_DEMAND_MAP', // 保存需求详情的map

};
// 咨询机构
const agencyType = {
    SAVE_AGENCY_INFO: 'SAVE_AGENCY_INFO', // 咨询机构列表信息(keyword list totalSize)
    CLEAR_AGENCY_INFO: 'CLEAR_AGENCY_INFO', // 清空咨询机构列表信息
    SAVE_AGENCY_MAP: 'SAVE_AGENCY_MAP', // 保存咨询机构详情的map
    SAVE_AGENCY_TAB: 'SAVE_AGENCY_TAB'         //保存机构tab
};
// 分享
const shareType = {
    SAVE_SHARE_INFO: 'SAVE_SHARE_INFO', // 分享列表(keyword list totalSize)
    CLEAR_SHARE_INFO: 'CLEAR_SHARE_INFO', // 清空分享列表信息
    SAVE_SHARE_MAP: 'SAVE_SHARE_MAP', // 保存分享详情的map
};

//保存分享统计
const SHARE = {
    SAVE_SHARE_STATISTICS: 'SAVE_SHARE_STATISTICS'
};

//保存所填项目表单
const PROJECT = {
    SAVE_PROJECT_FORM: 'SAVE_PROJECT_FORM',
    UPDATE_PROJECT_FORM: 'UPDATE_PROJECT_FORM',      //更新项目需求及成员   
    SAVE_PROJECT_INFO: 'SAVE_PROJECT_INFO',      //保存项目基本详情        
}

//用户信息
const USER = {
    SAVE_USER_DETAIL: 'SAVE_USER_DETAIL'
}

//loading
const GLOBAL = {
    HIDE_SUCCESS: 'HIDE_SUCCESS',
    SHOW_SUCCESS: 'SHOW_SUCCESS',
    HIDE_ERROR: 'HIDE_ERROR',
    SHOW_ERROR: 'SHOW_ERROR',
    HIDE_LOADING: 'HIDE_LOADING',
    SHOW_LOADING: 'SHOW_LOADING',
    SAVE_TAB: 'SAVE_TAB',
    HIDE_LOADING_WHITE: 'HIDE_LOADING_WHITE',
    SHOW_LOADING_WHITE: 'SHOW_LOADING_WHITE'
}

export const mobileActionType = {
    searchType,
    userType,
    appType,
    indexType,
    demandType,
    agencyType,
    shareType,
    GLOBAL,
    SHARE,
    PROJECT,
    USER
};