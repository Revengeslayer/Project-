using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPlayer : MonoBehaviour
{
    public static Transform playerPos;
    public static GameObject player;
    public static string colliderTag;
    public static Vector3 offect = new Vector3(-20f, 6f, 0);
    private Vector3 cameraVelocity = Vector3.zero;
    public static float smoothTime = 0.2f;
    public float xPos;
    private float yPos;
    public float zPos;
    //public float up = 1;
    //public float left = 1;
    //public float right = 1;
    public float rayLength;
    public LayerMask CameraHitLayer;
    private Vector3 upPos;
    private Vector3 downPos;
    private Vector3 leftPos;
    private Vector3 rightPos;
    private Vector3 hitPos;
    public float forDown;
    bool bRayDown;
    bool bRayRight;
    bool bRayUp;
    bool bRayLeft;
    bool ba1Ray;
    Ray a1Ray;
    private float i;
    private RaycastHit hit;
    public static float XT = 1;
    public static float ZT = 1;
    public static bool CARotate = false;
    public static Vector3 CMRotate;

    // Update is called once per frame

    private void Start()
    {
        //gameObject.transform.position = Vector3.SmoothDamp(transform.position, downPos, ref cameraVelocity, smoothTime);
        gameObject.transform.position = playerPos.position + new Vector3(offect.x , offect.y , offect.z );
        gameObject.transform.forward = new Vector3(gameObject.transform.forward.x * -1, gameObject.transform.forward.y, gameObject.transform.forward.z );
        CMRotate = gameObject.transform.forward;
    }
    void Update()
    {

        //gameObject.transform.rotation *= rotate;
        //嘗試失敗rotate

        //gameObject.transform.position = new Vector3(playerPos.position.x + offect.x , playerPos.position.y + offect.y, playerPos.position.z + offect.z);
        //修正後的原版

        //gameObject.transform.position = Vector3.SmoothDamp(transform.position, playerPos.position + new Vector3(0,0,0),ref cameraVelocity , smoothTime );
        //SmoothDamp(自己的pos,目標的pos,沒反應純照抄,滑動的時間)


        SetCameraPos();
        BasicMove();
        //CameraRaycast();

        //tryRay();
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        //Gizmos.DrawLine(gameObject.transform.position , ( playerPos.position - gameObject.transform.position ) * 10.0f);
    }
    //void OnTriggerEnter(Collider other)
    //{
    //    colliderTag = other.tag;
    //    if (colliderTag == "TriggerVillage")
    //    { 
    //        gameObject.transform.forward = new Vector3(gameObject.transform.forward.x * -XT, gameObject.transform.forward.y, gameObject.transform.forward.z * -ZT);
    //    }
    //}
    //void OnTriggerExit(Collider other)
    //{
    //    colliderTag = "";
    //}
    public static void SetCameraRotate()
    {
        //if(colliderTag == "TriggerVillage")
        //{
        //    BasicMove();
        //}
        //else
        //{
        //    BasicMove();
        //}

    }
    void BasicMove()
    {
        gameObject.transform.position = Vector3.SmoothDamp(transform.position, playerPos.position + offect, ref cameraVelocity, smoothTime);
        //gameObject.transform.forward = CMRotate;
        gameObject.transform.forward += (CMRotate / smoothTime * Time.deltaTime ) * (CMRotate -gameObject.transform.forward).magnitude;
        //i += 1f * Time.deltaTime * 0.0001f;
        //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, downPos, i);
        //if (i > 0.9)
        //{
        //    i = 0;
        //}
    }
    #region Set & Ray
    void CameraRaycast()
    {

        Vector3 CtoC = playerPos.position - gameObject.transform.position;
        Vector3 a = playerPos.position - downPos;  //downPos
        Vector3 b = playerPos.position - rightPos; //rightPos
        Vector3 c = playerPos.position - upPos;     //upPos
        Vector3 d = playerPos.position - leftPos;  //leftPos
        float fDist = CtoC.magnitude * rayLength;  //get length
        Ray a1Ray = new Ray(downPos, a + new Vector3(0, yPos, forDown));
        //for (i = 1; i < 10; i++) 
        //{
        //    a1Ray = new Ray(downPos, a + new Vector3(0, yPos, (forDown - playerPos.position.z) / 10 * i + forDown));
        //    ba1Ray = Physics.Raycast(a1Ray, out hit, rayLength, CameraHitLayer);
        //    Debug.DrawRay(downPos, a + new Vector3(0, yPos, (forDown - playerPos.position.z) / 10 * i + forDown) * fDist);
        //}

        a.Normalize();
        b.Normalize();
        c.Normalize();
        d.Normalize();


        //bool Physics.Raycast(Vector3 origin, Vector3 direction, float distance, int layerMask)
        bRayDown = Physics.Raycast(downPos, (a + new Vector3(0, yPos, 0)), fDist, CameraHitLayer);
        bRayRight = Physics.Raycast(rightPos, (b + new Vector3(0, yPos, 0)), fDist, CameraHitLayer);
        bRayUp = Physics.Raycast(upPos, (c + new Vector3(0, yPos, 0)), fDist, CameraHitLayer);
        bRayLeft = Physics.Raycast(leftPos, (d + new Vector3(0, yPos, 0)), fDist, CameraHitLayer);
        ba1Ray = Physics.Raycast(downPos, (a + new Vector3(0, yPos, forDown)), fDist, CameraHitLayer); //probe



        //Debug.DrawRay(gameObject.transform.position , (a + new Vector3(0, down, 0)) * fDist );
        Debug.DrawRay(gameObject.transform.position, (CtoC * fDist));  //Camera
        Debug.DrawRay(downPos, (a + new Vector3(0, yPos, 0)) * fDist);
        Debug.DrawRay(downPos, (a + new Vector3(0, yPos, forDown)) * fDist);  //drawprobe
        Debug.DrawRay(rightPos, (b + new Vector3(0, yPos, 0)) * fDist);
        Debug.DrawRay(upPos, (c + new Vector3(0, yPos, 0)) * fDist);
        Debug.DrawRay(leftPos, (d + new Vector3(0, yPos, 0)) * fDist);
        //ba1Ray = Physics.Raycast(a1Ray, out hit,rayLength, CameraHitLayer); // probe





        //Debug.Log(hitInfo);
        // a = b * c
        if (ba1Ray || bRayDown == true)
        {
            //備用
            //hitPos = (hit.transform.position - downPos); //get hit vec
            //hitPos.y = 0;
            //hitPos.Normalize();
            //a.y = 0;
            //a.Normalize();
            //float downPosToC = Vector3.Dot(a, hitPos);  //cos
            //備用
            a.y = 0;
            a.Normalize();
            b.y = 0;
            b.Normalize();
            //float dTor = Vector3.Dot(a, b);
            //gameObject.transform.position = (gameObject.transform.position.x * (1 - Vector3.Dot(a, b)), y = 0, gameObject.transform.position.z * (1 - (a.b));
            gameObject.transform.position = Vector3.SmoothDamp(transform.position, rightPos, ref cameraVelocity, smoothTime);
            //gameObject.transform.Rotate(0, (downPos.x - rightPos.x) * RotateSpeed * Time.deltaTime, 0);
            gameObject.transform.forward += (b / smoothTime * Time.deltaTime * 0.5f);

            Debug.Log(ba1Ray);
            Debug.Log(bRayDown);
            //Debug.DrawLine(downPos, hit.transform.position, Color.red, 0.1f, true);
        }
        else if (bRayDown == false)
        {
            BasicMove();
            gameObject.transform.forward += (a / smoothTime * Time.deltaTime * 0.5f);
            Debug.Log(bRayDown);
        }
    }
    void SetCameraPos()
    {
        upPos = playerPos.position + new Vector3(-offect.x, offect.y, -offect.z);
        downPos = playerPos.position + new Vector3(offect.x, offect.y, offect.z);
        leftPos = playerPos.position + new Vector3(offect.z, offect.y, -offect.x);
        rightPos = playerPos.position + new Vector3(-offect.z, offect.y, offect.x);
    }
    void tryRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
            Debug.Log(hit.transform.name);
        }
    }

    #endregion
}
