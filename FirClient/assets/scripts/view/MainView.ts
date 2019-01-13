const {ccclass, property} = cc._decorator;

@ccclass
export default class MainView extends cc.Component {

    @property(cc.Button)
    btn_enterGame: cc.Button = null;

    public onLoad () {
        this.btn_enterGame = this.node.getChildByName("EnterGame").getComponent(cc.Button);
    }
}
