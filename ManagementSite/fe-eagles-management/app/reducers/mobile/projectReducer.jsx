import * as actionType from '../../constants/ActionType/mobile';

const initialState = {
    projectForm: {
        projectId: '',
        projectName: '',
        members: [],
        demand: {},
    },
    projectInfos: new Map()
};

export default function project(state = initialState, action) {
    switch (action.type) {
        case actionType.mobileActionType.PROJECT.SAVE_PROJECT_FORM:
            return {
                ...state,
                projectForm: action.projectForm
            };
        case actionType.mobileActionType.PROJECT.UPDATE_PROJECT_FORM:
            console.log(state);
            const projectForm = { ...state.projectForm };
            projectForm.demand = action.demand;
            projectForm.members = action.members;
            return {
                ...state,
                projectForm
            };
        case actionType.mobileActionType.PROJECT.SAVE_PROJECT_INFO:
           const projects = {
               ...state
           }    
           const projectInfos = new Map(projects.projectInfos);
           projectInfos.set(action.projectInfo.basicData.id, action.projectInfo);
           projects.projectInfos = projectInfos;
           return projects;
        default:
            return state;
    }
}

