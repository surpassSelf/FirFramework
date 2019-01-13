import CtrlBehaviour from "./CtrlBehaviour";
import AlertView from "../view/AlertView";
import AppUtil from "../utility/AppUtil";
import CtrlNames from "../define/CtrlNames";

export default class AlertCtrl extends CtrlBehaviour implements IController {
    private message:string = null;
    private alertView: AlertView = null;
    private btnOK:cc.Button = null;
    private btnCancel:cc.Button = null;
    private lbMessage:cc.Label = null;

    public initialize(): void {
    }   

    public onViewCreated(go: any): void {
        this.gameObj = go;
        this.alertView = go.addComponent(AlertView);
        this.btnOK = this.alertView.btnOK;
        this.btnCancel = this.alertView.btnCancel;
        this.lbMessage = this.alertView.lbMessage;
        this.lbMessage.string = this.message;

        this.btnOK.node.on('click', this.onClick, this);
        this.btnCancel.node.on('click', this.onClick, this);
    }

    public showMessage(message:string):void {
        this.message = message; 
        AppUtil.showUI(CtrlNames.ALERT);
    }

    private onClick(event):void {
        AppUtil.closeUI(CtrlNames.ALERT);
    }
    
    public update(dt: any): void {
    }
    public getResPath(): string {
        return "prefabs/ui/AlertUI";
    }
    
    public dispose(): void { 
        this.btnOK.node.off('click', this.onClick, this);
        this.btnCancel.node.off('click', this.onClick, this);
    }
}
