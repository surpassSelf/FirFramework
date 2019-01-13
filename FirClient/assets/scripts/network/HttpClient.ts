import AppConst from "../define/AppConst";
import AppUtil from "../utility/AppUtil";

class CommandVo {
	public method: string = ""; //模块，暂无用途
	public action: string = ""; //处理接口，如login
	public data: string =""; //参数

	public constructor(action: string = "", params: any = null) {
		this.action = action;
		this.data = JSON.stringify(params);
	}
}

export default class HttpClient {
	public static cookie: string = "";
	public static REQUEST_TIMEOUT:number = 1;
	public static _timerId:number = 1;
	
	public static send(action:string, method: string, data: any, callBack: Function = null): void 
	{
        var command:CommandVo = new CommandVo();
        command.action = action;
        command.method = method;
        command.data = JSON.stringify(data);

		let js = JSON.stringify(command);
		let request = cc.loader.getXMLHttpRequest();
		request.timeout = 10000;
		request.open("POST", AppConst.HTTP_API, true);
		request.addEventListener("error", onError);
		request.addEventListener("abort", onError);
		request.setRequestHeader("Content-Type", "application/json");

		request.onreadystatechange = function() {
			if (request.readyState === 4 && (request.status >= 200 && request.status < 300)) {
				console.log("http res("+ request.responseText.length + "):" + request.responseText);
				try {
					onComplete(request.responseText);
				} catch (e) {
					console.log("err:" + e);
				}
				finally{}
			}
		}
		request.send(js);

		AppUtil.showLoading();
		function onComplete(responseText: string): void {
			AppUtil.hideLoading();
			var data = JSON.parse(responseText);
			var code: number = data["errcode"];
			console.warn("errcode:>" + code);

			if (code != 0) {
				AppUtil.showMessage(data["desc"]);
			}
			else 
			{
				if (callBack != null) {
					callBack.apply([data]);
				}
			}
		}

		function onError(evt): void {
			AppUtil.hideLoading();
			AppUtil.showMessage("请求接口失败：onError");
		}
	}

	///发送测试函数
	public static sendLogin(login:any, user:any, sysinfo:any): void {
		var data = { 
			'code': login.code, 
			'nickname': user.nickName,
			'gender': user.gender,
			'language': user.language,
			'city': user.city,
			'province': user.province,
			'avatarUrl': user.avatarUrl,
			'brand': sysinfo.brand,
			'model': sysinfo.model,
			'version': sysinfo.version,
			'system': sysinfo.system,
			'platform': sysinfo.platform,
		};
        HttpClient.send("user", "login", data, onData);
		
		function onData(newdata:Object):void 
		{
			//console.warn("onData..." + newdata);
		}
	}
}

