import LoadingView from "../view/LoadingView";
import CtrlBehaviour from "./CtrlBehaviour";
import SceneNames from "../define/SceneNames";
import CtrlNames from "../define/CtrlNames";
import AppUtil from "../utility/AppUtil";

export default class LoadingCtrl extends CtrlBehaviour implements IController {
    private loadingView:LoadingView = null;
    private label:cc.Label = null;
    private progressBar:cc.ProgressBar = null;
    private canRunning:boolean = true;

    public initialize(): void {
    }

    public onViewCreated(go:any): void {
        this.gameObj = go;
        this.loadingView = go.addComponent(LoadingView);

        this.label = this.loadingView.label;
        this.progressBar = this.loadingView.progressBar;

        this.label.string = "Loading...";
        this.progressBar.progress = 0;

        this.isUpdate = true;
    }

    public update (dt) {
        if (!this.canRunning) {
            return;
        }
        if (this.progressBar.progress >= 1) {
            this.canRunning = false;
            this.onLoadGame();
            return;
        }
        else {
            this.progressBar.progress += dt;
        }
    }

    private onLoadGame() {
        this.isUpdate = false;

        AppUtil.loadScene(SceneNames.GAME,  () => {
            console.warn("onLoadSceneOK...");
            AppUtil.closeUI(CtrlNames.LOADING);
            AppUtil.showUI(CtrlNames.MAIN);
        });
    }

    public getResPath(): string {
        return "prefabs/ui/LoadingUI";
    }

    public dispose(): void {
        console.error("loadingctrl dispose...");
    }
}
