using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPlayer : MonoBehaviour
{
    public static Transform playerPos;
    public Vector3 offect;
    private Vector3 cameraVelocity = Vector3.one;
    public float smoothTime = 2;
    public float xPos ;
    public float yPos ;
    public float zPos ;
    //public float up = 1;
    //public float left = 1;
    //public float right = 1;
    public float rayLength;
    public LayerMask CameraHitLayer;
    private Vector3 upPos;
    private Vector3 downPos;
    private Vector3 leftPos;
    private Vector3 rightPos;
    bool bRayDown;
    bool bRayRight;
    bool bRayUp;
    bool bRayLeft;


    // Update is called once per frame

    private void Start()
    {
        //gameObject.transform.position = Vector3.SmoothDamp(transform.position, downPos, ref cameraVelocity, smoothTime);
        gameObject.transform.position = playerPos.position + new Vector3(offect.x, offect.y, offect.z);
    }
    void Update()
    {

        //gameObject.transform.rotation *= rotate;
        //嘗試失敗rotate

        //gameObject.transform.position = new Vector3(playerPos.position.x + offect.x , playerPos.position.y + offect.y, playerPos.position.z + offect.z);
        //修正後的原版

        //gameObject.transform.position = Vector3.SmoothDamp(transform.position, playerPos.position + new Vector3(0,0,0),ref cameraVelocity , smoothTime );
        //SmoothDamp(自己的pos,目標的pos,沒反應純照抄,滑動的時間)

        //BasicMove();
        SetCameraPos();
        CameraRaycast();

        //tryRay();
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        //Gizmos.DrawLine(gameObject.transform.position , ( playerPos.position - gameObject.transform.position ) * 10.0f);
    }
    void BasicMove()
    {
        gameObject.transform.position = Vector3.SmoothDamp(transform.position, playerPos.position + new Vector3(offect.x, offect.y, offect.z), ref cameraVelocity, smoothTime);
    }
    void CameraRaycast()
    {

        Vector3 CtoC = playerPos.position - gameObject.transform.position;
        Vector3 a = playerPos.position - downPos ;  //downPos
        Vector3 b = playerPos.position - rightPos ; //rightPos
        Vector3 c = playerPos.position - upPos;     //upPos
        Vector3 d = playerPos.position - leftPos ;  //leftPos
        float fDist = CtoC.magnitude * rayLength;  //get length
        CtoC.y = 0;

        CtoC.Normalize();
        a.Normalize();       
        b.Normalize();        
        c.Normalize();        
        d.Normalize();


        //bool Physics.Raycast(Vector3 origin, Vector3 direction, float distance, int layerMask)
        bRayDown = Physics.Raycast( downPos , (a + new Vector3( 0 , yPos , 0 )), fDist , CameraHitLayer);
        bRayRight = Physics.Raycast(rightPos, ( b + new Vector3(0 , yPos , 0 )), fDist, CameraHitLayer);
        bRayUp = Physics.Raycast(upPos, (c + new Vector3( 0 , yPos , 0 )), fDist, CameraHitLayer);
        bRayLeft = Physics.Raycast(leftPos, (d + new Vector3( 0 , yPos , 0 )), fDist, CameraHitLayer);



        //Debug.DrawRay(gameObject.transform.position , (a + new Vector3(0, down, 0)) * fDist );
        Debug.DrawRay( gameObject.transform.position , (CtoC + new Vector3(0, yPos, 0)) * fDist );  //Camera
        Debug.DrawRay(downPos, (a + new Vector3(0, yPos, 0)) * fDist); 
        Debug.DrawRay(rightPos, (b + new Vector3(0, yPos, 0)) * fDist);
        Debug.DrawRay(upPos, (c + new Vector3(0, yPos, 0)) * fDist);
        Debug.DrawRay(leftPos, (d + new Vector3(0, yPos, 0)) * fDist);


        //Debug.Log(hitInfo);

        if (bRayDown == true )
        {
            //float CCdotR = Vector3.Dot( CtoC, -b );
            //gameObject.transform.position = Vector3.SmoothDamp(transform.position, playerPos.position + new Vector3(offect.x, offect.y + 5, offect.z), ref cameraVelocity, smoothTime);
            gameObject.transform.position = Vector3.SmoothDamp(transform.position , rightPos , ref cameraVelocity, smoothTime);
            //gameObject.transform.Rotate(0, (downPos.x - rightPos.x) * RotateSpeed * Time.deltaTime, 0);
            gameObject.transform.forward += (b / smoothTime * Time.deltaTime * 0.5f) ;

            Debug.Log(bRayDown);
        }
        //else if( bRayDown == false && bRayRight == false && bRayUp == false && bRayLeft == false )
        else if (bRayDown == false )
        {
            BasicMove();
            gameObject.transform.forward += (a / smoothTime * Time.deltaTime * 0.5f) ;
            Debug.Log(bRayDown);
        }
    }

    void SetCameraPos()
    {
        upPos = playerPos.position + new Vector3( -offect.x, offect.y, -offect.z );
        downPos = playerPos.position + new Vector3( offect.x, offect.y, offect.z ); 
        leftPos = playerPos.position + new Vector3( offect.z, offect.y, -offect.x ); 
        rightPos = playerPos.position + new Vector3( -offect.z, offect.y, offect.x ); 
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
}
