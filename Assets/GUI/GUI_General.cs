using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUI_General : MonoBehaviour {

	public Transform GUI_Dot;
	private GameController m_gameController;

	void OnEnable(){
		m_gameController = GameController.instance;
		m_gameController.MouseLockStateChanged += OnMouseLockChanged;
	}

	void OnDisable(){
		m_gameController.MouseLockStateChanged -= OnMouseLockChanged;
	}

    private void OnMouseLockChanged(bool newState){
		GUI_Dot.gameObject.SetActive(newState);
    }
}
