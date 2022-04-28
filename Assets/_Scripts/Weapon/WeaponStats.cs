using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FireMode
{
	Single,
	Rapid,
	Burst,
}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 2)]
public class WeaponStats : ScriptableObject
{
	[ReadOnly]
	public string ID;
	public new string name;

	[Header("Basic stats")]
	public float damage;
	public float range;
	public float fireRate;
	public FireMode fireMode;

	[Header("Reloading and ammo")]
	public int maxAmmo;

	[Range(0.1f, 10f)]
	public float reloadTime;

	[Header("Visual")]
	public GameObject weaponModel;

	[Header("Recoil")]
	public AnimationCurve recoilHoriz;
	public AnimationCurve recoilVerti;

	[Range(0.1f, 10f)]
	public float recoilHorizMult;
	[Range(0.1f, 10f)]
	public float recoilVertiMult;

	void OnValidate()
	{
		if (!System.Guid.TryParse(ID, out _))
			ID = System.Guid.NewGuid().ToString();
	}
}
