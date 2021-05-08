using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Assertions.Must;

/// <summary>
/// The base class for singleton scriptble objects
/// TO USE: 
/// 1. Make script inherit from SingletonScriptableObject<ScriptName>
/// 2. Make all references to other ScriptableObjects private 
///    serialized fields with a public static property getter
///    ex.
///    [SerializeField]
///    private Thing _thingRef;
///    public static Thing ThingRef {get {return Instance._value;}}
/// (Based on a tutorial by First Gear Games: https://www.youtube.com/watch?v=z-7odUto-V8)
/// </summary>

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // load the singleton
                T[] loaded = Resources.FindObjectsOfTypeAll<T>();
                if (loaded.Length > 0)
                {
                    _instance = loaded[0];
                }
            }
            // return the singleton
            return _instance;
        }
    }
}
