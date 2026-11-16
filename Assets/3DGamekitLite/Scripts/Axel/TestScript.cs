using System;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public string characterName = "Nalle puh";
    public int nummberOfGames;
    public bool zombie = false;
    public GameObject playerObject;

    public void OnCollisionEnter(Collision other)
    {
        throw new NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() 
    {


 }

    // Update is called once per frame
    void Update()
    {
        
    }
}
