using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    void Select();

    void DirectPosition(Vector2 pos);

    void Target(GameObject other);
}
