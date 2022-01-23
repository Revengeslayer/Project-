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
    //private void OnCollisionStay(Collision collision)
    //{
    //    var a = collision.gameObject.name;  //存出collision name
    //    var b = collision.gameObject;  //存出collision目標
    //    var c = b.transform.position - Camera.main.transform.position;  //目標 - Camera 的向量
    //    var d = gameObject.transform.position.y - Camera.main.transform.position.y;  //人物到Camera的y
    //    Debug.Log(collision.gameObject.name);
    //    if (a == "Viking_Bridge")
    //    {
    //        Camera.main.transform.forward += new Vector3(c.x, d, c.z) * Time.deltaTime / 20;
    //        FlowPlayer.offect = new Vector3(8.5f, 5.5f, 0);
    //            //* (new Vector3(8.5f, 5.5f, 0) - Camera.main.transform.position).magnitude * Time.deltaTime ;

    //    }
    //}
    void OnTriggerEnter(Collider other)
    {
        var CA = Camera.main;
        colliderTag = other.tag;
        Debug.Log(other.tag+ "enter");

        if (colliderTag == "Village")
        {
            //    //FlowPlayer.XT *= -1;
            //    //FlowPlayer.ZT *= -1;
            //    //FlowPlayer.SetCameraRotate();
            //    CA.transform.forward = 
            //    FlowPlayer.colliderTag = colliderTag;

            FlowPlayer.CARotate = true;
            FlowPlayer.offect = new Vector3(-8.5f, 7.5f, 0);
            FlowPlayer.CMRotate = CMRotateBattle01;
        }
        else if (colliderTag == "Battle01")
        {
            //    //FlowPlayer.XT *= -1;
            //    //FlowPlayer.ZT *= -1;
            //    //FlowPlayer.SetCameraRotate();
            //    CA.transform.forward = 
            //    FlowPlayer.colliderTag = colliderTag;

            Debug.Log(colliderTag + "EnterBattle");
            FlowPlayer.CARotate = true;
            FlowPlayer.offect = new Vector3(8.5f, 7.5f, 0);
            FlowPlayer.CMRotate = CMRotateVillage;
        }
        else if(colliderTag == "Boss01")
        {
            FlowPlayer.offect = new Vector3(5.5f, 4.5f, 0);
            FlowPlayer.CMRotate = CMRotateVillage; 
        }
        else if (colliderTag == "Boss01_1")
        {
            FlowPlayer.offect = new Vector3(0, 1.0f, 3f);
            //FlowPlayer.offect = new Vector3(0, 1.3f , 3);
            FlowPlayer.CMRotate = Boss01_1;//- new Vector3(0, 0.5f, 0);
        }
        else if (colliderTag == "Boss01_2")
        {
            FlowPlayer.offect = new Vector3(0, 4.5f, 6f);
            //FlowPlayer.offect = new Vector3(0, 1.3f , 3);
            FlowPlayer.CMRotate = Boss01_1 - new Vector3(0, 0.5f, 0);
        }
        else if (colliderTag == "Boss01_3")
        {
            FlowPlayer.offect = new Vector3(0, 5f, 6.5f);
            //FlowPlayer.offect = new Vector3(0, 1.3f , 3);
            FlowPlayer.CMRotate = Boss01_1 - new Vector3(0, 0.5f, 0);
        }
        else if (colliderTag == "Boss01_4")
        {
            FlowPlayer.offect = new Vector3(10.5f, 7.5f, 0);
            FlowPlayer.CMRotate = CMRotateVillage;
        }
        else
        {
            //FlowPlayer.offect = new Vector3(8.5f, 5.5f, 0);
            //colliderTag = "";
        }
    }
    void OnTriggerExit(Collider other)
    {
        //if (colliderTag == "TriggerVillage")
        //{
        //    //FlowPlayer.XT *= -1;
        //    //FlowPlayer.ZT *= -1;
        //    FlowPlayer.offect = new Vector3(8.5f, 5.5f, 0);
        //    Debug.Log(colliderTag + "Exit");
        //    colliderTag = "";
        //}
        Debug.Log(other.tag + "exit");
    }


}
