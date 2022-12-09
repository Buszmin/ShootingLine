using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static List<EnemyController> enemyControllers = new List<EnemyController>();

    Material materialStart;
    [Header("Components")]
    [SerializeField] GameObject baseObj;
    [SerializeField] Pooling pooling;
    [SerializeField] Renderer rend;
    [SerializeField] Material materialDead;
    [SerializeField] GameObject healthBar;
    [SerializeField] ParticleSystem ps;
    [SerializeField] EnemyData enemyData;

    Vector3 targetVector;

    [Header("Do not change in inspector Debug Only")]
    public float hp;
    public float speedBonus = 1;



    public EnemyData Data => enemyData;

    private void OnEnable()
    {
        enemyControllers.Add(this);
    }

    private void OnDisable()
    {
        enemyControllers.Remove(this);
    }

    void Start()
    {
        hp = enemyData.StartHp * enemyData.HpMultiplayer;
        materialStart = rend.material;
    }

    private void Update()
    {
        targetVector = new Vector3(baseObj.transform.position.x, baseObj.transform.position.y, 0);
        var step = speedBonus * enemyData.BasicSpeed * enemyData.SpeedMultiplayer * Time.deltaTime;
        baseObj.transform.position = Vector3.MoveTowards(baseObj.transform.position, targetVector, step);

        if (transform.position == targetVector)
        {
            CrossTheLine();
        }
    }

    public virtual void GetDamage()
    {
        hp = hp - PlayerManager.Instance.currentDmg;
        UpdateHelathBar();

        if(hp<=0)
        {
            Die();
        }
    }

    public void SetHp(float newHp)
    {
        hp = newHp;
        UpdateHelathBar();
    }

    protected void UpdateHelathBar()
    {
        //Debug.Log(hp + " " + enemyData.StartHp * enemyData.HpMultiplayer + " " + hp /( enemyData.StartHp * enemyData.HpMultiplayer));
        healthBar.transform.localScale =new Vector3( hp / (enemyData.StartHp * enemyData.HpMultiplayer), healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        /*  przeciwnicy ciemniej¹ wraz z iloœci¹ hp
        if (hp <= 100)
        {
            rend.material.Lerp(materialStart, materialDead, (1 - hp * 0.01f) * 0.1f);
        }
        */
    }

    protected virtual void Die()
    {
        PlayerManager.Instance.AddScore(enemyData.ScoreGain);
        PlayerManager.Instance.AddXP(enemyData.XpGain);

        gameObject.SetActive(false);
        ps.gameObject.SetActive(true);
        ps.GetComponent<Renderer>().material = rend.material;
        ps.Play();
        pooling.StartCoroutine(pooling.ReturnToPoolWithDelay(baseObj, 3f));
    }

    public void Revive()
    {
        SetHp(enemyData.StartHp * enemyData.HpMultiplayer);
    }

    protected virtual void CrossTheLine()
    {
        PlayerManager.Instance.LoseHp();
        Die();
    }
}
