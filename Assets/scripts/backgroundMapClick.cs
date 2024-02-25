using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMapClick : MonoBehaviour
{
    // Start is called before the first frame update
    public OrbitalCamera cameraController;
    public int countryID;


    
    void Start()
    {
       
    }
     private float lastClickTime = 0f;
    private const float clickDelay = 0.5f;
    private int clickCount = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                if (Time.time - lastClickTime < clickDelay)
                {
                    clickCount++;
                }
                else
                {
                    clickCount = 1;
                }

                lastClickTime = Time.time;

                if (clickCount == 3)
                {
                    cameraController.targetPosition = transform.position;
                    cameraController.selectedCountry = -1;
                    StartCoroutine(DebugAfterSecond());
                    clickCount = 0;
                }
            }
        }
    }

    IEnumerator DebugAfterSecond()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Triple mouse click detected");
    }

   
    
}
