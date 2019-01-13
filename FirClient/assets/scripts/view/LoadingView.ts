
const {ccclass, property} = cc._decorator;

@ccclass
export default class LoadingView extends cc.Component {
    public label: cc.Label = null;
    public progressBar: cc.ProgressBar = null;

    public onLoad () {
        this.label = this.node.getChildByName("label").getComponent(cc.Label);
        this.progressBar = this.node.getChildByName("progressBar").getComponent(cc.ProgressBar);
    }
}
