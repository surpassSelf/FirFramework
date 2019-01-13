import Behaviour from "../common/Behaviour";
import HttpClient from "../network/HttpClient";
import AppConst from "../define/AppConst";
import WebSocketClient from "../network/WebSocketClient";

export default class NetworkManager extends Behaviour implements IManager {
    private socketClient: WebSocketClient = null;
    
    public initialize(): void {
        if (!AppConst.USE_WEBSOCKET) {
            return;
        }
        this.socketClient = new WebSocketClient();
        this.socketClient.initialize(this.onOpen, this.onError, this.onMessage, this.onClose);
    } 

    public sendData(data: any):void {
        if (!AppConst.USE_WEBSOCKET) {
            return;
        }
        this.socketClient.sendData(data);
    }
    
    private onOpen(ev: Event) {
        console.log('onOpen');
    }

    private onError(ev: Event) {
        console.error('onError');
    }

    private onMessage(ev: MessageEvent) {
        console.log("onMessage:" + ev.data);
    }

    private onClose(ev: CloseEvent) {
        console.error('onClose:' + ev.code);
    }

    ///发送HTTP请求
    public sendHttpRequest(action, method, data, callBack: Function = null) {
        HttpClient.send(action, method, data, callBack);
    }

    public dispose(): void {
        this.socketClient.dispose();
    }
}
