using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveManager : MonoBehaviour {
	public static List<Item> itemsInInventoryToSave;

	public static void saveItems(){
		for (int x = 0; x < itemsInInventoryToSave.Count;x++){
			PlayerPrefs.SetInt ("item"+x, itemsInInventoryToSave[x].itemsNumero);
		}
	}
}
