using System;
using UnityEngine;

// Token: 0x0200074A RID: 1866
[ExecuteInEditMode]
public class PivotTest : MonoBehaviour
{
	// Token: 0x06003DDE RID: 15838 RVA: 0x000DF178 File Offset: 0x000DD378
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

	// Token: 0x06003DDF RID: 15839 RVA: 0x000DF1E4 File Offset: 0x000DD3E4
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

	// Token: 0x04001FDB RID: 8155
	public Transform child;

	// Token: 0x04001FDC RID: 8156
	public Vector3 pivot;

	// Token: 0x04001FDD RID: 8157
	public Vector3 pivotAngles;

	// Token: 0x04001FDE RID: 8158
	public Vector3 offsetTranslation;

	// Token: 0x04001FDF RID: 8159
	public Vector3 offsetRotation;
}
