import { Accordion, AccordionDetails, AccordionSummary, Button, Dialog, DialogActions, DialogContent, Typography } from "@mui/material"
import type { IRelationshipDescriptionDialogProps } from "./IRelationshipDescriptionDialogProps";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
export const RelationshipDescriptionDialog = (props: IRelationshipDescriptionDialogProps) => {



    const onClose = () => {
        if (props.onClose) {
            props.onClose();
        }
    }

    return (<Dialog maxWidth={'md'} fullWidth={true} open={props.open}>
        <DialogContent>
            <div>
                {props.data.fromSource && <div>
                    <h4>{props.data.fromSource.sourceNodeName} -&gt; {props.data.fromSource.destinationNodeName}</h4>
                    {props.data.fromSource.items?.map((item, index) => (
                        <Accordion key={index} >
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="fromSource-content"
                                id="fromSource-header"
                            >
                                <Typography fontWeight={'bold'}>{item.technology ?? "Undefined"}</Typography>
                            </AccordionSummary>
                            <AccordionDetails>
                                    {item.descriptions?.map((desc, index) => (
                                        <Typography key={index}>{desc}</Typography>
                                    ))}
                            </AccordionDetails>
                        </Accordion>
                    ))}

                </div>}
                {props.data.fromDestination && <div>
                    <h4>{props.data.fromDestination.sourceNodeName} -&gt; {props.data.fromDestination.destinationNodeName}</h4>
                    {props.data.fromDestination.items?.map((item, index) => (
                        <Accordion key={index}>
                            <AccordionSummary
                                expandIcon={<ExpandMoreIcon />}
                                aria-controls="fromDestination-content"
                                id="fromDestination-header"
                            >
                                <Typography fontWeight={'bold'}>{item.technology}</Typography>
                            </AccordionSummary>
                            <AccordionDetails>
                                    {item.descriptions?.map((desc, index) => (
                                        <Typography key={index}>{desc}</Typography>
                                    ))}
                            </AccordionDetails>
                        </Accordion>
                    ))}
                </div>}
            </div>
        </DialogContent>
        <DialogActions>
            <Button onClick={() => onClose()}>Close</Button>
        </DialogActions>
    </Dialog>)
}