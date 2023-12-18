using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform character;

    void Update()
    {
        if (character != null)
        {
            Vector3 position = transform.position;
            position.x = character.position.x;
            transform.position = position;
        }
    }
}
