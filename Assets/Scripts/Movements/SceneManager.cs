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
