using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    
    Vector3 moveVec;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal"); 
        vAxis = Input.GetAxisRaw("Vertical"); 
        //간단하게 hAxis, vAxis는 하나의 수직선이라고 보면 된다.
        //이 상황에서는 Horizontal과 Vertical을 합쳐 x축과 z축을 만듬.
        //Horizontal의 키보드 입력은 left와 right, Vertical은 up,down이다.
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        //normalized는 대각선으로 갈시 빨라지는 것을 보정해주는 것.
        
        transform.position += moveVec * speed * Time.deltaTime;

    }
}
