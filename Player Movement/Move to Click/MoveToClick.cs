// Move To Click script
// Moves GameObject the last left-clicked clicked position. You can also hold down left-click and it will follow the mouse.

using UnityEngine;
using System.Collections;

public class MoveToClick : MonoBehaviour {
    //Mouse co-ordinate variables
    private float mouseX;
    private float mouseY;

    //Mouse co-ordinate correction variables
    private float xCorrect;
    private float yCorrect;

    //Mouse position variable
    private Vector3 mousePos;

    //Speed variables
    public float moveSpeed;

    //The mouse axis value multiplyer, helps with exclaimation of the rotation.
    private float multiplyer = 10.0f;

    //The rotation to face when lerping.
    private Quaternion targetRotation;


    void Start () {
        //The movement speed, edit to match what you feel would fit.
        moveSpeed = 2.0f;

        //Correction of mouse position -- do not edit
        xCorrect = 8.8f;
        yCorrect = 5f;
    }
	

	void Update () {
	    if(Input.GetMouseButton(0)) {
            //Get the X and Y positions of the mouse
            mouseX = Input.mousePosition.x / Screen.width * 17.6f; //will be 0 if the mouse is on the very left
            mouseY = Input.mousePosition.y / Screen.height * 10; //will be 1 if the mouse is on the very right

            mousePos = new Vector3(mouseX - xCorrect, mouseY - yCorrect, 0.51f);
        }

        //Lerp the GameObject towards the cursor position, independant of the screen resolution
        if(transform.position != new Vector3(mouseX - xCorrect, 0, mouseY - yCorrect)) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, 0, mousePos.y), Time.deltaTime * moveSpeed);

            targetRotation = Quaternion.LookRotation(mousePos - transform.position);
            transform.rotation = targetRotation;
        }
    }
}