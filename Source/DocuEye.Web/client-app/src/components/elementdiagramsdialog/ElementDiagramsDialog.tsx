import { Button, Dialog, DialogActions, DialogContent, List, ListItem, ListItemButton, ListItemText } from "@mui/material"
import { AxiosResponse } from "axios"
import { useEffect, useState } from "react"
import DocuEyeApi from "../../api"
import { ViewWithElement } from "../../api/docueye-api"
import { IElementDiagramsDialogProps } from "./IElementDiagramsDialogProps"

export const ElementDiagramsDialog = (props: IElementDiagramsDialogProps) => {

  const [views, setViews] = useState<ViewWithElement[]>([]);
  const handleListItemClick = (view?: ViewWithElement) => {
    props.onClose(view?.id);
  }


  useEffect(() => {
    if (!props.open) {
      return;
    }
    if (!props.workspaceId || !props.elementId) {
      return;
    }
    DocuEyeApi.ViewsApi.apiWorkspacesWorkspaceIdViewsByelementElementIdGet(props.workspaceId, props.elementId)
      .then((response: AxiosResponse<ViewWithElement[]>) => {
        setViews(response.data);
      })
  }, [props])


  return (<Dialog open={props.open}>
    <DialogContent>
      <List sx={{ pt: 0 }}>
        {views.map((view) => (
          <ListItem disableGutters key={view.id}>
            <ListItemButton onClick={() => handleListItemClick(view)}>
              <ListItemText primary={view.name} />
            </ListItemButton>
          </ListItem>
        ))}
      </List></DialogContent>
    <DialogActions>
      <Button onClick={() => handleListItemClick()}>Close</Button>
    </DialogActions>
  </Dialog>)
}

