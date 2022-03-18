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

    //tu powinna by� mechanika:
    //lista obiekt�w, kt�re reaguj� na button. po klikni�ciu uruchamia si�
    //skrypt na wszystkich obiektach na li�cie. r�ne buttony tego samego typu (standard)
    //mog� uruchamia� r�ne obiekty, nie tylko drzwi, wi�c funkcja uruchamiana przez
    //PressButton() powinna uruchamia� r�ne funkcje na nich?
    
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
