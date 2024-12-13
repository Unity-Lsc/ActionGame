using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗管理器
/// </summary>
public class BattleManager : Singleton<BattleManager>
{
    /// <summary>
    /// 当前战斗类
    /// </summary>
    public Battle CurBattle { get; private set; }

    /// <summary>
    /// 创建战斗类
    /// </summary>
    /// <returns></returns>
    public Battle CreateBattle() {
        Battle battle = new Battle();
        battle.OnCreate();
        CurBattle = battle;
        return CurBattle;
    }

    public static void OnUpdate() {
        Instance.CurBattle?.OnUpdate();
    }

    /// <summary>
    /// 释放当前战斗类
    /// </summary>
    public void OnRelease() {
        CurBattle.OnDestroy();
        CurBattle = null;
    }

}
