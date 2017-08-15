using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

    public Text Rules;
    public Image Image;

    // Use this for initialization
    void Start()
    {
        Rules.rectTransform.sizeDelta = new Vector2(Screen.width - 55, Rules.rectTransform.rect.height);
        Rules.rectTransform.anchoredPosition = new Vector2(Screen.width / 2, -150);
        Image.rectTransform.anchoredPosition = new Vector2(- 100, -350);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Backspace)) //Go back to the game
        {
            Application.LoadLevel("Menu");
        }
    }
}
