using UnityEngine;
using UnityEngine.XR.Iteraction.Toolkit;

public class StarEvent : MonoBehaviour
{
    private XRGrabInteractable grab; 
    private bool isSelect = false;
    private Vector3 originalScale;
    private Vector3 shrunkScale = new Vector3(0.25f, 0.25f, 0.25f);
    private float timer = 0f;
    private float waitTime = 5f;


    private void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        originalScale = transform.localScale;
    }

    private void onEnable()
    {
        grab.selectEntered.AddListener(onSelectEntered);
        grab.selectExited.AddListener(onSelectExited);
    }

    private void onDiable()
    {
        grab.selectEntered.RemoveListener(onSelectEntered);
        grab.selectExited.RemoveListener(onSelectExited);
    }

    void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {
        if (isSelect)
        {
            timer += Time.deltaTime;
            if (timer >= waitTime)
            {
                // When the star is holding enough time, it disappear
                Destroy(gameObject);
            }
            else
            {
                // While we holding the star the star is shrinking
                transform.localScale = Vector3.Lerp(originalScale, shrunkScale, timer/waitTime);
            }

        }
        else
        {
            // If the star is not hold then the star goes back to the original size
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, timer/waitTime);
        }
    }

    private void onSelectEntered(SelectEnterEventArgs arg0)
    {
        isSelect = true;
    }
    private void onSelectExited(SelectExitEventArgs arg0)
    {
        isSelect = false;
    }
}


