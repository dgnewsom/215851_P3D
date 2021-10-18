using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    [SerializeField] private GameObject mObject;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxObjects;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float timeBetweenSpawns = 1f;
    [SerializeField] [Range(0f,10f)] private float launchSpeed = 5f;

    private string scorePreText = "Game Score: ";
    private string timePreText = "Total Time: ";

    private List<GameObject> mObjects;
    private int numObjects;
    private float coolDownTimer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        mObjects = new List<GameObject>();
        scoreText.text = scorePreText;
        timeText.text = timePreText;
        coolDownTimer = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (numObjects < maxObjects)
        {
            if (coolDownTimer < 0f)
            {
                SpawnObject(numObjects);
                numObjects++;
                coolDownTimer = timeBetweenSpawns;
            }
            else
            {
                coolDownTimer -= Time.deltaTime;
            }
        }
        else
        {
            for (int i = 0; i < mObjects.Count; i++)
            {
                if (mObjects[i] == null)
                {
                    string gameTime = Time.realtimeSinceStartup.ToString("f2");
                    mObjects.RemoveAt(i);
                    int count = (maxObjects - mObjects.Count);
                    scoreText.text = scorePreText + count;
                    //Debug.Log("Objects left: " + Objects.Count);
                    if (mObjects.Count == 0)
                    {
                        //Debug.Log("In here? " + gameTime);
                        timeText.text = timePreText + gameTime;
                    }
                }
            }
        }

    }

    void SpawnObject(int num)
    {
        if (mObject)
        {
            GameObject mObjectClone = Instantiate(mObject, spawnPoint.position, Quaternion.identity) as GameObject;
            mObjectClone.SetActive(true);
            if (mObjectClone.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.AddForce(Random.Range(-50,50)*launchSpeed, 250f, Random.Range(-50, 50)*launchSpeed);
            }

            if (mObjectClone.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
            {
                renderer.material.color = new Color(Random.value, Random.value, Random.value);
            }
            mObjects.Add(mObjectClone);
            //Debug.Log("List size " + Objects.Count);
        }
    }
}