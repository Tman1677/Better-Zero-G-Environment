using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class NetworkController : MonoBehaviour {

	public NetworkManager manager;
	public enum connectionTypes {host,server,client};
	public static string matchHost;
	public static connectionTypes connectionType;
	void Awake() {
		manager = GetComponent<NetworkManager>();
		if (connectionType == connectionTypes.host) {
			manager.StartHost ();
		} else if (connectionType == connectionTypes.server) {
			manager.StartServer ();
		} else if (connectionType == connectionTypes.client) {
			manager.StartClient ();
			manager.SetMatchHost("localhost", 1337, false);
		} else {

		}
	}
}
