import { configureStore } from "@reduxjs/toolkit"
import workspaceReducer from './slices/workspace/workspaceSlice'
import viewConfigurationReducer from './slices/viewConfiguration/viewConfigurationSlice'

const store = configureStore({
  reducer: {
    workspace: workspaceReducer,
    viewConfiguration: viewConfigurationReducer
  }
})

export default store