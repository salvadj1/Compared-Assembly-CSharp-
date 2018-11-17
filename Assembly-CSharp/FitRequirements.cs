using System;
using UnityEngine;

// Token: 0x0200071D RID: 1821
public class FitRequirements : ScriptableObject
{
	// Token: 0x06003C91 RID: 15505 RVA: 0x000D871C File Offset: 0x000D691C
	public bool Test(Matrix4x4 placePosition)
	{
		if (!object.ReferenceEquals(this.Conditions, null))
		{
			foreach (global::FitRequirements.Condition condition in this.Conditions)
			{
				if (!condition.Check(ref placePosition))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x06003C92 RID: 15506 RVA: 0x000D876C File Offset: 0x000D696C
	public bool Test(Vector3 origin, Quaternion rotation, Vector3 scale)
	{
		return this.Test(Matrix4x4.TRS(origin, rotation, scale));
	}

	// Token: 0x06003C93 RID: 15507 RVA: 0x000D877C File Offset: 0x000D697C
	public bool Test(Vector3 origin, Quaternion rotation)
	{
		return this.Test(Matrix4x4.TRS(origin, rotation, Vector3.one));
	}

	// Token: 0x04001ED5 RID: 7893
	[SerializeField]
	private global::FitRequirements.Condition[] Conditions;

	// Token: 0x04001ED6 RID: 7894
	[HideInInspector]
	[SerializeField]
	private string assetPreview;

	// Token: 0x0200071E RID: 1822
	public enum Instruction
	{
		// Token: 0x04001ED8 RID: 7896
		Raycast,
		// Token: 0x04001ED9 RID: 7897
		SphereCast,
		// Token: 0x04001EDA RID: 7898
		CapsuleCast,
		// Token: 0x04001EDB RID: 7899
		CheckCapsule,
		// Token: 0x04001EDC RID: 7900
		CheckSphere
	}

	// Token: 0x0200071F RID: 1823
	[Serializable]
	public class Condition
	{
		// Token: 0x06003C94 RID: 15508 RVA: 0x000D8790 File Offset: 0x000D6990
		public Condition()
		{
			this.flt0.a = 1f;
			this.flt1 = Vector3.up;
			this.flt1.a = 0.5f;
			this.flt2 = Vector3.up;
			this.mask = 536871936;
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06003C95 RID: 15509 RVA: 0x000D8800 File Offset: 0x000D6A00
		// (set) Token: 0x06003C96 RID: 15510 RVA: 0x000D8814 File Offset: 0x000D6A14
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

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06003C97 RID: 15511 RVA: 0x000D8858 File Offset: 0x000D6A58
		// (set) Token: 0x06003C98 RID: 15512 RVA: 0x000D886C File Offset: 0x000D6A6C
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

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06003C99 RID: 15513 RVA: 0x000D88B0 File Offset: 0x000D6AB0
		// (set) Token: 0x06003C9A RID: 15514 RVA: 0x000D88C0 File Offset: 0x000D6AC0
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

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06003C9B RID: 15515 RVA: 0x000D88D0 File Offset: 0x000D6AD0
		// (set) Token: 0x06003C9C RID: 15516 RVA: 0x000D88E0 File Offset: 0x000D6AE0
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

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06003C9D RID: 15517 RVA: 0x000D88F0 File Offset: 0x000D6AF0
		// (set) Token: 0x06003C9E RID: 15518 RVA: 0x000D8904 File Offset: 0x000D6B04
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

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x000D8948 File Offset: 0x000D6B48
		// (set) Token: 0x06003CA0 RID: 15520 RVA: 0x000D895C File Offset: 0x000D6B5C
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

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x000D89A0 File Offset: 0x000D6BA0
		// (set) Token: 0x06003CA2 RID: 15522 RVA: 0x000D89A8 File Offset: 0x000D6BA8
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

		// Token: 0x06003CA3 RID: 15523 RVA: 0x000D89B4 File Offset: 0x000D6BB4
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
			global::Gizmos2.DrawWireCapsule(new Vector3(0f, 0f, num5 * 0.5f - radius), radius, num5, 2);
			Gizmos.matrix = matrix2;
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x000D8ACC File Offset: 0x000D6CCC
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
			global::Gizmos2.DrawWireCapsule(new Vector3(0f, 0f, magnitude2 * 0.5f), radius, magnitude2, 2);
			Gizmos.matrix = matrix2;
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x000D8BAC File Offset: 0x000D6DAC
		public void DrawGizmo(ref Matrix4x4 matrix)
		{
			switch (this.instruction)
			{
			case global::FitRequirements.Instruction.Raycast:
			{
				Vector3 vector = matrix.MultiplyPoint3x4(this.center);
				Vector3 normalized = matrix.MultiplyVector(this.direction).normalized;
				Gizmos.DrawLine(vector, vector + normalized * (matrix.MultiplyVector(normalized).magnitude * this.distance));
				break;
			}
			case global::FitRequirements.Instruction.SphereCast:
				global::FitRequirements.Condition.GizmoCapsuleAxis(ref matrix, this.center, this.radius, this.distance, this.direction, null, null);
				break;
			case global::FitRequirements.Instruction.CapsuleCast:
				global::FitRequirements.Condition.GizmoCapsuleAxis(ref matrix, this.capStart, this.radius, this.distance, this.direction, null, null);
				global::FitRequirements.Condition.GizmoCapsuleAxis(ref matrix, this.capEnd, this.radius, this.distance, this.direction, null, null);
				global::FitRequirements.Condition.GizmoCapsulePoles(ref matrix, this.capStart, this.radius, this.capEnd);
				break;
			case global::FitRequirements.Instruction.CheckCapsule:
				global::FitRequirements.Condition.GizmoCapsulePoles(ref matrix, this.capStart, this.radius, this.capEnd);
				break;
			case global::FitRequirements.Instruction.CheckSphere:
				Gizmos.DrawSphere(matrix.MultiplyPoint3x4(this.center), matrix.MultiplyVector(matrix.MultiplyVector(Vector3.one).normalized).magnitude * this.radius);
				break;
			}
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x000D8D4C File Offset: 0x000D6F4C
		public bool Check(ref Matrix4x4 matrix)
		{
			bool flag;
			switch (this.instruction)
			{
			case global::FitRequirements.Instruction.Raycast:
			{
				Vector3 vector;
				flag = Physics.Raycast(matrix.MultiplyPoint3x4(this.center), vector = matrix.MultiplyVector(this.direction), matrix.MultiplyVector(vector.normalized).magnitude * this.distance, this.mask);
				break;
			}
			case global::FitRequirements.Instruction.SphereCast:
			{
				Ray ray;
				ray..ctor(matrix.MultiplyPoint3x4(this.center), matrix.MultiplyVector(this.direction));
				float magnitude = matrix.MultiplyVector(ray.direction).magnitude;
				flag = Physics.SphereCast(ray, magnitude * this.radius, magnitude * this.distance, this.mask);
				break;
			}
			case global::FitRequirements.Instruction.CapsuleCast:
			{
				Vector3 vector = matrix.MultiplyVector(this.direction);
				float magnitude = matrix.MultiplyVector(vector.normalized).magnitude;
				flag = Physics.CapsuleCast(matrix.MultiplyPoint3x4(this.capStart), matrix.MultiplyPoint3x4(this.capEnd), magnitude * this.radius, vector, magnitude * this.distance, this.mask);
				break;
			}
			case global::FitRequirements.Instruction.CheckCapsule:
				flag = Physics.CheckCapsule(matrix.MultiplyPoint3x4(this.capStart), matrix.MultiplyPoint3x4(this.capEnd), matrix.MultiplyVector(matrix.MultiplyVector(Vector3.one).normalized).magnitude * this.radius, this.mask);
				break;
			case global::FitRequirements.Instruction.CheckSphere:
				flag = Physics.CheckSphere(matrix.MultiplyPoint3x4(this.center), matrix.MultiplyVector(matrix.MultiplyVector(Vector3.one).normalized).magnitude * this.radius, this.mask);
				break;
			default:
				return true;
			}
			return flag != this.passOnFail;
		}

		// Token: 0x04001EDD RID: 7901
		[SerializeField]
		private global::FitRequirements.Instruction instruction;

		// Token: 0x04001EDE RID: 7902
		[SerializeField]
		private Color flt0;

		// Token: 0x04001EDF RID: 7903
		[SerializeField]
		private Color flt1;

		// Token: 0x04001EE0 RID: 7904
		[SerializeField]
		private Color flt2;

		// Token: 0x04001EE1 RID: 7905
		[SerializeField]
		private LayerMask mask;

		// Token: 0x04001EE2 RID: 7906
		[SerializeField]
		private bool failPass;
	}
}
