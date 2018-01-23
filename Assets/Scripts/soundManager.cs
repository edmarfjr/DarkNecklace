using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour {

	public AudioSource sfxSource;
	public AudioSource musicSound;
	public static soundManager instance = null;
	public float lovPitchRange = .95f;
	public float highPitchRange = 1.0f;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			//Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);

	}

	public void PlaySingle(AudioClip clip, float aux)
	{
		sfxSource.clip = clip;
        sfxSource.pitch = aux;
		sfxSource.Play ();
	}
    public void ParaSingle(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Stop();
    }

    public void RandomsizeSdx (params AudioClip[] clip)
	{
		int randomIndex = Random.Range (0, clip.Length);
		float randomPitch = Random.Range (lovPitchRange, highPitchRange);
		sfxSource.clip = clip [randomIndex];
		sfxSource.pitch = randomPitch;
		sfxSource.Play ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
