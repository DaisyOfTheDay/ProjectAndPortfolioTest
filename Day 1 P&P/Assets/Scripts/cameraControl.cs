using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class cameraControl : MonoBehaviour
{
    public int sensitivity;
    public int smoothing;

    Vector2 frameVelocity;
    Vector2 velocity;

    Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isPaused)
        {
            //Gets New Mouse Position
            Vector2 mouseMove = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            //Scales Movement By Sensitivity
            Vector2 rawFrameVelocity = Vector2.Scale(mouseMove, Vector2.one * sensitivity);

            //Interpolates between positions for smoothing
            frameVelocity = Vector2.Lerp(rawFrameVelocity, mouseMove, 1 / smoothing);

            //finalized position of rotation
            velocity += frameVelocity;

            //clamp y axis
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            //rotate camera y axis
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);

            //rotate player x axis
            parent.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        }


    }
}
