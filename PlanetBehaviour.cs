using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{

    Touch touch;

    float rotSpeedPc = 700f; // Rotation speed of the drag
    float rotSpeedMobile = 10f;
    float amountPC = 25f; // Inertia speed
    float amountMobile = 8f;
    int locked = 0; // Change is not advisible


    void FixedUpdate()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Application.platform == RuntimePlatform.WindowsEditor) // Code to work on windows
        {
            
            gameObject.GetComponent<Rigidbody>().angularDrag = 0.4f;

            if (!Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0) || !Physics.Raycast(ray) && locked == 0) // Natural rotation
            {

                transform.Rotate(new Vector3(0, -0.4f, 0));

            }

            if (Input.GetMouseButtonDown(0)) // Stop rotation when clicked
            {

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                Physics.Raycast(ray, out hit);

                if(hit.collider != null)
                {

                    if (hit.collider.CompareTag("Planet")) // Tag on the planet object
                    {

                        gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                        locked = 1;

                    }

                }
          
            }

        }

        if (Application.platform == RuntimePlatform.Android) // Code to work on mobile
        {

            gameObject.GetComponent<Rigidbody>().angularDrag = 0.4f;

            if (Input.touchCount == 0 || !Physics.Raycast(ray) && locked == 0) // Natural Rotation
            {

                transform.Rotate(new Vector3(0, -0.4f, 0));

            }

            if(Input.touchCount > 0) // Stop rotation when clicked
            {

                if (touch.phase == TouchPhase.Began)
                {

                    ray = Camera.main.ScreenPointToRay(touch.position);

                    Physics.Raycast(ray, out hit);
                    if (hit.collider != null)
                    {

                        if (hit.collider.CompareTag("Planet")) // Tag on the planet object
                        {

                            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                            locked = 1;

                        }

                    }

                }

            }

        }

    }

    void OnMouseDrag() // Movement when dragged 
    {

        if (Application.platform == RuntimePlatform.WindowsEditor) // On Windows
        {

            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonUp(0))
            {

                transform.Rotate(new Vector3(0, -Input.GetAxis("Mouse X"), 0) * Time.fixedDeltaTime * rotSpeedPc);

            }

        }

        if (Application.platform == RuntimePlatform.Android) // On mobile
        {
            if (Input.touchCount > 0)
            {

                touch = Input.GetTouch(0);

                if (touch.phase != TouchPhase.Began && touch.phase != TouchPhase.Ended)
                {

                    float pointer_x = touch.deltaPosition.x;
                    transform.Rotate(new Vector3(0, -pointer_x, 0) * Time.fixedDeltaTime * rotSpeedMobile);

                }

            }

        }

    }


    void LateUpdate()
    {

        if (Application.platform == RuntimePlatform.WindowsEditor) // On Windows
        {

            if (locked == 1 && Input.GetMouseButtonUp(0) && !Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0)) // Inertia after released
            {

                locked = 0;
                float x = -Input.GetAxis("Mouse X") * amountPC * Time.fixedDeltaTime;
                gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * x, ForceMode.VelocityChange);

            }

        }

        if (Application.platform == RuntimePlatform.Android) // On mobile
        {

            if (Input.touchCount > 0)
            {

                touch = Input.GetTouch(0);

                if (locked == 1 && touch.phase == TouchPhase.Ended && touch.phase != TouchPhase.Moved && touch.phase != TouchPhase.Began) // Inertia after released
                {

                    locked = 0;
                    float pointer_x = touch.deltaPosition.x;
                    float x = -pointer_x * amountMobile * Time.fixedDeltaTime;
                    gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * x, ForceMode.VelocityChange);

                }

            }

        }

    }

}
