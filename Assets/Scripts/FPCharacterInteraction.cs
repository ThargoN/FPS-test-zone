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

    [Header("OnHover")]
    public string GUITextOnHover;
    public Hovered OnHover;
    
    [Header("OnActivate")]
    public string GUITextOnActivate;
    public Activated OnActivate;

    [Header("OnActivateAdvanced")]
    public string GUITextOnActivateAdvanced;
    public Activated OnActivateAdvanced;

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable(){
        OnHover.Invoke(false);
    }
}
