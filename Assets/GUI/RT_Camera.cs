using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// GUI Camera script by Igor Zuev AKA ThargoN [SG]
/// Updates render texture's dimensions
/// </summary>

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class RT_Camera : MonoBehaviour {

	public Camera LeadCamera = null;
	private Camera cachedCamera = null;
	private int cachedWidth = 0;
	private int cachedHeight = 0;

	public void OnEnable() {
		cachedCamera = GetComponent<Camera>();

		if (LeadCamera == null) {
			LeadCamera = Camera.main;
		}
		
		cachedWidth = LeadCamera.pixelWidth;
		cachedHeight = LeadCamera.pixelHeight;

		if (cachedCamera.targetTexture == null) {
			Debug.LogWarning( "Creating render texture", this );
			cachedCamera.targetTexture = new RenderTexture( cachedWidth, cachedHeight, 16, RenderTextureFormat.ARGB32 );
		}
	}

	public void OnPreRender() {

		cachedCamera.fieldOfView = LeadCamera.fieldOfView;
		

		if (cachedWidth != LeadCamera.pixelWidth || cachedHeight != LeadCamera.pixelHeight) {
			RenderTexture rt = cachedCamera.targetTexture;
			cachedWidth = LeadCamera.pixelWidth;
			cachedHeight = LeadCamera.pixelHeight;

			if (rt.IsCreated()) {
				rt.Release();

				cachedCamera.targetTexture.width = cachedWidth;
				cachedCamera.targetTexture.height = cachedHeight;
				cachedCamera.ResetWorldToCameraMatrix();
				//rt.Create();
			}
			
		}
	}

	public void OnDisable() {
		if(cachedCamera != null && cachedCamera.targetTexture != null) {
			cachedCamera.targetTexture.Release();
		}
	}
}
