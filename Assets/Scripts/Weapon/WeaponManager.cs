using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	private Dictionary<string, WeaponObject> equippedWeapons = new Dictionary<string, WeaponObject>();
	private string currentWeaponID;

	[SerializeField] private int maxWeapons = 1;

	[SerializeField] private WeaponHolder weaponHolder;

	private Player player;
	private Camera playerCamera;
	private PlayerRecoil playerRecoil;

	public bool CanEquip => equippedWeapons.Count < maxWeapons;

	void Start()
	{
		player = transform.root.GetComponent<Player>();
		playerRecoil = transform.root.GetComponent<PlayerRecoil>();
		playerCamera = player.playerCam;
	}

	public void AddWeapon(WeaponObject wepObj)
	{
		weaponHolder.AddWeapon(wepObj);
		equippedWeapons.Add(wepObj.ID, wepObj);

		wepObj.logic.SetPlayer(player);

		currentWeaponID = wepObj.ID;
		playerRecoil.UpdateCurrentWeapon();
	}

	public void RemoveWeapon(WeaponObject wepObj)
	{
		equippedWeapons.Remove(wepObj.ID);
		wepObj.logic.UnsetPlayer();
	}

	public void RemoveWeapon(string wepId)
	{
		equippedWeapons[wepId].logic.UnsetPlayer();
		equippedWeapons.Remove(wepId);
	}

	public WeaponObject GetWeapon(string wepId) => equippedWeapons[wepId];

	public WeaponObject GetCurrentWeapon() => equippedWeapons[currentWeaponID];
}
