using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Figure 
{
    public GameObject gObject;
    public string name;
    public bool inGame;
    public bool inRoadToFinish;
    public bool inFinish;
    public Collider2D collider;
    public int startingPositionIndex;
    public int currentPositionIndex;
    public GameObject bcForHightlight;
    public List<Vector3> roadToFinishPositions;
    public Vector3 spawnPosition;
    public int moveCount;
    public SpriteRenderer currentSpriteRenderer;

    public Figure(GameObject gObject, int startingPossiotionIndex, List<Vector3> roadToFinishPositions)
    {
        this.gObject = gObject;
        inGame = false;
        name = gObject.name;
        inRoadToFinish = false;
        inFinish = false;
        collider = gObject.GetComponent<Collider2D>();
        collider.enabled = false;
        this.startingPositionIndex = startingPossiotionIndex;
        bcForHightlight = gObject.transform.GetChild(0).gameObject;
        this.roadToFinishPositions = roadToFinishPositions;
        moveCount = 0;
        spawnPosition = gObject.transform.position;
        currentSpriteRenderer = gObject.GetComponent<SpriteRenderer>();
    }

    public Vector3 GetPosition() 
    {
        return gObject.transform.position;
    }

    public void SetPosition(Vector3 positon)
    {
        gObject.transform.position = positon;
    }

    public void SetSprite(Sprite sprite) 
    {
        currentSpriteRenderer.sprite = sprite;
    }
}

public class Game : MonoBehaviour
{
    List<Player> players = new List<Player>();

    public Text[] playerNamesOnBoard;

    public GameObject historyPanel;
    public Text historyNames;
    public Text historyDiceTrows;
    public List<string> namesForHistory = new List<string>();
    public List<string> valuesForHistoryDiceTrows = new List<string>();

    public Button diceButton;
    public Sprite[] diceFaces;
    public Text diceThrowText;

    public GameObject diceThrowPanel;
    Image diceImage;
    int diceThrow;
    int playerCount;
    int WhoesRound = 0;


    public GameObject[] blueFigureGameObjecs;
    public GameObject[] greenFigureGameObjecs;
    public GameObject[] redFigureGameObjecs;
    public GameObject[] yellowFigureGameObjecs;

    public GameObject sampleForScale;

    public Figure[][] allFigures;
    public Vector3[][] allFigurePositions;


    List<Vector3> boardPositions = new List<Vector3>();
    List<Vector3> redRoadToFinishPositions = new List<Vector3>();
    List<Vector3> greenRoadToFinishPositions = new List<Vector3>();
    List<Vector3> blueRoadToFinishPositions = new List<Vector3>();
    List<Vector3> yellowRoadToFinishPositions = new List<Vector3>();


    public float scaleX; 
    public float scaleY;

    List<Figure> movableFigures;

    public List<int> figuresInFinish;

    public GameObject winMenu;
    public Text winMenuText;

    Collider2D c2d;

    public int[] takenEnemyIndex;





    void Start()
    {
        players = SavePlayerData.players;
        allFigures = new Figure[players.Count][];
        allFigurePositions = new Vector3[players.Count][];

        playerCount = players.Count;
        scaleX = sampleForScale.transform.localScale.x;
        scaleY = sampleForScale.transform.localScale.y;

        Button tmp = diceButton.GetComponent<Button>();
        diceImage = tmp.image;

        takenEnemyIndex = new int[2];

        diceThrowText.text = players[0].name + "'s turn";

        for (int i = 0; i < playerCount; i++)
        {
            figuresInFinish.Add(0);
        }

        winMenu.SetActive(false);


        GenerateBoardPositions();
        LoadPlayerNamesToBoard();
        GetAllFigurePositions();
        RenderFigures();

    }

    

   

    void GenerateBoardPositions()
    {
        Vector3 tmpBoardPosition = new Vector3(-8 / scaleX, -66.6f / scaleY, 0);
        Vector3 tmpReadtoFinishPosition;

        for (int i = 0; i < 52; i++)
        {
            //a sok ifet atirni majd swich case re
            if (i < 6)
            {
                tmpBoardPosition = new Vector3(tmpBoardPosition.x, tmpBoardPosition.y + 8.3f / scaleY, 0); //y+

            }
            else if (i < 12)
            {
                if (i == 6) tmpBoardPosition = new Vector3(-8.2f / scaleX, -8 / scaleY, 0);

                tmpBoardPosition = new Vector3(tmpBoardPosition.x - 8.3f / scaleX, tmpBoardPosition.y, 0); //x-
            }
            else if (i < 14)
            {
                tmpBoardPosition = new Vector3(tmpBoardPosition.x, tmpBoardPosition.y + 8.3f / scaleY, 0); //y+


                if (i == 12)
                {

                    tmpReadtoFinishPosition = tmpBoardPosition;
                    for (int j = 0; j < 6; j++)
                    {
                        tmpReadtoFinishPosition = new Vector3(tmpReadtoFinishPosition.x + 8.3f / scaleX, tmpReadtoFinishPosition.y, 0); //yellow
                        yellowRoadToFinishPositions.Add(tmpReadtoFinishPosition);
                    }
                }


            }
            else if (i < 19)
            {
                tmpBoardPosition = new Vector3(tmpBoardPosition.x + 8.3f / scaleX, tmpBoardPosition.y, 0); //x+
            }
            else if (i < 25)
            {
                if (i == 19) tmpBoardPosition = new Vector3(-8.2f / scaleX, 8.6f / scaleY, 0);

                tmpBoardPosition = new Vector3(tmpBoardPosition.x, tmpBoardPosition.y + 8.3f / scaleY, 0); //y+
            }
            else if (i < 27)
            {
                tmpBoardPosition = new Vector3(tmpBoardPosition.x + 8.3f / scaleX, tmpBoardPosition.y, 0); //x+


                if (i == 25)
                {

                    tmpReadtoFinishPosition = tmpBoardPosition;
                    for (int j = 0; j < 6; j++)
                    {
                        tmpReadtoFinishPosition = new Vector3(tmpReadtoFinishPosition.x, tmpReadtoFinishPosition.y - 8.3f / scaleY, 0); //green
                        greenRoadToFinishPositions.Add(tmpReadtoFinishPosition);
                    }
                }

            }
            else if (i < 32)
            {
                tmpBoardPosition = new Vector3(tmpBoardPosition.x, tmpBoardPosition.y - 8.3f / scaleY, 0); //y-
            }
            else if (i < 38)
            {
                if (i == 32) tmpBoardPosition = new Vector3(8.2f / scaleX, 8.6f / scaleY, 0);

                tmpBoardPosition = new Vector3(tmpBoardPosition.x + 8.3f / scaleX, tmpBoardPosition.y, 0); //x+
            }
            else if (i < 40)
            {
                tmpBoardPosition = new Vector3(tmpBoardPosition.x, tmpBoardPosition.y - 8.3f / scaleY, 0); //y-


                if (i == 38)
                {

                    tmpReadtoFinishPosition = tmpBoardPosition;
                    for (int j = 0; j < 6; j++)
                    {
                        tmpReadtoFinishPosition = new Vector3(tmpReadtoFinishPosition.x - 8.3f / scaleX, tmpReadtoFinishPosition.y, 0); //red
                        redRoadToFinishPositions.Add(tmpReadtoFinishPosition);
                    }
                }

            }
            else if (i < 45)
            {
                tmpBoardPosition = new Vector3(tmpBoardPosition.x - 8.3f / scaleX, tmpBoardPosition.y, 0); //x-
            }
            else if (i < 51)
            {
                if (i == 45) tmpBoardPosition = new Vector3(8.2f / scaleX, -8.6f / scaleY, 0);

                tmpBoardPosition = new Vector3(tmpBoardPosition.x, tmpBoardPosition.y - 8.3f / scaleY, 0); //y-
            }
            else
            {
                tmpBoardPosition = new Vector3(tmpBoardPosition.x - 8.3f / scaleX, tmpBoardPosition.y, 0); //x-

                if (i == 51)
                {

                    tmpReadtoFinishPosition = tmpBoardPosition;
                    for (int j = 0; j < 6; j++)
                    {
                        tmpReadtoFinishPosition = new Vector3(tmpReadtoFinishPosition.x, tmpReadtoFinishPosition.y + 8.3f / scaleY, 0); //blue
                        blueRoadToFinishPositions.Add(tmpReadtoFinishPosition);
                    }
                }

            }
            boardPositions.Add(tmpBoardPosition);

        }

    }



    void LoadPlayerNamesToBoard()
    {
        int tmpCounter = 0;

        ColorChangeOfPanels();



        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].colorStr == "Yellow")
            {
                playerNamesOnBoard[0].text = players[i].name;
                CreateFigureClassElements(yellowFigureGameObjecs, 14, tmpCounter, yellowRoadToFinishPositions);
            }
            else if (players[i].colorStr == "Green")
            {
                playerNamesOnBoard[1].text = players[i].name;
                CreateFigureClassElements(greenFigureGameObjecs, 27, tmpCounter, greenRoadToFinishPositions);
            }
            else if (players[i].colorStr == "Red")
            {
                playerNamesOnBoard[2].text = players[i].name;
                CreateFigureClassElements(redFigureGameObjecs, 40, tmpCounter, redRoadToFinishPositions);
            }
            else
            {
                playerNamesOnBoard[3].text = players[i].name;
                CreateFigureClassElements(blueFigureGameObjecs, 1, tmpCounter, blueRoadToFinishPositions);
            }
            tmpCounter++;
        }
    }

    void ColorChangeOfPanels() 
    {
        Image tmpColorForDiceThrowPanel = diceThrowPanel.GetComponent<Image>();
        Image tmpColorForHistoryPanel = historyPanel.GetComponent<Image>();
        Color tmpColor = new Color(players[WhoesRound].color.r, players[WhoesRound].color.g, players[WhoesRound].color.b, players[WhoesRound].color.a / 1.1f);

        tmpColorForDiceThrowPanel.color = tmpColor;
        tmpColorForHistoryPanel.color = tmpColor;
    }

    void CreateFigureClassElements(GameObject[] gObjecs, int startingPossitonIndex, int indexForAllFigures, List<Vector3> roadToFinishPositions)
    {
        List<Figure> tmpFigures = new List<Figure>();
        for (int i = 0; i < 4; i++)
        {
            tmpFigures.Add(new Figure(gObjecs[i], startingPossitonIndex, roadToFinishPositions));
        }
        allFigures[indexForAllFigures] = tmpFigures.ToArray();

    }

    void GetAllFigurePositions() 
    {
        List<Vector3> tmpPositions = new List<Vector3>();
        for (int i = 0; i < playerCount; i++) 
        {
            for (int j = 0; j < 4; j++)
            {
                tmpPositions.Add(allFigures[i][j].GetPosition());
                //Debug.Log(i + "_" + j + "    " + tmpPositions[j]);
            }
            allFigurePositions[i] = tmpPositions.ToArray();
        }
    }
    
    void LoadHistoryPanel(int diceThrow, string name)
    {
        valuesForHistoryDiceTrows.Insert(0, diceThrow.ToString());
        namesForHistory.Insert(0, name);
        if (valuesForHistoryDiceTrows.Count > 13)
        {
            valuesForHistoryDiceTrows.RemoveAt(valuesForHistoryDiceTrows.Count - 1);
            namesForHistory.RemoveAt(namesForHistory.Count - 1);
        }

        historyNames.text = "";
        historyDiceTrows.text = "";

        for (int i = 0; i < valuesForHistoryDiceTrows.Count; i++)
        {
            historyNames.text = historyNames.text + namesForHistory[i] + "\n";
            historyDiceTrows.text = historyDiceTrows.text + valuesForHistoryDiceTrows[i] + "\n";
        }
    }
    
    void RenderFigures() 
    {
        for (int i = 0; i < playerCount; i++) 
        {
            for (int j = 0; j < 4; j++) 
            {
                allFigures[i][j].gObject.SetActive(true);
                //allFigures[i][j].spawnPosition = new Vector3(allFigures[i][j].spawnPosition.x / scaleX, allFigures[i][j].spawnPosition.y / scaleY, 0);
            } 
        }
    }

    public void RestartTheGame()
    {
        ResetAllFigures();
        GetAllFigurePositions();

        historyNames.text = "";
        historyDiceTrows.text = "";

        namesForHistory.Clear();
        valuesForHistoryDiceTrows.Clear();

        for (int i = 0; i < valuesForHistoryDiceTrows.Count; i++)
        {
            historyNames.text = historyNames.text + namesForHistory[i] + "\n";
            historyDiceTrows.text = historyDiceTrows.text + valuesForHistoryDiceTrows[i] + "\n";
        }
        WhoesRound = players.Count - 1;

        EnableNextRound();

    }

    void ResetAllFigures() 
    {
        for (int i = 0; i < playerCount; i++)
        {
            figuresInFinish[i] = 0;
            for (int j = 0; j < 4; j++)
            {
                allFigures[i][j].SetPosition(allFigures[i][j].spawnPosition);
                allFigures[i][j].inFinish = false;
                allFigures[i][j].moveCount = 0;
                allFigures[i][j].inGame = false;
                allFigures[i][j].currentSpriteRenderer.sprite = players[i].figureSprite;
            }
        }
        winMenu.SetActive(false);
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            FireScreenRay();
        }
    }

    void FireScreenRay()
    {
        string selectedFigureName;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            selectedFigureName = hit.collider.gameObject.name;
            MoveFigure(selectedFigureName);
        }
    }


    public void CallRollDice()
    {
        diceButton.enabled = false;
        StartCoroutine(RollDice(players[WhoesRound].difficulty));
    }
    public IEnumerator RollDice(int dif)
    {
        int randomInt1 = 0;
        float waitTime = 0.05f;

        movableFigures = new List<Figure>();

        if(dif > 0) yield return new WaitForSeconds(1f);

        for (int i = 0; i < 20; i++)
        {
            randomInt1 = UnityEngine.Random.Range(0, diceFaces.Length);
            diceImage.sprite = diceFaces[randomInt1];
            waitTime = waitTime + (float)i / 1000;
            yield return new WaitForSeconds(waitTime);
        }

        diceThrow = randomInt1 + 1;

        LoadHistoryPanel(diceThrow, players[WhoesRound].name);

        if (diceThrow == 6)
        {
            foreach (Figure figure in allFigures[WhoesRound]) 
            {
                if(figure.inFinish == false) movableFigures.Add(figure);
            }
        }
        else 
        {
            for (int i = 0; i < 4; i++) 
            {
                if(allFigures[WhoesRound][i].inGame == true) movableFigures.Add(allFigures[WhoesRound][i]);
            }
        }

        if (players[WhoesRound].difficulty == 0)
        {
            if (movableFigures.Count == 0)
            {
                EnableNextRound();
            }
            else
            {
                EnableFigureColliders(movableFigures);
            }
        }
        else 
        {
            StartAiLogic(players[WhoesRound].difficulty);
        }
        
    }
    void MoveFigure(string selectedFigureName) 
    {
        int selectedFigureIndex = 0;
        Figure selectedFigure;
        List<Figure> tmpFigures = new List<Figure>();

        DisableeFigureColliders(allFigures[WhoesRound]);

        

        for (int i = 0; i < 4; i++)
        {
            if(allFigures[WhoesRound][i].name == selectedFigureName) selectedFigureIndex = i;
        }
        selectedFigure = allFigures[WhoesRound][selectedFigureIndex];

        //ha shildbol mozdul ki
        if (selectedFigure.currentSpriteRenderer.sprite != players[WhoesRound].figureSprite)
        {
            Debug.Log("Shildbol mozdul ki");
            selectedFigure.SetSprite(players[WhoesRound].figureSprite);
            tmpFigures = IsThereFriendly(selectedFigure.GetPosition());
            //maradek se marad shieldbe
            if (tmpFigures.Count < 3) foreach (Figure f in tmpFigures) f.SetSprite(players[WhoesRound].figureSprite);
            //maradek shieldbe marad
            else 
            {
                foreach (Figure f in tmpFigures) 
                {
                    if(f.currentSpriteRenderer.sprite != players[WhoesRound].figureSprite) f.SetSprite(players[WhoesRound].shieldSprites[tmpFigures.Count - 3]);
                }
            }
            
        }

        if (selectedFigure.inGame == true)
        {
            StartCoroutine(MoveFigureWithDelay(selectedFigure));
        }
        else //babu jatekba hozasa
        {
            selectedFigure.SetPosition(boardPositions[selectedFigure.startingPositionIndex]);
            selectedFigure.inGame = true;
            selectedFigure.currentPositionIndex = selectedFigure.startingPositionIndex;

            // shildbe megy
            tmpFigures = IsThereFriendly(selectedFigure.GetPosition());
            if (tmpFigures.Count > 1)
            {
                //selectedFigure.SetSprite(players[WhoesRound].shieldSprite);
                Debug.Log("///////////////////////");
                Debug.Log(tmpFigures.Count);
                foreach (Figure figure in tmpFigures) figure.SetSprite(players[WhoesRound].shieldSprites[tmpFigures.Count - 2]);
            }

            UpdatePossitionOnPosArray(selectedFigure);

            EnableNextRound();
        }

         
    }

    void UpdatePossitionOnPosArray(Figure selectedFigure) 
    {
        allFigurePositions[WhoesRound][System.Convert.ToInt32(selectedFigure.name)] = selectedFigure.GetPosition();
    }

    bool CanTakeEnemyFigure(Vector3 selectedFiguresVector)
    {
        int matchCounter = 0;
        for (int i = 0; i < playerCount; i++) 
        {
            if (i != WhoesRound) 
            {
                for (int j = 0; j < 4; j++)
                {
                    //Debug.Log(allFigurePositions[i][j] + " ?=? " + selectedFiguresVector);
                    if (Math.Round(allFigurePositions[i][j].x, 2) == Math.Round(selectedFiguresVector.x, 2) && Math.Round(allFigurePositions[i][j].y, 2) == Math.Round(selectedFiguresVector.y, 2))
                    {

                        matchCounter++;
                        takenEnemyIndex[0] = i;
                        takenEnemyIndex[1] = j;
                    }
                }
            }
        }
        if (matchCounter == 1)
        {
            return true;
        }
        else
        {
            return false;
        } 
    }

    void TakeEnemyFigure(int[] indexesOfTakenFigure) 
    {
        allFigures[indexesOfTakenFigure[0]][indexesOfTakenFigure[1]].SetPosition(allFigures[indexesOfTakenFigure[0]][indexesOfTakenFigure[1]].spawnPosition);
        allFigures[indexesOfTakenFigure[0]][indexesOfTakenFigure[1]].moveCount = 0;
        UpdatePossitionOnPosArray(allFigures[indexesOfTakenFigure[0]][indexesOfTakenFigure[1]]);
        allFigures[indexesOfTakenFigure[0]][indexesOfTakenFigure[1]].inGame = false;
    }


    IEnumerator MoveFigureWithDelay(Figure selectedFigure) 
    {
        List<Figure> tmpFigures = new List<Figure>();

        for (int i = 0; i < diceThrow; i++)
        {
            selectedFigure.currentPositionIndex++;
            selectedFigure.moveCount++;
            if (selectedFigure.currentPositionIndex == 52) selectedFigure.currentPositionIndex = 0;


            if (selectedFigure.moveCount < 51)
            {
                selectedFigure.SetPosition(boardPositions[selectedFigure.currentPositionIndex]);
            }
            else
            {
                //celba megy
                selectedFigure.inRoadToFinish = true;
                if (selectedFigure.moveCount < 56) selectedFigure.SetPosition(selectedFigure.roadToFinishPositions[selectedFigure.moveCount - 51]);
                else
                {
                    selectedFigure.SetPosition(selectedFigure.roadToFinishPositions[selectedFigure.moveCount - 51]);
                    selectedFigure.inFinish = true;
                    selectedFigure.inGame = false;
                    // ha counter 4 akkor winscrean

                    figuresInFinish[WhoesRound]++;

                    break;
                }
            }
            yield return new WaitForSeconds(0.3f);
        }

        // ha shildbe mozdul be
        tmpFigures = IsThereFriendly(selectedFigure.GetPosition());
        Debug.Log("most nezi meg a mozgas utannit");
        Debug.Log(tmpFigures.Count);
        if (tmpFigures.Count > 1)
        {
            //selectedFigure.SetSprite(players[WhoesRound].shieldSprite);
            foreach (Figure figure in tmpFigures) figure.SetSprite(players[WhoesRound].shieldSprites[tmpFigures.Count - 2]);
        }

        if (figuresInFinish[WhoesRound] == 4) 
        {
            winMenuText.text = "Congratulation " + players[WhoesRound].name + " !!!";
            winMenu.SetActive(true);
        }
        UpdatePossitionOnPosArray(selectedFigure);
        //babulevetel ha ugyanazon a pozicion all 1 elenseges babu
        if (CanTakeEnemyFigure(selectedFigure.GetPosition())) TakeEnemyFigure(takenEnemyIndex);
        //ha ugyan azon a pozicion frendli all kitalalni valamit

        if (winMenu.activeSelf == false)
        {
            Debug.Log("next round");
            EnableNextRound();
        }
        
    }

    void EnableNextRound() 
    {
        List<Figure> figures = new List<Figure>();
        foreach (Figure f in allFigures[WhoesRound]) 
        {
            f.gObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }


        if (WhoesRound + 1 == playerCount) WhoesRound = 0;
        else WhoesRound++;


        foreach (Figure f in allFigures[WhoesRound])
        {
            f.gObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        diceThrowText.text = players[WhoesRound].name + "'s turn";

        ColorChangeOfPanels();


        if (players[WhoesRound].difficulty == 0) diceButton.enabled = true;
        else 
        {
            AIturn();
        } 

    }


    void AIturn() 
    {
        StartCoroutine(RollDice(players[WhoesRound].difficulty));
    }

    void StartAiLogic(int difficulty) 
    {
        List<int> smartnesPointOfMove = new List<int>();
        int SmartnesCounter = 0;
        int IndexOfSmartestMove = 0;
        int tmpForFuturePositionIndex = 0;
        List<Figure> figuresWithMostSmartnesPoint = new List<Figure>();
        Figure choesenFigure;
        int randInt;
        int largestMoveCount = 0;
        int largestMoveCountIndex = 0;


        if (movableFigures.Count == 0)
        {
            EnableNextRound();
        }
        else
        {
            Debug.Log(movableFigures.Count + " figurat nez at");
            smartnesPointOfMove.Clear();
            figuresWithMostSmartnesPoint.Clear();


            //easyDif
            if (difficulty > 0)
            {

                foreach (Figure f in movableFigures)
                {

                    SmartnesCounter = 0;
                    //jatekba hozas
                    if (f.inGame == false) 
                    {
                        SmartnesCounter += 2;
                    }
                    else
                    {
                        tmpForFuturePositionIndex = f.currentPositionIndex + diceThrow;
                        


                        //le e tud utni valakit
                        if (tmpForFuturePositionIndex > 51) 
                        {
                            tmpForFuturePositionIndex -= 52;
                        }
                        //Debug.Log(f.name + " nek a futurePosIndexe: " + tmpForFuturePositionIndex);
                        //Debug.Log(f.name + " a futurepositionje: " + boardPositions[tmpForFuturePositionIndex]);   
                        if (CanTakeEnemyFigure(boardPositions[tmpForFuturePositionIndex])) 
                        {
                            SmartnesCounter += 5;
                        }


                        //be e tud menni safeZoneba vagy celba rogton
                        if (f.inRoadToFinish == false)
                        {
                            //be tud menni celba
                            if (f.moveCount + diceThrow > 55)
                            {
                                SmartnesCounter += 5;
                            }
                            //csak safezonba tud bemenni
                            else if (f.moveCount + diceThrow > 49) 
                            {
                                SmartnesCounter += 3;
                            } 
                        }
                        //ha mar safezoneba van
                        else 
                        {
                            //egybol celba e tud menni
                            if (f.moveCount + diceThrow > 55) SmartnesCounter += 3;
                        }


                        //mediumDif
                        if (players[WhoesRound].difficulty > 1) 
                        {
                            Debug.Log("mediumDif");
                            //megnezi veszelyben e van vannek e mogotte es hanyan
                            if (f.inGame == true) SmartnesCounter += IsFigureInDanger(f, 0);
                        }

                        //hardDif
                        if (players[WhoesRound].difficulty > 2)
                        {
                            Debug.Log("hardDif");
                            if (f.inGame == true) 
                            {
                                //megneyi ha ezzel lep akkor veszelyben e lessz
                                SmartnesCounter -= IsFigureInDanger(f, diceThrow);


                                //megnezi olyan helyre e lep ahol van baratsagos babu
                                if (IsThereFriendly(boardPositions[tmpForFuturePositionIndex]).Count > 1) 
                                {
                                    Debug.Log("ott egy friendly");
                                    SmartnesCounter += 3;
                                } 
                            } 

                            
                        }



                    }
                    smartnesPointOfMove.Add(SmartnesCounter);
                }


            }


            //megnezi melyik a legokosabb lepes
            for (int i = 0; i < movableFigures.Count; i++)
            {
                Debug.Log(movableFigures[i].name +"_    " + smartnesPointOfMove[i]);
                if (smartnesPointOfMove[IndexOfSmartestMove] < smartnesPointOfMove[i]) IndexOfSmartestMove = i;
            }
            for (int i = 0; i < movableFigures.Count; i++)
            {
                if (smartnesPointOfMove[IndexOfSmartestMove] == smartnesPointOfMove[i]) figuresWithMostSmartnesPoint.Add(movableFigures[i]);
            }


            if (figuresWithMostSmartnesPoint.Count > 1)
            {
                if (players[WhoesRound].difficulty > 1)
                {
                    //azzal megy ami koyelebb van a celhoz egyenloseg eseten
                    for (int i = 0; i < figuresWithMostSmartnesPoint.Count; i++) 
                    {
                        if (largestMoveCount < figuresWithMostSmartnesPoint[i].moveCount) 
                        {
                            largestMoveCount = figuresWithMostSmartnesPoint[i].moveCount;
                            largestMoveCountIndex = i;
                        }
                    }
                    choesenFigure = figuresWithMostSmartnesPoint[largestMoveCountIndex];
                }
                else
                {
                    //random valaszt ha egyenloseg van
                    choesenFigure = figuresWithMostSmartnesPoint[randInt = UnityEngine.Random.Range(0, figuresWithMostSmartnesPoint.Count)];
                }
            }
            else 
            {
                choesenFigure = figuresWithMostSmartnesPoint[0];
            }


            //Debug.Log("ezzel megy  " + choesenFigure.name);
            MoveFigure(choesenFigure.name);

        }
    }

    //if (players[WhoesRound].difficulty == 99) resz atirva
    int IsFigureInDanger(Figure f, int intForFuturePosition) 
    {
        int enemyCounterBehindFigure = 0;
        Vector3 analyzedVector = new Vector3();
        int analyzedPosIndex = f.currentPositionIndex + intForFuturePosition;

        if (analyzedPosIndex > 51) analyzedPosIndex -= 52;
        
        if (intForFuturePosition == 0 && f.currentSpriteRenderer.sprite != players[WhoesRound].figureSprite)
        {
            return 0;
        }
        for (int i = 1; i < 7; i++)
        {
            analyzedPosIndex--;
            if (analyzedPosIndex < 0)
            {
                analyzedPosIndex = 52 + analyzedPosIndex;
                //Debug.Log(analyzedPosIndex + " analized positionje");
            }
            analyzedVector = boardPositions[analyzedPosIndex];
            if (IsThereEnemy(analyzedVector).Count > 0)
            {
                enemyCounterBehindFigure++;
                //Debug.Log(f.name + "mogott van ellenseg");
            }
        }
        return enemyCounterBehindFigure;
    }

    List<Figure> IsThereEnemy(Vector3 vector) 
    {
        Vector3 tmpVector = new Vector3();
        List<Figure> figures = new List<Figure>();
        for (int i = 0; i < players.Count; i++) 
        {
            if (i != WhoesRound)
            {
                for (int j = 0; j < 4; j++) 
                {
                    tmpVector = allFigures[i][j].GetPosition();
                    //Debug.Log(tmpVector  + " ?=? " + vector);
                    //Debug.Log(Math.Round(tmpVector.x,2) + " == "+ Math.Round(vector.x, 2) + "___" + Math.Round(tmpVector.y, 2) + " == " + Math.Round(vector.y, 2));
                    if(Math.Round(tmpVector.x, 2) == Math.Round(vector.x, 2) && Math.Round(tmpVector.y, 2) == Math.Round(vector.y, 2)) figures.Add(allFigures[WhoesRound][j]);
                }
                
            }
        }
        //Debug.Log("====================");
        return figures;
    }

    List<Figure> IsThereFriendly(Vector3 vector)
    {
        Vector3 tmpVector = new Vector3();
        List<Figure> figures = new List<Figure>();
        {
            for (int j = 0; j < 4; j++)
            {
                tmpVector = allFigures[WhoesRound][j].GetPosition();
                //Debug.Log(tmpVector  + " ?=? " + vector);
                //Debug.Log(Math.Round(tmpVector.x, 2) + " == " + Math.Round(vector.x, 2) + "___" + Math.Round(tmpVector.y, 2) + " == " + Math.Round(vector.y, 2));
                if (Math.Round(tmpVector.x, 2) == Math.Round(vector.x, 2) && Math.Round(tmpVector.y, 2) == Math.Round(vector.y, 2)) figures.Add(allFigures[WhoesRound][j]);
            }

        }
        
        //Debug.Log("====================");
        return figures;
    }






    void EnableFigureColliders(List<Figure> fList)   
    {
        Debug.Log("most fut le     " + fList.Count());
        
        foreach (Figure f in fList) 
        {
            f.collider.enabled = true;
            Debug.Log(f.bcForHightlight.activeSelf);
            Debug.Log(f.bcForHightlight.name);
            f.bcForHightlight.SetActive(true);
            Debug.Log(f.bcForHightlight.activeSelf);
            Debug.Log("==========");
        } 
    }

    void DisableeFigureColliders(Figure[] fArray)
    {
        foreach (Figure f in fArray) 
        {
            f.collider.enabled = false;
            f.bcForHightlight.SetActive(false);
        } 
    }
}
