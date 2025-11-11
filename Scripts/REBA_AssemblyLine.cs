using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_AssemblyLine : MonoBehaviour
{
    public GameObject packetPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;
    private float spawnInterval = 4.0f;
    private float destroyDistance = 2f;
    private List<GameObject> activePackets = new List<GameObject>(); // List to keep track of active packets

    private void Start()
    {
        //spawnPoint.position = new Vector3(-5.06867123f, -5.72711658f, -18.7013645f);
        //endPoint.position = new Vector3(7.45900011f, -7.79400015f, -18.641f);
        InvokeRepeating("SpawnPacket", 0, spawnInterval);

    }
    // Method to start spawning packets
    public void StartSpawning()
    {
        // Start spawning packets at regular intervals
        InvokeRepeating("SpawnPacket", 0, spawnInterval);
    }

    void SpawnPacket()
    {
        // Instantiate a new packet at the spawn point
        GameObject packet = Instantiate(packetPrefab, spawnPoint.localPosition, Quaternion.identity);
        activePackets.Add(packet);
        // Move the packet along the assembly line
        StartCoroutine(MovePacket(packet));
    }
    // Method to stop spawning and moving packets
    public void StopSpawningAndMovement()
    {
        CancelInvoke("SpawnPacket"); // Stop spawning new packets
        StopAllCoroutines(); // Stop all packet movement coroutines
        foreach (var packet in activePackets)
        {
            packet.SetActive(false); // Deactivate all packets
        }
        activePackets.Clear(); // Clear the list
    }

    System.Collections.IEnumerator MovePacket(GameObject packet)
    {
        while (Vector3.Distance(packet.transform.position, endPoint.position) > destroyDistance)
        {
            yield return null;
        }
        activePackets.Remove(packet);
        // Destroy the packet when it reaches the end point
        Destroy(packet);
    }
}
