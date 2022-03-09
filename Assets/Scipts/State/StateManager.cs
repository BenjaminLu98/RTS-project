using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateManager
{
    Dictionary<string, State> stateDic = new Dictionary<string, State>();
    State current;
    // High level state machine conditions.
    Dictionary<string, Func<bool>> conditions = new Dictionary<string, Func<bool>>();

    public string Name { get => current.Name;
        set {
            State next;
            if (!stateDic.TryGetValue(value, out next)) return;
            current?.onExit();
            current = next;
            next.onStart();
        } }

    public StateManager addCondition(string name, Func<bool> condition)
    {
        conditions.Add(name, condition);
        return this;
    }

    public StateManager addStatus(string name,State state)
    {
        stateDic.Add(name, state);
        state.Name = name;
        return this;
    }

    public void onUpdate()
    {
        current.onStay();
        
        // High level state machine change state.
        foreach(KeyValuePair<string, Func<bool>> condition in conditions.Where(condition => condition.Key == current.Name && condition.Value())){
            Name = condition.Key;
            return;
        }
        
        // Low level state machine change state.
        foreach(KeyValuePair<string, Func<bool>> condition in current.Conditions.Where(condition => condition.Value())){
            Name = condition.Key;
            return;
        }
    }
    
}
