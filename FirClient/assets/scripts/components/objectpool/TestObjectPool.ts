import ObjectPool from "./ObjectPool";

class MyClass {
    public something:string;
    constructor() {
        MyClass.reset(this);
    }
    static reset(obj: MyClass) {
        obj.something = null;
    }
}

export default class TestObjectPool {
    public testOne(): void {
        var pool = new ObjectPool(MyClass);
      
        const obj1 = pool.get(); // returns new MyClass
        const obj2 = pool.get(); // returns new MyClass
        
        pool.release(obj1); // reset() is automatically called here
        
        const obj3 = pool.get(); // obj3 is now identical to obj1
    }
}
