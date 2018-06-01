using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.Cameras;

public class InventorySystem : MonoBehaviour
{
    public FreeLookCam camara;//Para activar y desactivar la rotación de la camara.
    public int slotsX, slotsY;
    public GameObject panelItems;
    public List<Image> objetos;
    public List<Text> numeroObjetos;
    public List<Item> inventory = new List<Item>();
    public List<Item> slots = new List<Item>();
    public Image flechaImg;
    public Text numeroFlechasText;
    public Image HierbaCurativaImg;
    public Text HierbaCurativaNumeroText;
    private ItemDatabase database;
    private bool showInventory;

    private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter playerController;
    // Use this for initialization
    void Start () {
        panelItems.SetActive(false);
        for (int i = 0; i<(slotsX * slotsY);i++) {
            slots.Add(new Item());
        }
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();

        inventory.Add(database.items[22]);//Flechas
        database.items[22].addItem1();
        database.items[22].addItem1();
        database.items[22].addItem1();
        database.items[22].addItem1();
        inventory.Add(database.items[7]);//Mineral
        database.items[7].addItem1();
        database.items[7].addItem1();
        database.items[7].addItem1();
        database.items[7].addItem1();
        inventory.Add(database.items[3]);//Madera
        database.items[3].addItem1();
        database.items[3].addItem1();
        database.items[3].addItem1();
        database.items[3].addItem1();
        inventory.Add(database.items[21]);//Plumas
        database.items[21].addItem1();
        database.items[21].addItem1();
        database.items[21].addItem1();
        database.items[21].addItem1();
        database.items[21].addItem1();
        database.items[21].addItem1();
        database.items[21].addItem1();
        database.items[21].addItem1();
        database.items[21].addItem1();
        database.items[21].addItem1();
        database.items[21].addItem1();
        database.items[21].addItem1();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
    }

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			showInventory = !showInventory;
			panelItems.SetActive (showInventory);
            MostrarInfoItems.inventoryShow = inventory;
            paintItemsMenu();
            if (showInventory)
            {
                playerController.stopPlayer();
                playerController.enMenu = true;
                camara.m_LockCursor = false;
                camara.enabled = false;
            }
            else {
                playerController.continueMovement();
                playerController.enMenu = false;
                //playerController.continueMovement();
                camara.m_LockCursor = true;
                camara.enabled = true;
            }
            
        }

        if (playerController.disparado && playerController.hayFlechas) {
            Debug.Log(playerController.disparado);
            playerController.disparado = false;
            deleteItems("Flecha");
            updateImageArrow();
        }

        if (inventory.Contains(database.items[22]))//Si esta en el inventario quiere decir que hay flechas 
        {
            playerController.hayFlechas = true;
            updateImageArrow();

        }
        else
        {
            playerController.hayFlechas = false;
            numeroFlechasText.text = "0";
        }

    }

    public void CraftArrows() {
        if (inventory.Contains(database.items[7]) && inventory.Contains(database.items[3]) && inventory.Contains(database.items[21])) {
            AñadirItem(database.items[22].itemID);
            deleteItems(database.items[7].itemName);
            deleteItems(database.items[3].itemName);
            deleteItems(database.items[21].itemName);
        }
    }

    public void AñadirItem(int itemId) {
        if (!inventory.Contains(database.items[itemId]))
        {
            inventory.Add(database.items[itemId]);
        }
        database.items[itemId].addItem1();
        paintItemsMenu();
    }

    public void deleteItems(string name) {
        for (int x = 0; x < inventory.Count; x++)
        {
            if (name  == inventory[x].itemName)
            {
                //Debug.Log("eliminar inventory:   " + inventory[x].itemName);
                inventory[x].substractItem1();
                if (inventory[x].itemsNumero == 0)
                {
                     //Debug.Log("TodoRoto    " + inventory[x].itemID);
                    updateDeletedItems(inventory[x].itemName);
                    inventory.Remove(inventory[x]);
                    
                }
                else {
                    updateDeletedItems(inventory[x].itemName);
                    paintItemsMenu();
                    
                }
            }
            /*Debug.Log("Objetos en el inventario de verdad");
            for (int z = 0; z < inventory.Count; z++)
            {
                Debug.Log(inventory.Count);
                Debug.Log(inventory[z].itemName);
            }*/
            
        }
     }

    private void updateDeletedItems(string nombre) {
        for (int x = 0; x < numeroObjetos.Count; x++)
        {
            //Debug.Log(nombre + ",  " + numeroObjetos[x].text);
            if (nombre + "Item" == numeroObjetos[x].gameObject.name) {
                numeroObjetos[x].text = "";
                numeroObjetos[x].gameObject.name = "TextNumObjetos";
                objetos[x].sprite = null;
            } 
            
           
        }
    }

    private void updateImageArrow()
    {
        flechaImg.sprite = inventory[inventory.IndexOf(database.items[22])].itemIcon;
        numeroFlechasText.text = "" + inventory[inventory.IndexOf(database.items[22])].itemsNumero;
    }

    private void paintItemsMenu(){
        int count = 0;

        for (int x = 0; x < inventory.Count; x++) {
            
            numeroObjetos[x].text = "" + inventory[x].itemsNumero;
            numeroObjetos[x].gameObject.name = inventory[x].itemName + "Item";
            objetos[x].sprite = inventory[x].itemIcon;

            if (inventory[x].itemID == 0) {
                HierbaCurativaImg.sprite = inventory[inventory.IndexOf(database.items[0])].itemIcon;
                HierbaCurativaNumeroText.text = "" + inventory[inventory.IndexOf(database.items[0])].itemsNumero;
            }
           
        }

        for (int x = 0; x < numeroObjetos.Count; x++) {
            if (numeroObjetos[x].gameObject.name != "TextNumObjetos")
            {
                count += 1;
            }
        } 
        Debug.Log(count + ",                      " + inventory.Count);
        if (inventory.Count < count) {//Eliminar el pintado sobrante si es que lo hay
            for (int x = inventory.Count; x < numeroObjetos.Count; x++)
            {
                numeroObjetos[x].text = "";
                numeroObjetos[x].gameObject.name = "TextNumObjetos";
                objetos[x].sprite = null;
            }
        }
	}
}
