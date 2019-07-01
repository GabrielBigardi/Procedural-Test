using UnityEngine;

public class BoundActivate : MonoBehaviour
{
    public BuracoManager buracoManager;

    public void SpawnRoom()
    {
        buracoManager.GenerateRoomObjects();
    }
    
}
