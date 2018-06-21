import { mobileActionType } from '../../constants/ActionType/mobile';

//保存新建项目
export const saveProjectForm = projectForm => {
    return {
        projectForm,
        type: mobileActionType.PROJECT.SAVE_PROJECT_FORM
    }
}

//更新项目信息
export const updateProjectForm = (demand, members) => {
    return {
        demand,
        members,
        type: mobileActionType.PROJECT.UPDATE_PROJECT_FORM
    }
}

//保存项目基本详情
export const saveProjectInfo = projectInfo => {
    return {
        projectInfo,
        type: mobileActionType.PROJECT.SAVE_PROJECT_INFO
    }
}