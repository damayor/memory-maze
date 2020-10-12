using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoCommand : MovementCommand
{

    public UndoCommand() : base(delegate (Player bot) { bot.Move(Direction.Undo); }, "moveUndo")
    { }
}
