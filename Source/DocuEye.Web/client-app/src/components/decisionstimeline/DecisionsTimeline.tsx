import { Timeline, TimelineConnector, TimelineContent, TimelineDot, TimelineItem, TimelineOppositeContent, TimelineSeparator } from "@mui/lab";
import { Box, Typography } from "@mui/material";
import { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import DocuEyeApi from "../../api";
import { FoundedDecision } from "../../api/docueye-api";
import Loader from "../loader";
import { IDecisionsTimelineProps } from "./IDecisionsTimelineProps";
import ArticleIcon from '@mui/icons-material/Article';

export const DecisionsTimeline = (props: IDecisionsTimelineProps) => {

    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [decisions, setDecisions] = useState<FoundedDecision[]>([]);
    const navigate = useNavigate();

    const formatDate = (dateString?: string) => {
        if (!dateString) {
            return "";
        }
        const date = new Date(dateString);
        let month = (date.getMonth() + 1).toString();
        let day = (date.getDay() + 1).toString();
        month = month.length === 1 ? "0" + month : month;
        day = day.length === 1 ? "0" + day : day;

        return `${date.getFullYear()}-${month}-${day}`;
    }

    const decisionColor = (status?: string | null) => {
        switch (status) {
            case "Proposed":
                return "#07b4f2"
            case "Accepted":
                return "#0f8721"
            case "Superseded":
                return "#f2e707"
            case "Deprecated":
                return "#9407f2"
            case "Rejected":
                return "#FF0000"
            default:
                return "#7a7977"
        }
    }

    useEffect(() => {
        if (!props.workspaceId) {
            return;
        }
        setIsLoading(true);
        const getDecisions = props.elementId ?
            DocuEyeApi.DecisionsApi.apiWorkspacesWorkspaceIdDecisionsElementElementIdGet(props.workspaceId, props.elementId)
            : DocuEyeApi.DecisionsApi.apiWorkspacesWorkspaceIdDecisionsWorkspaceGet(props.workspaceId);

        getDecisions.then((response: AxiosResponse<FoundedDecision[]>) => {
            setDecisions(response.data);
        }).finally(() => {
            setIsLoading(false);
        })

    }, [setIsLoading, props, setDecisions]);

    const goToDecision = (decisionId?: string | null) => {
        if (!decisionId) {
            return;
        }
        navigate('/workspace/' + props.workspaceId + '/decision/' + decisionId);
    }

    return (<Box padding={2} >
        {decisions.length === 0 &&
            <Typography sx={{ fontSize: 14 }} color="text.primary" gutterBottom>
                No decisions found
            </Typography>
        }
        {decisions.length > 0 && <Timeline position="alternate">
            {decisions.map((decision) => (
                <TimelineItem key={decision.id}>
                    <TimelineOppositeContent
                        sx={{ m: 'auto 0' }}
                        align="right"
                        variant="body2"
                        color="text.secondary"
                    >
                        {formatDate(decision.date)}
                    </TimelineOppositeContent>
                    <TimelineSeparator>
                        <TimelineConnector />
                        <TimelineDot sx={{ bgcolor: decisionColor(decision.status) }}>
                            <ArticleIcon />
                        </TimelineDot>
                        <TimelineConnector />
                    </TimelineSeparator>
                    <TimelineContent sx={{ py: '12px', px: 2 }}>
                        <Typography sx={{ cursor: 'pointer' }} onClick={() => goToDecision(decision.id)} variant="h6" component="span">
                            {decision.title}
                        </Typography>

                        <Typography>{decision.status}</Typography>
                        {decision.links?.map((link) => (
                            <Typography key={`${decision.id}-${link.id}`}>{link.description} {link.title}</Typography>
                        ))}
                    </TimelineContent>
                </TimelineItem>
            ))}
        </Timeline>
        }
        {isLoading && <Loader />}
    </Box>)
}