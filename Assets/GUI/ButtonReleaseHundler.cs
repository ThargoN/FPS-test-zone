using System;
using System.Collections;
using System.Collections.Generic;
//using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ButtonReleaseHundler : MonoBehaviour {

	private Text m_cachedText = null;

	void Awake(){
		m_cachedText = GetComponentInChildren<Text>();
		Assert.IsNotNull(m_cachedText, "ButtonReleaseHundler::Start(): No Text found!");
	}

	void OnEnable () {
        //GameController.instance.MouseLockStateChanged += UpdateButtonText;
        GameController.instance.MouseLockStateChanged.AddListener(UpdateButtonText);
    }

	void OnDisable(){
		//GameController.instance.MouseLockStateChanged -= UpdateButtonText;
		GameController.instance.MouseLockStateChanged.RemoveListener(UpdateButtonText);
	}

    public void UpdateButtonText( bool newState ){
		/*StringBuilder sb = new StringBuilder(30);

		sb.Append( newState ? "Release " : "Lock " );
		sb.Append( "mouse pointer (" );
		sb.Append( "ESC"); //TODO: get KeyName from custom InputManager
		sb.Append( ")" );

		m_cachedText.text = sb.ToString();*/

		m_cachedText.text = newState ? "Release mouse pointer (ESC)" : "Lock mouse pointer (click)";
    }
}
