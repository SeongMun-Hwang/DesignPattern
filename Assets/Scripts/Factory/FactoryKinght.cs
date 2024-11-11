using UnityEngine;
using UnityEngine.Pool;

public class FactoryKinght : UnitFactory
{
    public GameObject productPrefab;

    //public override IUnitProduct GetProduct(Vector3 position)
    //{
    //    GameObject gameObject=Instantiate(productPrefab,position,Quaternion.identity);
    //    ProductKnight newProduct = gameObject.GetComponent<ProductKnight>();
    //    newProduct.Initialize();

    //    return newProduct;
    //}

    ObjectPool<ProductKnight> pool;

    private void Awake()
    {
        pool = new ObjectPool<ProductKnight>(
            createFunc: CreateNewProduct,
            actionOnGet: product => product.gameObject.SetActive(true),
            actionOnRelease: product => product.gameObject.SetActive(false),
            actionOnDestroy: product => Destroy(product.gameObject),
            collectionCheck: false,
            defaultCapacity: 10
            );
    }
    public override IUnitProduct GetProduct(Vector3 position)
    {
        ProductKnight product = pool.Get();
        product.transform.position = position;
        product.Initialize();
        return product;
    }
    public override void ReturnProduct(IUnitProduct product)
    {
        if(product is ProductKnight)
        {
            pool.Release(product as ProductKnight);
        }
    }
    ProductKnight CreateNewProduct()
    {
        GameObject go=Instantiate( productPrefab );
        return go.GetComponent<ProductKnight>();
    }
}
