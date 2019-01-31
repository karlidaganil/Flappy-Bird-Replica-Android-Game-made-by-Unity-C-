using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Kontrol : MonoBehaviour {

    public Sprite []KusSprite;
    SpriteRenderer spriteRenderer;
    bool ileriGeriKontrol = true;
    int kusSayac = 0;
    float kusAnimasyonZaman = 0;
    Rigidbody2D fizik;
    public float jumpForce;

    public Text puanTexi;
    bool oyunBitti = true;

    int puan = 0;
    int enYuksekPuan = 0;
    oyunKontrol oyunKontrol;

    //AudioSource ses;

    //public AudioClip carpmaSesi;
    //public AudioClip puanSesi;
    //public AudioClip kanatSesi;

    AudioSource []sesler;
    

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        oyunKontrol = GameObject.FindGameObjectWithTag("oyunkontroltagi").GetComponent<oyunKontrol>();
        //ses = GetComponent<AudioSource>();

        sesler = GetComponents<AudioSource>();
        enYuksekPuan = PlayerPrefs.GetInt("enyuksekpuankayit");

	}
	
	
	void Update () {
        if(Input.GetMouseButtonDown(0) && oyunBitti)
        {
            fizik.velocity = new Vector2(0f, 0f); //hızı 0 yaptık 
            fizik.AddForce(new Vector2(0f, jumpForce));  //ondan sonra kuvvet uyguladık
            //ses.clip = kanatSesi;
            //ses.Play();
            sesler[0].Play();
        
        }
        if(fizik.velocity.y>0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 45);
        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 0f, -45);
        }
        animasyon();
	}
    void animasyon()
    {
        kusAnimasyonZaman = kusAnimasyonZaman + Time.deltaTime;
        if (kusAnimasyonZaman > 0.2f)
        {
            kusAnimasyonZaman = 0;
            if (ileriGeriKontrol)
            {
                spriteRenderer.sprite = KusSprite[kusSayac];
                kusSayac++;
                if (kusSayac == KusSprite.Length)
                {
                    kusSayac--;
                    ileriGeriKontrol = false;
                }
            }
            else
            {
                kusSayac--;
                spriteRenderer.sprite = KusSprite[kusSayac];
                if (kusSayac == 0)
                {
                    kusSayac++;
                    ileriGeriKontrol = true;
                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag=="puantagi")
        {
            puan++;
            puanTexi.text = "Puan =" + puan;
            //ses.clip = puanSesi;
            //ses.Play();
            sesler[1].Play();
            Debug.Log(puan);
        }
        if(coll.gameObject.tag=="engeltagi")
        {
            oyunBitti = false;
            //ses.clip = carpmaSesi;
            //ses.Play();
            sesler[2].Play();
            
            oyunKontrol.oyunBitti();
            GetComponent<CircleCollider2D>().enabled = false;
            
            if(puan>enYuksekPuan)
            {
                enYuksekPuan = puan;
                PlayerPrefs.SetInt("enyuksekpuankayit", enYuksekPuan);
            }
            Invoke("anamenuyedon", 2f);

        }
    }
    void anamenuyedon()
    {
        PlayerPrefs.SetInt("puanKayit", puan);
        SceneManager.LoadScene("Menu");
    }
}
