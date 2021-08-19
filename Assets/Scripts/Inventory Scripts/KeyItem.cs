using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Inventory/Key")]
public class KeyItem : Item
{
        private void Awake()
        {
            type = ItemType.Key;
        }

        public override void Use()
        {
        /*
            switch (this.name)
            {
            Aca codigo para usar la llave
            }
        */
        }
}
