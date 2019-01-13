import CtrlBehaviour from "./CtrlBehaviour";
import MainView from "../view/MainView";
import AppUtil from "../utility/AppUtil";
import CtrlNames from "../define/CtrlNames";

export default class MainCtrl extends CtrlBehaviour implements IController {
    private mainView: MainView = null;
    private btn_EnterGame:cc.Button = null;

    public initialize(): void {
    }  

    public onViewCreated(go: any): void {
        this.gameObj = go;
        this.mainView = go.addComponent(MainView);
        this.btn_EnterGame = this.mainView.btn_enterGame;
        this.btn_EnterGame.node.on('click', this.clickEvent, this);
    }

    private clickEvent(event) {
        console.error('clickEvent:>' + event.node.name);
        AppUtil.closeUI(CtrlNames.MAIN);
        AppUtil.loadMap('map001', ()=>{
            AppUtil.showUI(CtrlNames.BATTLE);
            console.warn("loadedMap:>map001");
        });
    }

    public update(dt: any): void {
    }

    public getResPath(): string {
        return 'prefabs/ui/MainUI';
    }

    public dispose(): void {
        this.btn_EnterGame.node.off('click', this.clickEvent, this);
    }
}
