using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour {
    public Renderer rend_;
    public Texture2D cursTxt_;
    public CursorMode cursMd_ = CursorMode.Auto;
    public Vector2 hotSpot_ = Vector2.zero;

    // Use this for initialization
    void Start () {
        Debug.Log("interactive bhvr initialised");
        rend_ = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseEnter()
    {
        //rend_.material.color = Color.red;
        Cursor.SetCursor(cursTxt_, hotSpot_, cursMd_);
        Debug.Log("set cursor");
    }

    void OnMouseOver()
    {
        //Debug.Log("Mouse is over obj.");
        rend_.material.color += new Color(-0.1F, 0, 0.3F) * Time.deltaTime;

    }

    void OnMouseExit() {
        Debug.Log("Mouse is no longer over obj.");
        rend_.material.color = Color.white;
        Cursor.SetCursor(null, Vector2.zero, cursMd_);
    }
}
