using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FadeText : MonoBehaviour {

    public float fadeSpeed = 2.0f;
    public bool active = false;
    public bool ready = false;

    private bool updateText = true;
    private bool updateText2 = true;
    private Color firstColor;

    void Start()
    {
        firstColor = GetComponent<TextMesh>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (active && !ready)
            fadeText();
        else if (ready && updateText)
            dontFade();
        else if (!active && !ready && updateText2)
            disableText();

    }

    void fadeText()
    {
        updateText = true;
        updateText2 = true;
        Color oldColor = GetComponent<TextMesh>().color;
        Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, Mathf.Sin(Time.time * fadeSpeed) + 1.0f);
        GetComponent<TextMesh>().color = newColor;
    }

    void disableText()
    {
        Color oldColor = GetComponent<TextMesh>().color;
        Color newColor = new Color(oldColor.r, oldColor.b, oldColor.g, 0);
        GetComponent<TextMesh>().color = newColor;
        updateText2 = false;
    }

    void dontFade()
    {
        updateText2 = true;
        GetComponent<TextMesh>().color = firstColor;
        updateText = false;
    }
}
