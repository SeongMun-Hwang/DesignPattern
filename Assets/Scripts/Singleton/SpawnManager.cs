using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public GameObject SpawnPrefab;
    public UnitFactory[] unitFactories;
    static SpawnManager instance;
    static public SpawnManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    public void Spawn()
    {
        Vector3 viewPort = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 0.5f), 0);

        Vector3 hitPoint = GetTerrainHitPoint(viewPort);
        //Instantiate(SpawnPrefab, hitPoint, Quaternion.Euler(0, Random.Range(0, 360), 0));

        UnitFactory factory = unitFactories[0];
        IUnitProduct product=factory.GetProduct(hitPoint);

        StartCoroutine(ReturnAfterDelay(factory, product,5f));
    }
    IEnumerator ReturnAfterDelay(UnitFactory factory, IUnitProduct product, float delay)
    {
        yield return new WaitForSeconds(delay);
        if(factory is FactoryKinght)
        {
            ((FactoryKinght)factory).ReturnProduct(product);
        }
    }
    Vector3 GetTerrainHitPoint(Vector3 viewportPoint)
    {
        Camera mainCamera = Camera.main;
        Ray ray = mainCamera.ViewportPointToRay(viewportPoint);
        RaycastHit[] hits = Physics.RaycastAll(ray, 1000);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Terrain")
            {
                return hit.point;
            }
        }
        return Vector3.zero;
    }
}
