import CtrlCenter from "./common/CtrlCenter";
import ManagerCenter from "./common/ManagerCenter";
import Behaviour from "./common/Behaviour";
import ManagerNames from "./define/ManagerNames";
import UIManager from "./managers/UIManager";
import CtrlNames from "./define/CtrlNames";

const {ccclass, property} = cc._decorator;
@ccclass
export default class Facade extends cc.Component {

    public onLoad () {
        cc.game.setFrameRate(60);
        CtrlCenter.initialize();
        ManagerCenter.initialize();
        Behaviour.initialize(CtrlCenter, ManagerCenter);

        cc.game.addPersistRootNode(this.node);
    }

    public start () {
        var uiMgr = ManagerCenter.getManager<UIManager>(ManagerNames.UI);
        if (uiMgr != null) {
            uiMgr.showUI(CtrlNames.LOADING);
        }
    }

    public update (dt) {
        CtrlCenter.update(dt);
        ManagerCenter.update(dt);
    }
}
