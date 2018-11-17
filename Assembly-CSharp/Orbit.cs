using System;
using UnityEngine;

// Token: 0x020005A0 RID: 1440
public class Orbit : MonoBehaviour
{
	// Token: 0x06002E93 RID: 11923 RVA: 0x000B311C File Offset: 0x000B131C
	private void OnDrawGizmosSelected()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawLine(this.orbitPosition, Vector3.zero);
		Gizmos.DrawSphere(this.orbitPosition, 0.01f);
	}

	// Token: 0x06002E94 RID: 11924 RVA: 0x000B315C File Offset: 0x000B135C
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

	// Token: 0x04001924 RID: 6436
	public Vector3 orbitPosition;

	// Token: 0x04001925 RID: 6437
	public Vector3 orbitEulerSpeed;

	// Token: 0x04001926 RID: 6438
	public bool orbitCenter;
}
