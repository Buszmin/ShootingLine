using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject gunSlide;
    [SerializeField] GameObject shellPrefab;
    [SerializeField] Transform shellPos;
    [SerializeField] Transform barrel;
    [SerializeField] GameObject trail;
    [SerializeField] GameObject expTrail;
    [SerializeField] AudioSource audioSource;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] Pooling poolingExpBullet;
    [SerializeField] Pooling poolingBullet;
    [SerializeField] Pooling poolingShell;

    public static bool isExpBulletActive;
    public static ExpBullet ActaiveExpBullet;

    // [Header("Settings")]
    // [Range(1f, 1000f)][SerializeField] float range = 100f;
    // [Range(1f, 30f)][SerializeField] float shellDropForce = 100f;



    Vector3 gunSlideOffSet = new Vector3(0, 0, -20);
    bool shotingBlocked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) )&& shotingBlocked == false)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Y) && shotingBlocked == false)
        {
            if (!isExpBulletActive)
            {
                if (PlayerManager.Instance.expShotUnlocked)
                {
                    ShootExp();
                    PlayerManager.Instance.expShotUnlocked = false;
                }
            }
            else
            {
                ActaiveExpBullet.Die();
                isExpBulletActive = false;
                ActaiveExpBullet = null;
            }
        }

        if (PlayerManager.Instance.expShotUnlocked == false && isExpBulletActive == false)
        {
            PlayerManager.Instance.UpdateShotTimer();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }


    void Shoot()
    {
        audioSource.PlayOneShot(audioSource.clip);
        muzzleFlash.Play();
        shotingBlocked = true;
        StartCoroutine(ShotCoroutine());

        Vector3 mousePos = Input.mousePosition;

        GameObject b = poolingBullet.activateNext();
        b.transform.position = barrel.transform.position;
        b.transform.rotation = Quaternion.Euler(transform.forward);

        GameObject t = Instantiate(trail);
        t.transform.position = barrel.transform.position;
        t.transform.rotation = Quaternion.Euler(transform.forward);

        Bullet bullet = b.GetComponent<Bullet>();
        bullet.trail = t;
    }

    void ShootExp()
    {
        audioSource.PlayOneShot(audioSource.clip);
        muzzleFlash.Play();
        shotingBlocked = true;
        StartCoroutine(ShotCoroutine());

        Vector3 mousePos = Input.mousePosition;

        GameObject b = poolingExpBullet.activateNext();
        b.transform.position = barrel.transform.position;
        b.transform.rotation = Quaternion.Euler(transform.forward);

        GameObject ActaiveExpBulletChild = b.transform.GetChild(0).gameObject;
        ActaiveExpBulletChild.SetActive(true);
        ActaiveExpBullet = ActaiveExpBulletChild.GetComponent<ExpBullet>();
        isExpBulletActive = true;

        GameObject t = Instantiate(expTrail);
        t.transform.position = barrel.transform.position;
        t.transform.rotation = Quaternion.Euler(transform.forward);

        Bullet bullet = ActaiveExpBullet.GetComponent<Bullet>();
        bullet.trail = t;
    }

    public IEnumerator ShotCoroutine()
    {
        float time = 0;

        while (time < 1f)
        {
            time += Time.deltaTime * 20;
            gunSlide.transform.localPosition = Vector3.Lerp(Vector3.zero, gunSlideOffSet, time);
            yield return null;
        }

        time = 0;

        GameObject newShell = poolingShell.activateNext();
        newShell.transform.position = shellPos.position;
        newShell.transform.rotation = shellPos.rotation;

        Rigidbody newShellRB = newShell.GetComponent<Rigidbody>();
        float shellDropForce = Random.Range(2f, 5f);
        newShellRB.AddForce((transform.up + transform.rotation * new Vector3 (Random.Range(0.1f, 1.2f), 0,0)) * shellDropForce, ForceMode.Impulse);
        newShellRB.AddTorque(new Vector3(Random.Range(0.1f, 10f), Random.Range(0.1f, 10f), Random.Range(0.1f, 10f)), ForceMode.Impulse);
        while (time < 1f)
        {
            time += Time.deltaTime * 2.22f;
            gunSlide.transform.localPosition = Vector3.Lerp(gunSlideOffSet, Vector3.zero, time);
            yield return null;
        }

        shotingBlocked = false;
        gunSlide.transform.localPosition = Vector3.zero;
    }
}
