export default class ObjectPool<T> {
  private pool: T[];
  private Func: ObjectPool.Resettable<T>;

  constructor(Func: ObjectPool.Resettable<T>) {
    this.pool = [];
    this.Func = Func;
  }

  get(): T {
    if (this.pool.length) {
      return this.pool.splice(0, 1)[0];
    }
    return new this.Func();
  }

  release(obj: T): void {
    if (this.Func.reset) {this.Func.reset(obj)}
    this.pool.push(obj);
  }
}

module ObjectPool {
  export interface Resettable<T extends Object> {
    // constructor
    new (): T;
    // static
    reset?(obj: T): void;
  }
}