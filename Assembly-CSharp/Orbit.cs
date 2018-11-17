using System;
using UnityEngine;

// Token: 0x020004E5 RID: 1253
public class Orbit : MonoBehaviour
{
	// Token: 0x06002AE1 RID: 10977 RVA: 0x000AB384 File Offset: 0x000A9584
	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawLine(this.orbitPosition, Vector3.zero);
		Gizmos.DrawSphere(this.orbitPosition, 0.01f);
	}

	// Token: 0x06002AE2 RID: 10978 RVA: 0x000AB3C4 File Offset: 0x000A95C4
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		if (deltaTime != 0f)
		{
			Vector3 vector;
			vector.x = this.orbitEulerSpeed.x * deltaTime;
			vector.y = this.orbitEulerSpeed.y * deltaTime;
			vector.z = this.orbitEulerSpeed.z * deltaTime;
			if (vector.x != 0f || vector.y != 0f || vector.z != 0f)
			{
				Quaternion quaternion = Quaternion.Euler(vector);
				Quaternion quaternion2 = base.transform.localRotation * quaternion;
				if (this.orbitCenter)
				{
					base.transform.localPosition = quaternion2 * this.orbitPosition;
				}
				else
				{
					base.transform.localPosition = quaternion2 * -this.orbitPosition + this.orbitPosition;
				}
				base.transform.localRotation = quaternion2;
			}
		}
	}

	// Token: 0x04001767 RID: 5991
	public Vector3 orbitPosition;

	// Token: 0x04001768 RID: 5992
	public Vector3 orbitEulerSpeed;

	// Token: 0x04001769 RID: 5993
	public bool orbitCenter;
}
