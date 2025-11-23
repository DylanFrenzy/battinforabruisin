using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static Controls;
[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/ Input Reader")]
public class InputHandler : MonoBehaviour
{
    public event Action<bool> RollEvent;
    private Camera _mainCamera;
    private Controls controls;
    GameObject clickedObject;

    public void Awake()
    {
        _mainCamera = Camera.main;
    }


    public void OnClickItem(InputAction.CallbackContext context)
    {
        Debug.Log("very big gay");
        if (context.performed)
        {
            clickedObject = GetClickedObject(Mouse.current.position.ReadValue());
            if (clickedObject == null) return;
            Debug.Log(clickedObject.name);
            if (clickedObject.TryGetComponent<IInteractable>(out var clickable))
            {
                clickable.Interact();
            }
        }
        else if (context.canceled)
        {
            if (clickedObject == null) return;
            if (clickedObject.TryGetComponent<IInteractable>(out var clickable))
            {
                clickable.Disengage();
            }
        }
    }

    public void OnScrollCardleft(InputAction.CallbackContext context)
    {
    
    }

    public void OnScrollCardRight(InputAction.CallbackContext context)
    {
        
    }

    GameObject GetClickedObject(Vector2 screenPos)
    {
        Ray ray = _mainCamera.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
            return hit.transform.gameObject;

        return null;
    }
}
