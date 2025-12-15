import { createSlice } from "@reduxjs/toolkit";
import { IViewConfigurationState } from "./IViewConfigurationState";


const initialState: IViewConfigurationState = {
    value: undefined
}

const viewConfigurationSlice = createSlice({
    name: 'viewConfiguration',
    initialState,
    reducers: {
        setViewConfiguration(state, action) {
            state.value = action.payload;
        }
    }
})

export const { setViewConfiguration } = viewConfigurationSlice.actions

export default viewConfigurationSlice.reducer