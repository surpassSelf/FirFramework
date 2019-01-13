import ManagerCenter from "../common/ManagerCenter";
import MapManager from "../managers/MapManager";
import ManagerNames from "../define/ManagerNames";
import UIManager from "../managers/UIManager";
import CtrlCenter from "../common/CtrlCenter";
import AlertCtrl from "../controllers/AlertCtrl";
import CtrlNames from "../define/CtrlNames";
import DataLoadingCtrl from "../controllers/DataLoadingCtrl";

export default class AppUtil {
    public static getParent(): cc.Node {
        return cc.find("Canvas");
    }

    public static loadMap(mapName:string, callback: Function): void {
        var mapMgr = ManagerCenter.getManager<MapManager>(ManagerNames.MAP);
        if (mapMgr != null) {
            mapMgr.loadMap(mapName, callback);
        }
    }

    public static loadScene(sceneName:string, callback: Function): void {
        var mapMgr = ManagerCenter.getManager<MapManager>(ManagerNames.MAP);
        if (mapMgr != null) {
            mapMgr.loadScene(sceneName, callback);
        }
    }

    public static showUI(ctrlName:string): void {
        var uiMgr = ManagerCenter.getManager<UIManager>(ManagerNames.UI);
        if (uiMgr != null) {
            uiMgr.showUI(ctrlName);
        }
    }

    public static closeUI(ctrlName:string): void {
        var uiMgr = ManagerCenter.getManager<UIManager>(ManagerNames.UI);
        if (uiMgr != null) {
            uiMgr.closeUI(ctrlName);
        }
    }

    public static showMessage(message:string): void {
        var alertCtrl = CtrlCenter.getCtrl<AlertCtrl>(CtrlNames.ALERT);
        if (alertCtrl != null) {
            alertCtrl.showMessage(message);
        }
    }

    public static showLoading(): void {
        var dataLoading = CtrlCenter.getCtrl<DataLoadingCtrl>(CtrlNames.DATALOADING);
        if (dataLoading != null) {
            dataLoading.show();
        }
    }

    public static hideLoading(): void {
        var dataLoading = CtrlCenter.getCtrl<DataLoadingCtrl>(CtrlNames.DATALOADING);
        if (dataLoading != null) {
            dataLoading.hide();
        }
    }
}
