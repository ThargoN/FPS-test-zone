using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projector : Electronics {

    private Light _cachedLight;
    
	[SerializeField]
	private GameObject cachedLightSurface;

    // Use this for initialization
    void Start () {
        _cachedLight = GetComponentInChildren<Light>(true);
		if(cachedLightSurface == null){
            cachedLightSurface = transform.FindChild("Projector/Light Surface").gameObject;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


	// IElectronicsOnOff interface
	public override void OnElectronicsOn(bool OnOff){
        cachedLightSurface.SetActive( OnOff );
        _cachedLight.enabled = OnOff;
    }

    public override void OnElectronicsToggleOnOff(){
        bool OnOff = _cachedLight.enabled;
        OnElectronicsOn(!OnOff);
    }
}
