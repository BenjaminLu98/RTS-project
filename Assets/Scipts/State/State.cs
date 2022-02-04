using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    string name;
    Dictionary<string, Func<bool>> conditions;

    Action onStart;
    Action onStay;
    Action onExit;

    public Dictionary<string, Func<bool>> Conditions { get => conditions; set => conditions = value; }
    public string Name { get => name; }
    public Action OnStart { get => onStart; set => onStart = value; }
    public Action OnStay { get => onStay; set => onStay = value; }
    public Action OnExit { get => onExit; set => onExit = value; }

    public State addCondition(string name, Func<bool> condition)
    {
        Conditions.Add(name, condition);
        return this;
    }

    public State(string name)
    {
        this.name = name;
    }



}
