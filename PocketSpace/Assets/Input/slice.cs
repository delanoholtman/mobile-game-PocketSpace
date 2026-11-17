using System.Collections;
using System.ComponentModel.Design;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SwipeBladeInputActions : MonoBehaviour
{
    public InputActionAsset inputActions;
    public GameObject meteorr;
    private InputAction touchPress;
    private InputAction touchPosition;
    private Vector2 lastPosition;
    private bool isSlicing;  

    void OnEnable()
    {
        var gameplay = inputActions.FindActionMap("actions");

        touchPress = gameplay.FindAction("TouchPress");
        touchPosition = gameplay.FindAction("TouchPosition");

        touchPress.Enable();
        touchPosition.Enable();

        touchPress.started += OnTouchStarted;
        touchPress.canceled += OnTouchEnded;
    }

    void OnDisable()
    {
        touchPress.started -= OnTouchStarted;
        touchPress.canceled -= OnTouchEnded;

        touchPress.Disable();
        touchPosition.Disable();
    }

    void Update()
    {
        if (!isSlicing) return;

        Vector2 screenPos = touchPosition.ReadValue<Vector2>();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        Slice(lastPosition, worldPos);
        lastPosition = worldPos;
    }

    void OnTouchStarted(InputAction.CallbackContext ctx)
    {
        Vector2 screenPos = touchPosition.ReadValue<Vector2>();
        lastPosition = Camera.main.ScreenToWorldPoint(screenPos);
        isSlicing = true;
    }

    void OnTouchEnded(InputAction.CallbackContext ctx)
    {
        isSlicing = false;
    }

    void Slice(Vector2 start, Vector2 end)
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(start, end);
        foreach (var hit in hits)
        {
            GameObject hitted = hit.collider.gameObject;
            if (hitted.CompareTag("Oxygen") || hitted.CompareTag("Fuel") || hitted.CompareTag("Stardust") || hitted.CompareTag("Mineral")) 
            {
                GameManager.instance.AstroidHit(hitted);
                meteorr.SetActive(true);
                meteorr.transform.position = hitted.transform.position;
                Destroy(hitted);
                meteorr.SetActive(false);
            }
            else if (hitted.CompareTag("buy"))
            {
                if(hitted.name == "buy1")
                {
                    GameManager.instance.BuyPressed(1);
                }
                else if(hitted.name == "buy2")
                {
                    GameManager.instance.BuyPressed(2);
                }
            }
            else if (hitted.CompareTag("reject"))
            {
                if(hitted.name == "Reject1")
                {
                    GameManager.instance.RejectPressed(1);
                }
                else if(hitted.name == "Reject2")
                {
                    GameManager.instance.RejectPressed(2);
                }
            }
            else if(hitted.CompareTag("Inventory"))
            {
                if(SceneManager.GetActiveScene().name == "meteor")
                {
                    SceneManager.LoadScene("inventory");
                }
                else if(SceneManager.GetActiveScene().name == "inventory")
                {
                    SceneManager.LoadScene("meteor");
                }
            }
        }
    }
}