using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static float masterVolumePercent = 0.5f;

    public static AudioManager Instance;

    public float getVolume() {
        return masterVolumePercent;
    }


    void Awake ()
    {
        Instance = GetComponent<AudioManager>();
        
    }


    public void PlaySound(AudioClip clip, Vector3 pos) {
        //Debug.Log(masterVolumePercent);
        AudioSource.PlayClipAtPoint(clip, pos, masterVolumePercent);
    }
    
    public void SetVolume(float volumePercent) {
        masterVolumePercent = volumePercent;
    }

}
