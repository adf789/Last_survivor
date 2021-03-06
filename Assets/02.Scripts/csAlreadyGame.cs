﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임의 성능을 위해 해당 클래스에서 필요한 오브젝트들을 미리 초기화한다.
public class csAlreadyGame : MonoBehaviour {
	private static Dictionary<string, Sprite> itemImgs = new Dictionary<string, Sprite> ();
	private static bool isLoaded = false;
	private static Transform dragItemView;
	private static Transform selectItemView;
	private static Transform focusObj;
	private static GameObject inventoryObj;
	private static GameObject crateObj;
	private static GameObject worktableObj;
	private static GameObject quickBarObj;
	private static GameObject hpBarObj;
	private static GameObject fatigueBarObj;

	// 싱글톤 객체들을 호출하여 미리 초기화를 한다.
	void Awake () {
		csItemList list = csItemList.Instance;
		csCombineInfo combInfo = csCombineInfo.Instance;
		csInventory inventory = csInventory.Instance;
		csCharacterStatus cs = csCharacterStatus.Instance;
	}

	// UI의 오브젝트들을 미리 찾는다.
	private static void FindInit(){
		Transform canvas = GameObject.Find ("Canvas").transform;
		inventoryObj = canvas.Find ("InventoryView").gameObject;
		crateObj = canvas.Find ("CrateInventory").gameObject;
		worktableObj = canvas.Find ("WorktableView").gameObject;
		quickBarObj = canvas.Find ("QuickBar").gameObject;
		focusObj = canvas.Find ("Focus");
		hpBarObj = canvas.Find ("StatusView").transform.Find ("HpBar").gameObject;
		fatigueBarObj = canvas.Find ("StatusView").transform.Find ("FatigueBar").gameObject;
	}

	// 아이템의 이미지를 로드한다.
	private static void LoadImg(){
		itemImgs.Add("Empty", Resources.Load<Sprite>("Empty"));
		itemImgs.Add("Iron", Resources.Load<Sprite>("Iron"));
		itemImgs.Add("Wood", Resources.Load<Sprite>("Wood"));
		itemImgs.Add("Crystal", Resources.Load<Sprite>("Crystal"));
		itemImgs.Add ("Meat", Resources.Load<Sprite> ("Meat"));
		itemImgs.Add ("Axe", Resources.Load<Sprite> ("Axe"));
		itemImgs.Add ("Pickaxe", Resources.Load<Sprite> ("Pickaxe"));
		itemImgs.Add ("Shovel", Resources.Load<Sprite> ("Shovel"));
		itemImgs.Add ("Tent", Resources.Load<Sprite> ("Tent"));
		itemImgs.Add ("Campfire", Resources.Load<Sprite> ("Campfire"));
	}

	public static Sprite GetImg(string key){
		if (!isLoaded) {
			LoadImg ();
			isLoaded = true;
		}

		if (!itemImgs.ContainsKey (key))
			return itemImgs ["Empty"];
		return itemImgs [key];
	}

	// 아이템을 드래그할 때 아이템의 이미지를 띄울 오브젝트를 반환한다.
	public static Transform DragItemView{
		get{
			if (dragItemView == null) {
				dragItemView = GameObject.Find ("Canvas").transform.Find ("DragImg");
			}
			return dragItemView;
		}
	}

	public static GameObject InventoryObj{
		get{
			if (inventoryObj == null) {
				FindInit ();
			}
			return inventoryObj;
		}
	}

	public static GameObject CrateObj{
		get{
			if (crateObj == null) {
				FindInit ();
			}
			return crateObj;
		}
	}

	public static GameObject WorktableObj{
		get{
			if (worktableObj == null) {
				FindInit ();
			}
			return worktableObj;
		}
	}

	public static GameObject QuickBarObj{
		get{
			if (quickBarObj == null) {
				FindInit ();
			}
			return quickBarObj;
		}
	}

	public static Transform SelectItemView{
		get{
			if (selectItemView == null) {
				selectItemView = GameObject.Find ("Canvas").transform.Find ("SelectItem");
			}
			return selectItemView;
		}
	}

	public static Transform FocusObj{
		get{
			if (focusObj == null) {
				FindInit ();
			}
			return focusObj;
		}
	}

	public static GameObject FatigueBarObj{
		get{
			if (fatigueBarObj == null) {
				FindInit ();
			}
			return fatigueBarObj;
		}
	}

	public static GameObject HpObj{
		get{
			if (hpBarObj == null) {
				FindInit ();
			}
			return hpBarObj;
		}
	}
}
