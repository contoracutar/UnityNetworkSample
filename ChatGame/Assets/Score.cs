using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {

	public static int nCount = 0;
	public static int dCount = 0;
	public static int oCount = 0;
	public static int mCount = 0;
	Text text;
	string tempText;

	void Start () {
		text = GetComponent<Text> ();
		tempText = text.text;
	}
	
	void Update () {
		if (gameObject.tag == "nomura") {
			text.text = tempText + nCount;
		}
		if (gameObject.tag == "douya") {
			text.text = tempText + dCount;
		}
		if (gameObject.tag == "ohata") {
			text.text = tempText + oCount;
		}
		if (gameObject.tag == "miura") {
			text.text = tempText + mCount;
		}
	}
}
