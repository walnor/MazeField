using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Bar : MonoBehaviour {

    public GameObject EndSprite;
    public GameObject MidScaleSprite;

    public Transform EndPos;

    public float Size = 1.0f;
    public float Percent = 100f;

    float SpriteSize = 0.16f;
	
	// Update is called once per frame
	void Update ()
    {
        float Scale = Size / SpriteSize;
        Scale *= 1f - (1f - Percent/100f);

        MidScaleSprite.transform.localScale = new Vector3(Scale, 1, 1);

        EndSprite.transform.position = EndPos.position;		
	}

    public void GivePercent(float p)
    {
        Percent = p * 100f;
    }
}
