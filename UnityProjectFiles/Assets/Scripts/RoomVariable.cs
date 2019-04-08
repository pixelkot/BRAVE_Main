using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RoomVariable : ScriptableObject, ISerializationCallbackReceiver
{

    public MazeRoom InitialValue;

    [NonSerialized]
    public MazeRoom RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitialValue;
    }

    public void OnBeforeSerialize()
    {
    }
}
