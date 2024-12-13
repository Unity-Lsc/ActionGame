using FixIntPhysics;
using FixMath;
using SpriteFramework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物逻辑控制
/// </summary>
public class MonsterLogicCtrl
{
    /// <summary>
    /// 模拟地图上的怪物位置信息
    /// </summary>
    private List<Vector3> m_MonsterPosList = new List<Vector3>() {
        new Vector3(-3,0,0),
        new Vector3(3,0,0),
    };

    private int[] m_MonsterIdArr = new int[] { 20001, 20004 };

    public List<MonsterLogic> m_MonsterLogicList = new List<MonsterLogic>();

    /// <summary>
    /// 初始化场景中的怪物
    /// </summary>
    public void OnInit() {
        for (int i = 0; i < m_MonsterIdArr.Length; i++) {
            //生成怪物到场景中
            var monsterObj = UnityUtils.LoadPrefabClone(SFConstDefine.MonsterRoot + m_MonsterIdArr[i]);
            FixIntVector3 initPos = new FixIntVector3(m_MonsterPosList[i]);

            //处理怪物碰撞数据
            BoxColliderGizmo boxInfo = monsterObj.GetComponent<BoxColliderGizmo>();
            boxInfo.enabled = false;

            //创建定点数碰撞体
            FixIntBoxCollider monsterBox = new FixIntBoxCollider(boxInfo.mSize, boxInfo.mConter);
            monsterBox.SetBoxData(boxInfo.mConter, boxInfo.mSize);
            monsterBox.UpdateColliderInfo(initPos, new FixIntVector3(boxInfo.mSize));

            MonsterRender monsterRender = monsterObj.GetComponent<MonsterRender>();
            MonsterLogic monsterLogic = new MonsterLogic(m_MonsterIdArr[i], monsterRender, monsterBox, initPos);
            monsterRender.SetLogicObject(monsterLogic);

            monsterLogic.OnInit();
            m_MonsterLogicList.Add(monsterLogic);
            monsterRender.OnInit();
        }
    }

    public void OnLogicFrameUpdate() {
        for (int i = 0; i < m_MonsterLogicList.Count; i++) {
            m_MonsterLogicList[i].OnLogicFrameUpdate();
        }
    }

}
