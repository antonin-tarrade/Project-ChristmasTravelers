using BoardCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Debugger : MonoBehaviour
{

    [Serializable]
    private class Debug
    {
        public string debugInput;
        public UnityEvent debugEvent;
    }

    [SerializeField]
    private List<Debug> debuggers;

    private void Update()
    {
        foreach (Debug debug in debuggers)
        {
            if (Input.GetKeyDown(debug.debugInput))
            {
                debug.debugEvent.Invoke();
            }
        }
    }

}
