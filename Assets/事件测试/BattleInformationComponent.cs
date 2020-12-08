using UnityEngine;

/// <summary>
/// 添加事件或取消事件，观察者模式自己调用别人
/// </summary>
public class BattleInformationComponent 
{
    public BaseUnit unit;

    //private void Start()
    //{
    //    //this.unit = gameObject.GetComponent<BaseUnit>();
    //    this.unit = BaseUnit.getIns();
    //    this.AddListener();
    //}
    //订阅BaseUnit定义的事件OnSubHp
    public void AddListener()
    {
        //构造BaseUnit.SubHpHandler 委托类型的一个实例
        //并且让它来引用BattleInformatiionComponent类
        //OnSubHp方法，之后向BaseUnit的OnSubHp事件登记
        //该回调方法
        this.unit = BaseUnit.getIns();
        this.unit.OnSubHp += new BaseUnit.SubHpHandler(this.OnSubHp);//创建委托对象
    }
    /// <summary>
    /// 取消对BaseUnit定义的事件OnSubHp的订阅
    /// </summary>
    private void RemoveListener()
    {
        //注销关注
        this.unit.OnSubHp -= new BaseUnit.SubHpHandler(this.OnSubHp);//创建委托对象
    }
    /// <summary>
    /// 当BaseUnit被攻击时，会调用该回调事件
    /// </summary>
    private void OnSubHp(BaseUnit source, float subHp, DamageType damageType, HpShowType showType)
    {
        string unitName = string.Empty;//string.Empty在内存堆里分配空间 ""在内存栈里分配空间
        string missStr = "闪避";
        string damageTypeStr = string.Empty;
        string damageHp = string.Empty;
        if (showType == HpShowType.Miss)
        {
            Debug.Log(missStr);
            return;
        }

        if (source.IsHero)
        {
            unitName = "英雄";
        }
        else
        {
            unitName = "士兵";
        }
        damageTypeStr = damageType == DamageType.Critical ? "暴击" : "普通攻击";
        damageHp = subHp.ToString();
        Debug.Log(unitName + damageTypeStr + damageHp);
    }
}
