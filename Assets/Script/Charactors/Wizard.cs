using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Wizard : MonoBehaviour
{
    // components
    private Rigidbody2D rb;
    private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    public GameObject summonEffect;
    public AudioSource summonAudio;

    // basic elements 
    [SerializeField]
    private float speed;
    [SerializeField]
    public int HP;

    public Slider slider;
    private float factor;

    public int  flip;

    // summon
    bool isSummonning;
    float summonTimer;
    private CardType cardType;

    //hurt
    float invincibleTimer = 1.0f;
    bool isInvincible;

    public AudioSource[] audioList;
    public GameObject losePanel;
    private bool isLose = false;
    private void Awake()
    {
        flip = 1;
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isSummonning = false;
        isInvincible = false;


        EventCenter.AddListener<CardType,float>(EventDefine.Summon, Summon);
        EventCenter.AddListener(EventDefine.WizardHurt, TakenDamage);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<CardType,float>(EventDefine.Summon, Summon);
        EventCenter.RemoveListener(EventDefine.WizardHurt, TakenDamage);
    }


    private void Update()
    {
        if(isSummonning)
        {
            summonTimer -= Time.deltaTime;
            if(summonTimer <= 0)
            {
                isSummonning=false;
                EventCenter.Broadcast(EventDefine.SummonFinished, cardType);
                Instantiate(summonEffect,transform.position, Quaternion.identity);
                summonAudio.Play();
            }
            rb.velocity = Vector2.zero;
            slider.gameObject.active = true;
            slider.value = 5 - (summonTimer / factor) * 5;
        }
        else
        {
            slider.gameObject.active = false;
            Movment();
        }

        if(HP <= 0 )
        {
            Debug.Log("U lose!");
            //EventCenter.Broadcast(EventDefine.ReStartScene);
            // test 
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            if (!isLose)
            {
                loseGame();
            }
            
        }

        if(isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }
        else
        {
            spriteRenderer.enabled = true;
        }
        
    }
    void loseGame()
    {
        isLose = true;
        Time.timeScale = 0f;
        audioList[5].Play();
        losePanel.SetActive(true);

    }

    private void Movment()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, y, 0).normalized;
        rb.velocity = dir * speed;

        if (dir.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            flip = -1;
        }
        else if(dir.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            flip = 1;
        }
        if (transform.position.x < -17) transform.position = new Vector3(-17,transform.position.y,transform.position.z);
        if (transform.position.x > 16) transform.position = new Vector3(16, transform.position.y, transform.position.z);
        if (transform.position.y > 9) transform.position = new Vector3(transform.position.x, 9, transform.position.z);
        if (transform.position.y < -9) transform.position = new Vector3(transform.position.x, -9, transform.position.z);
    }

    public void DamageTaken(int dmg)
    {
        HP -= dmg;
    }

    public void Summon(CardType card_type, float time)
    {
        isSummonning = true;
        cardType = card_type;
        summonTimer = time;
        factor = summonTimer;
    }

    private void TakenDamage()
    {
        if(!isInvincible)
        {
            HP -= 1;
            int audio = Random.Range(0, 5);
            audioList[audio].Play();
            isInvincible = true;
            invincibleTimer = 1.0f;
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        for(int i = 0;i<10;i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
