using UnityEngine;

public class PoopPlacer : MonoBehaviour
{
    public GameObject PoopPrefab;
    public void Place(Vector2 corgiPosition)
    {
        //instantiate (what making, where making, how rotate; only prefab)
        Instantiate(PoopPrefab, corgiPosition, Quaternion.identity);
    }
}
