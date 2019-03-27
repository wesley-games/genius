using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonController : MonoBehaviour
{
    // public enum ButtonType {blue, red, green, yellow, pink};
    public ButtonType Type;

    public delegate void ButtonClicked(ButtonController button);
    public static event ButtonClicked OnButtonClicked;

    private AudioSource Sound;

    public void SetClickable(bool clickable)
    {
        GetComponent<PolygonCollider2D>().enabled = clickable;
        Sound = GetComponent<AudioSource>();
    }

    public IEnumerator Animate()
    {
        Sound.Play();
        transform.localScale += new Vector3(0.1f, 0.1f, 0); // Inflate
        yield return new WaitForSeconds(0.3f);
        transform.localScale -= new Vector3(0.1f, 0.1f, 0); // Deflate
        yield return new WaitForSeconds(0.3f);
    }

    void OnMouseDown()
    {
        OnButtonClicked(this);
        StartCoroutine(Animate());
    }
}
