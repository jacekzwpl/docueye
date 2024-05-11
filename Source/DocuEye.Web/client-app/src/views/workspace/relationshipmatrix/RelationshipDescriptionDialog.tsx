import { Button, Dialog, DialogActions, DialogContent } from "@mui/material"
import { IRelationshipDescriptionDialogProps } from "./IRelationshipDescriptionDialogProps"

export const RelationshipDescriptionDialog = (props: IRelationshipDescriptionDialogProps) => {



    const onClose = () => {
        if (props.onClose) {
            props.onClose();
        }
    }

    return (<Dialog open={props.open}>
        <DialogContent>
            <div>
                {props.data.fromSource && <div>
                    <h4>{props.data.fromSource.sourceNodeName} -&gt; {props.data.fromSource.destinationNodeName}</h4>
                    {props.data.fromSource.items?.map((item, index) => (
                        <p key={index}>{item.technology}</p>
                    ))}
                    
                </div>}
                {props.data.fromDestination && <div>
                    <h4>{props.data.fromDestination.sourceNodeName} -&gt; {props.data.fromDestination.destinationNodeName}</h4>
                    {props.data.fromDestination.items?.map((item, index) => (
                        <p key={index}>{item.technology}</p>
                    ))}
                </div>}
            </div>
        </DialogContent>
        <DialogActions>
            <Button onClick={() => onClose()}>Close</Button>
        </DialogActions>
    </Dialog>)
}