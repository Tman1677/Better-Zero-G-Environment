using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GUICode : MonoBehaviour, PlayerScript{

	//crosshair
	public bool paused = false;
	Texture2D crosshairTexture;
	public float crosshairScale = 1;
	int crosshairSize = 14;
	int crosshairWidth = 2;
	CursorLockMode wantedMode;
	// Apply requested cursor state
	//	void SetCursorState ()
	//	{
	//		Cursor.lockState = wantedMode;
	//		// Hide cursor when locking
	//		Cursor.visible = (CursorLockMode.Locked != wantedMode);
	//	}
	public void onLoad() {

	}
	void OnGUI ()
	{
		GUI.DrawTexture (new Rect (new Vector2(Screen.width/2 - crosshairSize/2,Screen.height/2 - crosshairWidth/2),new Vector2(crosshairSize,crosshairWidth)), crosshairTexture);
		GUI.DrawTexture (new Rect (new Vector2(Screen.width/2 - crosshairWidth/2,Screen.height/2 - crosshairSize/2),new Vector2(crosshairWidth,crosshairSize)), crosshairTexture);
		//GUILayout.BeginVertical ();
		// Release cursor on escape keypress


		//GUILayout.Label ("Test");
		//		switch (Cursor.lockState)
		//		{
		//		case CursorLockMode.None:
		//			GUILayout.Label ("Cursor is normal");
		//			if (GUILayout.Button ("Lock cursor"))
		//				wantedMode = CursorLockMode.Locked;
		//			if (GUILayout.Button ("Confine cursor"))
		//				wantedMode = CursorLockMode.Confined;
		//			break;
		//		case CursorLockMode.Confined:
		//			GUILayout.Label ("Cursor is confined");
		//			if (GUILayout.Button ("Lock cursor"))
		//				wantedMode = CursorLockMode.Locked;
		//			if (GUILayout.Button ("Release cursor"))
		//				wantedMode = CursorLockMode.None;
		//			break;
		//		case CursorLockMode.Locked:
		//			GUILayout.Label ("Cursor is locked");
		//			if (GUILayout.Button ("Unlock cursor"))
		//				wantedMode = CursorLockMode.None;
		//			if (GUILayout.Button ("Confine cursor"))
		//				wantedMode = CursorLockMode.Confined;
		//			break;
		//		}

		//GUILayout.EndVertical ();

		//SetCursorState ();


	}


	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		crosshairTexture = new Texture2D(2,2);
	}

	// freezing motion and game events
	void Update () {


//		//Debug.Log (wantedMode);
//		//Cursor.lockState = wantedMode;
//		if (Cursor.visible == false) {
//			Cursor.lockState = CursorLockMode.Locked;
//			//Debug.Log ("Locking");
//		} else {
//			//Debug.Log ("no effect");
//		}



		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (paused) {
				paused = false;
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			} else {
				paused = true;
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			}

		}

	}
	//crosshair

}
