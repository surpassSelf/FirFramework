import AppConst from "../define/AppConst";

export default class WebSocketClient {
    private ws: WebSocket = null;

    public initialize(onOpen, onError, onMessage,  onClose): void {
        console.log("weboscket url: " + AppConst.WEBSOCK_API);
        
        this.ws = new WebSocket(AppConst.WEBSOCK_API);
        this.ws.onopen = onOpen;
        this.ws.onerror = onError;
        this.ws.onmessage = onMessage;
        this.ws.onclose = onClose;
    }

    public sendData(data: any):void {
        if (this.ws != null) {
            this.ws.send(data);
        }
    }

    public dispose(): void {
        if (this.ws != null) {
            this.ws.close();
        }
    }
}
