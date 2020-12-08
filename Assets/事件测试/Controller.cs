
using UnityEngine;

public enum DamageType 
{ 
    Normal,//普通伤害
    Critical//暴击伤害
}

public enum HpShowType
{
    Null,//不显示
    Damage,//伤害
    Miss//闪避
}
public class Controller : MonoBehaviour
{
    private BaseUnit unit;
    private BattleInformationComponent Battle;
    private void Start()
    {
        this.unit = BaseUnit.getIns();
        this.Battle = new BattleInformationComponent();
        this.Battle.AddListener();
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(880, 540, 150, 100), "攻击测试"))
            this.unit.BeAttacked();
    }
    }
