using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    int hp = 3;
    int totalScore = 0;
    int totalXp = 0;
    int currentLevel = 0;
    [SerializeField] List<GameObject> hearts;
    [SerializeField] TextMeshProUGUI playerLevelTmPro;
    [SerializeField] TextMeshProUGUI scoreTmPro;
    [SerializeField] TextMeshProUGUI explosvieShotTmPro;

    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject newHighScore;
    [SerializeField] TextMeshProUGUI finalScore;
    [SerializeField] TextMeshProUGUI highScore;

    private float expShotTimer;
    public bool expShotUnlocked;
    public float currentDmg;

    [System.Serializable]
    public struct Level
    {
        public int lvl;
        public float xpNeeded;
        public float damage;
        public float expShotTime;

        public Level(int lvl, float xpNeeded, float damageBonus, float expShotTime)
        {
            this.lvl = lvl;
            this.xpNeeded = xpNeeded;
            this.damage = damageBonus;
            this.expShotTime = expShotTime;
        }
    }

    [SerializeField] List<Level> levels;


    public static class HighScore
    {
        public static int score;
    }

    void Start()
    {
        Instance = this;
        expShotTimer = levels[0].expShotTime;
        currentDmg = levels[0].damage;
    }

    public void AddScore(int score)
    {
        totalScore += score;
        scoreTmPro.text = "Score: " + totalScore; 
    }

    public void AddXP(int xp)
    {
        totalXp += xp;
        if(currentLevel != levels.Count && levels[currentLevel+1].xpNeeded <= totalXp)
        {
            currentLevel++;
            playerLevelTmPro.text = "Player Level: " + currentLevel;
            currentDmg = levels[currentLevel].damage;
        }
    }

    public void LoseHp()
    {
        hp--;

        if (hp < 0)
        {
            Die();
        }
        else
        {
            hearts[hp].SetActive(false);
            Debug.Log("hp: " + hp);
        }
    }

    public void UpdateShotTimer()
    {
        explosvieShotTmPro.text = "Explosive shot: " + expShotTimer.ToString("F2");

        if (expShotTimer<=0)
        {
            expShotUnlocked = true;
            explosvieShotTmPro.text = "Explosive shot: READY";
        }
        else
        {
            expShotTimer -= Time.deltaTime;
        }
    }

    public void RestartShotTimer()
    {
        expShotTimer = levels[currentLevel].expShotTime;
    }

    private void Die()
    {
        Time.timeScale = 0;
        deathPanel.SetActive(true);

        highScore.text = "";
       
        Destroy(scoreTmPro);
        Destroy(playerLevelTmPro);
        Destroy(explosvieShotTmPro);

        finalScore.text = "Score: " + totalScore;

        if (HighScore.score != 0)
        {
            highScore.text = "High Score: " + HighScore.score;
        }

        if (HighScore.score < totalScore)
        {
            newHighScore.SetActive(true);
            HighScore.score = totalScore;
        }
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
