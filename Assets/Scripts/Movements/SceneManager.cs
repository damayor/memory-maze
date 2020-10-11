using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*With the suppoert of Razeware LLC */
public class SceneManager : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField]
    private Player bot = null;

    [SerializeField]
    //private UIManager uiManager = null;

    private List<MovementCommand> commands = new List<MovementCommand>();
    private Coroutine executeRoutine;

    [Header("Commands Panel")]
    [SerializeField]
    private Transform commandsGizmosPanel;

    [SerializeField]
    private GameObject commandGizmoPrefab;

    
    void Start()
    {
        
    }

    //2

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteCommands();
        }
        else
        {
            CheckForMovementCommands();
        }
    }

    //3
    private void CheckForMovementCommands()
    {
        var botCommand = InputHandler.HandleInputt();
        if (botCommand != null /*&& executeRoutine == null*/)
        {
            AddToCommands(botCommand);
        }
    }


    //4
    private void AddToCommands(MovementCommand botCommand)
    {
        commands.Add(botCommand);
        //5
        //uiManager.InsertNewText(botCommand.ToString()); //ToDo
        Debug.Log("Add " + botCommand.ToString());

        GameObject arrow = Instantiate(commandGizmoPrefab) as GameObject;

        //SendMessage("drawArrow", Direction.Up);
        switch (botCommand.ToString())
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
            command.Execute(bot); //yeeea
            //9
            //uiManager.RemoveFirstTextLine();
            yield return new WaitForSeconds(0.5f);
        }

        //10
        commands.Clear();

        //bot.ResetToLastCheckpoint();

        executeRoutine = null;
    }
}
