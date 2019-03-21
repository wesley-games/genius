using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    
    public void Inflate()
    {
        transform.localScale += new Vector3(0.3f, 0.3f, 0);
    }

    public void Deflate()
    {
        transform.localScale -= new Vector3(0.3f, 0.3f, 0);
    }
}
