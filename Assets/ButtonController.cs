using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public enum ButtonType {blue, red, green, yellow, pink};
    public ButtonType Type;

    public delegate void ButtonClicked(ButtonController button);
    public static event ButtonClicked OnButtonClicked;

    public IEnumerator Animate()
    {
        transform.localScale += new Vector3(0.3f, 0.3f, 0); // Inflate
        yield return new WaitForSeconds(0.5f);
        transform.localScale -= new Vector3(0.3f, 0.3f, 0); // Deflate
        yield return new WaitForSeconds(0.5f);
    }

    void OnMouseDown()
    {
        Debug.Log(Type);
        OnButtonClicked(this);
        StartCoroutine(Animate());
    }
}
