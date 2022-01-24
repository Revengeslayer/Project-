using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private string colliderTag;
    private bool CARotate = false;
    private Vector3 CMRotateVillage;
    private Vector3 CMRotateBattle01;
    private Vector3 Boss01_1;
    private float YT;


    // Start is called before the first frame update


    // Update is called once per frame
    private void Start()
    {
        CMRotateVillage = Camera.main.transform.forward;
        CMRotateBattle01 = new Vector3(CMRotateVillage.x * -1, CMRotateVillage.y, CMRotateVillage.z * -1);
        Boss01_1 = -Camera.main.transform.right;
    }
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        YT = 0;
        colliderTag = other.tag;
        Debug.Log(other.tag+ "enter");

        if (colliderTag == "Village")
        {
            if (FlowPlayer.offect.x > 0)
            {
                Camera.main.transform.position = gameObject.transform.position + new Vector3(-20f, 8.5f, 0);
                Camera.main.transform.forward = CMRotateBattle01;
            }

            FlowPlayer.offect = new Vector3(-20f, 8.5f, 0);
            FlowPlayer.CMRotate = CMRotateBattle01;
            FlowPlayer.smoothTime = 0.25f;
            //if (FlowPlayer.offect.x < 0)
            //{
            //    return;
            //}
            //else
            //{
            //    Debug.Log("<0");
            //    Camera.main.transform.forward = CMRotateBattle01;
            //}

        }
        else if (colliderTag == "Village01")
        {
            //    //FlowPlayer.XT *= -1;
            //    //FlowPlayer.ZT *= -1;
            //    //FlowPlayer.SetCameraRotate();
            //    CA.transform.forward = 
            //    FlowPlayer.colliderTag = colliderTag;

            FlowPlayer.CARotate = true;
            FlowPlayer.offect = new Vector3(-8.5f, 7.5f, 0);
            FlowPlayer.CMRotate = CMRotateBattle01;
            FlowPlayer.smoothTime = 1;
        }
        else if (colliderTag == "Battle01")
        {
            if (FlowPlayer.offect.x < 0)
            {
                Camera.main.transform.position = gameObject.transform.position + new Vector3(20f, 8.5f, 0);
                Camera.main.transform.forward = CMRotateVillage;
            }
            FlowPlayer.offect = new Vector3(20f, 8.5f, 0);
            FlowPlayer.CMRotate = CMRotateVillage;
            FlowPlayer.smoothTime = 0.25f;
            //if (FlowPlayer.offect.x > 0)
            //{
            //    return;
            //}
            //else
            //{
            //    Debug.Log(">0");
            //    Camera.main.transform.forward = CMRotateVillage;
            //}

        }
        else if (colliderTag == "Battle01(1)")
        {
            FlowPlayer.CARotate = true;
            FlowPlayer.offect = new Vector3(8.5f, 10f, 0);
            FlowPlayer.CMRotate = CMRotateVillage;
            FlowPlayer.smoothTime = 3f;
        }
        else if(colliderTag == "Boss01")
        {
            FlowPlayer.offect = new Vector3(10f, 9f, 0);
            FlowPlayer.CMRotate = CMRotateVillage;
            FlowPlayer.smoothTime = 3;
        }
        else if (colliderTag == "Boss01_1")
        {
            FlowPlayer.offect = new Vector3(0, 1.0f, 3f);
            //FlowPlayer.offect = new Vector3(0, 1.3f , 3);
            FlowPlayer.CMRotate = Boss01_1;//- new Vector3(0, 0.5f, 0);
            FlowPlayer.smoothTime = 2;
        }
        else if (colliderTag == "Boss01_2")
        {
            FlowPlayer.offect = new Vector3(10f, 9f, 0f);
            //FlowPlayer.offect = new Vector3(0, 1.3f , 3);
            //FlowPlayer.CMRotate = Boss01_1 - new Vector3(0, 0.5f, 0);
            FlowPlayer.CMRotate = CMRotateVillage;
            FlowPlayer.smoothTime = 2f;
        }
        else if (colliderTag == "Boss01_3")
        {
            FlowPlayer.offect = new Vector3(0, 5f, 6.5f);
            //FlowPlayer.offect = new Vector3(0, 1.3f , 3);
            FlowPlayer.CMRotate = Boss01_1 - new Vector3(0, 0.5f, 0);
            FlowPlayer.smoothTime = 2;
        }
        else if (colliderTag == "Boss01_4")
        {
            FlowPlayer.offect = new Vector3(13.5f, 7.5f, 0);
            FlowPlayer.CMRotate = CMRotateVillage;
            FlowPlayer.smoothTime = 0.5f;
        }
        else if (colliderTag == "Battle01_2")
        {
            FlowPlayer.offect = new Vector3(11.5f, 7.5f, 0);
            FlowPlayer.CMRotate = CMRotateVillage;
            FlowPlayer.smoothTime = 3f;
        }

        else
        {
            //FlowPlayer.offect = new Vector3(8.5f, 5.5f, 0);
            //colliderTag = "";
        }
    }
    private void OnTriggerStay(Collider other)
    {
        var Y = Input.GetKeyDown(KeyCode.Y);
        var T = 0.2f;
        YT += Time.deltaTime;
        if(Y == false)
        {
            return;
        }
        if (colliderTag == "Village")
        {
            if (Y && YT >T)
            {
                gameObject.transform.position = new Vector3(25.36502f, 3.036211f, 39.22498f);
                YT = 0;
                Y = false;
            }
        }
        if (colliderTag == "Battle01")
        {
            if (Y && YT > T)
            {
                gameObject.transform.position = new Vector3(30.50537f, 2.88238f, 38.95977f);
                YT = 0;
                Y = false;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        colliderTag = other.tag;
        if (colliderTag == "Boss01_2")
        {
            FlowPlayer.offect = new Vector3(10f, 9f, 0);
            FlowPlayer.CMRotate = CMRotateVillage;
            FlowPlayer.smoothTime = 2;
        }
    }


}
