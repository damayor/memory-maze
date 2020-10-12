using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*With the suppoert of Razeware LLC */
public static class InputHandler
{
   
    //1
    private static readonly MovementCommand MoveUp =
        //new MovementCommand(delegate (Player bot) { bot.Move(Direction.Up); }, "moveUp");
        new UpCommand();

    //2
    private static readonly MovementCommand MoveDown =
        //new MovementCommand(delegate (Player bot) { bot.Move(Direction.Down); }, "moveDown");
        new DownCommand();
    //3
    private static readonly MovementCommand MoveLeft =
        //new MovementCommand(delegate (Player bot) { bot.Move(Direction.Left); }, "moveLeft");
        new LeftCommand();

    //4
    private static readonly MovementCommand MoveRight =
        //new MovementCommand(delegate (Player bot) { bot.Move(Direction.Right); }, "moveRight");
        new RightCommand();

    private static readonly MovementCommand MoveUndo =
    //new MovementCommand(delegate (Player bot) { bot.Move(Direction.Right); }, "moveRight");
        new UndoCommand();


    public static MovementCommand HandleInputt()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("up");
            var m = MoveUp;
            return m;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return MoveDown;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return MoveRight;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return MoveLeft;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            return MoveUndo;
        }

        return null;
    }
}
