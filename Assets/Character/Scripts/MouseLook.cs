using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
	[Serializable]
	public class MouseLook
	{
		public float XSensitivity = 2f;
		public float YSensitivity = 2f;
		public bool clampVerticalRotation = true;
		public float MinimumX = -90F;
		public float MaximumX = 90F;
		public bool smooth;
		public float smoothTime = 5f;
		public bool lockCursorAfterStart = true;


		private Quaternion m_CharacterTargetRot;
		private Quaternion m_CameraTargetRot;
		public bool IsCursorLocked{ get; private set; }
        private GameController m_GameController = null;

		public void Init(Transform character, Transform camera)
		{
			m_CharacterTargetRot = character.localRotation;
			m_CameraTargetRot = camera.localRotation;

			m_GameController = GameController.instance;
            //m_GameController.MouseLockStateChanged += OnMouseLockChanged;
            m_GameController.MouseLockStateChanged.AddListener(OnMouseLockChanged);
			if(lockCursorAfterStart) IsCursorLocked = true;
        } 

        private void OnMouseLockChanged(bool newState)
        {
            IsCursorLocked = newState;
			InternalLockUpdate();
        }

        public void LookRotation(Transform character, Transform camera) {
			UpdateCursorLock();

			if (IsCursorLocked) {
				float yRot = CrossPlatformInputManager.GetAxis( "Mouse X" ) * XSensitivity;
				float xRot = CrossPlatformInputManager.GetAxis( "Mouse Y" ) * YSensitivity;

				m_CharacterTargetRot *= Quaternion.Euler( 0f, yRot, 0f );
				m_CameraTargetRot *= Quaternion.Euler( -xRot, 0f, 0f );

				if (clampVerticalRotation)
					m_CameraTargetRot = ClampRotationAroundXAxis( m_CameraTargetRot );

				if (smooth) {
					character.localRotation = Quaternion.Slerp( character.localRotation, m_CharacterTargetRot,
						smoothTime * Time.deltaTime );
					camera.localRotation = Quaternion.Slerp( camera.localRotation, m_CameraTargetRot,
						smoothTime * Time.deltaTime );
				} else {
					character.localRotation = m_CharacterTargetRot;
					camera.localRotation = m_CameraTargetRot;
				}

			}
		}

		// public void SetCursorLock(bool value)
		// {
		// 	lockCursor = value;
		// 	if(!lockCursor)
		// 	{//we force unlock the cursor if the user disable the cursor locking helper
		// 		Cursor.lockState = CursorLockMode.None;
		// 		Cursor.visible = true;
		// 	}
		// }

		public void UpdateCursorLock()
		{
            //if the user set "lockCursor" we check & properly lock the cursos
            if (lockCursorAfterStart)
            {
                InternalLockUpdate();
            }
        }

		private void InternalLockUpdate()
		{
			//TODO: Полностью переписать код блокировки курсора
			if(Input.GetKeyUp(KeyCode.Escape))
			{
				IsCursorLocked = false;
			}
			else if(Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject() )
			{
				IsCursorLocked = true;
			}

			m_GameController.IsCursorLocked = IsCursorLocked;
			// if (m_cursorIsLocked)
			// {
			// 	Cursor.lockState = CursorLockMode.Locked;
			// 	Cursor.visible = false;
			// }
			// else if (!m_cursorIsLocked)
			// {
			// 	Cursor.lockState = CursorLockMode.None;
			// 	Cursor.visible = true;
			// }
		}

		Quaternion ClampRotationAroundXAxis(Quaternion q)
		{
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1.0f;

			float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

			angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

			q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

			return q;
		}

	}
}