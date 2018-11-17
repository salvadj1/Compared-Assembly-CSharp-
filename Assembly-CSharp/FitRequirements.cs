using System;
using UnityEngine;

// Token: 0x02000659 RID: 1625
public class FitRequirements : ScriptableObject
{
	// Token: 0x0600389D RID: 14493 RVA: 0x000CFD3C File Offset: 0x000CDF3C
	public bool Test(Matrix4x4 placePosition)
	{
		if (!object.ReferenceEquals(this.Conditions, null))
		{
			foreach (FitRequirements.Condition condition in this.Conditions)
			{
				if (!condition.Check(ref placePosition))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600389E RID: 14494 RVA: 0x000CFD8C File Offset: 0x000CDF8C
	public bool Test(Vector3 origin, Quaternion rotation, Vector3 scale)
	{
		return this.Test(Matrix4x4.TRS(origin, rotation, scale));
	}

	// Token: 0x0600389F RID: 14495 RVA: 0x000CFD9C File Offset: 0x000CDF9C
	public bool Test(Vector3 origin, Quaternion rotation)
	{
		return this.Test(Matrix4x4.TRS(origin, rotation, Vector3.one));
	}

	// Token: 0x04001CDD RID: 7389
	[SerializeField]
	private FitRequirements.Condition[] Conditions;

	// Token: 0x04001CDE RID: 7390
	[HideInInspector]
	[SerializeField]
	private string assetPreview;

	// Token: 0x0200065A RID: 1626
	public enum Instruction
	{
		// Token: 0x04001CE0 RID: 7392
		Raycast,
		// Token: 0x04001CE1 RID: 7393
		SphereCast,
		// Token: 0x04001CE2 RID: 7394
		CapsuleCast,
		// Token: 0x04001CE3 RID: 7395
		CheckCapsule,
		// Token: 0x04001CE4 RID: 7396
		CheckSphere
	}

	// Token: 0x0200065B RID: 1627
	[Serializable]
	public class Condition
	{
		// Token: 0x060038A0 RID: 14496 RVA: 0x000CFDB0 File Offset: 0x000CDFB0
		public Condition()
		{
			this.flt0.a = 1f;
			this.flt1 = Vector3.up;
			this.flt1.a = 0.5f;
			this.flt2 = Vector3.up;
			this.mask = 536871936;
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x060038A1 RID: 14497 RVA: 0x000CFE20 File Offset: 0x000CE020
		// (set) Token: 0x060038A2 RID: 14498 RVA: 0x000CFE34 File Offset: 0x000CE034
		public Vector3 center
		{
			get
			{
				return this.flt0;
			}
			set
			{
				this.flt0.r = value.x;
				this.flt0.g = value.y;
				this.flt0.b = value.z;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060038A3 RID: 14499 RVA: 0x000CFE78 File Offset: 0x000CE078
		// (set) Token: 0x060038A4 RID: 14500 RVA: 0x000CFE8C File Offset: 0x000CE08C
		public Vector3 capStart
		{
			get
			{
				return this.flt0;
			}
			set
			{
				this.flt0.r = value.x;
				this.flt0.g = value.y;
				this.flt0.b = value.z;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x000CFED0 File Offset: 0x000CE0D0
		// (set) Token: 0x060038A6 RID: 14502 RVA: 0x000CFEE0 File Offset: 0x000CE0E0
		public float distance
		{
			get
			{
				return this.flt0.a;
			}
			set
			{
				this.flt0.a = value;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x060038A7 RID: 14503 RVA: 0x000CFEF0 File Offset: 0x000CE0F0
		// (set) Token: 0x060038A8 RID: 14504 RVA: 0x000CFF00 File Offset: 0x000CE100
		public float radius
		{
			get
			{
				return this.flt1.a;
			}
			set
			{
				this.flt1.a = value;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x000CFF10 File Offset: 0x000CE110
		// (set) Token: 0x060038AA RID: 14506 RVA: 0x000CFF24 File Offset: 0x000CE124
		public Vector3 direction
		{
			get
			{
				return this.flt1;
			}
			set
			{
				this.flt1.r = value.x;
				this.flt1.g = value.y;
				this.flt1.b = value.z;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060038AB RID: 14507 RVA: 0x000CFF68 File Offset: 0x000CE168
		// (set) Token: 0x060038AC RID: 14508 RVA: 0x000CFF7C File Offset: 0x000CE17C
		public Vector3 capEnd
		{
			get
			{
				return this.flt2;
			}
			set
			{
				this.flt2.r = value.x;
				this.flt2.g = value.y;
				this.flt2.b = value.z;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060038AD RID: 14509 RVA: 0x000CFFC0 File Offset: 0x000CE1C0
		// (set) Token: 0x060038AE RID: 14510 RVA: 0x000CFFC8 File Offset: 0x000CE1C8
		public bool passOnFail
		{
			get
			{
				return this.failPass;
			}
			set
			{
				this.failPass = value;
			}
		}

		// Token: 0x060038AF RID: 14511 RVA: 0x000CFFD4 File Offset: 0x000CE1D4
		private static void GizmoCapsuleAxis(ref Matrix4x4 matrix, Vector3 start, float radius, float distance, Vector3 direction, float? unitValueRadius = null, float? unitValueHeight = null)
		{
			Vector3 vector = matrix.MultiplyPoint3x4(start);
			Vector3 normalized = matrix.MultiplyVector(direction).normalized;
			float? num = null;
			float value;
			if (unitValueRadius != null)
			{
				value = unitValueRadius.Value;
			}
			else
			{
				float? num2;
				num = (num2 = new float?(matrix.MultiplyVector(normalized).magnitude));
				value = num2.Value;
			}
			float num3 = value;
			float num4 = (unitValueHeight == null) ? ((num == null) ? matrix.MultiplyVector(normalized).magnitude : num.Value) : unitValueHeight.Value;
			Matrix4x4 matrix2 = Gizmos.matrix;
			Gizmos.matrix = matrix2 * Matrix4x4.TRS(vector, Quaternion.LookRotation(normalized, matrix.MultiplyVector(Vector3.up)), Vector3.one);
			radius = num3 * radius;
			float num5 = num4 * (distance + radius * 2f);
			Gizmos2.DrawWireCapsule(new Vector3(0f, 0f, num5 * 0.5f - radius), radius, num5, 2);
			Gizmos.matrix = matrix2;
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000D00EC File Offset: 0x000CE2EC
		private static void GizmoCapsulePoles(ref Matrix4x4 matrix, Vector3 start, float radius, Vector3 end)
		{
			Vector3 vector = (end - start).normalized;
			start = matrix.MultiplyPoint3x4(start);
			end = matrix.MultiplyPoint3x4(end);
			float magnitude = matrix.MultiplyVector(vector).magnitude;
			radius *= magnitude;
			vector = (end - start).normalized;
			start -= vector * radius;
			end += vector * radius;
			vector = end - start;
			Matrix4x4 matrix2 = Gizmos.matrix;
			Gizmos.matrix = matrix2 * Matrix4x4.TRS(start, Quaternion.LookRotation(vector, matrix.MultiplyVector(Vector3.up)), Vector3.one);
			float magnitude2 = (end - start).magnitude;
			Gizmos2.DrawWireCapsule(new Vector3(0f, 0f, magnitude2 * 0.5f), radius, magnitude2, 2);
			Gizmos.matrix = matrix2;
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000D01CC File Offset: 0x000CE3CC
		public void DrawGizmo(ref Matrix4x4 matrix)
		{
			switch (this.instruction)
			{
			case FitRequirements.Instruction.Raycast:
			{
				Vector3 vector = matrix.MultiplyPoint3x4(this.center);
				Vector3 normalized = matrix.MultiplyVector(this.direction).normalized;
				Gizmos.DrawLine(vector, vector + normalized * (matrix.MultiplyVector(normalized).magnitude * this.distance));
				break;
			}
			case FitRequirements.Instruction.SphereCast:
				FitRequirements.Condition.GizmoCapsuleAxis(ref matrix, this.center, this.radius, this.distance, this.direction, null, null);
				break;
			case FitRequirements.Instruction.CapsuleCast:
				FitRequirements.Condition.GizmoCapsuleAxis(ref matrix, this.capStart, this.radius, this.distance, this.direction, null, null);
				FitRequirements.Condition.GizmoCapsuleAxis(ref matrix, this.capEnd, this.radius, this.distance, this.direction, null, null);
				FitRequirements.Condition.GizmoCapsulePoles(ref matrix, this.capStart, this.radius, this.capEnd);
				break;
			case FitRequirements.Instruction.CheckCapsule:
				FitRequirements.Condition.GizmoCapsulePoles(ref matrix, this.capStart, this.radius, this.capEnd);
				break;
			case FitRequirements.Instruction.CheckSphere:
				Gizmos.DrawSphere(matrix.MultiplyPoint3x4(this.center), matrix.MultiplyVector(matrix.MultiplyVector(Vector3.one).normalized).magnitude * this.radius);
				break;
			}
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000D036C File Offset: 0x000CE56C
		public bool Check(ref Matrix4x4 matrix)
		{
			bool flag;
			switch (this.instruction)
			{
			case FitRequirements.Instruction.Raycast:
			{
				Vector3 vector;
				flag = Physics.Raycast(matrix.MultiplyPoint3x4(this.center), vector = matrix.MultiplyVector(this.direction), matrix.MultiplyVector(vector.normalized).magnitude * this.distance, this.mask);
				break;
			}
			case FitRequirements.Instruction.SphereCast:
			{
				Ray ray;
				ray..ctor(matrix.MultiplyPoint3x4(this.center), matrix.MultiplyVector(this.direction));
				float magnitude = matrix.MultiplyVector(ray.direction).magnitude;
				flag = Physics.SphereCast(ray, magnitude * this.radius, magnitude * this.distance, this.mask);
				break;
			}
			case FitRequirements.Instruction.CapsuleCast:
			{
				Vector3 vector = matrix.MultiplyVector(this.direction);
				float magnitude = matrix.MultiplyVector(vector.normalized).magnitude;
				flag = Physics.CapsuleCast(matrix.MultiplyPoint3x4(this.capStart), matrix.MultiplyPoint3x4(this.capEnd), magnitude * this.radius, vector, magnitude * this.distance, this.mask);
				break;
			}
			case FitRequirements.Instruction.CheckCapsule:
				flag = Physics.CheckCapsule(matrix.MultiplyPoint3x4(this.capStart), matrix.MultiplyPoint3x4(this.capEnd), matrix.MultiplyVector(matrix.MultiplyVector(Vector3.one).normalized).magnitude * this.radius, this.mask);
				break;
			case FitRequirements.Instruction.CheckSphere:
				flag = Physics.CheckSphere(matrix.MultiplyPoint3x4(this.center), matrix.MultiplyVector(matrix.MultiplyVector(Vector3.one).normalized).magnitude * this.radius, this.mask);
				break;
			default:
				return true;
			}
			return flag != this.passOnFail;
		}

		// Token: 0x04001CE5 RID: 7397
		[SerializeField]
		private FitRequirements.Instruction instruction;

		// Token: 0x04001CE6 RID: 7398
		[SerializeField]
		private Color flt0;

		// Token: 0x04001CE7 RID: 7399
		[SerializeField]
		private Color flt1;

		// Token: 0x04001CE8 RID: 7400
		[SerializeField]
		private Color flt2;

		// Token: 0x04001CE9 RID: 7401
		[SerializeField]
		private LayerMask mask;

		// Token: 0x04001CEA RID: 7402
		[SerializeField]
		private bool failPass;
	}
}
