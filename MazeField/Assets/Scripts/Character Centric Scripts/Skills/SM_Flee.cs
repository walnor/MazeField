using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_Flee : Skill_Mode
{
    Character m_Owner;

    public void Init(Character owner)
    {
        m_Owner = owner;
    }

    public void Update()
    {
        State_DestinationMove();
    }

    int Skill_Mode.GetType()
    {
        return 0;
    }

    void State_AttackEnemy()
    {
        return;
    }

    void State_DestinationMove()
    {
        if ((m_Owner.m_Destination - m_Owner.gameObject.transform.position).magnitude > 0.3f)
        {
            Vector3 newPos = Vector3.Lerp(m_Owner.gameObject.transform.position, m_Owner.m_Destination, Time.deltaTime * m_Owner.m_MovementSpeed);

            Vector3 direction = newPos - m_Owner.gameObject.transform.position;
            direction.Normalize();

            newPos = m_Owner.gameObject.transform.position + direction * Time.deltaTime * (m_Owner.m_MovementSpeed * 1.8f);

            m_Owner.Move(newPos);
        }
    }

    public bool Hit(ref float[] results, float power = 5, float accuracy = 50, float criticalChance = 0)
    {
        return m_Owner.m_damage.hit(ref results, power, accuracy, criticalChance);
    }

    public void UpdateStats()
    {
        return;
    }
}
