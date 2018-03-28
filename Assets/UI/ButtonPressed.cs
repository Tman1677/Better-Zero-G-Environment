using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System;
public class ButtonPressed : MonoBehaviour {
	NetworkManager manager;
	void Start(){
		
	}

	public void commandArguments() {
		/*my idea was to allow a custom command line argument to start the game directly into server mode
		 * but I'm not 100% sure how realistic that was or if there's a better way to create a headless
		 * server for the game, plus I have higher priorities so I'll put working on this on the backburner*/
		string[] arguments = Environment.GetCommandLineArgs();
		Debug.Log ("Debugging command line arguments");
		foreach(string arg in arguments)
		{
			Debug.Log (arg.ToString());
			if (arg == "server") {

				server ();
			}
		}
	}

	public void host () {
		NetworkController.connectionType = NetworkController.connectionTypes.host;
		SceneManager.LoadScene ("MainGame");
	}
	public void server() {
		NetworkController.connectionType = NetworkController.connectionTypes.server;
		SceneManager.LoadScene ("MainGame");
	}
	public void joinGame() {
		NetworkController.connectionType = NetworkController.connectionTypes.client;
		NetworkController.matchHost = "localhost";
		SceneManager.LoadScene ("MainGame");
	}
}
