using UnityEngine;
using Pathfinding;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;

public class EnemyTest
{
    [Test]
    public void TakeDamageTest()
    {
        GameObject gameObject = new GameObject();
        var enemy = gameObject.AddComponent<Enemy>();
        enemy.SetHealth(100);
        enemy.TakeDamage(50);
        Assert.AreEqual(expected: 50, actual: enemy.GetHealth());
    }
    [UnityTest]
    public IEnumerator DieTest()
    {
        GameObject gameObject = new GameObject();
        var enemy = gameObject.AddComponent<Enemy>();
        enemy.SetHealth(100);
        enemy.TakeDamage(150);
        yield return null;
        Assert.IsTrue(enemy == null);
    }

    
}
