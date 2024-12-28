using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public AudioEventSO FXEvent;
    public AudioClip jumpAudio;
    public UICanvasEventSO UICanvasEvent;
    public float moveSpeed;
    public float jumpForce;
    public bool onGround;
    public float squatExtent;
    public float deathAniTime;
    public LayerMask groundLayer;
    public LayerMask platformliveLayer;
    public LayerMask platformnotliveLayer;
    [Header("事件监听")]
    public SetPlayerControllerInputEventSO SetPlayerControllerInputEvent;
    [Header("地面检测相关参数")]
    public Vector2 offSet;
    public float checkR;

    JamInputSystem jamInputSystem;
    Rigidbody2D rb;
    BoxCollider2D box;
    SpriteRenderer spriteRenderer;
    Character character;
    PlayerAniController playerAniController;
    float isSquat;
    float isntSquat;

    private void Awake()
    {
        jamInputSystem = new JamInputSystem();
        rb = gameObject.GetComponent<Rigidbody2D>();
        box = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        character = gameObject.GetComponent<Character>();
        playerAniController = gameObject.GetComponent<PlayerAniController>();
        SetPlayerControllerInputEvent.OnEventRaised += SetPlayerControllerInput;
    }
    private void OnEnable()
    {
        jamInputSystem.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        jamInputSystem.GamePlay.Jump.started += Jump;
        isntSquat = gameObject.transform.localScale.y;
        isSquat = isntSquat * squatExtent;
    }

    // Update is called once per frame
    void Update()
    {
        //Physics2D.OverlapCircle检测player是否在地面或平台上
        onGround = Physics2D.OverlapCircle(new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y), checkR, groundLayer);
        if(!onGround)
            onGround = Physics2D.OverlapCircle(new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y), checkR, platformliveLayer);
        if(!onGround)
            onGround = Physics2D.OverlapCircle(new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y), checkR, platformnotliveLayer);
        //用inputsystem获得移动方向
        Vector2 moveDirection = jamInputSystem.GamePlay.Move.ReadValue<Vector2>();
        //根据速度和方向进行横向位移
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        //player朝向
        if(moveDirection.x!=0)
        {
            if (moveDirection.x > 0)
                gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            else
                gameObject.transform.localScale = new Vector3(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        //下蹲逻辑
        if(moveDirection.y<0)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, isSquat, gameObject.transform.localScale.z);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, isntSquat, gameObject.transform.localScale.z);
        }
    }
    private void OnDisable()
    {
        jamInputSystem.Disable();        
    }

    //玩家按下跳跃键会执行此跳跃函数
    private void Jump(InputAction.CallbackContext obj)
    {
        if(onGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            playerAniController.PlayJumpAni();
            FXEvent.RaiseEvent(jumpAudio);
        }          
    }

    //OnDrawGizmosSelected展示地面检测的范围
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y), checkR);
    }
    //player死亡执行此逻辑
    public void PlayerDie()
    {
        //死亡不能被操控
        jamInputSystem.Disable();
        //暂时消失
        box.enabled = false;
        spriteRenderer.enabled = false;
        //播放死亡特效


        //打开StopMenu
        StartCoroutine(OpenStopMenu());
    }
    IEnumerator OpenStopMenu()
    {
        yield return new WaitForSeconds(deathAniTime);
        UICanvasEvent.RaiseEvent(UICanvas.Ignore, UICanvas.StopMenu);
    }
    //player复活执行此逻辑
    public void PlayerResurrected()
    {
        //可以被操控
        jamInputSystem.Enable();
        //关闭StopMenu
        UICanvasEvent.RaiseEvent(UICanvas.StopMenu,UICanvas.Ignore );
        //血量恢复
        gameObject.GetComponent<Character>().currentLife = gameObject.GetComponent<Character>().maxLife;
        //出现
        box.enabled = true;
        spriteRenderer.enabled = true;
        character.Resurrected();
    }
    void SetPlayerControllerInput(bool state)
    {
        if(state)
            jamInputSystem.Enable();
        else
            jamInputSystem.Disable();
    }
    private void OnDestroy()
    {
        SetPlayerControllerInputEvent.OnEventRaised -= SetPlayerControllerInput;
    }
}
