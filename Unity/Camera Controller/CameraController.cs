using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    //The object you want to follow.
    public Transform target;

    //The Distance and Height from the object.
    public float distance = 20.0f;
    public float height = 5.0f;

    //If you want to set an offset off the object, chanage this in the inspector.
    public float lookAtHeight = 0.0f;

    //The Rigidbody of the object you want to follow. Used to determine how far the camera should zoom out when the car moves forward.
    public Rigidbody targetRigidbody;

    //Time it takes to go to the original rotation (used for delay between current rotation and wanted rotation).
    public float rotationSnapTime = 0.3f;

    //Time it takes to go back to the original zoom distance dependant of target's Rigidbody velocity.
    public float distanceSnapTime;

    //Make this around 0.1f for a small zoom out or 0.5f for a large zoom (depending on the speed of your rigidbody).
    public float distanceMultiplier = 0.0f;

    //For the LookAt function (to look at following object)
    Vector3 lookAtVector;

    //Used for a reference to the last distance value
    float usedDistance;

    //These are the variables used to determine the wanted angle of rotation and height.
    float wantedRotationAngle;
    float wantedHeight;
    Vector3 wantedPosition;

    //Comparing these numbers to the wanted rotation angle and height.
    float currentRotationAngle;
    float currentHeight;

    //For use in calculating the SmoothDampAngle for the current rotation angle.
    float yVelocity = 0.0f;

    //For use in calculating the SmoothDampAngle for the used distance.
    float zVelocity = 0.0f;

    void Start() {
        //Setting the camera to look at the target.
        lookAtVector = new Vector3(0, lookAtHeight, 0);
    }

    void LateUpdate() {
        wantedHeight = target.position.y + height;
        currentHeight = transform.position.y;

        wantedRotationAngle = target.eulerAngles.y;
        currentRotationAngle = transform.eulerAngles.y;

        currentRotationAngle = Mathf.SmoothDampAngle(currentRotationAngle, wantedRotationAngle, ref yVelocity, rotationSnapTime);

        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, Time.deltaTime);

        wantedPosition = target.position;
        wantedPosition.y = currentHeight;

        usedDistance = Mathf.SmoothDampAngle(usedDistance, distance + (targetRigidbody.velocity.magnitude * distanceMultiplier), ref zVelocity, distanceSnapTime);

        wantedPosition += Quaternion.Euler(0, currentRotationAngle, 0) * new Vector3(0, 0, -usedDistance);

        transform.position = wantedPosition;

        transform.LookAt(target.position + lookAtVector);
    }
}