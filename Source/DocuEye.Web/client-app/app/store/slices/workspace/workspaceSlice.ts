import { createSlice } from "@reduxjs/toolkit";
import { type IWorkspaceState } from "./IWorkspaceState";


const initialState: IWorkspaceState = {
    value: undefined
};

const workspaceSlice = createSlice({
    name: 'workspace',
    initialState,
    reducers: {
        setWorkspaceData(state, action) {
            state.value = action.payload;
        }
    }
})

export const { setWorkspaceData } = workspaceSlice.actions

export default workspaceSlice.reducer