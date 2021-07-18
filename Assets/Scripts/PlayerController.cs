using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	//public characterMove cM;
	//[HideInInspector]
	//public PlayerInput pI;
	//[HideInInspector]
	/*public characterHealth cH;
	[HideInInspector]*/
	//public PlayerWeaponManager weaponManager;
	
	//[HideInInspector]
	[HideInInspector]
	public int pID;
	public int skinIndex, skinSwitch = 1;
	private MeshRenderer playerSkin;
	Material[] playerSkinMat;



	void Awake () {
		//cM=GetComponent<characterMove>();
		//pI=GetComponent<PlayerInput>();
		//cH=GetComponent<characterHealth>();
		//weaponManager=GetComponent<PlayerWeaponManager>();
	}
	
	public void SkinSwitcher() {
		switch (skinSwitch)
		{
			case 1:
				playerSkin.material = playerSkinMat[0];
				//playerCorona.color = Color.red;  
				break;
			case 2:
				playerSkin.material = playerSkinMat[1];
				//playerCorona.color = Color.blue;
				break;
			case 3:
				playerSkin.material = playerSkinMat[2];
				//playerCorona.color = new Color(.37f,.76f,.74f);
				break;
			case 4:
				playerSkin.material = playerSkinMat[3];
				//playerCorona.color = Color.green;
				break;
			default:
				break;
		}

		if (skinIndex == 0)
		{
			skinSwitch = 1;
		}

	}

	public void OnEnable() {
		//if(CamManager.instance)
			//CamManager.instance.Players.Add(this);
	}
	
	public void OnDisable() {
		//if(CamManager.instance)
			//CamManager.instance.Players.Remove(this);
	}
}
