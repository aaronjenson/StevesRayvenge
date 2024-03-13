using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

public class SteveScript : MonoBehaviour
{
    public AudioSource thatsNotGoodAudio;
    public AudioSource IhopeYouEnjoyedAudio;
    public AudioSource thatsPrettyCoolAudio;
    private int HP = 5;
    public TMP_Text HPText;
    private int score = 0;
    private int hitScoreBonus = 200;
    private float defeatedRotationTimer = 0;
    public TMP_Text ScoreText;
    private HashSet<GameObject> hitRays;
    private Vector3 startLocation;
    private int randomWalkRange = 15;
    private float timeUntilNewLocation;
    private float speed = 2f;
    private bool vulnerable;
    private float vulnerabilityTimer;
    private float invulerabilityTime = 999999;
    public Material vulnerableMaterial;
    public Material invulnerableMaterial;
    private Rigidbody rb;
    public GameObject torso;
    public GameObject head;
    public GameObject leftArm;
    public GameObject rightArm;
    public GameObject leftLeg;
    public GameObject rightLeg;
    private List<GameObject> bodyParts;
    private bool defeated;
    public Button startButton;
    private float gameStartedTimer = 21;
    private bool buttonClicked;
    private bool gameStarted;
    private float attackCooldown = 0.5f;
    private float attackTimer;
    public GameObject attackPrefab;
    private int winScoreBonus = 1000;

    // Start is called before the first frame update
    void Start()
    {
        thatsPrettyCoolAudio.Play();
        attackTimer = 0;
        gameStarted = false;
        buttonClicked = false;
        startButton.onClick.AddListener(ButtonClicked);
        defeated = false;
        vulnerable = false;
        HPText.SetText("HP: " + HP);
        ScoreText.SetText("Score: " + score);
        hitRays = new HashSet<GameObject>();
        startLocation = gameObject.transform.position + Vector3.zero;
        rb = GetComponent<Rigidbody>();
        bodyParts = new List<GameObject>
        {
            head,
            torso,
            leftArm,
            rightArm,
            leftLeg,
            rightLeg
        };
        ScoreText.alpha = 0;
        HPText.alpha = 0;
        SetMaterial(invulnerableMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && buttonClicked) {
            gameStartedTimer -= Time.deltaTime;
            if (gameStartedTimer < 0) {
                gameStarted = true;
                vulnerable = true;
                SetMaterial(vulnerableMaterial);
                GetNewLocation();
                ScoreText.alpha = 1;
                HPText.alpha = 1;
            }
        }
        if (gameStarted) {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackCooldown) {
                SpawnAttack();
                attackTimer = 0;
            }
            timeUntilNewLocation -= Time.deltaTime;
            if (timeUntilNewLocation < 0) {
                GetNewLocation();
            }
            vulnerabilityTimer -= Time.deltaTime;
            if (vulnerabilityTimer < 0) {
                vulnerable = true;
                SetMaterial(vulnerableMaterial);
            }
            if (defeated) {
                defeatedRotationTimer += Time.deltaTime;
                if (defeatedRotationTimer > Math.PI/2) {
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }
    }

    void SpawnAttack() {
        Instantiate(attackPrefab, GenerateLocationInRange(), Quaternion.identity);
    }

    void GetNewLocation() {
        Vector3 newLocation = GenerateLocationInRange();
        float distance = (newLocation - gameObject.transform.position).magnitude;
        timeUntilNewLocation = distance/speed;
        rb.velocity = (newLocation - gameObject.transform.position).normalized * speed;
    }

    Vector3 GenerateLocationInRange() {
        return startLocation +
            new Vector3(UnityEngine.Random.Range(-randomWalkRange,randomWalkRange),
            0, UnityEngine.Random.Range(-randomWalkRange,randomWalkRange));
    }

    void OnTriggerStay(Collider other)
    {
        if (vulnerable && other.gameObject.tag == "Ray" && !hitRays.Contains(other.gameObject)) {
            GameObject ray = other.gameObject;
            hitRays.Add(ray);
            HP -= 1;
            HPText.SetText("HP: " + HP);
            vulnerable = false;
            vulnerabilityTimer = invulerabilityTime;
            SetMaterial(invulnerableMaterial);
            addScore(hitScoreBonus);
            if (HP > 0) {
                thatsNotGoodAudio.Play();
            }
            if (HP <= 0) {
                BossDefeated();
            }
        }
    }

    void BossDefeated() {
        defeated = true;
        IhopeYouEnjoyedAudio.Play();
        vulnerabilityTimer = 999999;
        addScore(winScoreBonus);
        timeUntilNewLocation = 999999;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = gameObject.transform.forward;
    }

    void SetMaterial(Material material) {
        foreach (GameObject part in bodyParts) {
            part.GetComponent<MeshRenderer>().material = material;
        }
    }

    void ButtonClicked() {
        buttonClicked = true;
    }

    public void addScore(int scoreToAdd) {
        score += scoreToAdd;
        ScoreText.SetText("Score: " + score);
    }

    public void BecomeVulnerable() {
        vulnerable = true;
        SetMaterial(vulnerableMaterial);
    }

    public bool GetVulnerability() {
        return vulnerable;
    }

    public void PlayThatsPrettyCool() {
        thatsPrettyCoolAudio.Play();
    }
}