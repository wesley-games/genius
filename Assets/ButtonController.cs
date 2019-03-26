using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public enum ButtonType {blue, red, green, yellow, pink};
    public ButtonType Type;

    public delegate void ButtonClicked(ButtonController button);
    public static event ButtonClicked OnButtonClicked;

    public void SetClickable(bool clickable)
    {
        GetComponent<PolygonCollider2D>().enabled = clickable;
    }

    public IEnumerator Animate()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0); // Inflate
        yield return new WaitForSeconds(0.5f);
        transform.localScale -= new Vector3(0.1f, 0.1f, 0); // Deflate
        yield return new WaitForSeconds(0.5f);
    }

    void OnMouseDown()
    {
        OnButtonClicked(this);
        StartCoroutine(Animate());
    }
}
