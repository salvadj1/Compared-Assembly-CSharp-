using System;
using UnityEngine;

// Token: 0x020002A8 RID: 680
[RequireComponent(typeof(CharacterController))]
public sealed class CCDesc : MonoBehaviour
{
	// Token: 0x06001818 RID: 6168 RVA: 0x0005ABFC File Offset: 0x00058DFC
	public CCDesc()
	{
		Vector3 center = default(Vector3);
		center.y = 1f;
		this.m_Center = center;
		base..ctor();
	}

	// Token: 0x170006CB RID: 1739
	// (get) Token: 0x06001819 RID: 6169 RVA: 0x0005AC64 File Offset: 0x00058E64
	public float height
	{
		get
		{
			return this.m_Height;
		}
	}

	// Token: 0x170006CC RID: 1740
	// (get) Token: 0x0600181A RID: 6170 RVA: 0x0005AC6C File Offset: 0x00058E6C
	public float radius
	{
		get
		{
			return this.m_Radius;
		}
	}

	// Token: 0x170006CD RID: 1741
	// (get) Token: 0x0600181B RID: 6171 RVA: 0x0005AC74 File Offset: 0x00058E74
	public Vector3 center
	{
		get
		{
			return this.m_Center;
		}
	}

	// Token: 0x170006CE RID: 1742
	// (get) Token: 0x0600181C RID: 6172 RVA: 0x0005AC7C File Offset: 0x00058E7C
	public float slopeLimit
	{
		get
		{
			return this.m_SlopeLimit;
		}
	}

	// Token: 0x170006CF RID: 1743
	// (get) Token: 0x0600181D RID: 6173 RVA: 0x0005AC84 File Offset: 0x00058E84
	public float stepOffset
	{
		get
		{
			return this.m_StepOffset;
		}
	}

	// Token: 0x170006D0 RID: 1744
	// (get) Token: 0x0600181E RID: 6174 RVA: 0x0005AC8C File Offset: 0x00058E8C
	// (set) Token: 0x0600181F RID: 6175 RVA: 0x0005AC9C File Offset: 0x00058E9C
	public bool detectCollisions
	{
		get
		{
			return this.m_Collider.detectCollisions;
		}
		set
		{
			this.m_Collider.detectCollisions = value;
		}
	}

	// Token: 0x170006D1 RID: 1745
	// (get) Token: 0x06001820 RID: 6176 RVA: 0x0005ACAC File Offset: 0x00058EAC
	public CollisionFlags collisionFlags
	{
		get
		{
			return this.m_Collider.collisionFlags;
		}
	}

	// Token: 0x170006D2 RID: 1746
	// (get) Token: 0x06001821 RID: 6177 RVA: 0x0005ACBC File Offset: 0x00058EBC
	public bool isGrounded
	{
		get
		{
			return this.m_Collider.isGrounded;
		}
	}

	// Token: 0x170006D3 RID: 1747
	// (get) Token: 0x06001822 RID: 6178 RVA: 0x0005ACCC File Offset: 0x00058ECC
	public Vector3 velocity
	{
		get
		{
			return this.m_Collider.velocity;
		}
	}

	// Token: 0x170006D4 RID: 1748
	// (get) Token: 0x06001823 RID: 6179 RVA: 0x0005ACDC File Offset: 0x00058EDC
	public float skinWidth
	{
		get
		{
			return this.m_SkinWidth;
		}
	}

	// Token: 0x170006D5 RID: 1749
	// (get) Token: 0x06001824 RID: 6180 RVA: 0x0005ACE4 File Offset: 0x00058EE4
	public float minMoveDistance
	{
		get
		{
			return this.m_MinMoveDistance;
		}
	}

	// Token: 0x170006D6 RID: 1750
	// (get) Token: 0x06001825 RID: 6181 RVA: 0x0005ACEC File Offset: 0x00058EEC
	public float diameter
	{
		get
		{
			return this.m_Radius + this.m_Radius;
		}
	}

	// Token: 0x170006D7 RID: 1751
	// (get) Token: 0x06001826 RID: 6182 RVA: 0x0005ACFC File Offset: 0x00058EFC
	public float skinnedRadius
	{
		get
		{
			return this.m_Radius + this.m_SkinWidth;
		}
	}

	// Token: 0x170006D8 RID: 1752
	// (get) Token: 0x06001827 RID: 6183 RVA: 0x0005AD0C File Offset: 0x00058F0C
	public float skinnedDiameter
	{
		get
		{
			return this.m_Radius + this.m_Radius + this.m_SkinWidth + this.m_SkinWidth;
		}
	}

	// Token: 0x170006D9 RID: 1753
	// (get) Token: 0x06001828 RID: 6184 RVA: 0x0005AD2C File Offset: 0x00058F2C
	public float effectiveHeight
	{
		get
		{
			float num = this.m_Radius + this.m_Radius;
			return (num <= this.m_Height) ? this.m_Height : num;
		}
	}

	// Token: 0x170006DA RID: 1754
	// (get) Token: 0x06001829 RID: 6185 RVA: 0x0005AD60 File Offset: 0x00058F60
	public float effectiveSkinnedHeight
	{
		get
		{
			float num = this.m_Radius + this.m_Radius;
			return ((num <= this.m_Height) ? this.m_Height : num) + (this.m_SkinWidth + this.m_SkinWidth);
		}
	}

	// Token: 0x170006DB RID: 1755
	// (get) Token: 0x0600182A RID: 6186 RVA: 0x0005ADA4 File Offset: 0x00058FA4
	public float skinnedHeight
	{
		get
		{
			return this.m_Height + this.m_SkinWidth + this.m_SkinWidth;
		}
	}

	// Token: 0x170006DC RID: 1756
	// (get) Token: 0x0600182B RID: 6187 RVA: 0x0005ADBC File Offset: 0x00058FBC
	public Vector3 top
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = this.m_Radius;
			}
			result.y = this.m_Center.y + num;
			return result;
		}
	}

	// Token: 0x170006DD RID: 1757
	// (get) Token: 0x0600182C RID: 6188 RVA: 0x0005AE24 File Offset: 0x00059024
	public Vector3 skinnedTop
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = this.m_Radius;
			}
			result.y = this.m_Center.y + num + this.m_SkinWidth;
			return result;
		}
	}

	// Token: 0x170006DE RID: 1758
	// (get) Token: 0x0600182D RID: 6189 RVA: 0x0005AE94 File Offset: 0x00059094
	public Vector3 bottom
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = this.m_Radius;
			}
			result.y = this.m_Center.y - num;
			return result;
		}
	}

	// Token: 0x170006DF RID: 1759
	// (get) Token: 0x0600182E RID: 6190 RVA: 0x0005AEFC File Offset: 0x000590FC
	public Vector3 skinnedBottom
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = this.m_Radius;
			}
			result.y = this.m_Center.y - (num + this.m_SkinWidth);
			return result;
		}
	}

	// Token: 0x170006E0 RID: 1760
	// (get) Token: 0x0600182F RID: 6191 RVA: 0x0005AF6C File Offset: 0x0005916C
	public Vector3 worldTop
	{
		get
		{
			return this.OffsetToWorld(this.top);
		}
	}

	// Token: 0x170006E1 RID: 1761
	// (get) Token: 0x06001830 RID: 6192 RVA: 0x0005AF7C File Offset: 0x0005917C
	public Vector3 worldSkinnedTop
	{
		get
		{
			return this.OffsetToWorld(this.skinnedTop);
		}
	}

	// Token: 0x170006E2 RID: 1762
	// (get) Token: 0x06001831 RID: 6193 RVA: 0x0005AF8C File Offset: 0x0005918C
	public Vector3 worldCenter
	{
		get
		{
			return this.OffsetToWorld(this.m_Center);
		}
	}

	// Token: 0x170006E3 RID: 1763
	// (get) Token: 0x06001832 RID: 6194 RVA: 0x0005AF9C File Offset: 0x0005919C
	public Vector3 worldBottom
	{
		get
		{
			return this.OffsetToWorld(this.bottom);
		}
	}

	// Token: 0x170006E4 RID: 1764
	// (get) Token: 0x06001833 RID: 6195 RVA: 0x0005AFAC File Offset: 0x000591AC
	public Vector3 worldSkinnedBottom
	{
		get
		{
			return this.OffsetToWorld(this.skinnedBottom);
		}
	}

	// Token: 0x170006E5 RID: 1765
	// (get) Token: 0x06001834 RID: 6196 RVA: 0x0005AFBC File Offset: 0x000591BC
	public Vector3 centroidTop
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = 0f;
			}
			else
			{
				num -= this.m_Radius;
			}
			result.y = this.m_Center.y + num;
			return result;
		}
	}

	// Token: 0x170006E6 RID: 1766
	// (get) Token: 0x06001835 RID: 6197 RVA: 0x0005B030 File Offset: 0x00059230
	public Vector3 centroidBottom
	{
		get
		{
			Vector3 result;
			result.x = this.m_Center.x;
			result.z = this.m_Center.z;
			float num = this.m_Height * 0.5f;
			if (this.m_Radius > num)
			{
				num = 0f;
			}
			else
			{
				num -= this.m_Radius;
			}
			result.y = this.m_Center.y - num;
			return result;
		}
	}

	// Token: 0x170006E7 RID: 1767
	// (get) Token: 0x06001836 RID: 6198 RVA: 0x0005B0A4 File Offset: 0x000592A4
	public Vector3 worldCentroidTop
	{
		get
		{
			return this.OffsetToWorld(this.centroidTop);
		}
	}

	// Token: 0x170006E8 RID: 1768
	// (get) Token: 0x06001837 RID: 6199 RVA: 0x0005B0B4 File Offset: 0x000592B4
	public Vector3 worldCentroidBottom
	{
		get
		{
			return this.OffsetToWorld(this.centroidBottom);
		}
	}

	// Token: 0x170006E9 RID: 1769
	// (get) Token: 0x06001838 RID: 6200 RVA: 0x0005B0C4 File Offset: 0x000592C4
	public Quaternion flatRotation
	{
		get
		{
			Vector3 forward = base.transform.forward;
			forward.y = forward.x * forward.x + forward.z * forward.z;
			if (Mathf.Approximately(forward.y, 0f))
			{
				Vector3 right = base.transform.right;
				forward.z = right.x;
				forward.x = -right.z;
				forward.y = right.x * right.x + right.z * right.z;
			}
			if (forward.y != 1f)
			{
				forward.y = 1f / Mathf.Sqrt(forward.y);
			}
			forward.x *= forward.y;
			forward.z *= forward.z;
			forward.y = 0f;
			return Quaternion.LookRotation(forward, Vector3.up);
		}
	}

	// Token: 0x170006EA RID: 1770
	// (get) Token: 0x06001839 RID: 6201 RVA: 0x0005B1D4 File Offset: 0x000593D4
	public static global::CCDesc Moving
	{
		get
		{
			return global::CCDesc.s_CurrentMovingCCDesc;
		}
	}

	// Token: 0x170006EB RID: 1771
	// (get) Token: 0x0600183A RID: 6202 RVA: 0x0005B1DC File Offset: 0x000593DC
	public Rigidbody attachedRigidbody
	{
		get
		{
			return this.m_Collider.attachedRigidbody;
		}
	}

	// Token: 0x170006EC RID: 1772
	// (get) Token: 0x0600183B RID: 6203 RVA: 0x0005B1EC File Offset: 0x000593EC
	public Bounds bounds
	{
		get
		{
			return this.m_Collider.bounds;
		}
	}

	// Token: 0x170006ED RID: 1773
	// (get) Token: 0x0600183C RID: 6204 RVA: 0x0005B1FC File Offset: 0x000593FC
	// (set) Token: 0x0600183D RID: 6205 RVA: 0x0005B20C File Offset: 0x0005940C
	public bool enabled
	{
		get
		{
			return this.m_Collider.enabled;
		}
		set
		{
			this.m_Collider.enabled = value;
		}
	}

	// Token: 0x170006EE RID: 1774
	// (get) Token: 0x0600183E RID: 6206 RVA: 0x0005B21C File Offset: 0x0005941C
	// (set) Token: 0x0600183F RID: 6207 RVA: 0x0005B22C File Offset: 0x0005942C
	public bool isTrigger
	{
		get
		{
			return this.m_Collider.isTrigger;
		}
		set
		{
			this.m_Collider.isTrigger = value;
		}
	}

	// Token: 0x170006EF RID: 1775
	// (get) Token: 0x06001840 RID: 6208 RVA: 0x0005B23C File Offset: 0x0005943C
	// (set) Token: 0x06001841 RID: 6209 RVA: 0x0005B24C File Offset: 0x0005944C
	public PhysicMaterial material
	{
		get
		{
			return this.m_Collider.material;
		}
		set
		{
			this.m_Collider.material = value;
		}
	}

	// Token: 0x170006F0 RID: 1776
	// (get) Token: 0x06001842 RID: 6210 RVA: 0x0005B25C File Offset: 0x0005945C
	// (set) Token: 0x06001843 RID: 6211 RVA: 0x0005B26C File Offset: 0x0005946C
	public PhysicMaterial sharedMaterial
	{
		get
		{
			return this.m_Collider.sharedMaterial;
		}
		set
		{
			this.m_Collider.sharedMaterial = value;
		}
	}

	// Token: 0x170006F1 RID: 1777
	// (get) Token: 0x06001844 RID: 6212 RVA: 0x0005B27C File Offset: 0x0005947C
	public CharacterController collider
	{
		get
		{
			return this.m_Collider;
		}
	}

	// Token: 0x06001845 RID: 6213 RVA: 0x0005B284 File Offset: 0x00059484
	public CollisionFlags Move(Vector3 motion)
	{
		global::CCDesc ccdesc = global::CCDesc.s_CurrentMovingCCDesc;
		CollisionFlags result;
		try
		{
			global::CCDesc.s_CurrentMovingCCDesc = this;
			if (!object.ReferenceEquals(this.AssignedHitManager, null))
			{
				this.AssignedHitManager.Clear();
			}
			result = this.m_Collider.Move(motion);
		}
		finally
		{
			global::CCDesc.s_CurrentMovingCCDesc = ((!ccdesc) ? null : ccdesc);
		}
		return result;
	}

	// Token: 0x06001846 RID: 6214 RVA: 0x0005B308 File Offset: 0x00059508
	public bool SimpleMove(Vector3 speed)
	{
		global::CCDesc ccdesc = global::CCDesc.s_CurrentMovingCCDesc;
		bool result;
		try
		{
			global::CCDesc.s_CurrentMovingCCDesc = this;
			result = this.m_Collider.SimpleMove(speed);
		}
		finally
		{
			global::CCDesc.s_CurrentMovingCCDesc = ((!ccdesc) ? null : ccdesc);
		}
		return result;
	}

	// Token: 0x06001847 RID: 6215 RVA: 0x0005B370 File Offset: 0x00059570
	public Vector3 ClosestPointOnBounds(Vector3 position)
	{
		return this.m_Collider.ClosestPointOnBounds(position);
	}

	// Token: 0x06001848 RID: 6216 RVA: 0x0005B380 File Offset: 0x00059580
	public bool Raycast(Ray ray, out RaycastHit hitInfo, float distance)
	{
		return this.m_Collider.Raycast(ray, ref hitInfo, distance);
	}

	// Token: 0x06001849 RID: 6217 RVA: 0x0005B390 File Offset: 0x00059590
	public Vector3 OffsetToWorld(Vector3 offset)
	{
		if (offset.x != 0f || offset.z != 0f)
		{
			offset = this.flatRotation * offset;
		}
		Vector3 lossyScale = base.transform.lossyScale;
		offset.x *= lossyScale.x;
		offset.y *= lossyScale.y;
		offset.z *= lossyScale.z;
		Vector3 position = base.transform.position;
		offset.x += position.x;
		offset.y += position.y;
		offset.z += position.z;
		return offset;
	}

	// Token: 0x0600184A RID: 6218 RVA: 0x0005B464 File Offset: 0x00059664
	public global::CCDesc.HeightModification ModifyHeight(float newEffectiveSkinnedHeight, bool preview = false)
	{
		float num = this.m_Radius + this.m_Radius;
		float num2 = this.m_SkinWidth + this.m_SkinWidth + num;
		float num3 = (num <= this.m_Height) ? (this.m_Height + this.m_SkinWidth + this.m_SkinWidth) : num2;
		global::CCDesc.HeightModification result;
		result.original.effectiveSkinnedHeight = num3;
		result.original.center = this.m_Center;
		if (newEffectiveSkinnedHeight < num2)
		{
			result.modified.effectiveSkinnedHeight = num2;
		}
		else
		{
			result.modified.effectiveSkinnedHeight = newEffectiveSkinnedHeight;
		}
		if (result.differed = (result.original.effectiveSkinnedHeight != result.modified.effectiveSkinnedHeight))
		{
			float num4 = num3 * 0.5f;
			float num5 = result.original.center.y - num4;
			float num6 = result.original.center.y + num4;
			result.delta.effectiveSkinnedHeight = result.modified.effectiveSkinnedHeight - result.original.effectiveSkinnedHeight;
			result.scale = result.modified.effectiveSkinnedHeight / result.original.effectiveSkinnedHeight;
			float num7 = num5 * result.scale;
			float num8 = num6 * result.scale;
			result.modified.center.x = result.original.center.x;
			result.modified.center.z = result.original.center.z;
			result.modified.center.y = num7 + (num8 - num7) * 0.5f;
			result.delta.center.x = 0f;
			result.delta.center.z = 0f;
			result.delta.center.y = result.modified.center.y - result.original.center.y;
			if (result.applied = !preview)
			{
				this.m_Height = result.modified.effectiveSkinnedHeight - (this.m_SkinWidth + this.m_SkinWidth);
				this.m_Center = result.modified.center;
				if (result.scale < 1f)
				{
					this.m_Collider.center = this.m_Center;
					this.m_Collider.height = this.m_Height;
				}
				else
				{
					this.m_Collider.height = this.m_Height;
					this.m_Collider.center = this.m_Center;
				}
			}
		}
		else
		{
			result.modified = result.original;
			result.delta = default(global::CCDesc.HeightModification.State);
			result.applied = false;
			result.scale = 1f;
		}
		return result;
	}

	// Token: 0x04000CC3 RID: 3267
	[SerializeField]
	private float m_Height = 2f;

	// Token: 0x04000CC4 RID: 3268
	[SerializeField]
	private float m_Radius = 0.4f;

	// Token: 0x04000CC5 RID: 3269
	[SerializeField]
	private float m_SlopeLimit = 90f;

	// Token: 0x04000CC6 RID: 3270
	[SerializeField]
	private float m_StepOffset = 0.5f;

	// Token: 0x04000CC7 RID: 3271
	[SerializeField]
	private float m_SkinWidth = 0.05f;

	// Token: 0x04000CC8 RID: 3272
	[SerializeField]
	private float m_MinMoveDistance;

	// Token: 0x04000CC9 RID: 3273
	[SerializeField]
	private Vector3 m_Center;

	// Token: 0x04000CCA RID: 3274
	[PrefetchComponent]
	[SerializeField]
	[HideInInspector]
	private CharacterController m_Collider;

	// Token: 0x04000CCB RID: 3275
	[NonSerialized]
	public object Tag;

	// Token: 0x04000CCC RID: 3276
	private static global::CCDesc s_CurrentMovingCCDesc;

	// Token: 0x04000CCD RID: 3277
	[NonSerialized]
	internal global::CCDesc.HitManager AssignedHitManager;

	// Token: 0x020002A9 RID: 681
	public struct HeightModification
	{
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x0005B758 File Offset: 0x00059958
		public float bottomDeltaHeight
		{
			get
			{
				return this.modified.skinnedBottomY - this.original.skinnedBottomY;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600184C RID: 6220 RVA: 0x0005B774 File Offset: 0x00059974
		public float topDeltaHeight
		{
			get
			{
				return this.modified.skinnedTopY - this.original.skinnedTopY;
			}
		}

		// Token: 0x04000CCE RID: 3278
		public global::CCDesc.HeightModification.State original;

		// Token: 0x04000CCF RID: 3279
		public global::CCDesc.HeightModification.State modified;

		// Token: 0x04000CD0 RID: 3280
		public global::CCDesc.HeightModification.State delta;

		// Token: 0x04000CD1 RID: 3281
		public float scale;

		// Token: 0x04000CD2 RID: 3282
		public bool differed;

		// Token: 0x04000CD3 RID: 3283
		public bool applied;

		// Token: 0x020002AA RID: 682
		public struct State
		{
			// Token: 0x170006F4 RID: 1780
			// (get) Token: 0x0600184D RID: 6221 RVA: 0x0005B790 File Offset: 0x00059990
			public float skinnedBottomY
			{
				get
				{
					return this.center.y - this.effectiveSkinnedHeight * 0.5f;
				}
			}

			// Token: 0x170006F5 RID: 1781
			// (get) Token: 0x0600184E RID: 6222 RVA: 0x0005B7AC File Offset: 0x000599AC
			public float skinnedTopY
			{
				get
				{
					return this.center.y + this.effectiveSkinnedHeight * 0.5f;
				}
			}

			// Token: 0x04000CD4 RID: 3284
			public float effectiveSkinnedHeight;

			// Token: 0x04000CD5 RID: 3285
			public Vector3 center;
		}
	}

	// Token: 0x020002AB RID: 683
	public struct Hit
	{
		// Token: 0x0600184F RID: 6223 RVA: 0x0005B7C8 File Offset: 0x000599C8
		public Hit(ControllerColliderHit ControllerColliderHit)
		{
			this.CharacterController = ControllerColliderHit.controller;
			global::CCDesc s_CurrentMovingCCDesc = global::CCDesc.s_CurrentMovingCCDesc;
			if (!s_CurrentMovingCCDesc || s_CurrentMovingCCDesc.collider != this.CharacterController)
			{
				this.CCDesc = this.CharacterController.GetComponent<global::CCDesc>();
			}
			else
			{
				this.CCDesc = s_CurrentMovingCCDesc;
			}
			this.Collider = ControllerColliderHit.collider;
			this.Point = ControllerColliderHit.point;
			this.Normal = ControllerColliderHit.normal;
			this.MoveDirection = ControllerColliderHit.moveDirection;
			this.MoveLength = ControllerColliderHit.moveLength;
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x0005B864 File Offset: 0x00059A64
		public GameObject GameObject
		{
			get
			{
				return (!this.Collider) ? null : this.Collider.transform.gameObject;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0005B898 File Offset: 0x00059A98
		public Transform Transform
		{
			get
			{
				return (!this.Collider) ? null : this.Collider.transform;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0005B8BC File Offset: 0x00059ABC
		public Rigidbody Rigidbody
		{
			get
			{
				return (!this.Collider) ? null : this.Collider.attachedRigidbody;
			}
		}

		// Token: 0x04000CD6 RID: 3286
		public readonly CharacterController CharacterController;

		// Token: 0x04000CD7 RID: 3287
		public readonly global::CCDesc CCDesc;

		// Token: 0x04000CD8 RID: 3288
		public readonly Collider Collider;

		// Token: 0x04000CD9 RID: 3289
		public readonly Vector3 Point;

		// Token: 0x04000CDA RID: 3290
		public readonly Vector3 Normal;

		// Token: 0x04000CDB RID: 3291
		public readonly Vector3 MoveDirection;

		// Token: 0x04000CDC RID: 3292
		public readonly float MoveLength;
	}

	// Token: 0x020002AC RID: 684
	public class HitManager : IDisposable
	{
		// Token: 0x06001853 RID: 6227 RVA: 0x0005B8E0 File Offset: 0x00059AE0
		public HitManager(int bufferSize)
		{
			this.bufferSize = bufferSize;
			this.buffer = new global::CCDesc.Hit[bufferSize];
			this.filledCount = 0;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0005B910 File Offset: 0x00059B10
		public HitManager() : this(8)
		{
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001855 RID: 6229 RVA: 0x0005B91C File Offset: 0x00059B1C
		// (remove) Token: 0x06001856 RID: 6230 RVA: 0x0005B938 File Offset: 0x00059B38
		public event global::CCDesc.HitFilter OnHit;

		// Token: 0x06001857 RID: 6231 RVA: 0x0005B954 File Offset: 0x00059B54
		public bool Push(ControllerColliderHit cchit)
		{
			if (this.issuingEvent)
			{
				Debug.LogError("Push during event call back");
				return false;
			}
			if (!object.ReferenceEquals(cchit, null))
			{
				global::CCDesc.Hit hit = new global::CCDesc.Hit(cchit);
				return this.Push(ref hit);
			}
			return false;
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0005B998 File Offset: 0x00059B98
		public bool Push(ref global::CCDesc.Hit evnt)
		{
			if (this.issuingEvent)
			{
				Debug.LogError("Push during event call back");
				return false;
			}
			global::CCDesc.HitFilter onHit = this.OnHit;
			if (onHit != null)
			{
				bool flag = false;
				try
				{
					this.issuingEvent = true;
					flag = !onHit(this, ref evnt);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				finally
				{
					this.issuingEvent = false;
				}
				if (flag)
				{
					return false;
				}
			}
			int num = this.filledCount++;
			if (this.filledCount > this.bufferSize)
			{
				do
				{
					this.bufferSize += 8;
				}
				while (this.filledCount > this.bufferSize);
				if (this.filledCount > 1)
				{
					global::CCDesc.Hit[] sourceArray = this.buffer;
					this.buffer = new global::CCDesc.Hit[this.bufferSize];
					Array.Copy(sourceArray, this.buffer, this.filledCount - 1);
				}
				else
				{
					this.buffer = new global::CCDesc.Hit[this.bufferSize];
				}
			}
			this.buffer[num] = evnt;
			return true;
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0005BAE0 File Offset: 0x00059CE0
		public void Clear()
		{
			while (this.filledCount > 0)
			{
				this.buffer[--this.filledCount] = default(global::CCDesc.Hit);
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x0005BB28 File Offset: 0x00059D28
		public int Count
		{
			get
			{
				return this.filledCount;
			}
		}

		// Token: 0x170006FA RID: 1786
		public global::CCDesc.Hit this[int i]
		{
			get
			{
				if (i < 0 || i >= this.filledCount)
				{
					throw new ArgumentOutOfRangeException("i");
				}
				return this.buffer[i];
			}
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0005BB64 File Offset: 0x00059D64
		public void Dispose()
		{
			this.buffer = null;
			this.OnHit = null;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0005BB74 File Offset: 0x00059D74
		public void CopyTo(global::CCDesc.Hit[] array, int startIndex = 0)
		{
			for (int i = 0; i < this.filledCount; i++)
			{
				array[startIndex++] = this.buffer[i];
			}
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0005BBBC File Offset: 0x00059DBC
		public global::CCDesc.Hit[] ToArray()
		{
			global::CCDesc.Hit[] array = new global::CCDesc.Hit[this.filledCount];
			this.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04000CDD RID: 3293
		private global::CCDesc.Hit[] buffer;

		// Token: 0x04000CDE RID: 3294
		private int bufferSize;

		// Token: 0x04000CDF RID: 3295
		private int filledCount;

		// Token: 0x04000CE0 RID: 3296
		private bool issuingEvent;

		// Token: 0x04000CE1 RID: 3297
		public object Tag;
	}

	// Token: 0x020002AD RID: 685
	// (Invoke) Token: 0x06001860 RID: 6240
	public delegate bool HitFilter(global::CCDesc.HitManager hitManager, ref global::CCDesc.Hit hit);
}
