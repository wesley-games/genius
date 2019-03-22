using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public enum ButtonType {blue, red, green, yellow, pink};
    public ButtonType type;

    public delegate void ButtonClicked(ButtonController button);
    public static event ButtonClicked OnButtonClicked;

    public void Inflate()
    {
        transform.localScale += new Vector3(0.3f, 0.3f, 0);
    }

    public void Deflate()
    {
        transform.localScale -= new Vector3(0.3f, 0.3f, 0);
    }

    void OnMouseDown()
    {
        OnButtonClicked(this);
    }
}
