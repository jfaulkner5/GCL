// Follow Mouse Lerp script
// Moves the GameObject that this script is on, lerping towards where the mouse is.

using UnityEngine;
using System.Collections;

public class FollowMouseLerp : MonoBehaviour {
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

    //The rotation to face when lerping.
    private Quaternion targetRotation;


    public void Start() {
		//The speed in which the GameObject will lerp towards the mouse position
        moveSpeed = 0.05f;

        //Correction of mouse position -- do not edit
        xCorrect = 8.8f;
        yCorrect = 5f;
    }

	
	public void Update() {
		//Return the normalized mouse position (between 0 to 1) relative to the screen
		mouseX = Input.mousePosition.x / Screen.width * 17.6f; //will be 0 if the mouse is on the very left
		mouseY = Input.mousePosition.y / Screen.height * 10f; //will be 1 if the mouse is on the very right
		
		//Now create a new Vector3.
		//You can edit the normalised values of the mouse's X and Y positions to between -0.5f to 0.5f as shown below.
		//Of course you can edit the values to whatever you want, it probably makes sense though, to have 0 be the middle of the screen.
		//Your character (or whatever it is you have put this script on) should now follow your mouse independant of screen resolution.
		mousePos = new Vector3(mouseX - xCorrect, 0.51f, mouseY - yCorrect);
		
		//Lerp the GameObject towards the cursor position, independant of the screen resolution
        transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, 0, mousePos.z), moveSpeed);

        //If you need a rotation, keep this line and the Start() fucntion, otherwise comment out the Start() function and this line below.
        targetRotation = Quaternion.LookRotation(mousePos - transform.position);
        transform.rotation = targetRotation;
    }
}