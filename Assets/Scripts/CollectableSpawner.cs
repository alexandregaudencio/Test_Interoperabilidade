using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private gatinho gatinho;
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private Vector3 rangeSize;
    [SerializeField] private float interval;

    private List<GameObject> gafanhotosInstances = new List<GameObject>();
    public List<GameObject> GafanhotosInstances => gafanhotosInstances;
    public List<Vector2> GafanhotosPositions()
    {
        List<Vector2> ret = new List<Vector2>();
        for (int i = 0; i < gafanhotosInstances.Count; i++)
        {
            ret.Add(gafanhotosInstances[i].transform.position);
        } 
        return ret;
    }
    public static CollectableSpawner Instance { get; private set; }
    private void Start()
    {
        Instance = this;
    }


    void OnEnable()
    {
        gatinho.OnGatinhoDataLoaded += LoadGafanhotos;

        StartCoroutine(SpawnCollectableLoop());

    }

    private void OnDisable()
    {

        StopCoroutine(SpawnCollectableLoop());

    }


    public void RemoveGafanhoto(GameObject gameObject)
    {
        Debug.Log("Removed " + gameObject);
        gafanhotosInstances.Remove(gameObject);
    }

    private void LoadGafanhotos(GatinhoData gatinhoData)
    {
        Debug.Log(gatinhoData.GafanhotoPositions.Count);

        if (gatinhoData.GafanhotoPositions != null || gatinhoData.GafanhotoPositions.Count > 0)
        {
            foreach (Vector2 gafanhotoPosition in gatinhoData.GafanhotoPositions)
            {
                GameObject gameObject = Instantiate(spawnObject, gafanhotoPosition, Quaternion.identity);
                gafanhotosInstances.Add(gameObject);  

            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, rangeSize);

    }

    private IEnumerator SpawnCollectableLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            GameObject gameObject = Instantiate(spawnObject);
            gameObject.transform.position = GetRandomPosition();
            gafanhotosInstances.Add(gameObject);
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(
            rangeSize.x * Random.value - rangeSize.x / 2,
            rangeSize.y * Random.value - rangeSize.y / 2,
            rangeSize.z * Random.value - rangeSize.z / 2);
    }
}
