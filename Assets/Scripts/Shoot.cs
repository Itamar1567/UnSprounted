using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{

    [SerializeField] private float distanceFromPlayer = 0.5f;
    [SerializeField] private Transform shootPointRotator;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Camera mainCam;
    [SerializeField] private InputActionReference shootActionRef;
    private GameObject shooterRef;
    private Vector2 mousePos;

    private Dictionary<int, GameObject> shooterDictionary = new Dictionary<int, GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CacheWeaponsIntoDictionary();
    }

    // Update is called once per frame
    void Update()
    {
        RotateShootPointRotator();
        if (UIControl.Singleton.SelectedItemInHotbarSlot() != null)
        {
            if (shootActionRef.action.WasPressedThisFrame() && UIControl.Singleton.SelectedItemInHotbarSlot().IsShooter())
            {
                Shooter shooter = UIControl.Singleton.SelectedItemInHotbarSlot().GetItemGameObject().GetComponent<Shooter>();

                if (shooter != null && shooterDictionary.TryGetValue(shooter.GetId(), out GameObject heldShooter))
                {
                    shooterRef = heldShooter;
                }
                if (shooterRef.GetComponent<Shooter>())
                {
                    if (mousePos != null)
                    {
                        shooterRef.SetActive(true);
                        shooterRef.GetComponent<Bow>().Shoot(mousePos, gameObject);
                    }
                }
            }
        }
    }

    private void RotateShootPointRotator()
    {
        mousePos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        float angleBetween = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shootPointRotator.rotation = Quaternion.Euler(new Vector3(0, 0, angleBetween));
    }

    private void CacheWeaponsIntoDictionary()
    {
        shooterDictionary.Clear();
        for (int i = 0; i < shootPoint.childCount; i++)
        {
            Shooter shooterObjectRef = shootPoint.GetChild(i).GetComponent<Shooter>();
            if (shooterObjectRef != null)
            {
                int id = shooterObjectRef.GetId();
                //Does not contain the shooter already, then add it
                if (shooterDictionary.ContainsKey(id) == false)
                {
                    shooterDictionary.Add(id, shooterObjectRef.gameObject);
                }
            }
        }
    }
}
