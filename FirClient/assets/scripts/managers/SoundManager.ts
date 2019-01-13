import Behaviour from "../common/Behaviour";
import ManagerNames from "../define/ManagerNames";
import ResourceManager from "./ResourceManager";

export default class SoundManager extends Behaviour implements IManager {
    public initialize(): void {
    }    
    
    public playAudio(audioName:string):void {
        var manager = this.getManager(ManagerNames.RESOURCE);
        var resMgr = <ResourceManager>manager;
        if (resMgr != null) {
            var path = 'prefabs/audios/' + audioName;
            resMgr.loadAsset(path, null, (params:any, res: any)=>{
                cc.audioEngine.play(res, false, 0.5);
            });
        }
    }

    public dispose(): void {
    }
}
