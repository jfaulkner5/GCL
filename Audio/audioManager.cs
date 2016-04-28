using UnityEngine;
using System.Collections;




public class audioManager : MonoBehaviour
{
    //Two audiosources are needed so that you can fade the bgm without effecting the sound effects.
    public AudioSource efxSource;
    public AudioSource bgmSource;

    //An array of background clips
    public AudioClip[] bgmClips;

    //allows other scripts to call functions.
    public static audioManager instance = null;

    //The lowest and highest a sound can be ranomly pitched.
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    //Keeps the background music playing even if it's the background. Toggled off to begin with incase you dont want this feature.
    public bool DontDestroy = false;

    //The bgm audio.
    private float bgmAudio;

    //Checks if the player wants to change bgm
    private bool ChangeBGM;

    //Checks if the sound needs to fade in.
    private bool FadeIN = false;

    //Saves the current song number to a private int so other funcations can use it.
    private int bgmSongNumber;

    //The speed at which bgm changes.
    public float FadeSpeed = 0.5f;
	
	public bool PlayBgmOnStart;


    //sets up the instance & checks if you are using DontDestroyOnLoad.
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        if (DontDestroy == true)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
		if(PlayBgmOnStart == true)
		{
			bgmPlay(0);
		}
		
        bgmAudio = 1;
    }

    //Plays a single sound effect.
    public void efxPlay(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    //Checks if any bgm is playing, if their isnt any it plays the current song. If their is bgm playing it fades to the new song.
    public void bgmPlay(int songNumber)
    {
        bgmSongNumber = songNumber;
        if (bgmSource.clip == null)
        {
            bgmSource.clip = bgmClips[bgmSongNumber];
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else
        {
            ChangeBGM = true;
        }

    }

    //Picks a random pitch and clip and plays it.
    public void efxRandomize(params AudioClip[] clips)
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, clips.Length);

        //Choose a random pitch to play back our clip at between our high and low pitch ranges.
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        //Set the pitch of the audio source to the randomly chosen pitch.
        efxSource.pitch = randomPitch;

        //Set the clip to the clip at our randomly chosen index.
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    //Controls the master volume of the script. Set to 0 to mute sounds.
    public void MasterVolume(float volume)
    {
        bgmSource.volume = volume;
        efxSource.volume = volume;
    }

    //Lowers the bgm volume until it can't be heard. Then it runs fadeIn.
    void fadeOut()
    {
        //checks if the audio is below 0.01 and if the fadein bool has been effected.
        if (bgmAudio >= 0.01 && !FadeIN)
        {
            //lowers the bgm audio using the fadespeed varible.
            bgmAudio -= FadeSpeed * Time.deltaTime;
            //sets the bgm volume to the audio float.
            bgmSource.volume = bgmAudio;
        }
        else
            //runs fadeIn
            fadeIn();
    }

    //Raises the bgm volume until it is at max.
    void fadeIn()
    {
        //checks if the audio is above 1.
        if (bgmAudio < 1)
        {
            //sets the fade in bool to true, so that fadeout stops running.
            FadeIN = true;
            //Raises the bgm audio using the fadespeed varible.
            bgmAudio += FadeSpeed * Time.deltaTime;
            //sets the bgm volume to the audio float.
            bgmSource.volume = bgmAudio;
        }
        else
            //sets everything back to its default state.
            ChangeBGM = false;
        FadeIN = false;
    }


    void Update()
    {
        //checks if a new bgm is trying to be played.
        if (ChangeBGM == true)
        {
            //Runs fadeOut.
            fadeOut();
        }

        //Checks if the audio is below 0.1.
        if (bgmAudio <= 0.1)
        {
            //sets the bgm to the newest clip.
            bgmSource.clip = bgmClips[bgmSongNumber];
            bgmSource.Play();
        }

    }
}
