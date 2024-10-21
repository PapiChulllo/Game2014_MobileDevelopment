using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerBulletPrefab;
    [SerializeField]
    private GameObject enemyBulletPrefab;

    private List<GameObject> bullets;

    private void Start()
    {
        bullets = new List<GameObject>();
    }

    public GameObject GetBullet(bool isPlayerBullet)
    {
        GameObject bulletPrefab = isPlayerBullet ? playerBulletPrefab : enemyBulletPrefab;

        GameObject bullet = Instantiate(bulletPrefab);
        bullets.Add(bullet);
        return bullet;
    }
}
