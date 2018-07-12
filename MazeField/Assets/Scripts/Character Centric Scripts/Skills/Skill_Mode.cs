using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Skill_Mode
{
    void Init(Character owner);
    void Update();

    int GetType();

    bool Hit(ref float[] results, float power = 5, float accuracy = 50, float criticalChance = 0);

    void UpdateStats();

}
