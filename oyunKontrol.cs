using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyunKontrol : MonoBehaviour {

    public GameObject gokyuzuBir;
    public GameObject gokyuzuIki;
    public float arkaPlanHızı = -1.5f;

    Rigidbody2D fizikBir;
    Rigidbody2D fizikİki;

    float uzunluk = 0f;
    int sayac = 0;
    bool oyunbitti = true;

    public GameObject engel;
    public int kacAdetEngel=5;
    GameObject []engeller;

    float degisimZaman = 0f;


	void Start () {
        fizikBir = gokyuzuBir.GetComponent<Rigidbody2D>();
        fizikİki = gokyuzuIki.GetComponent<Rigidbody2D>();

        fizikBir.velocity = new Vector2(arkaPlanHızı, 0f);
        fizikİki.velocity = new Vector2(arkaPlanHızı, 0f);

        uzunluk = gokyuzuBir.GetComponent<BoxCollider2D>().size.x;

        engeller = new GameObject[kacAdetEngel];
        for(int i=0;i<engeller.Length;i++)
        {
            engeller[i] = Instantiate(engel, new Vector2(-20, -20), Quaternion.identity);
            Rigidbody2D fizikEngel = engeller[i].AddComponent<Rigidbody2D>();
            fizikEngel.gravityScale = 0f;
            fizikEngel.velocity = new Vector2(arkaPlanHızı, 0f);
            
        }

    }
	
	// Update is called once per frame
	void Update () {
        if(oyunbitti)
        {
            if (gokyuzuBir.transform.position.x <= -uzunluk)
            {
                gokyuzuBir.transform.position = gokyuzuBir.transform.position + new Vector3(uzunluk * 2, 0, 0);
            }
            if (gokyuzuIki.transform.position.x <= -uzunluk)
            {
                gokyuzuIki.transform.position = gokyuzuIki.transform.position + new Vector3(uzunluk * 2, 0, 0);
            }

            //-----------------------------------------------------------------------------------------
            degisimZaman = degisimZaman + Time.deltaTime;
            if (degisimZaman > 2f)
            {
                degisimZaman = 0;
                float Yeksenim = Random.Range(-0.50f, 1.10f);
                engeller[sayac].transform.position = new Vector3(18f, Yeksenim);
                sayac++;
                if (sayac >= engeller.Length)
                {
                    sayac = 0;
                }
            }
        }
        else
        {
            //Debug.Log("else durumu");
        }
     




    }
    public void oyunBitti()
    {
        for(int i=0; i<engeller.Length; i++)
        {
            engeller[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;//hızlarını 0 ladık
            fizikBir.velocity = Vector2.zero;
            fizikİki.velocity = Vector2.zero;
        }
        oyunbitti = false;
    }
}
