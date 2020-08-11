using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectScript : MonoBehaviour
{
    public GameObject[] available_Heroes;

    private int currentIndex;

    public Text selected_Text;
    public GameObject starIcon;
    public Image selectBtn_Image;
    public Sprite button_Green;
    public Sprite button__Blue;

    private bool[] heroes;

    public Text starScoreText;

    private void Start()
    {
        InitializeCharacters();
    }

    void InitializeCharacters()
    {
        currentIndex = GameManager.instance.selected_Index;

        for (int i = 0; i < available_Heroes.Length; i++)
            available_Heroes[i].SetActive(false);

        available_Heroes[currentIndex].SetActive(true);

        heroes = GameManager.instance.heroes;
    }

    public void NextHero()
    {
        available_Heroes[currentIndex].SetActive(false);

        if (currentIndex + 1 == available_Heroes.Length)
            currentIndex = 0;
        else
            currentIndex++;

        available_Heroes[currentIndex].SetActive(true);

        CheckIfCharacterIsUnlocked();
    }

    public void PreviousHero()
    {
        available_Heroes[currentIndex].SetActive(false);

        if (currentIndex - 1 == -1)
            currentIndex = available_Heroes.Length - 1;
        else
            currentIndex--;

        available_Heroes[currentIndex].SetActive(true);
    }

    void CheckIfCharacterIsUnlocked()
    {
        if (heroes[currentIndex]) //< If the hero is unlocked
        {
            starIcon.SetActive(false);

            if (currentIndex == GameManager.instance.selected_Index)
            {
                selectBtn_Image.sprite = button_Green;
                selected_Text.text = "Selected";
            }
            else
            {
                selectBtn_Image.sprite = button__Blue;
                selected_Text.text = "Select?";
            }
        }
        else //< Is the hero is locked
        {
            selectBtn_Image.sprite = button__Blue;
            starIcon.SetActive(true);
            selected_Text.text = "350";
        }
    }

    public void SelectHero()
    {
        if (!heroes[currentIndex])
        {
            if (currentIndex != GameManager.instance.selected_Index)
            {
                if (GameManager.instance.starScore >= 350)
                {
                    GameManager.instance.starScore -= 350; //< Subtract the actual starScore

                    // Then unlock the hero
                    selectBtn_Image.sprite = button_Green;
                    selected_Text.text = "Selected";
                    starIcon.SetActive(false);
                    heroes[currentIndex] = true;

                    starScoreText.text = GameManager.instance.starScore.ToString();

                    // Then save everything in game data
                    GameManager.instance.selected_Index = currentIndex;
                    GameManager.instance.heroes = heroes;

                    GameManager.instance.SaveGameData();
                }
                else
                {
                    print("You haven't enough coin to unlock this hero!");
                }
            }
        }
        else
        {
            selectBtn_Image.sprite = button_Green;
            selected_Text.text = "Selected";
            GameManager.instance.selected_Index = currentIndex;

            GameManager.instance.SaveGameData();
        }
    }
}
