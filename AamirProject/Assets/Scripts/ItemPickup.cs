using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour {

    public ItemType itemType = ItemType.coin;
    public enum ItemType { coin, sword };

    private void OnTriggerStay(Collider target)
    {
        if(target.tag == "Player")
        {
            Inventory inventory = target.GetComponent<Inventory>();            

            if (Input.GetKeyDown(KeyCode.F))
            {                
                if (itemType == ItemType.coin)
                {
                    inventory.coins++;          
                }
                else if(itemType == ItemType.sword)
                {
                    inventory.swordObject.SetActive(true);
                }

                Destroy(transform.gameObject);
            }
        }
    }

}