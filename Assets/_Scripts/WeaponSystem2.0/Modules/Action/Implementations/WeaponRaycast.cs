using UnityEngine;

namespace WeaponSystem
{
	public class WeaponRaycast : WeaponAction
	{
		[SerializeField]
		Transform point;

		WeaponStats weaponStats;
		WeaponState weaponState;

		GameObject ownerObject;

		public override void Init()
		{
			base.Init();

			weaponReference.Action.OnPerfom.AddListener(Action);
			weaponStats = weaponReference.weaponStats;
			weaponState = weaponReference.weaponState;

			ownerObject = weaponReference.owner.ownerObject;
		}

		// TODO: Implement rest of shooting function
		void Action()
		{
			if (Processor.performed)
			{
				weaponState.currentAmmo -= 1;

				weaponState.IncreaseHeat();

				// Shoot from camera
				RaycastHit hit;
				if (Physics.Raycast(
					point.transform.position, point.transform.forward, out hit, weaponStats.range
				))
				{
					//EventManager.Instance.m_HitEvent.Invoke(new Hit(temp, hit, weaponStats));
					DecalManager.Instance.PlaceDecal(hit.point, Quaternion.identity);
				}

				// Step 2 run visual stuff. Animations, particles.
				//weaponVFX.PlayMuzzleflash();

				// Step 3 apply recoil to player
				// FIXME: Temporary
				ownerObject.GetComponent<TempWeap>().m_ShootEvent.Invoke(new RecoilData(weaponState, weaponStats));
			}
		}
	}
}