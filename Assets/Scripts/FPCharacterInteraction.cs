using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FPCharacterInteraction : MonoBehaviour {
#region internalClasses
    [System.Serializable]
    public class Hovered : UnityEvent<bool>{
    }

	[System.Serializable]
	public class Activated : UnityEvent{
    }

#endregion

    public Hovered OnHover;

    public bool CanBeActivated = false;
    public Activated OnActivate;

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable(){
        OnHover.Invoke(false);
    }
}
