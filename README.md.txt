# 角色控制部分

这部分要注意和动画的联动，最好也采用状态机。













# 参考

## unity

### MonoBehaviour

- start函数：在游戏对象开始存在时调用

- update：每帧都调用
  - 准确的说是在每一帧渲染之前调用

- fixedupdate：每个物理时间调用
  - 在固定时间间隔内调用，可能在一帧之内被调用多次，确保物理计算的精确性
- lateupdate
  - 在所有的update的函数执行完毕后调用，通常用于处理相机跟随等需要在所有更新逻辑之后执行的操作

- OnCollisionEnter,OnTriggerEnter 在发生物理碰撞或者触发时调用

- OnDestroy：在销毁对象时调用

### 动画

[动画状态机 - Unity 手册 (unity3d.com)](https://docs.unity3d.com/cn/2022.3/Manual/AnimationStateMachines.html)

