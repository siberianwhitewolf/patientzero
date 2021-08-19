using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator animator;
    public Transform firepointUp;
    public Transform firepointDown;
    public Transform firepointRight;
    public Transform firepointLeft;
    public GameObject bulletDownPrefab;
    public GameObject bulletRightPrefab;
    public GameObject bulletLeftPrefab;
    public GameObject bulletUpPrefab;
    public WeaponItem weaponEquipped;
    public float horizontalPosition;
    public float verticalPosition;
    public int magazineSize;
    public int currentBulletsInMagazine;
    public bool isEquiped = false;

    private void Awake()
    {
        if (weaponEquipped)
        {
            this.currentBulletsInMagazine = weaponEquipped.currentBulletsInMagazine;
            this.magazineSize = weaponEquipped.maxBulletsInMagazine;
            this.weaponEquipped.firstTimeEquiped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel") && !(PauseMenu.GameIsPaused) && isEquiped)
        {
            if (currentBulletsInMagazine > 0)
            {
                Shoot();
                currentBulletsInMagazine--;
                weaponEquipped.currentBulletsInMagazine = this.currentBulletsInMagazine;
            }

            else
            {
                Debug.Log("You don't have any more bullets!");
            }
            
        }
    }


    void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("gunshot");
        float horizontalPosition = animator.GetFloat("Horizontal");
        float verticalPosition = animator.GetFloat("Vertical");


        switch (horizontalPosition) {

            case 1:
                Instantiate(bulletRightPrefab, firepointRight.position, firepointRight.rotation);
                break;

            case -1:
                Instantiate(bulletLeftPrefab, firepointLeft.position, firepointLeft.rotation);
                break;
        }


        switch (verticalPosition)
        {

            case 1:
                Instantiate(bulletUpPrefab, firepointUp.position, firepointUp.rotation);
                break;

            case -1:
                Instantiate(bulletDownPrefab, firepointDown.position, firepointDown.rotation);
                break;
        }

        animator.SetTrigger("Shooting");

    }

    public int Reload(int reloadAmount)
    {
        int aux = weaponEquipped.currentBulletsInMagazine + reloadAmount;
        if(aux > magazineSize)
        {
            Debug.Log("AUX > MAGAZINESIZE");
            Debug.Log("AUX ES "+aux);
            Debug.Log("MAGAZINESIZE ES"+magazineSize);
            int aux2 = aux - magazineSize;
            Debug.Log("AUX 2 ES " + aux2);
            Debug.Log("Reload amount ES " + reloadAmount);
            weaponEquipped.currentBulletsInMagazine += reloadAmount - aux2;
            this.currentBulletsInMagazine = weaponEquipped.currentBulletsInMagazine;
            reloadAmount -= reloadAmount - aux2;
            FindObjectOfType<WeaponUI>().WeaponSlotText.text = weaponEquipped.currentBulletsInMagazine.ToString();
            
            return reloadAmount;
        }
        else
        {
            weaponEquipped.currentBulletsInMagazine += reloadAmount;
            this.currentBulletsInMagazine = weaponEquipped.currentBulletsInMagazine;
            FindObjectOfType<WeaponUI>().WeaponSlotText.text = weaponEquipped.currentBulletsInMagazine.ToString();
            reloadAmount = 0;
            return reloadAmount;

        }
        
        
    }

    private void OnApplicationQuit()
    {
        if (weaponEquipped)
        {
            weaponEquipped.firstTimeEquiped = true;
            weaponEquipped.gunIsEquipped = false;
            weaponEquipped.currentBulletsInMagazine = 15;
            weaponEquipped.maxBulletsInMagazine = 15;
        }
    }
}
