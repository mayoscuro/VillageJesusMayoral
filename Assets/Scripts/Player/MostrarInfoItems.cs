using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MostrarInfoItems : MonoBehaviour, IPointerDownHandler
{
    public static List<Item> inventoryShow = new List<Item>();
    public Text textoInfo;
    public Text textoName;
    public Text textoTipo;
    public Text textoTipo2;
    public Text textoReward;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        string nombre = eventData.pointerEnter.name;
        Debug.Log(nombre);
        if (eventData.pointerEnter.gameObject.GetComponent<Image>().sprite != null)
        {
            switch (nombre)
            {
                case "Icono1":
                    textoInfo.text = "Description: " + inventoryShow[0].itemDescription;
                    textoName.text = "Name: " + inventoryShow[0].itemName;
                    textoTipo.text = "Type: " + inventoryShow[0].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[0].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[0].itemReward.ToString();
                    break;
                case "Icono2":

                    textoInfo.text = "Description: " + inventoryShow[1].itemDescription;
                    textoName.text = "Name: " + inventoryShow[1].itemName;
                    textoTipo.text = "Type: " + inventoryShow[1].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[1].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[1].itemReward.ToString();
                    break;
                case "Icono3":

                    textoInfo.text = "Description: " + inventoryShow[2].itemDescription;
                    textoName.text = "Name: " + inventoryShow[2].itemName;
                    textoTipo.text = "Type: " + inventoryShow[2].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[2].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[2].itemReward.ToString();
                    break;
                case "Icono4":

                    textoInfo.text = "Description: " + inventoryShow[3].itemDescription;
                    textoName.text = "Name: " + inventoryShow[3].itemName;
                    textoTipo.text = "Type: " + inventoryShow[3].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[3].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[3].itemReward.ToString();
                    break;
                case "Icono5":

                    textoInfo.text = "Description: " + inventoryShow[4].itemDescription;
                    textoName.text = "Name: " + inventoryShow[4].itemName;
                    textoTipo.text = "Type: " + inventoryShow[4].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[4].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[4].itemReward.ToString();
                    break;
                case "Icono6":

                    textoInfo.text = "Description: " + inventoryShow[5].itemDescription;
                    textoName.text = "Name: " + inventoryShow[5].itemName;
                    textoTipo.text = "Type: " + inventoryShow[5].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[5].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[5].itemReward.ToString();
                    break;
                case "Icono7":

                    textoInfo.text = "Description: " + inventoryShow[6].itemDescription;
                    textoName.text = "Name: " + inventoryShow[6].itemName;
                    textoTipo.text = "Type: " + inventoryShow[6].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[6].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[6].itemReward.ToString();
                    break;
                case "Icono8":

                    textoInfo.text = "Description: " + inventoryShow[7].itemDescription;
                    textoName.text = "Name: " + inventoryShow[7].itemName;
                    textoTipo.text = "Type: " + inventoryShow[7].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[7].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[7].itemReward.ToString();
                    break;
                case "Icono9":

                    textoInfo.text = "Description: " + inventoryShow[8].itemDescription;
                    textoName.text = "Name: " + inventoryShow[8].itemName;
                    textoTipo.text = "Type: " + inventoryShow[8].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[8].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[8].itemReward.ToString();
                    break;
                case "Icono10":

                    textoInfo.text = "Description: " + inventoryShow[9].itemDescription;
                    textoName.text = "Name: " + inventoryShow[9].itemName;
                    textoTipo.text = "Type: " + inventoryShow[9].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[9].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[9].itemReward.ToString();
                    break;
                case "Icono11":

                    textoInfo.text = "Description: " + inventoryShow[10].itemDescription;
                    textoName.text = "Name: " + inventoryShow[10].itemName;
                    textoTipo.text = "Type: " + inventoryShow[10].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[10].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[10].itemReward.ToString();
                    break;
                case "Icono12":

                    textoInfo.text = "Description: " + inventoryShow[11].itemDescription;
                    textoName.text = "Name: " + inventoryShow[11].itemName;
                    textoTipo.text = "Type: " + inventoryShow[11].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[11].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[11].itemReward.ToString();
                    break;
                case "Icono13":

                    textoInfo.text = "Description: " + inventoryShow[12].itemDescription;
                    textoName.text = "Name: " + inventoryShow[12].itemName;
                    textoTipo.text = "Type: " + inventoryShow[12].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[12].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[12].itemReward.ToString();
                    break;
                case "Icono14":

                    textoInfo.text = "Description: " + inventoryShow[13].itemDescription;
                    textoName.text = "Name: " + inventoryShow[13].itemName;
                    textoTipo.text = "Type: " + inventoryShow[13].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[13].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[13].itemReward.ToString();
                    break;
                case "Icono15":

                    textoInfo.text = "Description: " + inventoryShow[14].itemDescription;
                    textoName.text = "Name: " + inventoryShow[14].itemName;
                    textoTipo.text = "Type: " + inventoryShow[14].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[14].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[14].itemReward.ToString();
                    break;
                case "Icono16":

                    textoInfo.text = "Description: " + inventoryShow[15].itemDescription;
                    textoName.text = "Name: " + inventoryShow[15].itemName;
                    textoTipo.text = "Type: " + inventoryShow[15].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[15].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[15].itemReward.ToString();
                    break;
                case "Icono17":

                    textoInfo.text = "Description: " + inventoryShow[16].itemDescription;
                    textoName.text = "Name: " + inventoryShow[16].itemName;
                    textoTipo.text = "Type: " + inventoryShow[16].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[16].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[16].itemReward.ToString();
                    break;
                case "Icono18":

                    textoInfo.text = "Description: " + inventoryShow[17].itemDescription;
                    textoName.text = "Name: " + inventoryShow[17].itemName;
                    textoTipo.text = "Type: " + inventoryShow[17].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[17].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[17].itemReward.ToString();
                    break;
                case "Icono19":

                    textoInfo.text = "Description: " + inventoryShow[18].itemDescription;
                    textoName.text = "Name: " + inventoryShow[18].itemName;
                    textoTipo.text = "Type: " + inventoryShow[18].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[18].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[18].itemReward.ToString();
                    break;
                case "Icono20":

                    textoInfo.text = "Description: " + inventoryShow[19].itemDescription;
                    textoName.text = "Name: " + inventoryShow[19].itemName;
                    textoTipo.text = "Type: " + inventoryShow[19].itemType.ToString();
                    textoTipo2.text = "Type: " + inventoryShow[19].itemType2.ToString();
                    textoReward.text = "Reward: " + inventoryShow[19].itemReward.ToString();
                    break;
                default:
                    break;

            }

        }
    }
   

}
