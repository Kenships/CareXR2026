using UnityEngine;
using System.Collections.Generic;
public class Visualization : MonoBehaviour
{
    [SerializeField] private PositionSave positionSave;
    private Transform parentPos;
    [SerializeField] private GameObject prefab;
    private bool genGraph = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (genGraph) return;
        if (positionSave.records.Count >= 10)   // Make sure the graph only gets generated after the movements are done
        {
            ShowGraph();
            genGraph = true;
        }
    }

    public void ShowGraph()
    {
        List<(string name, Vector3 position)> records = positionSave.records;
        for (int i = 0; i < records.Count; i++)
        {
            Vector3 pos = records[i].position;

            if (parentPos != null)
            {
                Instantiate(prefab, pos, Quaternion.identity, parentPos);
            }
            else
            {
                Instantiate(prefab, pos, Quaternion.identity);
            }
        }
    }
}
