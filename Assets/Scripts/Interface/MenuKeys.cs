using UnityEngine;
using System.Collections;

public class MenuKeys : MonoBehaviour {

	private ClickToLoadAsync click;

	// Use this for initialization
	void Start () {
		click = GetComponent<ClickToLoadAsync> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			click.ClickAsync (1);
		}
	}
}
