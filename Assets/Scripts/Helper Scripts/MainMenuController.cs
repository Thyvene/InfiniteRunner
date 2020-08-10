using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
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
        // TODO Display the star score
    }

    public void HomeButton()
    {
        heroMenu.SetActive(false);
    }
}
