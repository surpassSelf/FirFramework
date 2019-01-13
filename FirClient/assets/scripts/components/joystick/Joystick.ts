import EasyTouch from "./EasyTouch";
import { JoyDefine } from "./JoyDefine";

const {ccclass, property} = cc._decorator;

@ccclass
export default class Joystick extends cc.Component {

    @property({type: cc.Node, displayName: '摇杆节点'})
    dot: cc.Node = null;

    @property({type: EasyTouch, displayName: '摇杆背景节点'})
    ring: EasyTouch = null;

    @property({type: cc.Float, displayName: '摇杆X位置'})
    stickX = 0;

    @property({type: cc.Float, displayName:'摇杆Y位置'})
    stickY = 0; 

    @property({type: cc.Enum(JoyDefine.TouchType), displayName: '触摸类型'})
    touchType: JoyDefine.TouchType = JoyDefine.TouchType.DEFAULT;

    @property({type: cc.Enum(JoyDefine.DirectionType), displayName: '方向类型'})
    directionType: JoyDefine.DirectionType = JoyDefine.DirectionType.ALL; 

    @property({type: cc.Node, displayName: '操控的目标'})
    sprite: cc.Node = null;

    @property({displayName: '摇杆当前位置'})
    _stickPos: cc.Vec2 = null;

    @property({displayName: '摇杆当前位置'})
    _touchLocation: cc.Vec2 = null;
 
    // LIFE-CYCLE CALLBACKS:

    public onLoad () {
        this._createStickSprite();
        if (this.touchType == JoyDefine.TouchType.FOLLOW) {
            this._initTouchEvent();
        }
    }

    private _createStickSprite () {
        this.ring.node.setPosition(this.stickX, this.stickY);
        this.dot.setPosition(this.stickX, this.stickY);
    }

    private _initTouchEvent() {
        this.node.on(cc.Node.EventType.TOUCH_START, this._touchStartEvent, this);
        this.node.on(cc.Node.EventType.TOUCH_MOVE, this._touchMoveEvent, this);
        this.node.on(cc.Node.EventType.TOUCH_END, this._touchEndEvent, this);
        this.node.on(cc.Node.EventType.TOUCH_CANCEL, this._touchEndEvent, this);
    }

    private _touchStartEvent(event) {
        this._touchLocation = event.getLocaltion();
        var touchPos = this.node.convertToNodeSpaceAR(event.getLocaltion());
        this.ring.node.setPosition(touchPos);
        this._stickPos = touchPos;

        console.error(touchPos.toString());
    }

    private _touchMoveEvent(event) {
        // 如果touch start位置和touch move相同，禁止移动
        if (this._touchLocation.x == event.getLocation().x && this._touchLocation.y == event.getLocation().y){
            return false;
        }
        // 以圆圈为锚点获取触摸坐标
        var touchPos = this.ring.node.convertToNodeSpaceAR(event.getLocation());
        var distance = this.ring.getDistance(touchPos, cc.v2(0,0));
        var radius = this.ring.node.width / 2;

        // 由于摇杆的postion是以父节点为锚点，所以定位要加上touch start时的位置
        var posX = this._stickPos.x + touchPos.x;
        var posY = this._stickPos.y + touchPos.y;
        if(radius > distance)
        {
            this.dot.setPosition(cc.v2(posX, posY));
        }
        else
        {
            //控杆永远保持在圈内，并在圈内跟随触摸更新角度
            var x = this._stickPos.x + Math.cos(this.ring.getRadian(cc.v2(posX,posY))) * radius;
            var y = this._stickPos.y + Math.sin(this.ring.getRadian(cc.v2(posX,posY))) * radius;
            this.dot.setPosition(cc.v2(x, y));
        }
        //更新角度
        this.ring.getAngle(cc.v2(posX,posY));
        //设置实际速度
        this.ring.setSpeed(cc.v2(posX,posY));
    }

    private _touchEndEvent(event) {
        this.dot.setPosition(this.ring.node.getPosition());
        this.ring._speed = 0;
    }
}
