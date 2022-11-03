using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Cannon")]
    public GameObject cannonPivot;
    public GameObject cannnonBarrel;
    public GameObject cannonFiringPoint;
    public float speed = 1.0f;
    public float minX = -45.0f;
    public float maxX = 30.0f;
    Vector3 euler;

    [Header("Projectile")]
    public Rigidbody projectilePrefab;
    public float projectileSpeed = 1000f;

    [Header("UI")]
    public GameObject inGamePanel;
    public GameObject winPanel;
    public Text timeText;
    public Text targetsText;
    public Text winTimeText;
    float timer = 0;
    int targetsLeft;


    void Start()
    {
        inGamePanel.SetActive(true);
        winPanel.SetActive(false);
        targetsLeft = FindObjectsOfType<Target>().Length;
        targetsText.text = "Targets: ";

    }
    public void UpdateTargets()
    {
        targetsLeft -= 1;
        targetsText.text = "Targets: " + targetsLeft;

        if (targetsLeft == 0)
        {
            inGamePanel.SetActive(false);
            winPanel.SetActive(true);
            winTimeText.text = "Time: " + timer.ToString("F2");
        }
    }


    void Update()
    {
        timer += Time.deltaTime;
        timeText.text = "Time: " + timer.ToString("F1");

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        cannonPivot.transform.Rotate(0, h * speed, 0);

        euler.x += v * speed;
        euler.x = Mathf.Clamp(euler.x, minX, maxX);
        cannnonBarrel.transform.localEulerAngles = euler;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody rb = Instantiate(projectilePrefab, cannonFiringPoint.transform.position, cannonFiringPoint.transform.rotation);
            rb.AddForce(cannonFiringPoint.transform.forward * projectileSpeed);
            Destroy(rb.gameObject, 5);
        }
    }
}

