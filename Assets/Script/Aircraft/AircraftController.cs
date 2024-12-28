using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AircraftController : MonoBehaviour
{
    public AudioClip attackAudio;
    public AudioEventSO FXEvent;
    public GameObject player;
    public float noRespondDistance;
    public float followDistance;
    public float appearHigh;
    public float appearDistance;
    public float firstAppearTime;
    public float attackSpeed;
    public float attackTime;
    public Color normalColor;
    public Color waitColor;
    public Color attackColor;
    [Header("事件监听")]
    public SetAircrftPositionEventSO SetAircrftPositionEvent;

    Rigidbody2D rb;
    Rigidbody2D playerRb;
    Transform playerTransform;
    PlayerController playerController;
    JamInputSystem jamInputSystem;
    SpriteRenderer spriteRenderer;
    BoxCollider2D box;
    bool aircraftAlive;
    bool isAttack;
    int attackDir;
    AircraftState aircraftState;
    float attackedTime;

    private void Awake()
    {
         jamInputSystem = new JamInputSystem();
    }
    private void OnEnable()
    {
        jamInputSystem.Enable();
        SetAircrftPositionEvent.OnEventRaised += SetAircrftPosition;
    }    

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerTransform = player.GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        box = gameObject.GetComponent<BoxCollider2D>();
        playerRb = player.GetComponent<Rigidbody2D>();
        jamInputSystem.GamePlay.Control.started += Control;
        jamInputSystem.GamePlay.Attack.started += Attack;
        aircraftAlive = false;
        isAttack = false;
        StartCoroutine("AircraftAppear");
        attackedTime = 0;
    }


    // Update is called once per frame
    void Update()
    {
        //获取飞行器状态
        aircraftState= GetAircraftState();
        //状态管理
        StateControl(aircraftState);
        //计算player距离aircraft多远
        float disPToA = playerTransform.position.x - transform.position.x;
        //获取攻击方向
        if(!isAttack)
        {
            if (disPToA > 0)
                attackDir = 1;
            else
                attackDir = -1;
        }
        //速度匹配
        if(!isAttack)
        {
            if (Mathf.Abs(disPToA) >= noRespondDistance)
            {
                rb.velocity = new Vector2(playerController.moveSpeed * ((disPToA - (disPToA / Mathf.Abs(disPToA)) * noRespondDistance) / (followDistance - noRespondDistance)), 0);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }  
        else
        {
            attackedTime += Time.deltaTime;
            rb.velocity = new Vector2(attackDir*(attackSpeed - attackSpeed * (attackedTime / attackTime)), 0);
        }
        //根据速度确定朝向
        if (rb.velocity.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if(rb.velocity.x<0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    private void OnDisable()
    {
        jamInputSystem.Disable();
    }
    private void Control(InputAction.CallbackContext obj)
    {
        //根据aircraft的存在状态执行对应逻辑
        if(aircraftAlive)
        {
            //关闭飞行器代码
            box.enabled = false;
            spriteRenderer.enabled = false;
            aircraftAlive = false;
        }
        else
        {
            //打开飞行器代码
            transform.position = new Vector3(playerTransform.position.x + appearDistance, playerTransform.position.y + appearHigh, transform.position.y);
            box.enabled = true;
            spriteRenderer.enabled = true;
            aircraftAlive = true;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }
    }
    //在游戏开始后的几秒钟执行此协程
    IEnumerator AircraftAppear()
    {
        yield return new WaitForSeconds(firstAppearTime);
        //打开飞行器代码
        transform.position = new Vector3(playerTransform.position.x + appearDistance, playerTransform.position.y + appearHigh, 0);
        box.enabled = true;
        spriteRenderer.enabled = true;
        aircraftAlive = true;
    }
    //判断飞行器状态的函数
    AircraftState GetAircraftState()
    {
        if (isAttack)
            return AircraftState.Attack;
        //飞行器活动距离
        float moveDis = followDistance - noRespondDistance;
        //飞行器与玩家距离
        float pAndADis = Mathf.Abs(playerTransform.position.x - transform.position.x);
        //飞行完成度
        float completeness = (pAndADis - noRespondDistance) / moveDis;
        if (completeness >= 0.8f)
            return AircraftState.Wait;
        else
            return AircraftState.Normal;
    }
    //按下攻击键执行
    private void Attack(InputAction.CallbackContext obj)
    {
        if(!isAttack&& aircraftState==AircraftState.Wait)
        {
            isAttack = true;
            FXEvent.RaiseEvent(attackAudio);
            StartCoroutine("EndAttack");
        }
    }
    //时间一到解除攻击状态
    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(attackTime);
        isAttack = false;
        attackedTime = 0;
    }
    //状态管理函数
    void StateControl(AircraftState state)
    {
        switch(state)
        {
            case AircraftState.Normal:
                {
                    spriteRenderer.color = normalColor;
                    if (playerRb.velocity.y < -0.2f)
                        gameObject.layer = 7;
                    else
                        gameObject.layer = 10;
                    break;
                }
            case AircraftState.Wait:
                {
                    spriteRenderer.color = waitColor;
                    gameObject.layer = 7;
                    break;
                }
            case AircraftState.Attack:
                {
                    spriteRenderer.color = attackColor;
                    gameObject.layer = 11;
                    break;
                }
        }
        if (gameObject.layer == 7|| gameObject.layer == 10)
            box.isTrigger = false;
        else
            box.isTrigger = true;
    }
    //设置飞行器位置
    private void SetAircrftPosition()
    {
        StartCoroutine("AircraftAppear");
    }
}
