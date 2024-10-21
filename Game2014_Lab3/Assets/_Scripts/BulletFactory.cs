using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BulletFactory
{
    public static GameObject CreateBullet(GameObject bulletPrefab)
    {
        return Object.Instantiate(bulletPrefab);
    }
}
