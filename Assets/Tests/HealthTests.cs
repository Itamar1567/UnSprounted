using UnityEngine;
using NUnit.Framework;
public class HealthTests
{
    [Test]
    public void TakeDamageTest()
    {
        var gameobject = new GameObject();
        var health = gameobject.AddComponent<Health>();
        health.SetHealth(100);
        health.TakeDamage(50);
        Assert.AreEqual(expected: 50, actual: health.GetHealth());
    }
    [Test]
    public void IncreaseHealthTest()
    {
        var gameobject = new GameObject();
        var health = gameobject.AddComponent<Health>();
        health.SetMaxHealth(100);
        health.SetHealth(health.GetMaxHealth());
        health.TakeDamage(50);
        health.IncreaseHealth(51);
        Assert.AreEqual(expected: health.GetMaxHealth(), actual: health.GetHealth());
    }
}
