using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private int maxBullets = 10; // Adjust based on game needs
    private List<GameObject> bullets;

    [SerializeField]
    private GameObject playerBulletPrefab;
    [SerializeField]
    private GameObject enemyBulletPrefab;

    private void Start()
    {
        bullets = new List<GameObject>(maxBullets);
    }

    public GameObject GetBullet(bool isPlayerBullet)
    {
        foreach (var bullet in bullets)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }
        GameObject newBullet = BulletFactory.CreateBullet(isPlayerBullet ? playerBulletPrefab : enemyBulletPrefab);
        bullets.Add(newBullet);
        return newBullet;
    }
}
