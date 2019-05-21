using UnityEngine;
using System;
/*
 * 控制摄像机的视野范围
*/
public class CameraMove : MonoBehaviour
{

    private Transform player;
    private Vector3 offsetPosition;
    private float distance;
    //private float scrollSpeed = 10; //鼠标滚轮速度
    private float speed = 2.0f;  //摄像机旋转速度
    private float moveSpeed = 15f;
    private Camera came;
    // Use this for initialization

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        came = this.GetComponent<Camera>();
    }

    void Start()
    {

        //摄像机朝向player
        transform.LookAt(player.position);
        //获取摄像机与player的位置偏移
        offsetPosition = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        //摄像机视野范围控制
        ScrollView();

        //摄像机在摄像机所在位置旋转
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(-Input.GetAxis("Mouse Y") * speed, Input.GetAxis("Mouse X") * speed, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        }
    }
    void ScrollView()
    {
        //放大视野
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (came.fieldOfView <= 300)
            {
                came.fieldOfView += 5;
            }
        }
        //缩小视野
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (came.fieldOfView >= 10)
            {
                came.fieldOfView -= 5;
            }
        }
    }

}