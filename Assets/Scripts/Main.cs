using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public float speed;
    public static bool canMove;
    private List<GameObject> monsterPrefabIns;
    private List<GameObject> terrainPrefabIns;
    public static  GameObject player;

    private Animator playerAnimator;
    private Animation playerAnimation;

    private Rigidbody playerRigidbody;
    public float jumpForce = 200;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;

    private float firstClickTime;
    private float secClickTime;
    private float doubleSpacing=0.2f;
    
    private void Awake()
    {
        canMove = true;
        Terrain();
        //Mobs();
        Player();
        SetCamera();
        
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Bye();
        
        bool isAttack=playerAnimator.GetBool("isAttack");
        bool isJump = playerAnimator.GetBool("isJump");
        bool isRun = playerAnimator.GetBool("isRun");

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isRun)
        {
            playerAnimator.SetBool("isRun", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && isRun)
        {
            playerAnimator.SetBool("isRun", false);
        }
        if (!playerAnimator.GetBool("isWalkF") && !playerAnimator.GetBool("isWalkB") && !playerAnimator.GetBool("isWalkL") && !playerAnimator.GetBool("isWalkR"))
        {
            playerAnimator.SetBool("isRun", false);
        }

        AnimatorStateInfo state = playerAnimator.GetCurrentAnimatorStateInfo(0);

        MoveFunc(isAttack, isJump, isRun);
        

        if (Input.GetKeyDown(KeyCode.Z) && !isJump)
        {
            playerAnimator.SetTrigger("Attack");
            playerAnimator.SetInteger("atkCount", playerAnimator.GetInteger("atkCount") + 1);
            player.transform.position += player.transform.forward * Time.deltaTime * speed;
            


        }
        if (Input.GetButtonDown("Jump") && Time.time > canJump && !isAttack)
        {
            playerAnimator.SetTrigger("Jump");
            playerRigidbody.AddForce(0, jumpForce, 0);
            canJump = Time.time + timeBeforeNextJump;
        }
    }

    void MoveFunc(bool isAttack, bool isJump , bool isRun)
    {
        float moveSpeed = 0;
        if (isRun)
        {
            DirControl(isAttack, ref moveSpeed, 2);
            if (canMove)
            {
                Move(moveSpeed);
            }
        }
        else
        {
            DirControl(isAttack, ref moveSpeed, 1);
            if (canMove)
            {
                Move(moveSpeed);
            }
        }     
    }

    void Bye()
    {
        if (Input.GetKey(KeyCode.B))
        {
            secClickTime = Time.time - firstClickTime;
            Debug.Log(secClickTime);
            if (secClickTime< doubleSpacing)
            {
                playerAnimator.SetBool("isRun",true);
            }
            else
            {
                playerAnimator.SetBool("isBye", true);
            }         
            firstClickTime = Time.time;
        }
        else
        {
            playerAnimator.SetBool("isBye", false);
        }
        if (Input.GetKeyUp(KeyCode.B) && playerAnimator.GetBool("isRun"))
        {
            playerAnimator.SetBool("isRun", false);
        }
    }

    void Mobs()
    {
        monsterPrefabIns = LoadMonster.LoadData();
        Mobsposition();
    }
    void Mobsposition()
    {
        int count = 0;
        for(int i=0; i< monsterPrefabIns.Count;i++)
        {
            monsterPrefabIns[i].transform.position = new Vector3(5.0f, 1.0f, 0.0f + count);
            count+=8;
        }
    }
    void SetCamera()
    {
        FlowPlayer.playerPos = player.transform;
    }
    void Player()
    {
        player = LoadCharacter.LoadData();

        playerAnimator = player.GetComponent<Animator>();
        
        playerAnimation = player.GetComponent<Animation>();
        playerRigidbody = player.GetComponent<Rigidbody>();


    }
    void Terrain()
    {
        terrainPrefabIns = LoadTerrain.LoadData();
    }

   Vector3 CheckForWard()
    {
        var x = -Input.GetAxis("Vertical");
        var z = Input.GetAxis("Horizontal");
        var a = -Camera.main.transform.forward * x;
        a.y = 0;
        a.Normalize();
        var b = Camera.main.transform.right * z;
        b.y = 0;
        b.Normalize();

        player.transform.forward = Vector3.Lerp(player.transform.forward, new Vector3(a.x, 0, b.z), 0.95f);
        //player.transform.forward = new Vector3(x, 0, z);
        //player.transform.position += new Vector3(a.x, 0, b.z) * Time.deltaTime * Accel() * n;



        return player.transform.forward;
    }

    void DirControl(bool isAttack, ref float moveSpeed,float speed)
    {
        //方向       上
        if (Input.GetKey(KeyCode.UpArrow) && !isAttack)
        {
            playerAnimator.SetBool("isWalkF", true);           
            if(Input.GetKey(KeyCode.DownArrow))
            {
                playerAnimator.SetBool("isWalkF", false);
                moveSpeed = 0;
            }
            else
            {
                moveSpeed = speed;
            }
        }
        else
        {
            playerAnimator.SetBool("isWalkF", false);
        }

        //方向       下
        if (Input.GetKey(KeyCode.DownArrow) && !isAttack)
        {
            playerAnimator.SetBool("isWalkB", true);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerAnimator.SetBool("isWalkB", false);
                moveSpeed = 0;
            }
            else
            {
                moveSpeed = speed;
            }
        }
        else
        {
            playerAnimator.SetBool("isWalkB", false);
        }

        //方向       左
        if (Input.GetKey(KeyCode.LeftArrow) && !isAttack)
        {
            playerAnimator.SetBool("isWalkL", true);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerAnimator.SetBool("isWalkL", false);
                moveSpeed = 0;
            }
            else
            {
                moveSpeed = speed;
            }
        }
        else
        {
            playerAnimator.SetBool("isWalkL", false);
        }

        //方向       右
        if (Input.GetKey(KeyCode.RightArrow) && !isAttack)
        {
            playerAnimator.SetBool("isWalkR", true);
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerAnimator.SetBool("isWalkR", false);
                moveSpeed = 0;
            }
            else
            {
                moveSpeed = speed;
            }
        }
        else
        {
            playerAnimator.SetBool("isWalkR", false);
        }
    }
    void Move(float n)
    {
        
        if (n != 0)
        {
            var x = -Input.GetAxis("Vertical");
            var z = Input.GetAxis("Horizontal");
            var a = -Camera.main.transform.forward * x;
            a.y = 0;
            a.Normalize();
            var b = Camera.main.transform.right * z;
            b.y = 0;
            b.Normalize();

            //player.transform.forward = new Vector3(x, 0, z);
            //player.transform.forward = Vector3.Lerp(player.transform.forward, new Vector3(x, 0, z), 0.95f);
            //player.transform.position += new Vector3(a.x, 0, b.z) * Time.deltaTime * Accel() * n;
            player.transform.position += CheckForWard() * Time.deltaTime * Accel() * n;
        }
    }
    float Accel()
    {
        float move = Mathf.Lerp(0, speed, 0.3f);
        return move;
    }
}
