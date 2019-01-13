
export default class Behaviour  {
    private static ctrlCenter: any = null;
    private static managerCenter: any = null;

    public isUpdate:boolean = false;

    ///初始化
    public static initialize(ctrlCenter, mgrCneter): void {
        Behaviour.ctrlCenter = ctrlCenter;
        Behaviour.managerCenter = mgrCneter;
    }

    ///获取控制器
    public getCtrl(name:string): IController {
        if (Behaviour.ctrlCenter == null) {
            return null;
        }
        return Behaviour.ctrlCenter.getCtrlByName(name);
    }

    ///获取管理器
    public getManager(name:string): IManager {
        if (Behaviour.managerCenter == null) {
            return null;
        }
        return Behaviour.managerCenter.getManagerByName(name); 
    }
}
