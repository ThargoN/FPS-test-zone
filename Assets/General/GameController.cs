using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class GameController : Singleton<GameController> {
#region internalClasses
    [System.Serializable]
	public class MouseStateChangedEvent : UnityEvent<bool> {
    }
#endregion

    //public event Action<bool> MouseLockStateChanged;
    public MouseStateChangedEvent MouseLockStateChanged;

    // Unity callbacks /////////////////////////////////////////////////////////////////////////////////////////
    void Start () {
		
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

	void OnApplicationQuit(){
		
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
					MouseLockStateChanged.Invoke(value);
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

	public void ToggleLockMousePointer(){
		IsCursorLocked = !IsCursorLocked;
	}
	
	public void QuitApplication() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}


	// Events

}
