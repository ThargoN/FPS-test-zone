using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class StandLeverTerminal : MonoBehaviour {

	public GameObject Lever = null;

    public bool InitialOnOffState;
    public GameObject HighlightBox;
    
    private bool _isOn = false;
	public bool isOn{
        get {
            return _isOn;
        }
        set {
			if(_isOn != value){
                _isOn = value;
                updateAnimation();
				if( connectedElectronics != null){
					foreach( var ce in connectedElectronics){
                        ce.OnElectronicsOn(value);
                    }
				}
            }
        }
    }

    public Electronics[] connectedElectronics;
    
    private Animator leverAnimator;


	private void Start(){
        leverAnimator = Lever.gameObject.GetComponent<Animator>();
        Assert.IsNotNull(leverAnimator, "No animator found!");
        _isOn = InitialOnOffState;
        updateAnimation();
    }

    private void updateAnimation(){
        leverAnimator.SetFloat("LeverState", _isOn ? 1.0f : 0f);
    }

    public void Toggle(){
        isOn = !_isOn;
    }

}
