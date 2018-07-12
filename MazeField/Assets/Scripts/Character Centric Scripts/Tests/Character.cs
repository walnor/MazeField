using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Interactable, Attackable
{
    public GameObject gameObject;
    public GameObject m_CharImage;

    public CharEvents m_Events = new CharEvents();

    public Stats m_Stats;

    public float attackTimer = 0.0f;
    public float m_AttackRange = 1.0f;
    public float m_AttackRate = 1.0f;

    public float m_MovementSpeed = 5.0f;

    public Vector3 m_Destination;
    public Vector3 previousPos;

    public Attackable m_target = null;

    public int GroupId = 0;

    public Skill_Mode m_Mode = null;

    public DamageAble m_damage = null;

    DisplayAttackLog m_attLog = null;
    public void Update()
    {
        try
        {
            m_Mode.Init(this);
        }
        catch (Exception) { Debug.Log("Here!"); }

        if (m_damage == null || m_damage.m_Stats == null)
            m_Events.MissingDamageComponent = true;

        if (m_Stats == null)
            m_Events.MissingStats = true;

        if (m_attLog == null)
            m_Events.MissingDamageLogComponent = true;

        m_Mode.Update();
    }

    public void GiveLog(DisplayAttackLog log)
    {
        m_attLog = log;
        m_Events.MissingDamageLogComponent = false;
    }

    public void DirectPosition(Vector2 pos)
    {
        m_target = null;

        m_Destination = pos;
    }

    public bool hit(ref float[] results, float power = 5, float accuracy = 50, float criticalChance = 0)
    {
        bool toReturn = m_Mode.Hit(ref results, power, accuracy, criticalChance);

        if (m_attLog != null)
               m_attLog.GiveAttackResults(results);

        return toReturn;

    }

    public bool isEnemyAffiliated(int group)
    {
        if (group != GroupId) return true; else return false;
    }

    public void Select()
    {
        Debug.Log("Hello, Interactable is working!");
    }

    public GameObject SelectAsTarget()
    {
        return gameObject;
    }

    public void Target(GameObject other)
    {
        m_target = other.GetComponent<Attackable>();
    }


    public void Init(GameObject owner,GameObject img, int GID = 0, Skill_Mode SM = null, DamageAble DA = null, DisplayAttackLog log = null)
    {
        gameObject = owner;
        m_CharImage = img;
        
        m_Destination = gameObject.transform.position;
        previousPos = gameObject.transform.position;

        GroupId = GID;

        m_Mode = SM;
        if(m_Mode != null)
            m_Mode.Init(this);

        if (DA != null)
            m_damage = DA;
        else
            m_damage = gameObject.GetComponent<DamageAble>();

        if (m_damage)
            m_damage.Init(m_Stats);

        if (log != null)
            m_attLog = log;
        else
            m_attLog = gameObject.GetComponent<DisplayAttackLog>();
    }

    public void ChangeMode(Skill_Mode mode)
    {
        m_Mode = mode;

        m_Destination = gameObject.transform.position;
    }

    public void Move(Vector3 pos)
    {
        previousPos = gameObject.transform.position;
        gameObject.transform.position = pos;


        Vector3 vectorToTarget = previousPos - gameObject.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

        m_CharImage.transform.rotation = q;
    }

    public void GiveStats(Stats s)
    {
        m_Stats = s;
        if (m_damage)
            m_damage.Init(m_Stats);
    }

    public void OnAttack()
    {
    }
}