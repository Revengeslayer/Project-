using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageFenceTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Wood Gate").transform.Rotate(0, -75 , 0);
    }

    void OnTriggerExit(Collider other)
    {
        GameObject.Find("Wood Gate").transform.Rotate(0, 75, 0);
    }
}
