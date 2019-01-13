import ManagerNames from "../define/ManagerNames";
import UIManager from "../managers/UIManager";
import ResourceManager from "../managers/ResourceManager";
import NetworkManager from "../managers/NetworkManager";
import ObjectManager from "../managers/ObjectManager";
import MapManager from "../managers/MapManager";
import SoundManager from "../managers/SoundManager";

export default class ManagerCenter {
    private static managerMaps: {[key: string]: IManager};

    ///初始化
    public static initialize(): void { 
        this.managerMaps = { };
        this.addManager(ManagerNames.UI, new UIManager());
        this.addManager(ManagerNames.RESOURCE, new ResourceManager());
        this.addManager(ManagerNames.NETWORK, new NetworkManager());
        this.addManager(ManagerNames.OBJECT, new ObjectManager());
        this.addManager(ManagerNames.MAP, new MapManager());
        this.addManager(ManagerNames.SOUND, new SoundManager());

        for(var key in this.managerMaps) {
            var mgr = this.managerMaps[key];
            if (mgr != null) {
                mgr.initialize();
            }
        }
    }

    ///添加管理器
    public static addManager(name:string, manager:IManager): void {
        this.managerMaps[name] = manager;
    }

    ///获取管理器
    public static getManager<T extends IManager>(name:string): T {
        var manager = this.managerMaps[name];
        return <T>manager;
    }

    public static getManagerByName(name:string): IManager {
        return this.managerMaps[name];
    }

    ///更新管理器
    public static update(dt): void {
    }
 
    ///获取管理器映射
    public static getManagerMaps(): any {
        return this.managerMaps; 
    }
}
