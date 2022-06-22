using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GameObject hpUI;    
    public GameObject GameOverUI;
    public int HealthAmount = 10;
    public int PointAmount = 0;

    public GameObject Player;

    public AudioClip HurtSound;
    public AudioClip GameOverSound;
    public AudioClip GetPoint;

    private AudioSource ASaudio;

    public Text SocreText;

    // Start is called before the first frame update
    void Start()
    {
        this.hpUI = GameObject.Find("HP");
        ASaudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void decreaseHP()
    {
        this.hpUI.GetComponent<Image>().fillAmount -= 0.1f;
        this.HealthAmount -= 1;
        this.ASaudio.clip = HurtSound;

        if (HealthAmount == 0)
        {
            ShowGameOver();
            Player.SetActive(false);
            this.ASaudio.clip = GameOverSound;
        }

        this.ASaudio.Play();
    }

    private void ShowGameOver()
    {
        this.GameOverUI.SetActive(true);
    }

    public void IncreasePonit()
    {
        this.PointAmount += 10;
        SocreText.text = this.PointAmount.ToString();
        this.ASaudio.clip = GetPoint;
        this.ASaudio.Play();
    }
}
