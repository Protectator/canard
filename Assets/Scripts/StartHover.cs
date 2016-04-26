using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Text theText;

	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = Vector2.zero;

	public void OnPointerEnter(PointerEventData enventData)
	{
		theText.color = Color.white;
		Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
	}

	public void OnPointerExit(PointerEventData eventData) 
	{
		theText.color = Color.black;
		Cursor.SetCursor (null, Vector2.zero, cursorMode);
	}

}
