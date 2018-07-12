using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Skill_Active
{
    void Init(Character owner);
    bool Hit(ref float[] results, float power = 5, float accuracy = 50, float criticalChance = 0);

    void Activate(Vector3 location, Vector3 direction);
}