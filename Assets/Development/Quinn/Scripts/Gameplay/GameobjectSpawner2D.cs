using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameobjectSpawner2D : MonoBehaviour
{
    [SerializeField] private float spawnChance = 0.5f;      //de kans dat een frame iets spawnt
    [SerializeField] private float spawnDelaySeconds = 1;   //de delay in seconden tussen elke spawn "attempt"
    [SerializeField] private float xSpawnDistance;          //de hoeveelheid in de X richting dat dingen gepsawned kunnen worden
    [SerializeField] private float ySpawnDistance;          //de hoeveelheid in de Y richting dat dingen gepsawned kunnen worden
    [SerializeField] private bool spawnerEnabled = true;    //of de spawner enabled is
    [SerializeField] private GameObject[] spawnArray;       //de gameobjecten die gespawned worden inzitten

    private Coroutine spawnCoroutine;     //de coroutine dat 

    //of de spawner enabled is (readonly property)
    public bool SpawnerEnabled { get => SpawnerEnabled; }

    /// <summary>
    /// zet de spawner aan en start de coroutine
    /// </summary>
    public void Enable()
    {
        //als de spawner al enabled is
        if (spawnerEnabled)
            //genereer een error
            throw new System.Exception("spawner is alread enabled");

        //zet de boolean naar true
        spawnerEnabled = true;

        //start de coroutine
        spawnCoroutine = StartCoroutine(SpawnTimer());
    }

    /// <summary>
    /// stopt de spawner en de coroutine
    /// </summary>
    public void Disable()
    {
        //als de spawner al disabled is
        if (spawnerEnabled)
            //genereer een error
            throw new System.Exception("spawner is alread disabled");


        spawnerEnabled = false;
        StopCoroutine(spawnCoroutine);
    }

    /// <summary>
    /// spawnt een willekeurig <see cref="GameObject"/> uit <see cref="spawnArray"/><br/>
    /// op een willekeurige plek binnen het veld <see cref="xSpawnDistance"/> bij <see cref="ySpawnDistance"/>
    /// </summary>
    private void Spawn()
    {
        //selecteer een willekeurig GameObject
        int spawnIndex = Random.Range(0, spawnArray.Length);

        //genereer de willekeurige relatieve positie
        float distanceX = Random.Range(0f, xSpawnDistance + float.Epsilon); //epsilon = kleinste positieve waarde dat een floating point kan bevatten
        float distanceY = Random.Range(0f, ySpawnDistance + float.Epsilon);

        //bereken de werkelijke positie
        Vector2 newPos = transform.position + new Vector3(distanceX, distanceY);

        //spawnt het GameObject
        Instantiate(spawnArray[spawnIndex], newPos, Quaternion.identity);
    }

    private IEnumerator SpawnTimer()
    {
        //zolang de spawner enabled is
        while (spawnerEnabled)
        {
            //probeer iets te spawnen
            if (Random.value < spawnChance) //als het percentage berrijkt is
                Spawn(); //spawn een gameObject

            //wacht voor de delay
            yield return new WaitForSeconds(spawnDelaySeconds);
        }
    }

    /// <summary>
    /// opgeroepen op de eerste frame
    /// </summary>
    private void Start()
    {
        //als de spawner enabled is
        if (spawnerEnabled)
            //start de coroutine
            StartCoroutine(SpawnTimer());
    }

    private void OnDrawGizmos()
    {
        //bereken wat het midden is
        Vector3 cubeCenter = transform.position + (new Vector3(xSpawnDistance, ySpawnDistance) / 2);

        //krijg de groote
        Vector3 cubeSize = new(xSpawnDistance, ySpawnDistance);

        //teken de vorm
        Gizmos.DrawWireCube(cubeCenter, cubeSize);
    }
}