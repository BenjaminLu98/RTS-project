using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    string name;
    Dictionary<string, Func<bool>> conditions=new Dictionary<string, Func<bool>>();

    private static Action EmptyAction = () => { };

    public Action onStart = EmptyAction;
    public Action onStay = EmptyAction;
    public Action onExit = EmptyAction;


    public Dictionary<string, Func<bool>> Conditions { get => conditions; set => conditions = value; }
    public string Name { get => name; set => name = value; }

    public State OnStart(Action action)
    {
        onStart = action;
        return this;
    }
    public State OnStay(Action action)
    {
        onStay = action;
        return this;
    }

    public State OnExit(Action action)
    {
        onExit = action;
        return this;
    }

    public State addCondition(string name, Func<bool> condition)
    {
        Conditions.Add(name, condition);
        return this;
    }

}
