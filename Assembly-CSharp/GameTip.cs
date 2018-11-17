using System;
using UnityEngine;

// Token: 0x020003E7 RID: 999
public class GameTip : MonoBehaviour
{
	// Token: 0x0600250D RID: 9485 RVA: 0x0008E564 File Offset: 0x0008C764
	public bool TestLineOfSight(float fDistance)
	{
		Vector3 vector = base.transform.position + this.testOffset - Camera.main.transform.position;
		vector.Normalize();
		Ray ray;
		ray..ctor(Camera.main.transform.position, vector);
		RaycastHit raycastHit;
		return Physics.Raycast(ray, ref raycastHit, fDistance) && raycastHit.distance <= fDistance - 0.5f && (!this.TargetCollider || !(raycastHit.collider == this.TargetCollider)) && !base.transform.IsChildOf(raycastHit.collider.gameObject.transform) && !raycastHit.collider.gameObject.transform.IsChildOf(base.transform);
	}

	// Token: 0x0600250E RID: 9486 RVA: 0x0008E650 File Offset: 0x0008C850
	public void OnWillRenderObject()
	{
		if (Camera.main != Camera.current)
		{
			return;
		}
		float num = Vector3.Distance(base.transform.position, Camera.main.transform.position);
		if (num > this.maxDistance)
		{
			return;
		}
		if (this.lineOfSight && this.TestLineOfSight(num))
		{
			return;
		}
		float num2 = num / this.maxDistance;
		float alpha = 1f - num2;
		float fscale = 2f / (num * (2f * Mathf.Tan(Camera.main.fieldOfView / 2f * 0.0174532924f)));
		GameTooltipManager.Singleton.UpdateTip(base.gameObject, this.text, base.transform.position + this.positionOffset, this.textColor, alpha, fscale);
	}

	// Token: 0x040011F6 RID: 4598
	public string text = "Tooltip Text";

	// Token: 0x040011F7 RID: 4599
	public bool lineOfSight = true;

	// Token: 0x040011F8 RID: 4600
	public Vector3 testOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x040011F9 RID: 4601
	public Vector3 positionOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x040011FA RID: 4602
	public Color textColor = Color.white;

	// Token: 0x040011FB RID: 4603
	public float maxDistance = 16f;

	// Token: 0x040011FC RID: 4604
	public Collider TargetCollider;
}
