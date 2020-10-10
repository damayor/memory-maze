using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCommand : MovementCommand
{

    //private string commandName = "upMove";

    //public delegate void ExecuteCallback(Player player);


    //public ExecuteCallback Execute { get; private set; }


    //public ExecuteCallback Execute { get; private set; }


    //public override void ExecuteCommand()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public UpCommand(ExecuteCallback executeMethod, string name)
    //{
    //    Execute = delegate (Player bot) { bot.Move(Direction.Up); };
    //    commandName = name;
    //}

    public UpCommand() : base(delegate (Player bot) { bot.Move(Direction.Up); }, "upMove")
    { }
}
