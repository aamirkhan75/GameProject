using UnityEngine;
using System.Collections;

public class LevelScript : MonoBehaviour {

    private int EnemyCount = 0;
    private bool locked = true;
    public Transform door;
    private Vector3 doorstart;
    private float doormove = -2f;
    private float doorspeed = -10f;
    private bool movedoor = false;

	// Use this for initialization
	void Start () {
        doorstart = door.position;
	}
	
	// Update is called once per frame
	void Update () {

 
        if (movedoor == true) {
            if (locked == false)
            {
                if (door.position.y > doormove)
                {
                    door.position -= Vector3.down * doorspeed * Time.deltaTime;
                } else
                {
                    movedoor = false;
                }

            }
        }


	}

    public void AddEnemy()
    {
        EnemyCount++;
    }

    public void RemoveEnemy()
    {
        EnemyCount--;
        Debug.Log(EnemyCount);
        if (EnemyCount < 1)
        {
            DoorToggle();
        }
    }

    void DoorToggle()
    {
        locked = false;
        movedoor = true; 
    }

}
