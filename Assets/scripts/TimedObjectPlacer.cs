using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TimedObjectPlacer : MonoBehaviour
{
    public GameObject Prefab;
    public float minimumSecondsToWait;
    public float maximumSecondsToWait;
    private bool isOkToCreate = true;
    private bool isActive = false;
    private Coroutine countdownCoroutine;
    
    void Update()
    {
        if (!isActive)
            return;
        if (isOkToCreate)
        {
            countdownCoroutine = StartCoroutine(CountDownUntilCreation());
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

    public void StartPlacing()
    {
        isActive = true;
        isOkToCreate = true;
        if (countdownCoroutine != null)
            StopCoroutine(countdownCoroutine);
    }

    public void StopPlacing()
    {
        isActive = false;
        isOkToCreate = false;
        CleanupPlacedObjects();
    }

    private void CleanupPlacedObjects()
    {
            List<GameObject> placedObjects = GameObject.FindGameObjectsWithTag(Prefab.tag).ToList();
            for (int i = 0; i < placedObjects.Count; i++)
            {
                Destroy(placedObjects[i]);
            }
    }
}
