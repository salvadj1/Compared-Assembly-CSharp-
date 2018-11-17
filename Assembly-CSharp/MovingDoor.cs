using System;
using UnityEngine;

// Token: 0x02000653 RID: 1619
[NGCAutoAddScript]
public class MovingDoor : BasicDoor
{
	// Token: 0x17000B16 RID: 2838
	// (get) Token: 0x06003874 RID: 14452 RVA: 0x000CF1F0 File Offset: 0x000CD3F0
	public Rigidbody rigidbody
	{
		get
		{
			if (!this._gotRigidbody)
			{
				this._rigidbody = base.rigidbody;
				this._gotRigidbody = true;
			}
			return this._rigidbody;
		}
	}

	// Token: 0x06003875 RID: 14453 RVA: 0x000CF224 File Offset: 0x000CD424
	protected void UpdateMovement(double openFraction, out Vector3 localPosition, out Quaternion localRotation)
	{
		if (openFraction == 0.0)
		{
			localPosition = this.originalLocalPosition;
			localRotation = this.originalLocalRotation;
			return;
		}
		if (this.smooth)
		{
			openFraction = (double)((openFraction >= 0.0) ? Mathf.SmoothStep(0f, 1f, (float)openFraction) : Mathf.SmoothStep(0f, -1f, (float)(-(float)openFraction)));
		}
		double num2;
		double num;
		double num3;
		if (!this.slerpMovementByDegrees || this.degrees == 0f || openFraction == 0.0 || openFraction == 1.0 || (num = Math.Sin(num2 = (double)this.degrees * 3.1415926535897931 / 180.0)) == 0.0)
		{
			num3 = openFraction;
		}
		else
		{
			double num4 = Math.Sin(openFraction * num2) / num;
			num3 = num4;
		}
		Quaternion quaternion = (openFraction != 0.0) ? Quaternion.AngleAxis((float)((double)this.degrees * ((!this.rotationABS) ? openFraction : Math.Abs(openFraction))), this.rotationAxis) : Quaternion.identity;
		Vector3 vector = this.openMovement * (float)((!this.movementABS) ? num3 : Math.Abs(num3));
		Vector3 vector2 = quaternion * -this.closedPositionPivot + this.closedPositionPivot;
		if (this.rotationFirst)
		{
			localPosition = this.originalLocalPosition + this.originalLocalRotation * (vector2 + quaternion * vector);
		}
		else
		{
			localPosition = this.originalLocalPosition + this.originalLocalRotation * (vector2 + vector);
		}
		localRotation = ((openFraction != 0.0) ? (this.originalLocalRotation * quaternion) : this.originalLocalRotation);
	}

	// Token: 0x06003876 RID: 14454 RVA: 0x000CF438 File Offset: 0x000CD638
	protected void UpdateMovement(double openFraction)
	{
		Vector3 localPosition;
		Quaternion localRotation;
		this.UpdateMovement(openFraction, out localPosition, out localRotation);
		base.transform.localPosition = localPosition;
		base.transform.localRotation = localRotation;
	}

	// Token: 0x06003877 RID: 14455 RVA: 0x000CF468 File Offset: 0x000CD668
	protected override void OnDoorFraction(double fractionOpen)
	{
		this.UpdateMovement(fractionOpen);
	}

	// Token: 0x06003878 RID: 14456 RVA: 0x000CF474 File Offset: 0x000CD674
	protected override BasicDoor.IdealSide IdealSideForPoint(Vector3 worldPoint)
	{
		float num = Vector3.Dot(base.transform.InverseTransformPoint(worldPoint), Vector3.Cross(this.rotationCross, this.rotationAxis));
		if (float.IsInfinity(num) || float.IsNaN(num) || Mathf.Approximately(0f, num))
		{
			return BasicDoor.IdealSide.Unknown;
		}
		return (num <= 0f) ? BasicDoor.IdealSide.Reverse : BasicDoor.IdealSide.Forward;
	}

	// Token: 0x06003879 RID: 14457 RVA: 0x000CF4E0 File Offset: 0x000CD6E0
	private static void DrawOpenGizmo(Vector3 closedPositionPivot, Vector3 rotationCross, Vector3 rotationAxis, float degrees, Vector3 openMovement, bool movementABS, bool rotationABS, bool rotationFirst, bool reversed)
	{
		Color color = Gizmos.color;
		Vector3 vector = closedPositionPivot;
		Vector3 vector2 = vector + rotationCross;
		bool flag = !Mathf.Approximately(degrees, 0f);
		bool flag2 = !Mathf.Approximately(openMovement.magnitude, 0f);
		Quaternion quaternion = Quaternion.identity;
		for (int i = 1; i < 21; i++)
		{
			float num = (float)i / 20f;
			Quaternion quaternion2 = Quaternion.AngleAxis(degrees * ((!reversed || rotationABS) ? num : (-num)), rotationAxis);
			Vector3 vector3;
			if (rotationFirst)
			{
				vector3 = closedPositionPivot + quaternion2 * (openMovement * ((!reversed || movementABS) ? num : (-num)));
			}
			else
			{
				vector3 = closedPositionPivot + openMovement * ((!reversed || movementABS) ? num : (-num));
			}
			if (flag2)
			{
				Gizmos.color = Color.Lerp(Color.red, (!reversed) ? Color.green : Color.yellow, num);
				Gizmos.DrawLine(vector, vector3);
			}
			vector = vector3;
			Vector3 vector4 = vector3 + quaternion2 * rotationCross;
			if (flag)
			{
				Gizmos.color = Color.Lerp(Color.blue, (!reversed) ? Color.cyan : Color.magenta, num);
				Gizmos.DrawLine(vector2, vector4);
			}
			vector2 = vector4;
			quaternion = quaternion2;
		}
		if (flag)
		{
			Vector3 vector5 = closedPositionPivot + ((!rotationFirst) ? openMovement : (quaternion * openMovement));
			Gizmos.color = new Color(1f, (!reversed) ? 0f : 1f, 0f, 0.5f);
			Gizmos.DrawLine(Vector3.Lerp(vector5, vector2, 0.5f), vector2);
		}
		Gizmos.color = color;
	}

	// Token: 0x04001CB3 RID: 7347
	private const float kFixedTimeElapsedMinimum = 1.5f;

	// Token: 0x04001CB4 RID: 7348
	[SerializeField]
	protected Vector3 closedPositionPivot;

	// Token: 0x04001CB5 RID: 7349
	[SerializeField]
	protected Vector3 openMovement = Vector3.up;

	// Token: 0x04001CB6 RID: 7350
	[SerializeField]
	protected Vector3 rotationAxis = Vector3.up;

	// Token: 0x04001CB7 RID: 7351
	[SerializeField]
	protected Vector3 rotationCross = Vector3.right;

	// Token: 0x04001CB8 RID: 7352
	[SerializeField]
	protected float degrees;

	// Token: 0x04001CB9 RID: 7353
	[SerializeField]
	protected bool rotationFirst;

	// Token: 0x04001CBA RID: 7354
	[SerializeField]
	protected bool smooth;

	// Token: 0x04001CBB RID: 7355
	[SerializeField]
	protected bool movementABS;

	// Token: 0x04001CBC RID: 7356
	[SerializeField]
	protected bool rotationABS;

	// Token: 0x04001CBD RID: 7357
	[SerializeField]
	protected bool slerpMovementByDegrees;

	// Token: 0x04001CBE RID: 7358
	[NonSerialized]
	private Vector3 lastLocalPosition;

	// Token: 0x04001CBF RID: 7359
	[NonSerialized]
	private Quaternion lastLocalRotation;

	// Token: 0x04001CC0 RID: 7360
	[NonSerialized]
	private bool onceMoved;

	// Token: 0x04001CC1 RID: 7361
	[NonSerialized]
	private int timesBoundKinematic;

	// Token: 0x04001CC2 RID: 7362
	[NonSerialized]
	private float kinematicFrameStart;

	// Token: 0x04001CC3 RID: 7363
	[NonSerialized]
	protected Rigidbody _rigidbody;

	// Token: 0x04001CC4 RID: 7364
	[NonSerialized]
	protected bool _gotRigidbody;

	// Token: 0x04001CC5 RID: 7365
	private Quaternion baseRot;
}
