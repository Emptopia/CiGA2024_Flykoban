using UnityEngine;
/*
  @Author:Rekite
 */
public class CharaController : MonoBehaviour
{
    CharacterController player;  //定义角色控制器组件
    public new Transform camera; //新建一个camera对象用于放入所要实现的第一人称相机
    public float speed = 2f;			 //角色移动速度
    public float x, y;                  //相机旋转x，y轴控制
    public float g = 10f;               //重力
    Vector3 playerrun;           //控制玩家运动的向量
    public float jumpSpeed = 2;//跳跃速度
    public float gravity = 2;//模拟重力
    public bool isJump = false;//是否在跳跃
    public float jumpTime= 0.5f;//跳跃时间
    public float jumpTimeFlag = 0;//累计跳跃时间用来判断是否结束跳跃
    void Start()
    {
        player = GetComponent<CharacterController>();//获取人物的角色控制器组件    
    }
 
    void Update()
    {
 
        Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标到视图中心
        Cursor.visible = false;//隐藏鼠标
 
        //控制玩家运动
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
        if (player.isGrounded)
        {
            playerrun = new Vector3(_horizontal, 0, _vertical);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerrun.y += jumpSpeed;
            }
        }
        playerrun.y -= g * Time.deltaTime;
        player.Move(player.transform.TransformDirection(playerrun * Time.deltaTime * speed));
 
        //使用鼠标来控制相机的视角的旋转
        x += Input.GetAxis("Mouse X");
        y -= Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(0, x, 0);
        y = Mathf.Clamp(y, -45f, 45f);
        camera.eulerAngles = new Vector3(y, x, 0);
 
        //让相机z轴保持不变，防止相机倾斜
        if (camera.localEulerAngles.z != 0)
        {
            float rotX = camera.localEulerAngles.x;
            float rotY = camera.localEulerAngles.y;
            camera.localEulerAngles = new Vector3(rotX, rotY, 0);
        }
        
    }
}