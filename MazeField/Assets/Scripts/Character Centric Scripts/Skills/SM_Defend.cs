﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SM_Defend : Skill_Mode
{
    Character m_Owner;

    float[] P;

    public void Init(Character owner)
    {
        m_Owner = owner;

        P = m_Owner.m_Stats.getBaseCloseAttackPower();

        P[0] *= 1f;
        P[1] *= 1f;
    }

    public void Update()
    {
        if (m_Owner.m_target != null)
            State_AttackEnemy();
        else
            State_DestinationMove();
    }

    int Skill_Mode.GetType()
    {
        return 0;
    }

    void State_AttackEnemy()
    {
        Vector3 enemyPos = Vector3.zero;
        try
        {
            enemyPos = m_Owner.m_target.SelectAsTarget().transform.position;
        }
        catch (Exception)
        {
            m_Owner.m_target = null;

            m_Owner.m_Destination = m_Owner.gameObject.transform.position;
        }
        m_Owner.attackTimer += (Time.deltaTime * 0.5f);

        if ((enemyPos - m_Owner.gameObject.transform.position).magnitude > m_Owner.m_AttackRange)
        {
            Vector3 newPos = Vector3.Lerp(m_Owner.gameObject.transform.position, enemyPos, Time.deltaTime * (m_Owner.m_MovementSpeed * 0.5f));

            Vector3 direction = newPos - m_Owner.gameObject.transform.position;
            direction.Normalize();

            newPos = m_Owner.gameObject.transform.position + direction * Time.deltaTime * (m_Owner.m_MovementSpeed * 0.5f);

            m_Owner.Move(newPos);
        }
        else
        {
            if (m_Owner.attackTimer >= m_Owner.m_AttackRate)
            {
                float[] AttackResults = new float[1];

                m_Owner.m_target.hit(ref AttackResults, power: UnityEngine.Random.Range(P[0], P[1]), accuracy: m_Owner.m_Stats.getBaseAccuracy(), criticalChance: m_Owner.m_Stats.getBaseCriticalChance());

                m_Owner.attackTimer = 0.0f;
            }
            else
            if (m_Owner.attackTimer + 0.3f >= m_Owner.m_AttackRate)
            {
                m_Owner.m_Events.MeleeAttack = true;
            }
        }
    }

    void State_DestinationMove()
    {
        if ((m_Owner.m_Destination - m_Owner.gameObject.transform.position).magnitude > 0.3f)
        {
            Vector3 newPos = Vector3.Lerp(m_Owner.gameObject.transform.position, m_Owner.m_Destination, Time.deltaTime * (m_Owner.m_MovementSpeed * 0.5f));

            Vector3 direction = newPos - m_Owner.gameObject.transform.position;
            direction.Normalize();

            newPos = m_Owner.gameObject.transform.position + direction * Time.deltaTime * (m_Owner.m_MovementSpeed * 0.5f);

            m_Owner.Move(newPos);
        }
    }
    public bool Hit(ref float[] results, float power = 5, float accuracy = 50, float criticalChance = 0)
    {
        return m_Owner.m_damage.hit(ref results, power, accuracy, criticalChance,1.35f,0,1,10);
    }

    public void UpdateStats()
    {
        P = m_Owner.m_Stats.getBaseCloseAttackPower();

        P[0] *= 1f;
        P[1] *= 1f;
    }
}
