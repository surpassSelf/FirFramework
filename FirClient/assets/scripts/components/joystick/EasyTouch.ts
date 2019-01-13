import { JoyDefine } from "./JoyDefine";

const {ccclass, property} = cc._decorator;

@ccclass
export default class EasyTouch extends cc.Component {
    @property({type: cc.Node, displayName: "摇杆节点"})
    dot: cc.Node = null;
    
    @property({displayName:'joy Node'})
    _joyCom =  null;

    @property({displayName: '被操作的目标Node'})
    _playerNode = null;

    @property({displayName: '当前触摸的角度'})
    _angle = null;

    @property({displayName: '弧度'})
    _radian = null;

    _speed = 0;          //实际速度
    _speed1 = 1;         //一段速度
    _speed2 = 2;         //二段速度
    _opacity = 0;        //透明度

    public onLoad() {
        this._joyCom = this.node.parent.getComponent('Joystick');
        this._playerNode = this._joyCom.sprite;
        if (this._joyCom.touchType == JoyDefine.TouchType.DEFAULT) {
            this._initTouchEvent();
        }
    }

    public update(dt) {
        switch(this._joyCom.directionType) {
            case JoyDefine.DirectionType.ALL:
                this._allDirectionMove();
            break;
            default: break;
        }
    }

    private _allDirectionMove() {
        if (this._playerNode != null) {
            this._playerNode.x += Math.cos(this._angle * (Math.PI/180)) * this._speed;
            this._playerNode.y += Math.sin(this._angle * (Math.PI/180)) * this._speed;
        }
    }

    private _initTouchEvent() {
        this.node.on(cc.Node.EventType.TOUCH_START, this._touchStartEvent, this);
        this.node.on(cc.Node.EventType.TOUCH_MOVE, this._touchMoveEvent, this);
        this.node.on(cc.Node.EventType.TOUCH_END, this._touchEndEvent, this);
        this.node.on(cc.Node.EventType.TOUCH_CANCEL, this._touchEndEvent, this);
    }

    private _touchStartEvent(event) {
        // 获取触摸位置的世界坐标转换成圆圈的相对坐标（以圆圈的锚点为基准）
        var touchPos = this.node.convertToNodeSpaceAR(event.getLocation());
        //触摸点与圆圈中心的距离
        var distance = this.getDistance(touchPos, cc.v2(0,0));
        
        //圆圈半径
        var radius = this.node.width / 2;
        // 记录摇杆位置，给touch move使用
        
        var posX = this.node.getPosition().x + touchPos.x;
        var posY = this.node.getPosition().y + touchPos.y;
            //手指在圆圈内触摸,控杆跟随触摸点
        if(radius > distance)
        {
            this.dot.setPosition(cc.v2(posX, posY));
            return true;
        }
        return false;
    }

    private _touchMoveEvent(event) {
        var touchPos = this.node.convertToNodeSpaceAR(event.getLocation());
        var distance = this.getDistance(touchPos, cc.v2(0,0));
        var radius = this.node.width / 2;
        // 由于摇杆的postion是以父节点为锚点，所以定位要加上ring和dot当前的位置(stickX,stickY)
        var posX = this.node.getPosition().x + touchPos.x;
        var posY = this.node.getPosition().y + touchPos.y;
        if(radius > distance)
        {
            this.dot.setPosition(cc.v2(posX, posY));
        }
        else
        {
            //控杆永远保持在圈内，并在圈内跟随触摸更新角度
            var x = this.node.getPosition().x + Math.cos(this.getRadian(cc.v2(posX,posY))) * radius;
            var y = this.node.getPosition().y + Math.sin(this.getRadian(cc.v2(posX,posY))) * radius;
            this.dot.setPosition(cc.v2(x, y));
        }
        //更新角度
        this.getAngle(cc.v2(posX, posY));
        //设置实际速度
        this.setSpeed(cc.v2(posX, posY));
    }

    private _touchEndEvent(event) {
        this.dot.setPosition(this.node.getPosition());
        this._speed = 0;
    }

    public getDistance(pos1, pos2): number {
        return Math.sqrt(Math.pow(pos1.x - pos2.x, 2) + Math.pow(pos1.y - pos2.y, 2));
    }

    public getRadian(point) {
        this._radian = Math.PI / 180 * this.getAngle(point);
        return this._radian;
    }

    public getAngle(point) {
        var pos = this.node.getPosition();
        this._angle = Math.atan2(point.y - pos.y, point.x - pos.x) * (180 / Math.PI);
        return this._angle;
    }

    public setSpeed(point) {
        //触摸点和遥控杆中心的距离
        var distance = this.getDistance(point, this.node.getPosition());

        //如果半径
        this._speed = this._speed2;
    }
}
