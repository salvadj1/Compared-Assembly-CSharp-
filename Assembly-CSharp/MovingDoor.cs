using System;
using UnityEngine;

// Token: 0x02000716 RID: 1814
[global::NGCAutoAddScript]
public class MovingDoor : global::BasicDoor
{
	// Token: 0x17000B96 RID: 2966
	// (get) Token: 0x06003C60 RID: 15456 RVA: 0x000D7AA0 File Offset: 0x000D5CA0
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

	// Token: 0x06003C61 RID: 15457 RVA: 0x000D7AD4 File Offset: 0x000D5CD4
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

	// Token: 0x06003C62 RID: 15458 RVA: 0x000D7CE8 File Offset: 0x000D5EE8
	protected void UpdateMovement(double openFraction)
	{
		Vector3 localPosition;
		Quaternion localRotation;
		this.UpdateMovement(openFraction, out localPosition, out localRotation);
		base.transform.localPosition = localPosition;
		base.transform.localRotation = localRotation;
	}

	// Token: 0x06003C63 RID: 15459 RVA: 0x000D7D18 File Offset: 0x000D5F18
	protected override void OnDoorFraction(double fractionOpen)
	{
		this.UpdateMovement(fractionOpen);
	}

	// Token: 0x06003C64 RID: 15460 RVA: 0x000D7D24 File Offset: 0x000D5F24
	protected override global::BasicDoor.IdealSide IdealSideForPoint(Vector3 worldPoint)
	{
		float num = Vector3.Dot(base.transform.InverseTransformPoint(worldPoint), Vector3.Cross(this.rotationCross, this.rotationAxis));
		if (float.IsInfinity(num) || float.IsNaN(num) || Mathf.Approximately(0f, num))
		{
			return global::BasicDoor.IdealSide.Unknown;
		}
		return (num <= 0f) ? global::BasicDoor.IdealSide.Reverse : global::BasicDoor.IdealSide.Forward;
	}

	// Token: 0x06003C65 RID: 15461 RVA: 0x000D7D90 File Offset: 0x000D5F90
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

	// Token: 0x04001EA8 RID: 7848
	private const float kFixedTimeElapsedMinimum = 1.5f;

	// Token: 0x04001EA9 RID: 7849
	[SerializeField]
	protected Vector3 closedPositionPivot;

	// Token: 0x04001EAA RID: 7850
	[SerializeField]
	protected Vector3 openMovement = Vector3.up;

	// Token: 0x04001EAB RID: 7851
	[SerializeField]
	protected Vector3 rotationAxis = Vector3.up;

	// Token: 0x04001EAC RID: 7852
	[SerializeField]
	protected Vector3 rotationCross = Vector3.right;

	// Token: 0x04001EAD RID: 7853
	[SerializeField]
	protected float degrees;

	// Token: 0x04001EAE RID: 7854
	[SerializeField]
	protected bool rotationFirst;

	// Token: 0x04001EAF RID: 7855
	[SerializeField]
	protected bool smooth;

	// Token: 0x04001EB0 RID: 7856
	[SerializeField]
	protected bool movementABS;

	// Token: 0x04001EB1 RID: 7857
	[SerializeField]
	protected bool rotationABS;

	// Token: 0x04001EB2 RID: 7858
	[SerializeField]
	protected bool slerpMovementByDegrees;

	// Token: 0x04001EB3 RID: 7859
	[NonSerialized]
	private Vector3 lastLocalPosition;

	// Token: 0x04001EB4 RID: 7860
	[NonSerialized]
	private Quaternion lastLocalRotation;

	// Token: 0x04001EB5 RID: 7861
	[NonSerialized]
	private bool onceMoved;

	// Token: 0x04001EB6 RID: 7862
	[NonSerialized]
	private int timesBoundKinematic;

	// Token: 0x04001EB7 RID: 7863
	[NonSerialized]
	private float kinematicFrameStart;

	// Token: 0x04001EB8 RID: 7864
	[NonSerialized]
	protected Rigidbody _rigidbody;

	// Token: 0x04001EB9 RID: 7865
	[NonSerialized]
	protected bool _gotRigidbody;

	// Token: 0x04001EBA RID: 7866
	private Quaternion baseRot;
}
