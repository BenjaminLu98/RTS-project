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

    public StateManager addCondition(string name, Func<bool> condition)
    {
        conditions.Add(name, condition);
        return this;
    }

    public StateManager addStatus(string name,State state)
    {
        stateDic.Add(name, state);
        return this;
    }

    public void onUpdate()
    {
        current.OnStay();
        
        // High level state machine change state.
        foreach(KeyValuePair<string, Func<bool>> condition in conditions.Where(condition => condition.Key == current.Name && condition.Value())){
            current = stateDic[condition.Key];
            return;
        }
        
        // Low level state machine change state.
        foreach(KeyValuePair<string, Func<bool>> condition in current.Conditions.Where(condition => condition.Value())){
            current = stateDic[condition.Key];
            return;
        }
    }
    
}
