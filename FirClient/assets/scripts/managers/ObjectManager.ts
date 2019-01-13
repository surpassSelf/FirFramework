import Behaviour from "../common/Behaviour";
import GameObjectPool from "../components/objectpool/GameObjectPool";
import ObjectPool from "../components/objectpool/ObjectPool";

export default class ObjectManager extends Behaviour implements IManager {
    private objPoolMaps: {[key: string]: GameObjectPool};
    private clsPoolMaps: {[key: string]: any};

    public initialize(): void {
        this.objPoolMaps = { };
        this.clsPoolMaps = { };
    }    

    ///创建对象池
    public createObjPool(poolName, poolSize, maxSize, parent, poolPrefab) {
        var pool = this.objPoolMaps[poolName];
        if (pool == null) {
            pool = new GameObjectPool(poolName, poolSize, maxSize, parent, poolPrefab);
            this.objPoolMaps[poolName] = pool;
        }
    }

    ///获取对象
    public getObject(poolName:string): any {
        var pool = this.objPoolMaps[poolName];
        if (pool == null) {
            return null;
        }
        return pool.nextAvailableObject();
    }

    ///释放对象
    public relObject(poolName:string, go:any): void {
        var pool = this.objPoolMaps[poolName];
        if (pool == null) {
            return null;
        }
        pool.returnObjectToPool(poolName, go);
    }
    
    ///-------------------------------------------------------------------------------
    ///创建对象池
    public createClassPool(classType) {
        var name = classType.name;
        var pool = this.clsPoolMaps[name];
        if (pool == null) {
            pool = new ObjectPool(classType);
            this.clsPoolMaps[name] = pool;
        }
    }

    ///获取实例
    public getInstance(classType): any {
        var name = classType.name;
        var pool = this.clsPoolMaps[name];
        if (pool == null) {
            return null;
        }
        return pool.get();
    }

    ///释放实例
    public relInstance(obj) {
        var name = typeof(obj);
        var pool = this.clsPoolMaps[name];
        if (pool == null) {
            return;
        }
        pool.release(obj);
    }

    public dispose(): void {
    }
}
