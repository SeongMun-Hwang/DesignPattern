using UnityEngine;

public class ProductKnight : MonoBehaviour, IUnitProduct
{
    string unitName = "Normal Knight";
    public string UnitName
    {
        get { return unitName; }
        set { unitName = value; }
    }
    public AudioClip battleCry;
    public void Initialize()
    {
        gameObject.name = unitName;
        gameObject.transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0));

        GetComponent<AudioSource>().PlayOneShot(battleCry);
    }
}
