using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnOffButton : InteractableObject
{
    private Animator _animator;

    public bool isPressed;

    [Header("Button events")]
    public UnityEvent onButtonDown;
    public UnityEvent onButtonUp;

    private void Start()
    {
        _animator = gameObject.GetComponentInParent<Animator>();
    }

    public override void Interact()
    {
        base.Interact();
        PressButton();
    }
    
    public void PressButton()
    {
        if(!isPressed)
        {
            //foreach (GameObject go in goConnectedToButton)
            //{
            //    go.gameObject.SendMessage("OnButtonDown", SendMessageOptions.DontRequireReceiver);
            //}

            _animator.SetBool("IsClicked", true);
            isPressed = true;

            if (onButtonDown != null) onButtonDown.Invoke();
        }
        else
        {
            _animator.SetBool("IsClicked", false);
            isPressed = false;

            if(onButtonUp != null) onButtonUp.Invoke();
        }
    }
}
