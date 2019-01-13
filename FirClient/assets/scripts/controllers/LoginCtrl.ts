import CtrlBehaviour from "./CtrlBehaviour";

export default class LoginCtrl extends CtrlBehaviour implements IController {
    public initialize(): void {
    }

    public onViewCreated(gameObj): void {
        this.isUpdate = false;
    }    

    public update(dt): void {
        console.warn("LoginCtrl update");
    }

    public getResPath(): string {
        return "prefabs/ui/LoginUI";
    }

    public dispose(): void {
    }
}
