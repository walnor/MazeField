using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, Attackable
{
    public Character m_Char = new Character();
    public GameObject m_Image;

    public GameObject m_Target;

    public DisplayAttackLog m_attLog;

    public HP_Bar m_healthBar;

    public bool Generated = true;
    public bool Run = false;

    // Use this for initialization
    void Start()
    {
        if (!Generated)
            Init();
    }
    public void Init()
    {
        try
        {
            m_Char.Init(gameObject, m_Image, 1, new SM_Defend(), gameObject.GetComponent<DamageAble>(), m_attLog);
        }
        catch (Exception) { }

        StatFactory sf = gameObject.GetComponent<StatFactory>();
        if (sf)
        {
            m_Char.GiveStats(sf.Get());
        }
        Run = true;
    }

    void GiveStats()
    {
        Debug.Log("Give!");
        StatFactory sf = gameObject.GetComponent<StatFactory>();
        if (sf)
        {
            m_Char.GiveStats(sf.Get());
        }
        m_Char.m_Events.MissingStats = false;
    }

    void GiveDMG()
    {
        Debug.Log("Give! DMG");

        DamageAble dmg = gameObject.GetComponent<DamageAble>();
        StatFactory sf = gameObject.GetComponent<StatFactory>();
        dmg.m_Stats = sf.Get();

        m_Char.m_damage = dmg;
        m_Char.m_Events.MissingDamageComponent = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Run)
            return;

        if (m_Char.m_Events.MissingDamageComponent)
            GiveDMG();
        if (m_Char.m_Events.MissingStats)
            GiveStats();
        if (m_Char.m_Events.MissingDamageLogComponent)
            m_Char.GiveLog(m_attLog);

        if (PlayerInRange())
        {
            m_Char.Target(m_Target);
        }

        m_Char.Update();

        m_healthBar.GivePercent(m_Char.m_damage.GetDamagePercent());
    }

    bool PlayerInRange()
    {
        if(m_Target)
        return (m_Target.transform.position - transform.position).magnitude < 5.0f;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        m_Char.Move(m_Char.previousPos);
        m_Char.m_Destination = m_Char.previousPos;
        m_Char.m_target = null;
    }

    public bool isEnemyAffiliated(int group)
    {
        return m_Char.isEnemyAffiliated(group);
    }

    public GameObject SelectAsTarget()
    {
        return m_Char.SelectAsTarget();
    }

    public bool hit(ref float[] results, float power = 5, float accuracy = 50, float criticalChance = 0)
    {
        return m_Char.hit(ref results, power, accuracy, criticalChance);
    }
}
