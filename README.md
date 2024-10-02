# 角色

## 角色控制部分

这部分要注意和动画的联动，最好也采用状态机。

## Player



### 背包系统

使用动态数组作为底层的数据结构，存储Item类即可。



### UI

显示player的HP，MP，持有道具等。



## Enemy，NPC

考虑player和怪，NPC的互动



# Item

采用Component，组件的方式来简化实现。

Component是通过组合来实现复杂功能的一种设计方式。

所有的具体的道具从Item类派生。

Item类的功能由ItemFunction类的子类实现。

## ItemFunction

ItemFunction作为父类，从该类派生出实现具体功能的子类。



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

