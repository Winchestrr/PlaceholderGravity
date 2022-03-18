using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardButton : InteractableObject
{
    public Transform buttonTopLimit;
    public Transform buttonDownLimit;
    public Transform buttonBase;
    private Animator _animator;

    public bool isPressed;

    public List<GameObject> goConnectedToButton = new List<GameObject>();

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public override void Interact()
    {
        base.Interact();
        PressButton();
    }

    //tu powinna byæ mechanika:
    //lista obiektów, które reaguj¹ na button. po klikniêciu uruchamia siê
    //skrypt na wszystkich obiektach na liœcie. ró¿ne buttony tego samego typu (standard)
    //mog¹ uruchamiaæ ró¿ne obiekty, nie tylko drzwi, wiêc funkcja uruchamiana przez
    //PressButton() powinna uruchamiaæ ró¿ne funkcje na nich?
    
    public void PressButton()
    {
        if(isPressed)
        {
            foreach (GameObject go in goConnectedToButton)
            {
                //goConnectedToButton.
            }
            _animator.SetBool("IsClicked", false);
            isPressed = !isPressed;
        }
        else
        {
            _animator.SetBool("IsClicked", true);
            isPressed = !isPressed;
        }
    }
}
