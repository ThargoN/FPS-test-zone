////////////////////////////////////////////////////////////
/*														  */
/*  Light controller  by                                  */
/*  Igor Zuev AKA ThargoN [SG]							  */
/*														  */
////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	public GameObject LightSurface;
	public Light LightSource;

	public float LightIntensity = 1f;
	public Color LightColor = Color.white;
	public bool InitSettingsFromLightSrc = false;

	public float LightOnIntensityThreshold = 0.2f;
	public int FramesToUpdate = 3;

	// Use this for initialization
	void Start () {
		if( InitSettingsFromLightSrc && LightSource != null ) {
			LightIntensity = LightSource.intensity;
			LightColor = LightSource.color;
		}

		if(LightSource == null) {
			this.enabled = false;
		}

		UpdateLight();
	}
	

	private int frameCounter = 0;
	// Update is called once per frame

	void Update () {
		if(++frameCounter >= FramesToUpdate) {
			frameCounter = 0;

			UpdateLight();
		}
	}


	void UpdateLight() {
		LightSource.intensity = LightIntensity;
		LightSource.color = LightColor;

		if( LightIntensity < LightOnIntensityThreshold) {
			if (LightSurface != null) {
				LightSurface.SetActive( false );
			}
		}else{
			if (LightSurface != null) {
				LightSurface.SetActive( true );
				Material[] mats = LightSurface.GetComponent<MeshRenderer>().materials;
				mats[0].color = LightColor;
				mats[0].SetColor( "_EmissionColor", LightColor * 2f );
			}
		}
		
	}

}
