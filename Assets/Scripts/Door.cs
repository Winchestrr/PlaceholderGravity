using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool opened = false;
    private Animator anim;

    public void Pressed()
    {
        anim = this.gameObject.GetComponent<Animator>();
        opened = !opened;
        anim.SetBool("Opened", !opened);
    }
}
