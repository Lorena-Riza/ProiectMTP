using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject arrowPrefab;
    public Transform projectileSpawnPoint;
    public float timeBetweenShots;
    private float shotTimer;

    private int copperBulletsCount;

    private Item.ItemType selectedProjectileType;
    private Inventory inventory;

    public Animator animator;

    private void Start()
    {
        InitializeShooting();
    }

    private void OnDestroy()
    {
        UnsubscribeFromInventoryEvents();
    }

    private void InitializeShooting()
    {
        shotTimer = 0;
        selectedProjectileType = Item.ItemType.Crossbow;
        inventory = FindObjectOfType<UI_Inventory>()?.GetInventory();

        if (inventory == null)
        {
            Debug.LogError("Inventory not found!");
            return;
        }

        copperBulletsCount = inventory.GetItemAmount(Item.ItemType.CopperBullets);
        SubscribeToInventoryEvents();

        animator = GetComponentInParent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found in parent!");
        }
    }

    private void SubscribeToInventoryEvents()
    {
        if (inventory != null)
        {
            inventory.OnItemAdded += Inventory_OnItemAdded;
            inventory.OnItemListChanged += Inventory_OnItemListChanged;
        }
        else
        {
            Debug.LogError("Inventory is null.");
        }
    }

    private void UnsubscribeFromInventoryEvents()
    {
        if (inventory != null)
        {
            inventory.OnItemAdded -= Inventory_OnItemAdded; // Unsubscribe to avoid memory leaks
            inventory.OnItemListChanged -= Inventory_OnItemListChanged;
            Debug.Log("Unsubscribed from inventory events.");
        }
    }

    private void Inventory_OnItemAdded(Item.ItemType itemType, int amount)
    {
        if (itemType == Item.ItemType.CopperBullets)
        {
            copperBulletsCount = inventory.GetItemAmount(Item.ItemType.CopperBullets);
        }
    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
        copperBulletsCount = inventory.GetItemAmount(Item.ItemType.CopperBullets);
        Debug.Log("Inventory item list changed. Copper bullets count: " + copperBulletsCount);
    }

    void Update()
    {
        if (PauseMenu.isPaused) return;

        shotTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && shotTimer >= timeBetweenShots)
        {
            ShootSelectedProjectile();
            shotTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSelectedProjectileType(Item.ItemType.Crossbow);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSelectedProjectileType(Item.ItemType.CopperGun);
        }
    }

    public void SetSelectedProjectileType(Item.ItemType itemType)
    {
        if (itemType == Item.ItemType.Crossbow || itemType == Item.ItemType.CopperGun)
        {
            selectedProjectileType = itemType;
            Debug.Log("Selected projectile type: " + selectedProjectileType);
        }
    }

    void ShootSelectedProjectile()
    {
        GameObject projectilePrefab = null;

        switch (selectedProjectileType)
        {
            case Item.ItemType.Crossbow:
                projectilePrefab = arrowPrefab;
                animator.SetTrigger("IsArrow");
                break;
            case Item.ItemType.CopperGun:
                if (copperBulletsCount > 0)
                {
                    projectilePrefab = bulletPrefab;
                    copperBulletsCount--;
                    inventory.RemoveItem(new Item(Item.ItemType.CopperBullets, 1));
                    animator.SetTrigger("IsBullet");
                }
                else
                {
                    Debug.Log("No copper bullets left!");
                    return;
                }
                break;
            default:
                return;
        }

        if (projectilePrefab != null)
        {
            ShootProjectile(projectilePrefab);
        }
    }

    void ShootProjectile(GameObject projectilePrefab)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;

        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        newProjectile.layer = LayerMask.NameToLayer("Projectile");

        // Set up the projectile's direction and force
        ProjectileScript projectileScript = newProjectile.GetComponent<ProjectileScript>();
        if (projectileScript != null)
        {
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileScript.force;
            }

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            newProjectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}