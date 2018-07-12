using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SA_Swipe : Skill_Active
{
    Character m_Owner;

    List<Attackable> m_Targets;

    public void Activate(Vector3 location, Vector3 direction)
    {
        throw new System.NotImplementedException();
    }

    public bool Hit(ref float[] results, float power = 5, float accuracy = 50, float criticalChance = 0)
    {
        throw new System.NotImplementedException();
    }

    public void Init(Character owner)
    {
        m_Owner = owner;
    }
}