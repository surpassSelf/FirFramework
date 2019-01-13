const {ccclass, property} = cc._decorator;

@ccclass
export default class AlertView extends cc.Component {

    public btnOK: cc.Button = null;
    public btnCancel: cc.Button = null;
    public lbMessage: cc.Label = null;

    public onLoad () {
        this.btnOK = this.node.getChildByName("btn_ok").getComponent(cc.Button);
        this.btnCancel = this.node.getChildByName("btn_cancel").getComponent(cc.Button);
        this.lbMessage = this.node.getChildByName("lb_message").getComponent(cc.Label);
    }
}
