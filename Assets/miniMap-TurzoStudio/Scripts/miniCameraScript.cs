using UnityEngine;
using System.Collections;

public class miniCameraScript : MonoBehaviour {


	public Transform MiniMapTarget;
	public Shader EffectShader;
	Camera camera;

    void Start()
    {
		camera = GetComponent<Camera>();
		camera.SetReplacementShader(EffectShader, "RenderType");
	}
	void LateUpdate () {

		transform.position = new Vector3 (MiniMapTarget.position.x,transform.position.y,MiniMapTarget.position.z);
		transform.eulerAngles = new Vector3( transform.eulerAngles.x, MiniMapTarget.eulerAngles.y, transform.eulerAngles.z );

	}
}