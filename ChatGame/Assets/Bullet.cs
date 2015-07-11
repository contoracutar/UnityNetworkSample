using UnityEngine;
using System.Collections;

public class Bullet : Mover {

	int count = 0;
	public NetworkViewID nId;
	Vector3 vec;
	public void Init (NetworkViewID id, Vector3 v) {
		nId = id;
		vec = v;
	}

	void Start () {
	}
	
	void Update () {
		transform.position += vec;

		count++;
		if (count > 120) {
			Network.Destroy (gameObject);
		}
		GetComponent <NetworkView> ().RPC ("MovePlayer", RPCMode.Others, transform.position);
	}

	[RPC]
	public void MovePlayer (Vector3 position) {
		transform.position = position;
	}
}
