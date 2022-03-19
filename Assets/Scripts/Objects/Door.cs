using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpened = false;
    private Animator _animator;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        if (isOpened) return;
        isOpened = true;
        _animator.SetBool("Opened", isOpened);
    }

    public void CloseDoor()
    {
        if (!isOpened) return;
        isOpened = false;
        _animator.SetBool("Opened", isOpened);
    }
}
