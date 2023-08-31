using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OyunKontrol : MonoBehaviour
{

    // GENEL AYARLAR
    public int hedefbasari;
    int ilkSecimDegeri;
    int anlikbasari;    
    //-----------------------
    GameObject secilenButon;
    GameObject butonunKendisi;
    //-----------------------
    public Sprite defaultSprite;
    public Animation anim;

    public AudioSource[] sesler;
    public GameObject[] Butonlar;
    public TextMeshProUGUI Sayac;
    public GameObject[] OyunSonuPaneller;    
    // SAYAÇ
    public float ToplamZaman = 3;
    float dakika;
    float saniye;
    bool zamanlayici;
    //-----------------------  

    void Start()
    {
        ilkSecimDegeri = 0;
        zamanlayici = true;
        
    }
    private void Update()
    {

        if (zamanlayici && ToplamZaman>1)
        {
            ToplamZaman -= Time.deltaTime;

            dakika = Mathf.FloorToInt(ToplamZaman / 60); //  119 / 2 = 1       
            saniye = Mathf.FloorToInt(ToplamZaman % 60); // 119 / 2 = 1 *****  => 59     

            // Sayac.text = Mathf.FloorToInt(ToplamZaman).ToString();
            Sayac.text = string.Format("{0:00}:{1:00}", dakika, saniye);

        }else
        {
            zamanlayici = false;
            GameOver();
        }
       
    }
    public void Oyunudurdur()
    {
        OyunSonuPaneller[2].SetActive(true);
        anim = null;
        anim.Play("Pause animation");
        Time.timeScale = 0;

    }
    public void OyunaDevamEt()
    {
        OyunSonuPaneller[2].SetActive(false);
        Time.timeScale = 1;

    }
    void GameOver()
    {
        OyunSonuPaneller[0].SetActive(true);

    }
    void Win()
    {
        OyunSonuPaneller[1].SetActive(true);

    }
    public void AnaMenu()
    {
        SceneManager.LoadScene("AnaMenu");

    }

    public void sonlvlbır() 
    {
        SceneManager.LoadScene("2");
    
    }
    public void sonlvmap()
    {
        SceneManager.LoadScene("map");

    }
    public void levelsonu()
    {
        SceneManager.LoadScene("bitti");

    }
    public void sonlvlıkı()
    {
        SceneManager.LoadScene("3");

    }
    public void sonlvluc()
    {
        SceneManager.LoadScene("4");

    }
    public void sonlvlbes()
    {
        SceneManager.LoadScene("5");

    }
    public void sonlvlaltı()
    {
        SceneManager.LoadScene("6");

    }
    public void TekrarOyna()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      
    }

    public void ObjeVer(GameObject objem) 
    {
        butonunKendisi = objem;

        butonunKendisi.GetComponent<Image>().sprite = butonunKendisi.GetComponentInChildren<SpriteRenderer>().sprite;
        butonunKendisi.GetComponent<Image>().raycastTarget = false;
        sesler[0].Play();
    }
    public void effectver(ParticleSystem pt) 
    {
    
     
        pt.Play();

    
    }

    void Butonlarindurumu(bool durum)
    {
        foreach (var item in Butonlar)
        {
            if (item!=null)
            {
                item.GetComponent<Image>().raycastTarget = durum;

            }            
        }

    }
    
    public void ButonTikladi(int deger)
    {

        Kontrol(deger);
        
    }   

    void Kontrol(int gelendeger)
    {

        if (ilkSecimDegeri == 0)
        {
            ilkSecimDegeri = gelendeger;
            secilenButon = butonunKendisi;
        }
        else
        {
            StartCoroutine(kontroletbakalim(gelendeger));
        }

    }
    IEnumerator kontroletbakalim(int gelendeger)
    {
        Butonlarindurumu(false);
        yield return new WaitForSeconds(1);

        if (ilkSecimDegeri == gelendeger)
        {
            anlikbasari++;
            secilenButon.GetComponent<Image>().enabled = false;
            butonunKendisi.GetComponent<Image>().enabled = false;
            // secilenButon.GetComponent<Button>().enabled = false;
            // butonunKendisi.GetComponent<Button>().enabled = false;
            ilkSecimDegeri = 0;
            secilenButon = null;
            Butonlarindurumu(true);

            if (hedefbasari == anlikbasari)
            {
                Win();

            }
        }
        else
        {
          
            sesler[1].Play();
            secilenButon.GetComponent<Image>().sprite = defaultSprite;
            butonunKendisi.GetComponent<Image>().sprite = defaultSprite;            
            ilkSecimDegeri = 0;
            secilenButon = null;
            Butonlarindurumu(true);


        }

    }
    public void maplevel() 
    {


        SceneManager.LoadScene("map");
    
    }
}
