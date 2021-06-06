using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenAdjustment : MonoBehaviour
{
	public SpriteRenderer rink;

    void Start()
    {
		float ortoSize = rink.bounds.size.x * Screen.height / Screen.width * 0.5f;

		Camera.main.orthographicSize = ortoSize;
    }
}
