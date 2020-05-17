using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private AudioClip clickButton = null;
    [SerializeField] private GameObject audioManager;
    private AudioSource _audioSource;
    

    private void Awake()
    {
        _audioSource = audioManager.GetComponent<AudioSource>();
    }
    public void Click()
    {
        _audioSource.PlayOneShot(clickButton);
    }
}
