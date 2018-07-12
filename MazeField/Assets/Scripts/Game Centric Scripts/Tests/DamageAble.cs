using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAble : MonoBehaviour
{
    public Stats m_Stats = null;

    private void Update()
    {
        if (m_Stats.TotalDamage >= m_Stats.BaseHealth)
            Death();
    }

    public void Init(Stats s)
    {
        m_Stats = s;
    }

    public bool hit(ref float[] results, float power = 5, float accuracy = 50, float criticalChance = 0,
        float DefenseMult = 1f, float DefenseAdd = 0f, float ProtectionMult = 1f, float ProtectionAdd = 0f)
    {
        float Defense = (m_Stats.BaseDefense * DefenseMult) + DefenseAdd;
        float Protection = (m_Stats.BaseProtection * ProtectionMult) + ProtectionAdd;

        results = new float[7];

        float Echance = Random.Range(0f, m_Stats.getBaseEvasion());
        float Hchance = Random.Range(0f, accuracy);

        results[1] = Hchance;
        results[2] = Echance;

        if (Hchance < Echance)
        {
            results[0] = -1;
            return false;
        }

        results[3] = power;
        results[4] = accuracy;
        results[5] = criticalChance - (Protection / 100.0f);

        float damage = (power / 100) * (100 - Protection);

        damage -= Defense;

        if (damage < 0) damage = 0;

        float critCheck = Random.Range(0.0f, 1.0f);

        results[6] = critCheck;

        if (critCheck < (criticalChance - (Protection / 100.0f)))
        {
            damage *= 1.5f;
        }

        results[0] = damage;

        m_Stats.TotalDamage += damage;

        return true;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public float GetDamagePercent()
    {
        float hp = m_Stats.BaseHealth;
        float dmg = m_Stats.TotalDamage;

        return (hp - dmg) / hp;
    }

}
