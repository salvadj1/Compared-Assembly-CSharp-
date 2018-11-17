using System;
using UnityEngine;

// Token: 0x02000497 RID: 1175
public class GameTip : MonoBehaviour
{
	// Token: 0x0600287F RID: 10367 RVA: 0x00093F50 File Offset: 0x00092150
	public bool TestLineOfSight(float fDistance)
	{
		Vector3 vector = base.transform.position + this.testOffset - Camera.main.transform.position;
		vector.Normalize();
		Ray ray;
		ray..ctor(Camera.main.transform.position, vector);
		RaycastHit raycastHit;
		return Physics.Raycast(ray, ref raycastHit, fDistance) && raycastHit.distance <= fDistance - 0.5f && (!this.TargetCollider || !(raycastHit.collider == this.TargetCollider)) && !base.transform.IsChildOf(raycastHit.collider.gameObject.transform) && !raycastHit.collider.gameObject.transform.IsChildOf(base.transform);
	}

	// Token: 0x06002880 RID: 10368 RVA: 0x0009403C File Offset: 0x0009223C
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
		global::GameTooltipManager.Singleton.UpdateTip(base.gameObject, this.text, base.transform.position + this.positionOffset, this.textColor, alpha, fscale);
	}

	// Token: 0x04001370 RID: 4976
	public string text = "Tooltip Text";

	// Token: 0x04001371 RID: 4977
	public bool lineOfSight = true;

	// Token: 0x04001372 RID: 4978
	public Vector3 testOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x04001373 RID: 4979
	public Vector3 positionOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x04001374 RID: 4980
	public Color textColor = Color.white;

	// Token: 0x04001375 RID: 4981
	public float maxDistance = 16f;

	// Token: 0x04001376 RID: 4982
	public Collider TargetCollider;
}
