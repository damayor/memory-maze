using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    /*With the suppoert of Razeware LLC */
    [Header("Set In Inspector")]
    [SerializeField]
    private Player player = null;

    [SerializeField]
    //private UIManager uiManager = null;

    private List<MovementCommand> commands = new List<MovementCommand>();
    private Coroutine executeRoutine;



    [Header("Commands Panel")]
    [SerializeField]
    private Transform commandsGizmosPanel;
    [SerializeField]
    private Transform lostPanel;

    [SerializeField]
    private GameObject commandGizmoPrefab;

    [Header("Memorize Countdown")]
    public float timeRemaining ;
    public bool timerIsRunning = false;
    public Text timerLabel ;

    public LevelManager levelM;

    private bool hasWon = false;

    void Start()
    {
        timeRemaining = ToolboxStaticData.memorizeTime;

        timerIsRunning = true;

        //levelM = GetComponent<LevelManager>();


        levelM.OnWin += HandleWin;
        levelM.OnWin += HandleLost;
    }

    //2

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteCommands();
            commandsGizmosPanel.parent.parent.parent.gameObject.SetActive(false);
        }
        else
        {
            CheckForMovementCommands();
        }

        CheckMemorizeTimer();
       
    }


    //3
    private void CheckForMovementCommands()
    {
        var playerCommand = InputHandler.HandleInputt();
        if (playerCommand != null && executeRoutine == null)
        {
            AddToCommands(playerCommand);
        }
    }


    //4
    private void AddToCommands(MovementCommand playerCommand)
    {
        commands.Add(playerCommand);
        //5
        //uiManager.InsertNewText(playerCommand.ToString()); //ToDo
        Debug.Log("Add " + playerCommand.ToString());

        GameObject arrow = Instantiate(commandGizmoPrefab) as GameObject;

        //SendMessage("drawArrow", Direction.Up);
        switch (playerCommand.ToString())
        {
            //to confirm si: vector.down si lo baja en mi arreglo de pos, 
            case "upMove":
                //pos = pos + Vector2.down;
                arrow.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 90));
                //arrow.transform.SetParent(commandsGizmosPanel);
                break;
            case "moveDown":
                arrow.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -90));
                break;
            case "moveLeft":
                arrow.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 180));
                //arrow.transform.SetParent(commandsGizmosPanel);
                break;
            case "moveRight":
                //arrow.transform.SetParent(commandsGizmosPanel);
                break;
        }
        arrow.transform.SetParent(commandsGizmosPanel);
    }


    //6 !!!
    private void ExecuteCommands()
    {
        if (executeRoutine != null)
        {
            return;
        }

        executeRoutine = StartCoroutine(ExecuteCommandsRoutine());
    }


    private IEnumerator ExecuteCommandsRoutine()
    {
        Debug.Log("Executing...");
        //7
        //uiManager.ResetScrollToTop();

        //8
        for (int i = 0, count = commands.Count; i < count; i++)
        {
            var command = commands[i];
            command.Execute(player); //yeeea

            //desde aca ya esta la posicion actualizada? Sip
            //check win distint with undo 
            if ( !  ToolboxStaticData.GetObstacled() )
            {
                if (player.pos != ToolboxStaticData.way[i + 1].pos)
                {
                    HandleLost();
                    break;
                }

            }
            else
            {
                if (player.pos != ToolboxStaticData.way[ i ].pos)
                {
                    HandleLost();
                    break;
                }

            }


            if (player.pos == ToolboxStaticData.way[ToolboxStaticData.way.Count - 1].pos)
            {
                //Debug.Log("GANASTES!");
                HandleWin();
            }


            //9
            //uiManager.RemoveFirstTextLine();
            yield return new WaitForSeconds(0.5f);
        }

        //10
        commands.Clear();
        foreach (Transform child in commandsGizmosPanel)
        {
            Destroy(child.gameObject);
        }

        //player.ResetToLastCheckpoint();

        executeRoutine = null;
    }

    private void CheckMemorizeTimer()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                if (timeRemaining < 5)
                {
                    timerLabel.GetComponent<Text>().text = timeRemaining.ToString("0");
                }
            }
            else
            {
                Debug.Log("Time has run out! Try it!");
                timeRemaining = 0;
                timerIsRunning = false;

                commandsGizmosPanel.parent.parent.parent.gameObject.SetActive(true);
                timerLabel.GetComponent<Text>().text = "";
            }
        }
    }

    /*suscription to LevelManager events*/
    public void HandleWin()
    {
        hasWon = true;
        Debug.Log("Event GANASTE!");
    }

    public void HandleLost()
    {
        hasWon = true;
        Debug.Log("Event Perdiste!");
        lostPanel.gameObject.SetActive(true);
    }

    public void CleanCommands()
    {
        
        commands.Clear(); //ojo xq no vuelve a ejecutar

        //commands = new List<MovementCommand>();
        
        foreach (Transform child in commandsGizmosPanel)
        {
            Destroy(child.gameObject);
        }
        Debug.Log("All moves cleaned");

    }

    public void TryAgain()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
