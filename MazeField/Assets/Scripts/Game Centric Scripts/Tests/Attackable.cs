using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Attackable
{
    bool isEnemyAffiliated(int group);

    GameObject SelectAsTarget();

    bool hit(ref float[] results,float power = 5, float accuracy = 50, float criticalChance = 0);
}
