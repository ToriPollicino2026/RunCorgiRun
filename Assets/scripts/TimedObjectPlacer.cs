using UnityEngine;
using System.Collections;

public class TimedObjectPlacer : MonoBehaviour
{
    public GameObject Prefab;
    public float minimumSecondsToWait;
    public float maximumSecondsToWait;
    private bool isOkToCreate = true;
    void Update()
    {
        if (isOkToCreate)
        {
            StartCoroutine(CountDownUntilCreation());
        }
    }

    IEnumerator CountDownUntilCreation()
    {
        isOkToCreate = false;
        float secondsToWait = Random.Range(minimumSecondsToWait, maximumSecondsToWait);
        yield return new WaitForSeconds(secondsToWait);
        Place();
        isOkToCreate = true;
    }

    public virtual void Place()
    {
        Instantiate(Prefab, SpawnTools.RandomLocationWorldSpace(), Quaternion.identity);
    }
}
