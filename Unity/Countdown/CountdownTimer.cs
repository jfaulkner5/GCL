// Countdown Timer script
// A basic countdown timer for use in any situation that you would need a countdown. 

using UnityEngine;
using System.Collections;

public class CountdownTimer : MonoBehaviour {

    //Set the time you want to start counting down from, on the script in the Unity Editor.
    public float timeRemaining;

    //This is here so that the timer doesn't start as soon as the game is loaded.
    //To activate the timer, reference the GameObject that has this script on it and add "isActive = true".
    //For example you may do timeToExplosion.isActive = true; if you have referenced a GameObject with this script on it and called it timeToExplosion.
    private bool isActive = false;

    //If you want your Game Over screen to show when the timer is up, atatch your Game Over Screen on the script in the Unity Editor.
    //Otherwise you can comment this out or delete it.
    //If you are wanting to restart the game from the Game Over Screen, make sure you remember to set the timeRemaining variable back to the number you chose
    public GameObject gameOverScreen;
	

	void Update () {
	    if(isActive)  {
            if(timeRemaining > 0.0f) {
                timeRemaining -= Time.deltaTime;
            } else {
                isActive = false;
                timeRemaining = 0.0f;

                //If you want your Game Over screen to show, keep this line. 
                //If you want something to activate instead, like an explosion, delete this line and instead reference the thing you want activated (like an explosion event).
                gameOverScreen.SetActive(true);
            }
        }
	}


    //If you want the countdown timer to be shown on screen, keep this, otherwise comment it out or delete it.
    //You can edit where the timer is shown by changing the 4 numbers. Edit the text section to whatever you want to display. The "F2" makes the timer show to 2 decimal places. you can put F0 for 0 decimal places.
    void OnGUI() {
        GUI.Label(new Rect(10, 10, 400, 90), "Time Remaining: " + timeRemaining.ToString("F2") + " seconds");
    }
}
