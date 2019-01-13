export default class GameObjectPool {
    private maxSize: number;
    private poolSize: number;
    private poolName: string;
    private poolRoot: cc.Node;
    private poolPrefab: any;
    private currPool:cc.Node[];

    public constructor(poolName, poolSize, maxSize, parent, poolPrefab) {
        this.currPool = [];
        this.maxSize = maxSize;
        this.poolSize = poolSize;
        this.poolName = poolName;
        this.poolRoot = parent;
        this.poolPrefab = poolPrefab;

        var index: number;
        for(index = 0; index < poolSize; index++) {
            this.addObjectToPool(this.newObject());
        }
    }

    private addObjectToPool(go: cc.Node):void {
        if (go == null) {
            return;
        }
        go.active = false;
        go.parent = this.poolRoot;
        this.currPool.push(go);
    }

    private newObject(): any {
        if (this.poolPrefab == null) {
            return null;
        }
        return cc.instantiate(this.poolPrefab);
    }

    public nextAvailableObject(): any {
        if (this.currPool.length == 0) {
            if ((this.poolSize + 1) > this.maxSize) {
                console.error('No object available & cannot grow pool:' + this.poolName);
                return null;
            }
            this.poolSize++;
            this.addObjectToPool(this.newObject());
        }
        var go:cc.Node = this.currPool.pop();
        go.active = true;
        return go;
    }

    public returnObjectToPool(pool:string, go:any): void {
        if (pool != this.poolName) {
            console.error('Trying to add object to incorrect pool:>' + pool);
            return;
        }
        this.addObjectToPool(go);
    }
}
