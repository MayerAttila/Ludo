using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player
{ 
    public string name;
    public Color color;
    public string colorStr;
    public int difficulty;
    public int score;
    public Sprite figureSprite;
    public List<Sprite> shieldSprites;

    public Player(string name, Color color, string colorStr, int difficulty, int score,  Sprite figureSprite, List<Sprite> shieldSprites) 
    {
        this.name = name;
        this.color = color;
        this.colorStr = colorStr;
        this.difficulty = difficulty;
        this.score = score;
        this.figureSprite = figureSprite;
        this.shieldSprites = shieldSprites;

    }

    public void PlayerInfos() 
    {
        Debug.Log("name:" + name + " color:" + colorStr + " dif:" + difficulty.ToString());
        
    }
}

public class SavePlayerData : MonoBehaviour
{
    public static List<Player> players = new List<Player>();



    public List<string> unusedColorStrings = new List<string>();
    public string tmpColorString;
    public List<Color> unusedColorColors = new List<Color>();
    public Color tmpColor;
    public List<Sprite> figureSprites = new List<Sprite>();
    public Sprite tmpFigureSprite;
    
    public List<Sprite> tmpShieldSprite;
    public List<Sprite> redShieldSprites;
    public List<Sprite> greenShieldSprites;
    public List<Sprite> blueShieldSprites;
    public List<Sprite> yellowShieldSprites;
    public List<List<Sprite>> allShieldSprites = new List<List<Sprite>>();

    public static List<string> usedPlayerNames = new List<string>();
    public string tmpPlayerName;
    public int tmpOpponentDifficulty;
    public string tmpOpponentDifficultyToString;


    
    public InputField[] inputField;
    public Text[] errorText;
    public Dropdown[] dropdown;
    public Text[] dropdownSelectedText;
    public GameObject[] panel;
    public Button[] submitButtons;
    public Button[] backButtons;
    public Toggle[] isAiButtons;
    public Slider[] difficultySliders;
    public GameObject[] bcOfDifficultyslider;
    public GameObject[] playerPanels;
    public GameObject[] submitedPanels;
    public Text[] submitedText;
    public GameObject startGameButton;
    public Text[] difText;
    
    public int playerCount = 0;



    private void Start()
    {
        players.Clear();
        usedPlayerNames.Clear();
        LoadDropdown();

        for (int i = 0; i < 4; i++) 
        {
            dropdown[i].onValueChanged.AddListener(UpdateColor);
        }

        allShieldSprites.Add(redShieldSprites);
        allShieldSprites.Add(greenShieldSprites);
        allShieldSprites.Add(blueShieldSprites);
        allShieldSprites.Add(yellowShieldSprites);
        Debug.Log(allShieldSprites.Count);
        Debug.Log(allShieldSprites[0].Count);
    }

    public void StoreDatas() 
    {
        tmpPlayerName = inputField[playerCount].text;

        if (tmpPlayerName == "")
        {
            errorText[playerCount].text = "Name space is empty!";
            errorText[playerCount].color = Color.red;
        }
        else if (usedPlayerNames.Contains(tmpPlayerName)) 
        {
            errorText[playerCount].text = "Name is already in use!";
            errorText[playerCount].color = Color.red;
        }
        else
        {
            SaveDifficulty();

            tmpColor = unusedColorColors[dropdown[playerCount].value];
            tmpFigureSprite = figureSprites[dropdown[playerCount].value];
            tmpShieldSprite = allShieldSprites[dropdown[playerCount].value];
            tmpColorString = dropdown[playerCount].options[dropdown[playerCount].value].text;
            unusedColorColors.Remove(tmpColor);
            unusedColorStrings.Remove(tmpColorString);
            figureSprites.Remove(tmpFigureSprite);
            allShieldSprites.Remove(tmpShieldSprite);
            usedPlayerNames.Add(tmpPlayerName);

            CreatePlayer();

            creatingSumbitedPanel();

            playerCount++;
            ActivatingStartButton();

            if (playerCount < 4) 
            {
                LoadDropdown();
                ActivatePlayerPanel();
            }
            
        }
    }

    void CreatePlayer() 
    {
        players.Add(new Player(tmpPlayerName, tmpColor, tmpColorString, tmpOpponentDifficulty, 0, tmpFigureSprite, tmpShieldSprite));

        players[players.Count - 1].PlayerInfos();

    }

    void creatingSumbitedPanel() 
    {
        submitedText[playerCount].text = "Name:  " + tmpPlayerName + "\n\nColor:  " + tmpColorString;
        if (players[players.Count-1].difficulty > 0)
        {
            switch (players[players.Count - 1].difficulty)
            {
                case 1:
                    tmpOpponentDifficultyToString = "Easy";
                    break;
                case 2:
                    tmpOpponentDifficultyToString = "Medium";
                    break;
                case 3:
                    tmpOpponentDifficultyToString = "Hard";
                    break;

            }

            submitedText[playerCount].text = submitedText[playerCount].text + "\n\nDifficulty:  " + tmpOpponentDifficultyToString;
        }
        submitedText[playerCount].color = Color.black;

        submitedPanels[playerCount].SetActive(true);
        Image submitedPanelColor = submitedPanels[playerCount].GetComponent<Image>();
        submitedPanelColor.color = tmpColor;
    }

    void LoadDropdown() 
    {
        dropdown[playerCount].options.Clear();
        foreach (string color in unusedColorStrings)
        {
            dropdown[playerCount].options.Add(new Dropdown.OptionData(color));
        }
        dropdown[playerCount].value = 0;
        dropdownSelectedText[playerCount].text = unusedColorStrings[0];
    }

    public void UpdateColor(int arg)
    {
        Image panelColor = panel[playerCount].GetComponent<Image>();
        panelColor.color = unusedColorColors[dropdown[playerCount].value];

    }

    public void ActivatePlayerPanel()
    { 
        playerPanels[playerCount].SetActive(true);
        UpdateColor(0);
    }

    public void InactivatePlayerPanel() 
    {
        if (playerCount < 3)
        {
            playerPanels[playerCount+1].SetActive(false);
        }
        submitedPanels[playerCount].SetActive(false);
        inputField[playerCount].text = "";
        errorText[playerCount].text = "";   
        dropdown[playerCount].value = 0;
        dropdown[playerCount].Select();
        dropdown[playerCount].RefreshShownValue();
    
    }

    public void BackButtonScript() 
    {
        playerCount--;
        ActivatingStartButton();
        InactivatePlayerPanel();

        errorText[playerCount].text = "";

        unusedColorColors.Add(players[players.Count-1].color);
        unusedColorStrings.Add(players[players.Count - 1].colorStr);
        figureSprites.Add(players[players.Count - 1].figureSprite);
        allShieldSprites.Add(players[players.Count - 1].shieldSprites);

        usedPlayerNames.Remove(players[players.Count - 1].name);

        players.RemoveAt(players.Count - 1);

        LoadDropdown();
        UpdateColor(0);


    }

    public void SaveDifficulty() 
    {
        if (playerCount == 0)
        {
            tmpOpponentDifficulty = 0;
        }
        else 
        {
            
            if (isAiButtons[playerCount - 1].isOn)
            {
                tmpOpponentDifficulty = (int)difficultySliders[playerCount - 1].value;
            }
            else
            {
                tmpOpponentDifficulty = 0;
            }
        }
        
    }

    public void BcColorChangeOfDifficultySlider() 
    {
        Image difficultySliderBcColor = bcOfDifficultyslider[playerCount-1].GetComponent<Image>();

        if (difficultySliders[playerCount - 1].value == 1)
        {
            difText[playerCount - 1].text = "Dif: Easy";
        }
        else if (difficultySliders[playerCount - 1].value == 2)
        {
            difficultySliderBcColor.color = Color.yellow;
            difText[playerCount - 1].text = "Dif: Medium";
        }
        else 
        {
            difficultySliderBcColor.color = Color.red;
            difText[playerCount - 1].text = "Dif: Hard"; 
        } 
    }

    public void ActivatingStartButton() 
    {
        if (playerCount > 1) startGameButton.SetActive(true);
        else startGameButton.SetActive(false);
    }


}
