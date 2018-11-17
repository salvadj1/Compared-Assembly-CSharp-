using System;
using UnityEngine;

// Token: 0x02000685 RID: 1669
[ExecuteInEditMode]
public class PivotTest : MonoBehaviour
{
	// Token: 0x060039E6 RID: 14822 RVA: 0x000D6798 File Offset: 0x000D4998
	public void Update()
	{
		if (this.child)
		{
			Quaternion quaternion = Quaternion.Euler(this.pivotAngles);
			Vector3 vector = quaternion * -this.pivot + this.pivot;
			vector += this.offsetTranslation;
			this.child.localRotation = quaternion;
			this.child.localPosition = vector;
		}
	}

	// Token: 0x060039E7 RID: 14823 RVA: 0x000D6804 File Offset: 0x000D4A04
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.DrawWireSphere(this.pivot, 0.01f);
		Gizmos.color = new Color(0f, 0f, 0f, 0.5f);
		Gizmos.DrawLine(Vector3.zero, this.offsetTranslation);
		Quaternion quaternion = Quaternion.Euler(this.pivotAngles);
		Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
		Gizmos.DrawLine(this.offsetTranslation, quaternion * -this.pivot + this.pivot + this.offsetTranslation);
		Gizmos.matrix *= Matrix4x4.TRS(this.pivot + this.offsetTranslation, Quaternion.Euler(this.pivotAngles), Vector3.one);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(Vector3.zero, Vector3.forward);
		Gizmos.color = Color.red;
		Gizmos.DrawLine(Vector3.zero, Vector3.right);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(Vector3.zero, Vector3.up);
		if (this.child)
		{
			Gizmos.matrix = this.child.localToWorldMatrix;
			Gizmos.color = Color.white;
			Gizmos.DrawWireCube(Vector3.zero, new Vector3(0.02f, 0.02f, 0.02f));
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(Vector3.zero, Vector3.forward);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(Vector3.zero, Vector3.right);
			Gizmos.color = Color.green;
			Gizmos.DrawLine(Vector3.zero, Vector3.up);
		}
	}

	// Token: 0x04001DE3 RID: 7651
	public Transform child;

	// Token: 0x04001DE4 RID: 7652
	public Vector3 pivot;

	// Token: 0x04001DE5 RID: 7653
	public Vector3 pivotAngles;

	// Token: 0x04001DE6 RID: 7654
	public Vector3 offsetTranslation;

	// Token: 0x04001DE7 RID: 7655
	public Vector3 offsetRotation;
}
