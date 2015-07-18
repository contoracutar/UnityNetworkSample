using UnityEngine;
using System.Collections;

public class Contoroller : Mover {

	public bool isMine;
	float s = 0.5f;

	GameObject bullet, b;
	int nC, dC, oC, mC;
	void Start () {
		bullet = (GameObject)Resources.Load ("Bullet");
	}

	void Update () {
		if (!NetworkViewManager.connected) {
			return;
		}

		if (isMine) {
			Vector3 v = new Vector3 (Input.GetAxis ("Horizontal"), 0 ,Input.GetAxis ("Vertical"));
			transform.Translate (v*Time.deltaTime*10f);
			if (Input.GetKeyDown (KeyCode.Space)) {
				int rad = 360 / 6;
				for(int i = 0; i < 360; i += rad){
					Vector3 vel = new Vector3(Mathf.Cos(Radians(i)),0, Mathf.Sin(Radians(i))) * 0.2f;
					b = (GameObject)Network.Instantiate (bullet, transform.position, bullet.transform.rotation, 1);
					GetComponent <NetworkView> ().RPC ("InitBullet", RPCMode.All, GetComponent<NetworkView> ().viewID, vel);
				}
			}
			GetComponent <NetworkView> ().RPC ("MovePlayer", RPCMode.Others, transform.position);
		}
	}

	[RPC]
	public void MovePlayer (Vector3 position) {
		transform.position = position;
	}

	[RPC]
	public void InitBullet (NetworkViewID id, Vector3 vel) {
		b.GetComponent<Bullet> ().Init (id, vel);
	}

	[RPC]
	public void ScoreCounter (int n) {
		if (n == 0) { Score.nCount++; }
		if (n == 1) { Score.dCount++; }
		if (n == 2) { Score.oCount++; }
		if (n == 3) { Score.mCount++; }
	}

	void OnTriggerEnter(Collider c){
		if (c.gameObject.GetComponent<Bullet>().nId != GetComponent<NetworkView> ().viewID){
		
			if (CameraControl.n.GetComponent<NetworkView> ().viewID == c.GetComponent<Bullet> ().nId) {
				GetComponent<NetworkView> ().RPC ("ScoreCounter", RPCMode.All, 0);
			}
			if (CameraControl.d.GetComponent<NetworkView> ().viewID == c.GetComponent<Bullet> ().nId) {
				GetComponent<NetworkView> ().RPC ("ScoreCounter", RPCMode.All, 1);
			}
			if (CameraControl.o.GetComponent<NetworkView> ().viewID == c.GetComponent<Bullet> ().nId) {
				GetComponent<NetworkView> ().RPC ("ScoreCounter", RPCMode.All, 2);
			}
			if (CameraControl.m.GetComponent<NetworkView> ().viewID == c.GetComponent<Bullet> ().nId) {
				GetComponent<NetworkView> ().RPC ("ScoreCounter", RPCMode.All, 3);
			}

			Network.Destroy (c.gameObject);
			Network.Destroy (gameObject);
		}
	}
}
