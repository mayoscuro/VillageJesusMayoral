using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    public List<Item> items = new List<Item>();
    public List<Item> itemsLobo = new List<Item>();
    public List<Item> itemsCiervo = new List<Item>();
    public List<Item> itemsZorro = new List<Item>();
    public List<Item> itemsFrutas = new List<Item>();
    public List<Item> itemsInsectos = new List<Item>();
    public List<Item> itemsHierbas = new List<Item>();
    public List<Item> itemsMateriales = new List<Item>();
    void Start() {
        items.Add(new Item("Hierba curativa",0,
            "It can be delivered to the head of the village to prepare remedies, but it " +
            "can also be equipped to heal the character.",
            20, 100, Item.ItemType.Herbs, Item.ItemType.Consumable, 1));

		items.Add(new Item("Hierba de Montaña",1,
            "It serves as food for the King Chicks",
			40, 0, Item.ItemType.Herbs, Item.ItemType.Nothing,1));

		items.Add(new Item("Hierba",2,
            " It is necessary for preparing food or healing remedies, in combination with other herbs.",
			30, 0, Item.ItemType.Herbs, Item.ItemType.Nothing,1));

		items.Add(new Item("Madera",3 ,
            " It serves to make fire or build village buildings if it is delivered to the head of the village.In case the player needs it, it can also be used to craft arrows.",
			50, 0, Item.ItemType.ArrowMaterial, Item.ItemType.Nothing,1));

		items.Add(new Item("Bellota",4 ,
            "It can be used to prepare some meals.",
			50, 0, Item.ItemType.Food, Item.ItemType.Nothing,1));

        items.Add(new Item("Piña", 5,
            "It is get from the pines and can be used to prepare some meals",
            50, 0, Item.ItemType.Food, Item.ItemType.Nothing, 1));

        items.Add(new Item("Insecto", 6,
            "They are only decorative elements without usage or interaction.",
            50, 0, Item.ItemType.Nothing, Item.ItemType.Nothing, 1));

        items.Add(new Item("Mineral", 7,
            "It can be obtained from small stones that appear inside mountains, or nearby, "+
            "among other things useful for crafting arrows. They can be delivered to the head of the "+
            "village or can be used to craft arrows by the player.",
            50, 0, Item.ItemType.ArrowMaterial, Item.ItemType.Nothing, 1));

        items.Add(new Item("Carne(Lobo)", 8,
            "It is necessary to prepare food.",
            50, 0, Item.ItemType.Food, Item.ItemType.Nothing, 1));

        items.Add(new Item("Piel(Lobo)", 9,
            "This is necessary to craft clothes.",
            50, 0, Item.ItemType.Leather, Item.ItemType.Nothing, 1));

        items.Add(new Item("Huesos(Lobo)", 10,
            " It is necessary to craft common use tools.",
            50, 0, Item.ItemType.Bones, Item.ItemType.Nothing, 1));

        items.Add(new Item("Carne(Fenrir)", 11,
            "Carne deliciosa y de muy gran tamaño que el cocinero de la aldea puede utilizar para preparar suculentos platos",
            50, 0, Item.ItemType.Food, Item.ItemType.Nothing, 1));

        items.Add(new Item("Piel(Fenrir)", 12,
            "Piel de buena calidad muy resistente y de gran tamaño que se puede entregar al jefe de la aldea",
            50, 0, Item.ItemType.Leather, Item.ItemType.Nothing, 1));

        items.Add(new Item("Huesos(Fenrir)", 13,
            "Huesos de lobo ideales para preparar caldo, o para desarrollar instrumentos para la aldea, se tiene que entregar al jefe dela aldea",
            50, 0, Item.ItemType.Bones, Item.ItemType.Nothing, 1));

        items.Add(new Item("Dientes(Fenrir)", 14,
            "Dientes de Fenrir muy codiciados por su escasez, se puede entregar en la aldea",
            50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));

        items.Add(new Item("CarnePequeña(Zorro)", 15,
           "This is necessary to prepare food.",
           50, 0, Item.ItemType.Food, Item.ItemType.Nothing, 1));

        items.Add(new Item("Cola(Zorro)", 16,
           "It can be used as an amulet.",
           50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));

        items.Add(new Item("Huesos(Zorro)", 17,
           "This is necessary to craft common use tools.",
           50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));

        items.Add(new Item("Piel(Zorro)", 18,
           "It is necessary to craft clothes.",
           50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));

        items.Add(new Item("Piel(Ciervo)", 19,
           "It is necessary to prepare food",
           50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));

        items.Add(new Item("Carne(Ciervo)", 20,
           "This is necessary to craft clothes.",
           50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));

        items.Add(new Item("Pluma(Polluelo rey)", 21,
           "It can be used to make warm outfits. It is also useful for crafting arrows.",
           50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));

        items.Add(new Item("Flecha", 22,
           "It can be crafted using minerals, Wood and Chick feather. It will be used automatically when the bow will be shot.",
           50, 0, Item.ItemType.ArrowMaterial, Item.ItemType.Nothing, 1));

        items.Add(new Item("Lord of the Forest blessing", 23,
           "It gives a bonus which consists of doubling the amount of rank gain when delivering resources to the village.",
           50, 0, Item.ItemType.ArrowMaterial, Item.ItemType.Nothing, 1));

        items.Add(new Item("Lord of the Forest tear", 24,
           "It is an item that the Lord of the Forest gives if he notices that his forest is dying.",
           50, 0, Item.ItemType.ArrowMaterial, Item.ItemType.Nothing, 1));

        items.Add(new Item("Forest Lord head", 25,
           "This can not bring anything good.",
           50, 0, Item.ItemType.ArrowMaterial, Item.ItemType.Nothing, 1));

        items.Add(new Item("Guardian blessing", 26,
           "It gives a bonus that will cause a 1.5% increase of the rank gain when delivering a resource to the village.",
           50, 0, Item.ItemType.ArrowMaterial, Item.ItemType.Nothing, 1));

        items.Add(new Item("Bad omen", 27,
           " It is simply a warning about the irresponsible damage that I am causing.",
           50, 0, Item.ItemType.ArrowMaterial, Item.ItemType.Nothing, 1));

        items.Add(new Item("Kodama leaf", 28,
          "It is a leaf that kodamas use to appear and disappear, this has been left for some reason.",
          50, 0, Item.ItemType.ArrowMaterial, Item.ItemType.Nothing, 1));






        //ITEMS HIERBAS:


        itemsHierbas.Add(new Item("Hierba curativa", 0,
            "Hierba curativa que se puede entregar al jefe de la aldea, o utilizar para ser " +
            "equipado y curar al personaje",
            20, 100, Item.ItemType.Herbs, Item.ItemType.Consumable, 1));

        itemsHierbas.Add(new Item("Hierba de Montaña", 1,
            "Alimento favorito de los polluelos rey, solo puede ser entregada al jefe de la aldea",
            40, 0, Item.ItemType.Herbs, Item.ItemType.Nothing, 1));

        itemsHierbas.Add(new Item("Hierba", 2,
            "Util para mezclar con otras hierbas y crear medicamentos, solo puede ser entregada al jefe de la aldea",
            30, 0, Item.ItemType.Herbs, Item.ItemType.Nothing, 1));





        //ITEMS MATERIALES:

        itemsMateriales.Add(new Item("Madera", 0,
            "Se puede utilizar como material para crear flechas, pero tambien es util para consetruir elementos de la aldea",
            50, 0, Item.ItemType.ArrowMaterial, Item.ItemType.Nothing, 1));

        itemsMateriales.Add(new Item("Mineral", 1,
           "Mineral util, entre otras cosas para crear flechas",
           50, 0, Item.ItemType.ArrowMaterial, Item.ItemType.Nothing, 1));



        //ITEMS FRUTAS:

        itemsFrutas.Add(new Item("Bellota", 0,
            "Fruto de los arbustos que el cocinero de la aldea puede utilizar para preparar suculentos platos",
            50, 0, Item.ItemType.Food, Item.ItemType.Nothing, 1));

        itemsFrutas.Add(new Item("Piña", 1,
            "Fruto de los pinos que el cocinero de la aldea puede utilizar para preparar suculentos platos",
            50, 0, Item.ItemType.Food, Item.ItemType.Nothing, 1));



        //ITEMS INSECTOS:

        itemsInsectos.Add(new Item("Insecto", 0,
            "Insectos que aparecen en algunos arbustos, algunos son inofensivos, pero en algunos casos pueden dañar los frutos de los arbustos",
            50, 0, Item.ItemType.Nothing, Item.ItemType.Nothing, 1));



        //ITEMS LOBOS:

        itemsLobo.Add(new Item("Carne(Lobo)", 0,
            "Carne deliciosa cocinero de la aldea puede utilizar para preparar suculentos platos",
            50, 0, Item.ItemType.Food, Item.ItemType.Nothing, 1));

        itemsLobo.Add(new Item("Piel(Lobo)", 1,
            "Piel de buena calidad muy resistente que se puede entregar al jefe de la aldea",
            50, 0, Item.ItemType.Leather, Item.ItemType.Nothing, 1));

        itemsLobo.Add(new Item("Huesos(Lobo)", 2,
            "Huesos de lobo ideales para preparar caldo, o para desarrollar instrumentos para la aldea, se tiene que entregar al jefe dela aldea",
            50, 0, Item.ItemType.Bones, Item.ItemType.Nothing, 1));




        //ITEMS CIERVOS:
        itemsCiervo.Add(new Item("Piel(Ciervo)", 0,
           "Dientes de Fenrir muy codiciados por su escasez, se puede entregar en la aldea",
           50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));

        itemsCiervo.Add(new Item("Carne(Ciervo)", 1,
           "Dientes de Fenrir muy codiciados por su escasez, se puede entregar en la aldea",
           50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));



        //ITEMS ZORROS:

        itemsZorro.Add(new Item("Carne pequeña (Zorro)", 0,
           "Dientes de Fenrir muy codiciados por su escasez, se puede entregar en la aldea",
           50, 0, Item.ItemType.Food, Item.ItemType.Nothing, 1));

        itemsZorro.Add(new Item("Cola(Zorro)", 1,
           "Dientes de Fenrir muy codiciados por su escasez, se puede entregar en la aldea",
           50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));

        itemsZorro.Add(new Item("Huesos(Zorro)", 2,
           "Dientes de Fenrir muy codiciados por su escasez, se puede entregar en la aldea",
           50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));

        itemsZorro.Add(new Item("Piel(Zorro)", 3,
           "Dientes de Fenrir muy codiciados por su escasez, se puede entregar en la aldea",
           50, 0, Item.ItemType.Special, Item.ItemType.Nothing, 1));
    }
}
