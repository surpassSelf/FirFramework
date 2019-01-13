import CtrlBehaviour from "./CtrlBehaviour";
import AppUtil from "../utility/AppUtil";
import CtrlNames from "../define/CtrlNames";

export default class DataLoadingCtrl extends CtrlBehaviour implements IController {
    public initialize(): void {
    }    
    public onViewCreated(go: any): void {
    }

    public show(): void {
        AppUtil.showUI(CtrlNames.DATALOADING);
    }

    public hide(): void {
        AppUtil.closeUI(CtrlNames.DATALOADING);
    }

    public update(dt: any): void {
    }
    public getResPath(): string {
        return "";
    }
    public dispose(): void {
    }
}
