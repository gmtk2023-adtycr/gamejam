using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Subject<T>
{

    private T _currentValue;
    private List<Action<T>> _actions = new();

    public T CurrentValue => _currentValue;

    public Subject(T firstValue){
        _currentValue = firstValue;
    }
    
    public void Subscribe(Action<T> callback){
        _actions.Add(callback);
    }

    public void Next(T nextValue){
        if(_currentValue.Equals(nextValue)) return;
        
        _currentValue = nextValue;
        foreach (var ac in _actions){
            ac.Invoke(_currentValue);
        }
    }

}