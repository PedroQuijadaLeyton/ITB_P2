using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObjectRoom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

    Canvas myCanvas;
    bool is_dragged = false;
    bool record_once = false;

    // Use this for initialization
    void Start ()
    {
        myCanvas = GameObject.FindGameObjectWithTag("canvas").GetComponent<Canvas>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(is_dragged)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
            transform.position = myCanvas.transform.TransformPoint(pos);
        }
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        is_dragged = true;
        if(!record_once)
            FindObjectOfType<Manager>().button_room_down();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        is_dragged = false;

        if (!record_once)
        {
            FindObjectOfType<Manager>().button_room_up();
            record_once = true;
        }


    }
}
