using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour //< Every functions in this class are called in Unity with OnClick function
{
    public GameObject heroMenu;
    public Text starScoreText;

    public Image music_Img;
    public Sprite music_Off;
    public Sprite music_On;

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void HeroMenu()
    {
        heroMenu.SetActive(true);
        starScoreText.text = "" + GameManager.instance.starScore;
    }

    public void HomeButton()
    {
        heroMenu.SetActive(false);
    }

    public void MusicButton()
    {
        if (GameManager.instance.playSound)
        {
            music_Img.sprite = music_Off;
            GameManager.instance.playSound = false;
        }
        else
        {
            music_Img.sprite = music_On;
            GameManager.instance.playSound = true;
        }
    }
}
