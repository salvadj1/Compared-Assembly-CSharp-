using System;
using UnityEngine;

// Token: 0x020006DE RID: 1758
public class Camera_FollowTarget : MonoBehaviour
{
	// Token: 0x06003B77 RID: 15223 RVA: 0x000D4460 File Offset: 0x000D2660
	private void Start()
	{
		this.quatCameraAngles = Quaternion.identity;
	}

	// Token: 0x06003B78 RID: 15224 RVA: 0x000D4470 File Offset: 0x000D2670
	private void Update()
	{
		if (!this.bDropCamera)
		{
			this.UpdateCameraPosition();
		}
		else
		{
			base.transform.position = this.vecLastCameraPosition;
		}
		base.transform.rotation = Quaternion.LookRotation(this.goTarget.transform.position + Vector3.up - base.transform.position);
	}

	// Token: 0x06003B79 RID: 15225 RVA: 0x000D44E0 File Offset: 0x000D26E0
	private void UpdateCameraPosition()
	{
		Vector3 vector = this.goTarget.transform.TransformDirection(Vector3.forward);
		Quaternion quaternion = Quaternion.AngleAxis(this.flCameraYawOffset, Vector3.up);
		base.transform.position = this.goTarget.transform.position + Vector3.up + quaternion * vector * this.flDistanceFromPlayer;
		this.vecLastCameraPosition = base.transform.position;
	}

	// Token: 0x04001DA0 RID: 7584
	public GameObject goTarget;

	// Token: 0x04001DA1 RID: 7585
	public float flDistanceFromPlayer = 4f;

	// Token: 0x04001DA2 RID: 7586
	public float flCameraYawOffset = 45f;

	// Token: 0x04001DA3 RID: 7587
	private Quaternion quatCameraAngles;

	// Token: 0x04001DA4 RID: 7588
	public bool bDropCamera;

	// Token: 0x04001DA5 RID: 7589
	private Vector3 vecLastCameraPosition;
}
