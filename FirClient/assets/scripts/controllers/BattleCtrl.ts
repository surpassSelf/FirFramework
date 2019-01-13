import CtrlBehaviour from "./CtrlBehaviour";


export default class BattleCtrl extends CtrlBehaviour implements IController {
    public initialize(): void {
    }

    public onViewCreated(go: any): void {
        console.error("onViewCreated--->>" + go.name);
    }

    public update(dt: any): void {
    }

    public getResPath(): string {
        return 'prefabs/ui/BattleUI';
    }

    public dispose(): void {
    }
}
