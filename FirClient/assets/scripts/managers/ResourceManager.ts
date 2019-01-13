import Behaviour from "../common/Behaviour"

export default class ResourceManager extends Behaviour implements IManager {
    public initialize(): void {
    }

    public loadAsset(path: string, params:any, callback:(params: any, resource: any) => void) {
        cc.loader.loadRes(path, function(err, prefab) {
            callback.call(this, params, prefab);
        });
    }
    
    public dispose(): void {
    }
}
