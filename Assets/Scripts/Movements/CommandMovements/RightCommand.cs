using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCommand : MovementCommand
{

    public RightCommand() : base(delegate (Player bot) { bot.Move(Direction.Right); }, "moveRight")
    { }
}
