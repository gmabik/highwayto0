using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public static GenerateLevel Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject[] sections;
    [SerializeField] private GameObject[] spawnedSections;
    [SerializeField] private GameObject StartSection;
    [SerializeField] private int zPos = 40;

    private void Start()
    {
        spawnedSections = new GameObject[5];
        spawnedSections[spawnedSections.Length - 1] = StartSection;
    }

    public void GenerateSection()
    {
        GameObject newSection = Instantiate(sections[Random.Range(0, sections.Length)], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 40;
        try { Destroy(spawnedSections[0]); }
        catch { }
        Functions.RelocateObjectsInArray<GameObject>(spawnedSections, newSection);
    }
}
