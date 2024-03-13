using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class IdleState : IState<enemi>

{
    public void OnEnter(enemi e)
    {
        e.changeAnim("Idle");
        e.Counter.Start(() => e.ChangeState(new Patrol()), Random.Range(1f, 3f));
    }

    public void OnExecute(enemi e)
    {
        e.Counter.Excute();
    }

    public void OnExit(enemi e)
    {

    }
}
