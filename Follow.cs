using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    //설정한 카메라 위치(3인칭)의 구도로 플레이어를 따라가게 하는 오프셋
    public float delaytime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * 15 *delaytime );
    }
}
