# CombatSystemDemo

* untiy版本：2021.3.6f1c1
* 实现了一个RPG战斗系统，通过有限状态机的形式去实现玩家和敌人的基本功能，并通过ScriptableObject进行数据驱动完成任务配置
* 将各部分逻辑分层，例如Player的Model、Audio、Input层，以及各种状态继承自StateBase，最后通过继承自FSMController的Player_Controller调用这些逻辑层，实现具体逻辑，Enemy也同理
