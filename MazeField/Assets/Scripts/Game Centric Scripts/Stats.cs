using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats{
    
    public float BaseHealth; //Characters Health

    //Damage to health equals (power - (power/100 * Protection)) - Defense
    public float BaseDefense;
    public float BaseProtection;

    // Effects attack speed(minor) and evasion(major)
    public float BaseSpeed;

    // Effects attack power (min and max) Close Physical (major) ranged Physical (minor)
    public float BaseStrength;

    // Effects protection (minor) effects evasion (minor) effects accuracy (minor) effects ranged Physical (major)
    public float BaseDex;
    // Effects chance to hit (major) effects ranged Physical (minor) effects evasion (minor)
    public float BaseAccuracy;
    // Effects everything (minor) effects critcal chance (major)
    public float BaseLuck;

    public float TotalDamage = 0;

    public Stats(float h, float bd, float bp, float bs, float bstr, float bdex, float bA, float bL)
    {
        BaseHealth = h;
        BaseDefense = bd;
        BaseProtection = bp;
        BaseSpeed = bs;
        BaseStrength = bstr;
        BaseDex = bdex;
        BaseAccuracy = bA;
        BaseLuck = bL;
    }

    private float LuckMultiplier() { return (1.0f + (BaseLuck / 100f)); }

    public float[] getBaseCloseAttackPower()
    {
        float[] range = new float[2];

        range[0] = 5;
        range[0] += 0.5f * BaseStrength * LuckMultiplier();

        range[1] = 10;
        range[1] += 1.0f * BaseStrength * LuckMultiplier();

        return range;
    }

    public float[] getBaseRangedAttackPower()
    {
        float[] range = new float[2];

        range[0] = 3;
        range[0] += 0.3f * (3 * BaseDex) * LuckMultiplier();

        range[1] = 7;
        range[1] += 0.6f * (((3f * BaseDex) + BaseAccuracy + BaseStrength) / 4f) * LuckMultiplier();

        return range;
    }

    public float getBaseAccuracy()
    {
        float Accuracy = 3 * BaseAccuracy;
        Accuracy += BaseDex;

        Accuracy *= 1.3f * LuckMultiplier();

        return Accuracy;
    }

    public float getBaseEvasion()
    {
        float Evasion = 2 * BaseSpeed;
        Evasion += BaseDex + (0.5f * BaseAccuracy);

        Evasion *= 1.3f * LuckMultiplier();

        return Evasion;
    }

    public float getBaseCriticalChance()
    {
        return (1f / 100f) * BaseLuck;
    }
}
