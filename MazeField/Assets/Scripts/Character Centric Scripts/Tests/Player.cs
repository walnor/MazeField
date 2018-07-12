using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Interactable, Attackable
{
    public Character m_Char = new Character();
    public GameObject m_Image;

    public AttackAni_Sword m_Sword;

    public DisplayAttackLog m_attLog;

    public HP_Bar m_healthBar;

    SM_Attack m_Attack;
    SM_Defend m_Defend;
    SM_Ranged m_Ranged;
    SM_Flee m_Flee;


    public void DirectPosition(Vector2 pos)
    {
        m_Char.DirectPosition(pos);
    }

    public void Select()
    {
        m_Char.Select();
    }

    public void Target(GameObject other)
    {
        m_Char.Target(other);
    }

    // Use this for initialization
    void Start ()
    {
        m_Attack = new SM_Attack();
        m_Defend = new SM_Defend();
        m_Ranged = new SM_Ranged();
        m_Flee = new SM_Flee();

        try
        {
            m_Attack.Init(m_Char);
            m_Defend.Init(m_Char);
            m_Ranged.Init(m_Char);
            m_Flee.Init(m_Char);
        }
        catch (Exception) { Debug.Log("here!"); }

        m_Char.Init(gameObject, m_Image,0, m_Attack, gameObject.GetComponent<DamageAble>(), m_attLog);        
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
    void Update ()
    {
        if (m_Char.m_Events.MissingDamageComponent)
            GiveDMG();
        if (m_Char.m_Events.MissingStats)
            GiveStats();
        if (m_Char.m_Events.MissingDamageLogComponent)
            m_Char.GiveLog(m_attLog);

        m_Char.Update();

        if (m_Char.m_Events.MeleeAttack)
        {
            m_Sword.Swing();
            m_Char.m_Events.MeleeAttack = false;
        }

        m_healthBar.GivePercent(m_Char.m_damage.GetDamagePercent());
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

    public void ModeSelection(int i)
    {
        if (i == 0)
        {
            Debug.Log("Attack Mode");
            m_Char.ChangeMode(m_Attack);
            m_Char.m_AttackRange = 1.0f;
        }
        else
        if (i == 1)
        {
            Debug.Log("Defense Mode");
            m_Char.ChangeMode(m_Defend);
            m_Char.m_AttackRange = 0.7f;
        }
        else
        if (i == 2)
        {
            Debug.Log("Ranged Mode");
            m_Char.ChangeMode(m_Ranged);
            m_Char.m_AttackRange = 8.0f;
        }
        else
        if (i == 3)
        {
            Debug.Log("Flee Mode");
            m_Char.ChangeMode(m_Flee);
        }

    }
}
