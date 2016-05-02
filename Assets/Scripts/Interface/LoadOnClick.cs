using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingImage;

	public void LoadScene(int level)
	{
		loadingImage.SetActive (true);
		Application.LoadLevel (level);
	}
}
