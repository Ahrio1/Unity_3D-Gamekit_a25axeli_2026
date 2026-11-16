using Gamekit3D;
using UnityEngine;

public class SurvivalMode : MonoBehaviour
{
    [SerializeField] private float thirstLevel = 100f;
    [SerializeField] private float thirstSpeed;
    [SerializeField] private float timer;

    [SerializeField] private bool thirsty;
    [SerializeField] private bool parched;
    [SerializeField] private GameObject playerObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {    
        //Subtracting the current value of thirstLevel with thirstSpeed. thirstSpeed is normalized
        // by using Time-deltaTime, so that it's frame rate independent. math.clamp restricts the thirstLevel
        // between its current max value and 0
        
        thirstLevel -= Mathf.Clamp(thirstSpeed*Time.deltaTime,0,thirstLevel);
        if (thirstLevel <= 50f && thirsty == false)
        {
            Debug.Log("You are thirsty");
            thirsty = true;
    
        }
        else if (thirstLevel > 50f && thirsty == true)
        {
            thirsty = false;
        }
        if (thirstLevel <= 25f && parched == false)
        {
            Debug.Log("You are parched!!");
            parched = true;
        }
        else if (thirstLevel > 25f && parched == true)
        {
            parched = false;
        }

        if (thirstLevel == 0 && thirsty)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
               Damageable.DamageMessage msg = new Damageable.DamageMessage()
                           {
                               amount = 1,
                               damager = this,
                               damageSource = new Vector3(x: 0, y: 0, z: 0),
                               direction = new Vector3(x: 0, y: 0, z: 0),
                               throwing = false,
                               stopCamera = false
                           };
                           playerObject.GetComponent<Damageable>().ApplyDamage(msg);
                           
                           Debug.Log("You are hurt by your thirst");
                           timer = 10f;
            }
            
        }


    }
}
