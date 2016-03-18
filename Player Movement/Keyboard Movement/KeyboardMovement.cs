// Keyboard Movement script.
// Moves the GameObject that this script is on, via the keyboard. This may also work with controllers, but I do not have one to test with.

using UnityEngine;
using System.Collections;

public class KeyboardMovement : MonoBehaviour {

    //In a 3D space, the GameObject that you have attached this script to, will move in any direction you choose, at the speed which you can change.
    public float speed;
    private float xPos;
    private float zPos;

    //Rotation speed
    public float rotateSpeed;

    //The mouse axis value multiplyer, helps with exclaimation of the rotation.
    public float multiplyer = 20.0f;

    void Start () {
        speed = 10.0f;

        //The rotate speed, edit to match what you feel would fit.
        rotateSpeed = 0.5f;
    }
	
	void Update () {
        //What direction are you wanting to move?
        xPos = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        zPos = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        //Move towards the direction you are implying
        transform.Translate(xPos, 0, zPos);

        //If you need a rotation, keep this line and the Start() fucntion, otherwise comment out the Start() function and this line below. 
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(zPos * multiplyer, 0, -xPos * multiplyer), rotateSpeed);
    }
}
