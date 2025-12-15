export const getRankDirection = (direction: string | null | undefined) : string => {
    if(!direction) {
        return "TB";
    }
    switch(direction) {
        case "TopBottom":
            return "TB";
        case "BottomTop":
            return "BT";
        case "LeftRight":
            return "LR";
        case "RightLeft":
            return "RL";
        default:
            return "TB";
    }
}