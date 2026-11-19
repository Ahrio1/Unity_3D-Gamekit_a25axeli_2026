using UnityEngine;
using FMOD.Studio; 
using FMODUnity;

[CreateAssetMenu(fileName = "Interactabale", menuName = "Scriptable Objects/Interactabale")]
public class Interactabale : ScriptableObject
{ 
    //Declares a EventReference that holds a reference to our specific event
    [SerializeField] private EventReference pickUpHealthAudio, boxDestroyAudio, doorAudio, switchAudio;

    //Declares a method that should play the pickUpHealthAudio sound without 3D-positioning by referencing a Transfrom position
    public void pickUpHealthPlay(Transform objectTransfrom)
    {
        RuntimeManager.PlayOneShot(pickUpHealthAudio,  objectTransfrom .position);
    }
    
    //Declares a method that should play the boxDestroy sound with 3D-positioning by referencing a Transfrom position
    public void boxDestroyPlay(Transform objectTransfrom)
    {
        RuntimeManager.PlayOneShot(boxDestroyAudio,  objectTransfrom .position);
    }
    public void swictchAudioPlay(Transform objectTransfrom)
    {
        RuntimeManager.PlayOneShot(switchAudio,  objectTransfrom .position);
    }
}
