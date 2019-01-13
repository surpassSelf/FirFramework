import Behaviour from "../common/Behaviour";
import ManagerNames from "../define/ManagerNames";
import ResourceManager from "./ResourceManager";
import AppUtil from "../utility/AppUtil";

export default class MapManager extends Behaviour implements IManager {
    public initialize(): void {
    }    
    
    public loadScene(sceneName:string, callback: Function): void {
        console.error("loadScene..." + sceneName);
        cc.director.loadScene(sceneName, callback);
    }

    public loadMap(mapName:string, callback: Function): void {
        console.error("loadMap..." + mapName);
        var manager = this.getManager(ManagerNames.RESOURCE);
        var resMgr = <ResourceManager>manager;
        if (resMgr != null) {
            var path = "prefabs/maps/" + mapName;
            var params:any = {
                name: mapName,      //地图名字
                func: callback,     //回调函数
            }
            resMgr.loadAsset(path, params, this.onCreateMap);
        }
    }

    private onCreateMap(params:any, prefab: any): void {
        var mapName:string = params.name;
        var callback: Function = params.func;

        var gameObj:cc.Node = cc.instantiate(prefab);
        gameObj.name = mapName;
        gameObj.parent = AppUtil.getParent();

        if (callback != null) {
            callback.call(this);
        }
    }

    public dispose(): void {
    }
}
