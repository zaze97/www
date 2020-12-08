using UnityEngine;

public class BaseUnit 
{

    /// <summary>
    /// 委托任务
    /// </summary>
    /// <param name="source">BaseUnit</param>
    /// <param name="subHp"></param>
    /// <param name="damageType"></param>
    /// <param name="showType"></param>
    public delegate void SubHpHandler(BaseUnit source, float subHp, DamageType damageType, HpShowType showType);
    public event SubHpHandler OnSubHp;
    static BaseUnit Ins = null;
    public static BaseUnit getIns()
    {
        if (Ins == null)
        {
            Ins = new BaseUnit();
        }
        return Ins;

    }
    public void BeAttacked()
    {
        float possibility = UnityEngine.Random.value;
        bool isCritical = UnityEngine.Random.value > 0.5f;
        bool isMiss = UnityEngine.Random.value > 0.5f;
        float harmNumber = 10000f;

        OnBeAttacked(harmNumber, isCritical, isMiss);
    }
    /// <summary>
    /// 随机判定什么类型伤害或是否闪避
    /// </summary>
    /// <param name="harmNumber">表示伤害数字的float型的harmNumber</param>
    /// <param name="isCritical">表示是否是暴击伤害的bool型的isCritical</param>
    /// <param name="isMiss">表示是否是闪避的bool型isMiss</param>
    private void OnBeAttacked(float harmNumber, bool isCritical, bool isMiss)
    {
        DamageType damageType = DamageType.Normal;
        HpShowType showType = HpShowType.Damage;
        if (isCritical)
            damageType = DamageType.Critical;
        if (isMiss)
            showType = HpShowType.Miss;

        //首先判断是否有方法订阅了该事件，如果有则通知他们

        if (OnSubHp != null)
            OnSubHp(this, harmNumber, damageType, showType);
    }

    public bool IsHero
    {
        get
        {
            return true;
        }
    }
}
