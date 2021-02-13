using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetBehaviour : MonoBehaviour
{

    Touch touch;

    float rotSpeedPc = 700f;
    float rotSpeedMobile = 10f;
    public float amountPC = 6f;
    public float amountMobile = 8f;

    void FixedUpdate()
    {

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {

            if (gameObject.GetComponent<Rigidbody>().angularVelocity.magnitude < 0.25f)
            {

                gameObject.GetComponent<Rigidbody>().angularDrag = 0;

            }
            else
            {

                gameObject.GetComponent<Rigidbody>().angularDrag = 0.4f;

            }

            if (!Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
            {

                transform.Rotate(new Vector3(0, -0.4f, 0));

            }

        }

        if (Application.platform == RuntimePlatform.Android)
        {

            if (gameObject.GetComponent<Rigidbody>().angularVelocity.magnitude < 0.25f)
            {

                gameObject.GetComponent<Rigidbody>().angularDrag = 0;

            }
            else
            {

                gameObject.GetComponent<Rigidbody>().angularDrag = 0.4f;

            }

            if (Input.touchCount == 0)
            {

                transform.Rotate(new Vector3(0, -0.4f, 0));

            }

        }

    }

    void OnMouseDrag()
    {

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {

            if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonUp(0))
            {

                transform.Rotate(new Vector3(0, -Input.GetAxis("Mouse X"), 0) * Time.fixedDeltaTime * rotSpeedPc);

            }

        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {

                touch = Input.GetTouch(0);

                if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Began)
                {

                    float pointer_x = touch.deltaPosition.x;
                    transform.Rotate(new Vector3(0, -pointer_x, 0) * Time.fixedDeltaTime * rotSpeedMobile);

                }

            }

        }

    }

    void LateUpdate()
    {

        if (Application.platform == RuntimePlatform.WindowsEditor)
        {

            if (Input.GetMouseButtonDown(0))
            {

                gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            }

            if (Input.GetMouseButtonUp(0) && !Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0))
            {
                float x = -Input.GetAxis("Mouse X") * amountPC * Time.fixedDeltaTime;
                gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * x, ForceMode.VelocityChange);

            }

        }

        if (Application.platform == RuntimePlatform.Android)
        {

            touch = Input.GetTouch(0);

            if (Input.touchCount > 0)
            {

                if(touch.phase == TouchPhase.Began)
                {

                    gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

                }

                if (touch.phase == TouchPhase.Ended && touch.phase != TouchPhase.Moved && touch.phase != TouchPhase.Began)
                {

                    float pointer_x = touch.deltaPosition.x;
                    float x = -pointer_x * amountMobile * Time.fixedDeltaTime;
                    gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * x, ForceMode.VelocityChange);

                }

            }

        }

    }

}
