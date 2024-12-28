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
    [Header("�¼�����")]
    public SetPlayerControllerInputEventSO SetPlayerControllerInputEvent;
    [Header("��������ز���")]
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
        //Physics2D.OverlapCircle���player�Ƿ��ڵ����ƽ̨��
        onGround = Physics2D.OverlapCircle(new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y), checkR, groundLayer);
        if(!onGround)
            onGround = Physics2D.OverlapCircle(new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y), checkR, platformliveLayer);
        if(!onGround)
            onGround = Physics2D.OverlapCircle(new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y), checkR, platformnotliveLayer);
        //��inputsystem����ƶ�����
        Vector2 moveDirection = jamInputSystem.GamePlay.Move.ReadValue<Vector2>();
        //�����ٶȺͷ�����к���λ��
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        //player����
        if(moveDirection.x!=0)
        {
            if (moveDirection.x > 0)
                gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            else
                gameObject.transform.localScale = new Vector3(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        //�¶��߼�
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

    //��Ұ�����Ծ����ִ�д���Ծ����
    private void Jump(InputAction.CallbackContext obj)
    {
        if(onGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            playerAniController.PlayJumpAni();
            FXEvent.RaiseEvent(jumpAudio);
        }          
    }

    //OnDrawGizmosSelectedչʾ������ķ�Χ
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y), checkR);
    }
    //player����ִ�д��߼�
    public void PlayerDie()
    {
        //�������ܱ��ٿ�
        jamInputSystem.Disable();
        //��ʱ��ʧ
        box.enabled = false;
        spriteRenderer.enabled = false;
        //����������Ч


        //��StopMenu
        StartCoroutine(OpenStopMenu());
    }
    IEnumerator OpenStopMenu()
    {
        yield return new WaitForSeconds(deathAniTime);
        UICanvasEvent.RaiseEvent(UICanvas.Ignore, UICanvas.StopMenu);
    }
    //player����ִ�д��߼�
    public void PlayerResurrected()
    {
        //���Ա��ٿ�
        jamInputSystem.Enable();
        //�ر�StopMenu
        UICanvasEvent.RaiseEvent(UICanvas.StopMenu,UICanvas.Ignore );
        //Ѫ���ָ�
        gameObject.GetComponent<Character>().currentLife = gameObject.GetComponent<Character>().maxLife;
        //����
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
