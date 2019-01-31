using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class anaMenu : MonoBehaviour {

    public Text puantext;
    public Text puan;
	void Start () {
        int enYuksekSkor = PlayerPrefs.GetInt("enyuksekpuankayit");
        int puanGelen = PlayerPrefs.GetInt("puanKayit");

        puantext.text = "EN YUKSEK PUAN =" + enYuksekSkor;
        puan.text = "Puan= " + puanGelen;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void oyunaGit()
    {
        SceneManager.LoadScene("level1");
    }
    public void oyundanCik()
    {
        Application.Quit();
    }
}
