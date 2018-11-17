using System;
using UnityEngine;

// Token: 0x02000492 RID: 1170
public class SimpleHitbox : BaseHitBox
{
	// Token: 0x060029B1 RID: 10673 RVA: 0x000A35F8 File Offset: 0x000A17F8
	private void Start()
	{
		this.myCap = (base.collider as CapsuleCollider);
		this.parent = base.transform.parent;
		this.root = this.parent.root;
		this.offset = Vector3.zero;
		base.transform.parent = null;
		this.rootTransform = this.root.transform;
		this.myTransform = base.transform;
	}

	// Token: 0x060029B2 RID: 10674 RVA: 0x000A366C File Offset: 0x000A186C
	private void Snap()
	{
		if (base.idMain.stateFlags.crouch != this.oldCrouch)
		{
			if (base.idMain.stateFlags.crouch)
			{
				this.myCap.height = this.crouchHeight;
			}
			else
			{
				this.myCap.height = this.standingHeight;
			}
			this.oldCrouch = base.idMain.stateFlags.crouch;
		}
		Vector3 position = this.parent.TransformPoint(this.offset);
		position.y = this.rootTransform.position.y + this.myCap.height * 0.5f;
		this.myTransform.position = position;
	}

	// Token: 0x060029B3 RID: 10675 RVA: 0x000A3730 File Offset: 0x000A1930
	private void Update()
	{
		if (!this.fixedUpdate)
		{
			this.Snap();
		}
	}

	// Token: 0x060029B4 RID: 10676 RVA: 0x000A3744 File Offset: 0x000A1944
	private void FixedUpdate()
	{
		if (this.fixedUpdate)
		{
			this.Snap();
		}
	}

	// Token: 0x04001593 RID: 5523
	private bool oldCrouch;

	// Token: 0x04001594 RID: 5524
	private CapsuleCollider myCap;

	// Token: 0x04001595 RID: 5525
	public bool fixedUpdate;

	// Token: 0x04001596 RID: 5526
	private Transform parent;

	// Token: 0x04001597 RID: 5527
	private Transform root;

	// Token: 0x04001598 RID: 5528
	private Vector3 offset;

	// Token: 0x04001599 RID: 5529
	public float crouchHeight = 1f;

	// Token: 0x0400159A RID: 5530
	public float standingHeight = 1.85f;

	// Token: 0x0400159B RID: 5531
	private Transform rootTransform;

	// Token: 0x0400159C RID: 5532
	private Transform myTransform;
}
