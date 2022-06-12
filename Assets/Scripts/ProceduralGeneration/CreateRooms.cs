using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRooms : MonoBehaviour {
    [SerializeField] private List<GameObject> rooms;

    private void Start() {
        Vector3 position = new Vector3(0, 0, 100);
        foreach (GameObject room in rooms) {
            GameObject roomObject = Instantiate(room);
            roomObject.transform.position = position;
            
            position.x += 70;
            if(position.x == 70*5) {
                position.x = 0;
                position.y += 70;
            }
        }
    }
}
