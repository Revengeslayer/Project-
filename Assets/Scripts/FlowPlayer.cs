using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowPlayer : MonoBehaviour
{
    public static Transform playerPos;
    public Vector3 offect;
    private Vector3 cameraVelocity = Vector3.one;
    public float smoothTime = 1.0f;
    public float down = -1;
    public float up = 1;
    public LayerMask CameraHitLayer;



    // Update is called once per frame
    void Update()
    {

        //gameObject.transform.rotation *= rotate;
        //嘗試失敗rotate

        //gameObject.transform.position = new Vector3(playerPos.position.x + offect.x , playerPos.position.y + offect.y, playerPos.position.z + offect.z);
        //修正後的原版

        //gameObject.transform.position = Vector3.SmoothDamp(transform.position, playerPos.position + new Vector3(0,0,0),ref cameraVelocity , smoothTime );
        //SmoothDamp(自己的pos,目標的pos,沒反應純照抄,滑動的時間)

        //BasicMove();

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

        Vector3 a = playerPos.position - gameObject.transform.position;
        float fDist = a.magnitude;  //get length
        a.Normalize();


        //bool Physics.Raycast(Vector3 origin, Vector3 direction, float distance, int layerMask)

        var RayDown = Physics.Raycast(gameObject.transform.position, (a + new Vector3(0, down, 0)), fDist, CameraHitLayer);


        Debug.DrawRay(gameObject.transform.position, (a + new Vector3(0, down, 0)) * fDist);


        //Debug.Log(hitInfo);

        if (RayDown == true )
        {
            gameObject.transform.position = Vector3.SmoothDamp(transform.position, playerPos.position + new Vector3(offect.x, offect.y + 5, offect.z), ref cameraVelocity, smoothTime);

        }
        else
        {
            BasicMove();
            Debug.Log(RayDown);
        }
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
