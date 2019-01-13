import Behaviour from "../common/Behaviour";
import ManagerNames from "../define/ManagerNames";
import ResourceManager from "./ResourceManager";
import AppUtil from "../utility/AppUtil";

export default class UIManager extends Behaviour implements IManager {
    private resManager:ResourceManager = null;

    public initialize(): void {
    }

    private getResManager(): ResourceManager {
        if (this.resManager == null) {
            var manager = this.getManager(ManagerNames.RESOURCE);
            if (manager != null) {
                this.resManager = <ResourceManager>manager;
            }
        }
        return this.resManager;
    }

    ///显示UI
    public showUI(ctrlName:string): void {
        var ctrl = this.getCtrl(ctrlName);
        if (ctrl != null) {
            var resPath = ctrl.getResPath();
            var resMgr = this.getResManager();
            var params:any = {
                c: ctrl,    //控制器
            }
            resMgr.loadAsset(resPath, params, this.onCreateUI);
        }
    }

    ///创建UI
    private onCreateUI(params:any, prefab: any): void {
        var ctrl:IController = params.c;

        var gameObj:cc.Node = cc.instantiate(prefab);
        gameObj.parent = AppUtil.getParent();
        if (ctrl != null) {
            ctrl.onViewCreated(gameObj);
        }
    }

    ///关闭UI
    public closeUI(ctrlName:string): void {
        var ctrl = this.getCtrl(ctrlName);
        if (ctrl != null) {
            ctrl.dispose();
            
            ///销毁对象
            var ctrlObj = <any>ctrl;
            if (ctrlObj.canDestroy && ctrlObj.gameObj != null) {
                ctrlObj.gameObj.destroy();
            }
        }
    }

    public dispose(): void {
    }
}
