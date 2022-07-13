using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;


    bool wDown;
    bool jDown;

    bool dDown;

    bool isJump;

    bool isDodge;

    Rigidbody rigid;
    
    Animator anim;
    
    Vector3 moveVec;

    Vector3 DodgeVec;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
    }

    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); 
        vAxis = Input.GetAxisRaw("Vertical"); 
        //간단하게 hAxis, vAxis는 하나의 수직선이라고 보면 된다.
        //이 상황에서는 Horizontal과 Vertical을 합쳐 x축과 z축을 만듬.
        //Horizontal의 키보드 입력은 left와 right, Vertical은 up,down이다.
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
        dDown = Input.GetButtonDown("Dodge");
        
    }

    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        if(isDodge)
        {
            moveVec = DodgeVec;
        }
        transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;
        //걷는 속도 느리게 설정
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
        //플레이어 보는 방향 설정
    }

    void Jump()
    {
        if(jDown && !isJump && !isDodge)
        {
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }

    }

    void Dodge()
    {
        if(dDown && moveVec != Vector3.zero && !isJump && !isDodge)
        {
            DodgeVec = moveVec;
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;
            Invoke("DodgeOut",0.4f);
        }

        

    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
        
    }
    



    void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.tag == "Floor")
        {
            isJump = false;
            anim.SetBool("isJump",false);
        }
    }
}

