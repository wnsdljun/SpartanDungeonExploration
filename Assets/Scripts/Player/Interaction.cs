using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float checkRange;
    public LayerMask layerMask;

    public GameObject currentInteractObject;
    private IInteractable currentInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

            if (Physics.Raycast(ray, out RaycastHit hit, checkRange, layerMask))
            {
                if (hit.collider.gameObject != currentInteractObject)
                {
                    currentInteractObject = hit.collider.gameObject;
                    currentInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                currentInteractable = null;
                currentInteractObject = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = currentInteractable.GetInteractablePrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started && currentInteractable != null)
        {
            currentInteractable.OnInteract();
            currentInteractable = null;
            currentInteractObject = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
