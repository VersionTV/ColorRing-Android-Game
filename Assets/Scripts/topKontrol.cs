using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class topKontrol : MonoBehaviour
{
    [SerializeField]
    Text scoreText,recordText,tapText;
    [SerializeField]
    Image countdown;
    [SerializeField]
    Animator ani;
    [SerializeField]
    GameObject hintpanel;
    public static int score = 0;
    Rigidbody2D rb;
    public float ziplamaKuvveti = 3f;
    bool basildimi = false;
    public string mevcutRenk;
    public Color topunRengi;
    public Color turkuaz, sari, mor, pembe;
    public GameObject halka, renktekeri;
    int bestScore =0;
    float timer = 3f;
    bool isTap = false;
    bool hintEnable=false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bestScore = PlayerPrefs.GetInt("BestS");
        recordText.text = "Record : " + bestScore;
    }
    private void Start()
    {
        RastgeleRenkBelirle();
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        scoreText.text = "Score: " + score;
    }
    private void Update()
    {
        if(isTap)
        {
            
            ani.SetTrigger("start");
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                //oyun basliyo
                countdown.enabled = false;
                rb.constraints = RigidbodyConstraints2D.None;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            basildimi = true;
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            basildimi = false;
        }
    }
    private void FixedUpdate()
    {
        if(basildimi)
        {
            rb.velocity = Vector2.up * ziplamaKuvveti;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "RenkTekeri")
        {
            RastgeleRenkBelirle();
            Destroy(collision.gameObject);
            return;
        }
       if(collision.tag!=mevcutRenk && collision.tag != "PuanArttirici" && collision.tag != "RenkTekeri")
        {
            if (score > bestScore)
            {
                PlayerPrefs.SetInt("BestS", score);
            }
            score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.tag == "PuanArttirici")
        {
            score += 5;
            scoreText.text = "Score: " + score;
            Destroy(collision.gameObject);
            Instantiate(halka, new Vector3(transform.position.x, transform.position.y + 8f, transform.position.z),Quaternion.identity);

        }
        if ((collision.tag == "die"))
        {
            score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    void RastgeleRenkBelirle()
    {
        int rastgeleSayi = Random.Range(0, 4);

        switch (rastgeleSayi)
        {
            case 0:
                mevcutRenk = "Turkuaz";
                topunRengi = turkuaz;
                break;


            case 1:
                mevcutRenk = "Sari";
                topunRengi = sari;
                break;
            case 2:
                mevcutRenk = "Pembe";
                topunRengi = pembe;
                break;
            case 3:
                mevcutRenk = "Mor";
                topunRengi = mor;
                break;

        }
        GetComponent<SpriteRenderer>().color = topunRengi;
    }
    public void TaptoPlay()
    {
        isTap=true;
        tapText.enabled = false;
    }
    public void HintMenu()
    {
        if(!hintEnable)
        {
            hintEnable = !hintEnable;
            hintpanel.SetActive(hintEnable);
            



        }
        else
        {
            hintpanel.SetActive(false);
            hintEnable = !hintEnable;
        }
        
    }
}
