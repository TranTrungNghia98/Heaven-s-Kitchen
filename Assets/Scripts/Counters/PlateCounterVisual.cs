using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] PlateCounter plateCounter;
    [SerializeField] Transform plateVisualPrefab;
    [SerializeField] Transform topCounter;

    private List<GameObject> plateVisualGameObjectList;
    // Start is called before the first frame update
    private void Start()
    {
        plateVisualGameObjectList = new List<GameObject>();

        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateVisualGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateVisualGameObject);
        Destroy(plateVisualGameObject);
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
       Transform plateVisualTransform =  Instantiate(plateVisualPrefab, topCounter);
        float offset = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateVisualGameObjectList.Count * offset, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
