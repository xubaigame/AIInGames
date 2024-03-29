# 游戏中的AI
## 一、寻路算法

### 1. 介绍

寻路算法分为传统寻路算法(TraditionalPathfinding)和A*寻路算法(AStarPathfinding)。

传统寻路算法包括：

- 深度优先搜索算法——BFS
- 广度优先搜索算法——DFS

启发式搜索算法包括A\*寻路算法。该算法综合了[最良优先搜索](https://zh.wikipedia.org/w/index.php?title=最良優先搜索&action=edit&redlink=1)和[Dijkstra算法](https://zh.wikipedia.org/wiki/Dijkstra算法)的优点：在进行启发式搜索提高算法效率的同时，可以保证找到一条最优路径（基于评估函数）。

### 2. 使用方法

**传统寻路算法的使用方法：**

1. 导入要使用UnityPackage或者DLL文件。UnityPackage中的Example文件夹下提供了一个示例程序。
2. BFSSystem和DFSSystem为两个静态工具类，用来进行寻路操作。
3. 在进行寻路前需要调用`InitMap`方法。方法接受两个参数，第一个参数是将游戏地图数字化的二维整形数组，第二个参数是整形类型的障碍物标识，代表地图中不可通过的点在数字地图中的标识。
4. 调用`Search`方法来获得查找的路径。方法返回值位bool类型，表示寻路是否成功。方法接受三个参数，第一个参数是Node类型的对象，代表起始节点。第二个参数是Node类型的对象，代表目标节点。第三个参数是Node的列表，代表寻找到的路径节点，使用ref关键字标识。Node类是工具组已经定义的类，构造方法接受三个参数——X、Y、Value，分别代表横坐标、纵坐标、该位置数字地图中的标识。

**A\*寻路算法的使用方法**

1. 导入要使用UnityPackage或者DLL文件。UnityPackage中的Example文件夹下提供了一个示例程序。
2. 工具中提供了一个基础的地图节点类`AStarNode`，该类可以满足基本简单地图的寻路工作。该类支持用户继承并重写方法来构造自己的节点类型。AStarNode类中的F值属性、当前节点的邻居节点属性、获得G值的方法和获得H值的方法都支持重写。AStarNode类构造方法接受三个参数——X、Y、nodeType，分别代表横坐标、纵坐标、该位置数字地图中的标识。
3. AStarSystem是一个普通类，使用时需要先声明一个该类的对象。
4. 在进行寻路前需要调用`InitMap`方法。方法接受两个参数，第一个参数是将游戏地图数字化的二维整形数组，第二个参数是整形类型的障碍物标识，代表地图中不可通过的点在数字地图中的标识。
5. 调用`FindPath`方法来获得查找的路径。方法返回值位bool类型，表示寻路是否成功。方法接受三个参数，第一个参数是AStarNode类型的对象，代表起始节点。第二个参数是AStarNode类型的对象，代表目标节点。第三个参数是AStarNode的列表，代表寻找到的路径节点，使用ref关键字标识。
6. 在使用用户自定的节点类时，有几个需要注意的地方：
   - `FindPath`方法传递的初始与目标节点的类型必须为用户自定义的节点类型的对象，如果使用默认的AStarNode类型的对象，程序会执行最基础的查询方法。
   - 当用户自定义的节点类型的构造函数存在比默认AStarNode节点类型多的参数时，用户需要重写在自定义的节点类型中重写获得当前节点的邻居节点方法。在该方法中创建自定义的节点类型对象并返回。具体可参考AStarNode类中的NeighborNotes属性，自定义类型中重写该方法时只需要将AStarNode类改为自定义的类型并传递参数即可。

### 3. 注意事项

- BFS与DFS方法为四方向寻路算法，可以根据需要对BFSSystem/DFSSystem的进行更改来支持八方向。
- DFS并不适合作为寻路算法，DFS更适合用来判断图中的连通性。
- 在地图特别庞大时DFS的效率比BFS效率高一些。但是DFS寻找的路径并不是最优的。
- 工具中提供的DFS方法只返回第一条深度搜索到的路径，所以基本不是最优的。使用DFS可以得到最优路径，只需要遍历所有符合的路径并选出最短的路径即可（该方法可以剪枝）。
- A\*寻路算法可以通过自定义实现很神奇的效果，详细可以参考笔者的[密室寻宝](https://github.com/PositiveMumu/TreasureHunt)。其中对A\*寻路算法进行了定制以实现在寻路过程中优先获得路径上的道具的功能。

## 二、有限状态机（Finite-State Machine）

### 1. 介绍

有限状态机Unity维基地址：http://wiki.unity3d.com/index.php/Finite_State_Machine。

FSM ，如其名有限状态机，就是说啊这是一个可以枚举出有限个状态，并且这些个状态在特定条件下能够来回切换的机器。在小游戏里面出现的简单 AI 体验：**`怪物巡逻、怪物追击、目标丢失继续巡逻、发生战斗血量不足逃跑、发生战斗血量为0死亡`**等等，大多出自它手啦！另外FSM的理念又似乎随处可见，细心的你有没有在某一刻发现 Unity 的 Animator 其实就是一个有限状态机呢？

在设计模式中有这样一种思想：

> 细节依赖抽象，抽象不依赖细节，基于抽象编程，让框架先跑起来。

FSM 把上面这句话演绎的淋漓尽致，俨然已经算的上是一个简单的框架了，只要遵循他的规则，你只要写细节实现就行，其他事情全部帮你驱动。FSM 跟 Switch case 语法做的事一样一样的，类比于 Switch 就是将case中的逻辑封装到各个State类型中了。那为啥这样做呢？答案很简单，就是为了方便扩展，如果后期需要加入新状态，只需要继承基类，添加实现就好，不用修改原来的代码，也不用担心什么时候调用啦，这就叫**开闭原则**（开闭原则：对修改关闭对扩展开放）。

Tips：FSM 还是设计模式里的**状态模式**理念的集大成哦。

### 2. 使用方式

1. 导入要使用UnityPackage或者DLL文件。UnityPackage中的Example文件夹下提供了一个示例程序。
2. 创建一些状态类，并让他们继承FSMState，在状态类中重写`Action`方法（负责执行当前状态的操作）和`Reason`方法（负责判断并切换状态）。
3. 在需要使用状态机的类中进行如下操作：
   1. 实例化当前状态机管理的状态（通过构造方法传递参数），得到状态对象。
   2. 调用状态对象`AddTransition`方法添加当前状态的转换条件和转换后的状态的名称。方法接受两个参数：第一个参数String类型，表示转换条件；第二个参数String类型，表示在当前状态在转换条件下要转换到的状态的名称。
   3. 实例化状态机管理类FSMSystem，得到状态机对象。
   4. 调用状态机对象的`AddState`方法向状态机中添加管理的状态，第一个添加的状态会被设置成当前状态。方法接受一个参数，类型为FSMState，表示要管理的状态。
   5. 在需要地方可以调用状态机对象的`PerformTransition`方法来转换当前状态机执行的状态。方法接受一个String类型的参数，表示转换条件。
   6. 在Update函数中调用状态机对象的`UpdateMethod`方法，状态机开始运行。

### 3. 注意事项

1. 由于笔者提供的是一个小型框架，所以转换状态和状态名称都定义为String类型，在调用时可能会因为参数传入错误导致状态机运行错误。有两个解决方法：
   1. 在维基中提供的代码中将转换状态和状态名称都定义为枚举类型，可以有效的解决这个问题。
   2. 单独定义一个保存静态数据的类，将转换状态和状态名称定义为静态变量，在使用时直接使用变量，可以有效的解决这个问题。
2. 关于状态类中的数据，可以通过状态类的构造方法传递，也可以通过数据管理器、单例类等方法获得需要操作的数据。
3. 状态对象中的`RemoveTransition`方法可以删除转换状态和转换到的状态。方法接受一个String类型的参数，表示要删除的转换条件。
4. 状态机对象的`DeleteState`方法可以删除管理的状态。方法接受一个String类型的参数，表示要删除的转换条件的名称。
5. 状态机对象的`RevokeTransition`方法可以跳转回上一个状态（当前状态会变成上一个状态）。方法不需要参数。

### 4. 示例程序说明

示例程序与维基代码提供的基本相同。敌人维护一个状态机，状态机管理巡逻状态和追踪状态。敌人初始状态为沿着路径点移动的巡逻状态，当周围一定范围内出现玩家控制的角色时敌人会追踪玩家，当玩家原理敌人移动距离后敌人会切换回巡逻状态。玩家通过W、A、S、D控制移动。

## 三、分层有限状态机（Hierarchical Finite-State Machine）

### 1. 介绍

当FSM管理的状态太多的时候，不好维护。于是将状态分类，抽离出来，将同类型的状态做为一个状态机，然后再做一个大的状态机，来维护这些子状态机。框架中提供的为二层状态机，更多层次的状态机可以参考本程序的框架思路。

本框架为分层有限状态机和下压式有限状态机的组合。大状态机中管理小状态机的方式是栈，当转换状态时正在执行的子状态机会被压栈。新转换的状态机会执行。当当前状态机执行结束后可以转到新的子状态机，也可以结束执行，栈中上一状态的子状态机会出栈并继续执行。

详情参考示例程序。

### 2. 使用方式

1. 导入要使用UnityPackage或者DLL文件。UnityPackage中的Example文件夹下提供了一个示例程序。
2. 创建一些状态类，并让他们继承HFSMState，在状态类中重写`Action`方法（负责执行当前状态的操作）和`Reason`方法（负责判断并切换状态）。
3. 创建一些子状态机类，并让他们继承HFSMBaseSystem，可以在子类中添加子状态机特有参数、重写`UpdateMethod`方法。如果以上操作都不需要，则直接使用HFSMBaseSystem类即可。
4. 在需要使用状态机的类中进行如下操作：
   1. 实例化子状态机管理的状态（通过构造方法传递参数），得到状态对象。
   2. 调用状态对象`AddTransition`方法添加当前状态的转换条件和转换后的状态的名称。方法接受两个参数：第一个参数String类型，表示转换条件；第二个参数String类型，表示在当前状态在转换条件下要转换到的状态的名称。
   3. 实例化子状态机类，得到状态机对象。
   4. 调用子状态机对象的`AddState`方法向状态机中添加管理的状态，第一个添加的状态会被设置成当前状态。方法接受一个参数，类型为FSMState，表示要管理的状态。
   5. 实例化状态机管理类HFSMManagerSystem，得到管理状态机对象。
   6. 在需要地方可以调用子状态机对象的`PerformTransition`方法来转换当前状态机执行的状态。方法接受一个String类型的参数，表示转换条件。
   7. 在需要的地方可以调用管理状态机对象的`ChangeSystem`方法来切换子状态机。方法接受两个参数，第一个参数String类型，表示要转换的子状态机的名称，第二个可选参数为转换状态，代表切换到子状态机后，子状态机进行内部状态转移的名称，不输入即为不转换。
   8. 在Update函数中调用管理状态机对象的`UpdateMethod`方法，状态机开始运行。

### 3. 注意事项

1. 由于笔者提供的是一个小型框架，所以转换状态和状态名称都定义为String类型，在调用时可能会因为参数传入错误导致状态机运行错误。有两个解决方法：
   1. 在维基中提供的代码中将转换状态和状态名称都定义为枚举类型，可以有效的解决这个问题。
   2. 单独定义一个保存静态数据的类，将转换状态和状态名称定义为静态变量，在使用时直接使用变量，可以有效的解决这个问题。
2. 关于状态类中的数据，可以通过状态类的构造方法传递，也可以通过数据管理器、单例类等方法获得需要操作的数据。
3. 子类状态机对象中的`RemoveTransition`方法可以删除转换状态和转换到的状态。方法接受一个String类型的参数，表示要删除的转换条件。
4. 子类状态机对象的`DeleteState`方法可以删除管理的状态。方法接受一个String类型的参数，表示要删除的转换条件的名称。
5. 子类状态机对象的`RevokeTransition`方法可以跳转回上一个状态（当前状态会变成上一个状态）。方法不需要参数。
6. 管理状态机对象的`QuitCurrentState`方法可退出当前子状态机，继续执行上一个子状态机。方法接受一个可选的String类型的参数，代表切换到子状态机后，子状态机进行内部状态转移的名称。不输入即为不转换。

### 4. 示例程序

存在两个子状态机：家中子状态机，管理读书、做饭和睡觉三个状态；超时子状态机管理购物和付款状态，大的状态机管理两个子状态机。

两个子状态机都由时间驱动：

1. 家中子状态机前两秒为读书状态，再三秒为做饭状态，后十秒为睡觉状态。之后再转到读书状态。做饭时需要用到食盐，如果当前食盐不够会转到超市子状态机进行购物。
2. 超市子状态机两秒为购物状态，后三秒为付款状态。付款后会转回家中子状态机，在转回家中子状态机之前会采购一些食盐。

## 四、行为树（Behavior Tree）

### 1. 介绍

在设计游戏AI的时候，我们的目标就是找到一个简单，可扩展的编辑逻辑的方案，从而加速游戏开发的迭代速度。这里以士兵为例子，假设士兵有**空闲、战斗、逃跑**三种状态，状态机（FSM）是最先映入脑海的方案。但是随着开发进行，状态一多，状态机维护起来就没那么轻松了，**状态机**之间的转换线有如脱缰野马，驾驭不住。比如再添加个躲藏和返回起点状态，那么连线就会越来越复杂。所以前人大佬们设计出了**行为树**的抽象来解决这个难题。

行为树，英文是Behavior Tree，简称BT，是一棵用于控制 AI 决策行为的、包含了层级节点的树结构。当我们要决策当前这个士兵要做什么样的行为的时候，**我们就会自顶向下的，通过一些条件来搜索这颗树，最终确定需要做的行为（叶节点），并且执行它**，这就是行为树的基本原理。

行为树主要由以下四种节点抽象而成**组合节点、装饰节点、条件节点、行为节点。**

①**组合节点（Composites）**

> 主要包含：Sequence顺序条件，Selector选择条件，Parallel平行条件以及他们之间相互组合的条件。

**②修饰节点（Decorator）**

> 连接树叶的树枝，就是各种类型的修饰节点，这些节点决定了 AI 如何从树的顶端根据不同的情况，来沿着不同的路径来到最终的叶子这一过程。
> 如让子节点循环操作（LOOP）或者让子task一直运行直到其返回某个运行状态值（Util），或者将task的返回值取反（NOT）等等

**③条件节点（Conditinals）**

> 用于判断某条件是否成立。目前看来，是Behavior Designer为了贯彻职责单一的原则，将判断专门作为一个节点独立处理，比如判断某目标是否在视野内，其实在攻击的Action里面也可以写，但是这样Action就不单一了，不利于视野判断处理的复用。一般条件节点出现在Sequence控制节点中，其后紧跟条件成立后的Action节点。

**④行为节点（Action）**

> 行为节点是真正做事的节点，行为节点在树的最末端，都是叶子节点，就是这些 AI 实际上去做事情的命令；

通过使用行为树内的节点之间的关联来驱动角色的行为，比直接用具体的代码告诉一个角色去做什么事情，要来得有意思得多，这也是行为树最让人兴奋的一点。这样我们只要抽象好行为，就不用去理会战斗中具体发生了什么。

### 2. 使用方式

本项目中提供的实现太过简单，仅作为读者理解行为树的简单参考。强烈推荐使用[Behavior Designer插件](https://assetstore.unity.com/packages/tools/visual-scripting/behavior-designer-behavior-trees-for-everyone-15277?locale=zh-CN)，或者参考伍一峰大佬的[BT-Framework](https://github.com/f15gdsy/BT-Framework)项目。

### 3.注意事项

行为树仍然存在许多问题，例如：

1. nontrivial overhead，尤其是树比较深得时候。
2. 不适合响应式的AI，写着写着就会退化成扁平的有大量predicate的树，比FSM还难管理。

等等。

有时，我们需要根据项目的实际情况来判断那种方式更好。

请务必记住：**没用最完美的方法，只有最合适的方法**。

## 五、行为设计插件（Behavior Designer）

### 1. 介绍

Behavior Designer是Unity引擎下的行为树插件，它的主要作用并非可以不写代码（还是要写不少代码的），而是能让游戏中逻辑最混乱的模块——AI模块能更有序的组织，方便查看、调试和修改。具体有关Behavior Designer插件的介绍可以参考示例中的官方文档（虽然是英文，但非常通俗易懂。放在翻译软件直接翻译大概就能看懂）。

### 2. 示例

项目中提供的示例是一个夺旗战的小游戏（CTF）。共分为两个阵营：

- 夺旗者
- 守旗者
  - 站桩守卫者
  - 巡逻守卫者

夺旗者触碰旗帜时会拾取旗帜。防守者会主动追击夺旗者，触碰夺旗者时会消灭夺旗者。夺旗者会在出生点重生。

夺旗的行为逻辑如下：

1. 抢夺到旗帜前：如果看到守卫者，远离守卫者。如果没看到守卫者，向旗帜移动，抢夺旗帜。
2. 抢夺到旗帜后：如果旗帜在身上，向终点出发。如果旗帜不在身上，向旗帜移动。

站桩防守的行为逻辑如下：

1. 旗帜被夺走之前：如果看到夺旗者，追逐夺旗者。如果没看到夺旗者，移动至旗帜周围指定位置后保持静止。
2. 旗帜被夺走后：追逐旗帜。

巡逻防守的行为逻辑如下：

1. 旗帜被夺走之前：如果看到夺旗者，追逐夺旗者。如果没看到夺旗者，按照指定路径进行巡逻。
2. 旗帜被夺走后：追逐旗帜。

运行项目可以看到模拟结果。

## 六、遗传算法与神经网络

### 1. 介绍

#### 遗传算法

遗传算法的详细描述可以参考参考博文：https://blog.csdn.net/u010451580/article/details/51178225。

#### 神经网络

神经网络基本模型可以参考博文：https://www.cnblogs.com/subconscious/p/5058741.html。但理解相对复杂。

后面笔者也会自己整理详细的笔记，尽量简单的介绍两个算法的特点以及他们如何联合工作。

### 2. 示例

示例程序参考地址：https://github.com/trulyspinach/Unity-Neural-Network-Tanks-AI。该程序的目的是训练坦克。

运行程序：初始时，因为所有数据随机给出，坦克随机行动。经过几代的迭代后，坦克逐渐掌握了消灭敌人的方法。

## 七、拓展技术

除了上面介绍的一些方法外，还有许多零碎的知识点，在设计游戏的非玩家控制角色时可以考虑的地方：

1. 视野：设置物体能看见的视野范围，范围外的物体无法被看见。

2. 听觉：敌人可以根据声音判断玩家位置并进行追击。

3. 语言：在不同状态下令不同角色发出相应的提示语音来推动游戏的进行。

4. 行为：角色的不同状态会产生不同的行为（类似于王者荣耀中的梦奇）。

5. Unity与TensorFlow的结合：ml-agents。详细信息可以参考https://github.com/Unity-Technologies/ml-agents。