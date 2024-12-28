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
    [Header("�¼�����")]
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
        //��ȡ������״̬
        aircraftState= GetAircraftState();
        //״̬����
        StateControl(aircraftState);
        //����player����aircraft��Զ
        float disPToA = playerTransform.position.x - transform.position.x;
        //��ȡ��������
        if(!isAttack)
        {
            if (disPToA > 0)
                attackDir = 1;
            else
                attackDir = -1;
        }
        //�ٶ�ƥ��
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
        //�����ٶ�ȷ������
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
        //����aircraft�Ĵ���״ִ̬�ж�Ӧ�߼�
        if(aircraftAlive)
        {
            //�رշ���������
            box.enabled = false;
            spriteRenderer.enabled = false;
            aircraftAlive = false;
        }
        else
        {
            //�򿪷���������
            transform.position = new Vector3(playerTransform.position.x + appearDistance, playerTransform.position.y + appearHigh, transform.position.y);
            box.enabled = true;
            spriteRenderer.enabled = true;
            aircraftAlive = true;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }
    }
    //����Ϸ��ʼ��ļ�����ִ�д�Э��
    IEnumerator AircraftAppear()
    {
        yield return new WaitForSeconds(firstAppearTime);
        //�򿪷���������
        transform.position = new Vector3(playerTransform.position.x + appearDistance, playerTransform.position.y + appearHigh, 0);
        box.enabled = true;
        spriteRenderer.enabled = true;
        aircraftAlive = true;
    }
    //�жϷ�����״̬�ĺ���
    AircraftState GetAircraftState()
    {
        if (isAttack)
            return AircraftState.Attack;
        //�����������
        float moveDis = followDistance - noRespondDistance;
        //����������Ҿ���
        float pAndADis = Mathf.Abs(playerTransform.position.x - transform.position.x);
        //������ɶ�
        float completeness = (pAndADis - noRespondDistance) / moveDis;
        if (completeness >= 0.8f)
            return AircraftState.Wait;
        else
            return AircraftState.Normal;
    }
    //���¹�����ִ��
    private void Attack(InputAction.CallbackContext obj)
    {
        if(!isAttack&& aircraftState==AircraftState.Wait)
        {
            isAttack = true;
            FXEvent.RaiseEvent(attackAudio);
            StartCoroutine("EndAttack");
        }
    }
    //ʱ��һ���������״̬
    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(attackTime);
        isAttack = false;
        attackedTime = 0;
    }
    //״̬������
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
    //���÷�����λ��
    private void SetAircrftPosition()
    {
        StartCoroutine("AircraftAppear");
    }
}
