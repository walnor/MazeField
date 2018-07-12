using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float m_Speed = 3.0f;

	// Update is called once per frame
	void Update ()
    {
        float x = 0.0f;
        float y = 0.0f;

        if (Input.GetKey(KeyCode.W)) y++;
        if (Input.GetKey(KeyCode.S)) y--;
        if (Input.GetKey(KeyCode.D)) x++;
        if (Input.GetKey(KeyCode.A)) x--;

        gameObject.transform.position = transform.position + (new Vector3(x, y, 0) * m_Speed * Time.deltaTime);
    }
}
