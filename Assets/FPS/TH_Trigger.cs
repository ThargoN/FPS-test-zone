using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class TH_Trigger : MonoBehaviour
{
    // A multi-purpose script which causes an action to occur when
    // a trigger collider is entered.
    public enum Mode
    {
        Activate = 2,   // Activate the target GameObject
        Enable = 3,     // Enable a component
        Animate = 4,    // Start animation on target
        Deactivate = 5  // Decativate target GameObject
    }

    public Mode action = Mode.Activate;         // The action to accomplish
    public Object target;                       // The game object to affect. If none, the trigger work on this game object
    public GameObject source;
    public bool reverseOnExit = true;

    private void DoActivateTrigger(bool OnOrOff){
        Object currentTarget = target ?? gameObject;
        Behaviour targetBehaviour = currentTarget as Behaviour;
        GameObject targetGameObject = currentTarget as GameObject;
        
        if (targetBehaviour != null)
        {
            targetGameObject = targetBehaviour.gameObject;
        }

        switch (action)
        {
            case Mode.Activate:
                if (targetGameObject != null)
                {
                    targetGameObject.SetActive( OnOrOff );
                }
                break;
            case Mode.Enable:
                if (targetBehaviour != null)
                {
                    targetBehaviour.enabled = OnOrOff;
                }
                break;
            case Mode.Animate:
                if (targetGameObject != null)
                {
                    if( OnOrOff ){
                        targetGameObject.GetComponent<Animation>().Play();
                    } else {
                        targetGameObject.GetComponent<Animation>().Stop();
                    }
                }
                break;
            case Mode.Deactivate:
                if (targetGameObject != null)
                {
                    targetGameObject.SetActive( !OnOrOff );
                }
                break;
        }

    }


    private void OnTriggerEnter(Collider other) {
        DoActivateTrigger( true );
    }

    private void OnTriggerExit(Collider other){
        if (reverseOnExit)
            DoActivateTrigger( false );
    }
}

