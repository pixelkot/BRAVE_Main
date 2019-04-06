using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTest : MonoBehaviour
{

    //private UnityAction someListener;

    //void Awake()
    //{
    //    someListener = new UnityAction(SomeFunction);
    //}

    void OnEnable()
    {
        EventManager.StartListening("test", SomeFunction);
        EventManager.StartListening("Spawn", SomeOtherFunction);
        EventManager.StartListening("Destroy", SomeThirdFunction);
    }

    void OnDisable()
    {
        EventManager.StopListening("test", SomeFunction);
        EventManager.StopListening("Spawn", SomeOtherFunction);
        EventManager.StopListening("Destroy", SomeThirdFunction);
    }

    void SomeFunction()
    {
        Debug.Log("Some Function was called!");
    }

    void SomeOtherFunction()
    {
        Debug.Log("Some Other Function was called!");
    }

    void SomeThirdFunction()
    {
        Debug.Log("Some Third Function was called!");
    }
}
