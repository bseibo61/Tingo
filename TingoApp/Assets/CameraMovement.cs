using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	// pan and zoom speed
    private static readonly float PanSpeed = 20f;
    private static readonly float zoomSpeedTouch = 0.1f;
    private static readonly float zoomSpeedMouse = 0.5f;

	//restrict movement to these bounds
    private static readonly float[] BoundsX = new float[] { -10f, 5f };
    private static readonly float[] BoundsZ = new float[] { -18f, 2f };
    private static readonly float[] ZoomBounds = new float[] { 10f, 85f };
    
    private Camera cam;

	// keeps track of the previous position to calculate how far finger moved
    private Vector3 lastPanPosition;
    private int panFIngerId;

	// keeps track of the previous position to calculate how far fingers moved
    private bool wasZoomingLastFrame;
    private Vector2[] lastZoomPositions;

	//getting the camera
    void Awake(){
        cam = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer){
            HandleTouch();
        }else{
            HandleMouse();
        }
    }

    void HandleMouse(){
        if (Input.GetMouseButtonDown(0)){
            lastPanPosition = Input.mousePosition;
        }else if (Input.GetMouseButton(0)){
            PanCamera(Input.mousePosition);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, zoomSpeedMouse);
    }

    void HandleTouch(){

        switch (Input.touchCount)
        {
            case 1: //One Finger
                wasZoomingLastFrame = false;

                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began){
                    lastPanPosition = touch.position;
                    panFIngerId = touch.fingerId;
                }else if(touch.fingerId == panFIngerId && touch.phase == TouchPhase.Moved){
                    PanCamera(touch.position);
                }
                break;

            case 2: // Two Fingers
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!wasZoomingLastFrame){
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                }else{
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, zoomSpeedTouch);

                    lastZoomPositions = newPositions;
                }
                break;

            default:

                wasZoomingLastFrame = false;
                break;
        }

    }

    void PanCamera(Vector3 newPosition){
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPosition);
        Vector3 move = new Vector3(offset.x * PanSpeed, 0, offset.y * PanSpeed * 4);

		transform.Translate(move, Space.World);

		// Locking view to determined bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
        pos.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);
		pos.y = Mathf.Clamp(transform.position.y, 1.55f, 1.55f);
		transform.position = pos;

        lastPanPosition = newPosition;
    }

    void ZoomCamera(float offset, float speed){
        if(offset == 0)return;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }
}
