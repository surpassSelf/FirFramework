import CtrlNames from "../define/CtrlNames";
import LoginCtrl from "../controllers/LoginCtrl";
import LoadingCtrl from "../controllers/LoadingCtrl";
import TipsCtrl from "../controllers/TipsCtrl";
import DataLoadingCtrl from "../controllers/DataLoadingCtrl";
import BattleCtrl from "../controllers/BattleCtrl";
import MainCtrl from "../controllers/MainCtrl";
import AlertCtrl from "../controllers/AlertCtrl";

export default class CtrlCenter {
    private static ctrlMaps: {[key: string]: IController};

    ///初始化
    public static initialize(): void {
        this.ctrlMaps = { };
        this.addCtrl(CtrlNames.LOGIN, new LoginCtrl());
        this.addCtrl(CtrlNames.LOADING, new LoadingCtrl());
        this.addCtrl(CtrlNames.TIPS, new TipsCtrl());
        this.addCtrl(CtrlNames.ALERT, new AlertCtrl());
        this.addCtrl(CtrlNames.DATALOADING, new DataLoadingCtrl());
        this.addCtrl(CtrlNames.BATTLE, new BattleCtrl());
        this.addCtrl(CtrlNames.MAIN, new MainCtrl());

        for(var key in this.ctrlMaps) {
            var ctrl = <any>this.ctrlMaps[key];
            if (ctrl != null) {
                ctrl.initialize();
            }
        }
    }

    ///添加管理器
    public static addCtrl(name:string, ctrl:IController): void {
        this.ctrlMaps[name] = ctrl;
    }

    ///获取控制器
    public static getCtrl<T extends IController>(name:string): T {
        var ctrl = this.ctrlMaps[name];
        return <T>ctrl;
    }

    public static getCtrlByName(name:string): IController {
        return this.ctrlMaps[name];
    }

    ///更新
    public static update(dt): void {
        for(var key in this.ctrlMaps) {
            var ctrl = <any>this.ctrlMaps[key];
            if (ctrl != null && ctrl.isUpdate) {
                ctrl.update(dt);
            }
        }
    }

    ///获取控制器映射
    public static getCtrlMaps(): any {
        return this.ctrlMaps;
    }
}
