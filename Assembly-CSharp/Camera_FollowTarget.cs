using System;
using UnityEngine;

// Token: 0x0200061D RID: 1565
public class Camera_FollowTarget : MonoBehaviour
{
	// Token: 0x06003797 RID: 14231 RVA: 0x000CBD88 File Offset: 0x000C9F88
	private void Start()
	{
		this.quatCameraAngles = Quaternion.identity;
	}

	// Token: 0x06003798 RID: 14232 RVA: 0x000CBD98 File Offset: 0x000C9F98
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

	// Token: 0x06003799 RID: 14233 RVA: 0x000CBE08 File Offset: 0x000CA008
	private void UpdateCameraPosition()
	{
		Vector3 vector = this.goTarget.transform.TransformDirection(Vector3.forward);
		Quaternion quaternion = Quaternion.AngleAxis(this.flCameraYawOffset, Vector3.up);
		base.transform.position = this.goTarget.transform.position + Vector3.up + quaternion * vector * this.flDistanceFromPlayer;
		this.vecLastCameraPosition = base.transform.position;
	}

	// Token: 0x04001BB5 RID: 7093
	public GameObject goTarget;

	// Token: 0x04001BB6 RID: 7094
	public float flDistanceFromPlayer = 4f;

	// Token: 0x04001BB7 RID: 7095
	public float flCameraYawOffset = 45f;

	// Token: 0x04001BB8 RID: 7096
	private Quaternion quatCameraAngles;

	// Token: 0x04001BB9 RID: 7097
	public bool bDropCamera;

	// Token: 0x04001BBA RID: 7098
	private Vector3 vecLastCameraPosition;
}
