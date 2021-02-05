using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    //Drag speed
    float rotSpeed = 700f;

    //Inertia speed
    public float amount = 20f;

    void FixedUpdate()
    {

        //When playing on Windows
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {

            if (gameObject.GetComponent<Rigidbody>().angularVelocity.magnitude < 0.25f)
            {

                gameObject.GetComponent<Rigidbody>().angularDrag = 0;

            }
            else

            //Maintain Minimun autonomous speed
            {

                gameObject.GetComponent<Rigidbody>().angularDrag = 0.4f;

            }

            if (Input.GetMouseButtonUp(0))
            {
                float x = -Input.GetAxis("Mouse X") * amount * Time.fixedDeltaTime;
                gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * x, ForceMode.VelocityChange);

            }

            else if (Input.GetMouseButton(0))
            {

                transform.Rotate(new Vector3(0, -Input.GetAxis("Mouse X"), 0) * Time.fixedDeltaTime * rotSpeed);

            }

            if (Input.GetMouseButtonDown(0))
            {

                gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            }
            //Minimun autonomous speed
            if (!Input.GetMouseButtonDown(0))
            {

                transform.Rotate(new Vector3(0, -0.4f, 0));

            }

        }

        //When playing on Android
        if (Application.platform == RuntimePlatform.Android)
        {

            Touch touch = Input.GetTouch(0);

            if (gameObject.GetComponent<Rigidbody>().angularVelocity.magnitude < 0.25f)
            {

                gameObject.GetComponent<Rigidbody>().angularDrag = 0;

            }

            //Maintain Minimun autonomous speed
            else
            {

                gameObject.GetComponent<Rigidbody>().angularDrag = 0.4f;

            }

            if (touch.phase == TouchPhase.Ended)
            {

                float pointer_x = touch.deltaPosition.x;
                float x = pointer_x * amount * Time.fixedDeltaTime;
                gameObject.GetComponent<Rigidbody>().AddTorque(transform.up * -x, ForceMode.VelocityChange);

            }

            if (touch.phase == TouchPhase.Moved)
            {

                float pointer_x = touch.deltaPosition.x;
                transform.Rotate(new Vector3(0, -pointer_x, 0));

            }

            if (touch.phase == TouchPhase.Began)
            {

                gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            }
            //Minimun autonomous speed
            if (Input.touchCount == 0)
            {

                transform.Rotate(new Vector3(0, -0.4f, 0));

            }

        }

    }

}
