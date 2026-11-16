using UnityEngine;
using FMOD.Studio; 
using FMODUnity;

[CreateAssetMenu(fileName = "Interactabale", menuName = "Scriptable Objects/Interactabale")]
public class Interactabale : ScriptableObject
{ 
    //Declares a EventReference that holds a reference to our specific event
    [SerializeField] private EventReference pickUpHealth;
}
