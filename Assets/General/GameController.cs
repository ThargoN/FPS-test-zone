using UnityEngine;
using System.Collections;
using System;

public class GameController : Singleton<GameController> {

	public event Action testEvent;
	public event Action<bool> MouseLockStateChanged;

	// Unity callbacks /////////////////////////////////////////////////////////////////////////////////////////
	void Start () {
		testEvent += this.QuitApplication;
	}
		
	void Update() {
		// Выход. Пока вот такой вот простой
		if (Input.GetButtonUp( "Quit" )) {
			QuitApplication();
		}

	}

	void OnEnable() {
		IsCursorLocked = true;
	}

	void OnDisable() {
		IsCursorLocked = false;
	}


	// Properties //////////////////////////////////////////////////////////////////////////////////////////////
	public bool IsCursorLocked {
		set {
			bool lastState = !Cursor.visible;
			Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
			Cursor.visible = !value;

			// If MouseLock state changed, warn others
			if(lastState != value){
				if( MouseLockStateChanged != null){
					MouseLockStateChanged(value);
				}
			}
		}
		get {
			return !Cursor.visible;
		}
		
	}
	
	
	// Public methods //////////////////////////////////////////////////////////////////////////////////////////

	public void ReleaseMousePointer(){
		IsCursorLocked = false;
	}

	public void LockMousePointer(){
		IsCursorLocked = true;
	}
	
	public void QuitApplication() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}


	// Events
	protected void OnTest(){
		testEvent();
	}

}
