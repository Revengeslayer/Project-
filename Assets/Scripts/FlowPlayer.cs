using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPlayer : MonoBehaviour
{
    public float Heightx;
    public float Heighty;
    public float Heightz;
    public static Transform playerPos;
    public Vector3 offect;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(playerPos.position.x + offect.x + Heightx, gameObject.transform.position.y + Heighty, playerPos.position.z + offect.z + Heightz);
    }
}
