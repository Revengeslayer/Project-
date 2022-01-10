using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public float speed;
    private List<GameObject> monsterPrefabIns;
    private List<GameObject> terrainPrefabIns;
    private GameObject player;

    private Animator playerAnimator;
    private Animation playerAnimation;

    private void Awake()
    {
        Terrain();
        //Mobs();
        Player();
        Camera();
        
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool isAttack=playerAnimator.GetBool("isAttack");

        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerAnimator.SetTrigger("Attack");          
        }


        if (Input.GetKey(KeyCode.UpArrow)&&!isAttack)
        {
            playerAnimator.SetBool("isWalk", true);
            player.transform.position += player.transform.forward * Time.deltaTime * speed;         
        }
        else 
        {
            playerAnimator.SetBool("isWalk", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !isAttack)
        {
            playerAnimator.SetBool("isWalkL", true);
            player.transform.Rotate(0, -100 * Time.deltaTime, 0);
            player.transform.position += player.transform.forward * Time.deltaTime / 10* speed;

        }
        else
        {
            playerAnimator.SetBool("isWalkL", false);
        }
        if (Input.GetKey(KeyCode.DownArrow) && !isAttack)
        {
            playerAnimator.SetBool("isWalkB", true);
            player.transform.position -= player.transform.forward * Time.deltaTime* speed;
        }
        else
        {
            playerAnimator.SetBool("isWalkB", false);
        }
        if (Input.GetKey(KeyCode.RightArrow) && !isAttack)
        {
            playerAnimator.SetBool("isWalkR", true);
            player.transform.Rotate(0, 100 * Time.deltaTime, 0);
            player.transform.position += player.transform.forward * Time.deltaTime / 10* speed;
        }
        else
        {
            playerAnimator.SetBool("isWalkR", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.transform.position += new Vector3(0, 1 , 0);
            player.transform.position += player.transform.forward * Time.deltaTime ;
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
    void Camera()
    {
        FlowPlayer.playerPos = player.transform;
    }
    void Player()
    {
        player = LoadCharacter.LoadData();

        playerAnimator = player.GetComponent<Animator>();
        
        playerAnimation = player.GetComponent<Animation>();

       

    }
    void Terrain()
    {
        terrainPrefabIns = LoadTerrain.LoadData();
    }
}
