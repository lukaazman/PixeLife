using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    public Animator anim2;
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            weaponWheelSelected = !weaponWheelSelected;
        }

        if(weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }

        switch(weaponID)
        {
            case 0: //nothing selected
                selectedItem.sprite = noImage;
                break;
            case 1: //Desert Eagle
                Debug.Log("Desert Eagle");
                anim2.SetBool("HasGun", true);
                break;
            case 2: //Beer
                Debug.Log("Beer");
                break;
            case 3: //Vape
                Debug.Log("Vape");
                break;
            case 4: //Baguette
                Debug.Log("Baguette");
                break;
        }
    }
}
