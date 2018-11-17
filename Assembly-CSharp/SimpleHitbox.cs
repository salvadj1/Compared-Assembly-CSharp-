using System;
using UnityEngine;

// Token: 0x0200054D RID: 1357
public class SimpleHitbox : global::BaseHitBox
{
	// Token: 0x06002D63 RID: 11619 RVA: 0x000AB390 File Offset: 0x000A9590
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

	// Token: 0x06002D64 RID: 11620 RVA: 0x000AB404 File Offset: 0x000A9604
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

	// Token: 0x06002D65 RID: 11621 RVA: 0x000AB4C8 File Offset: 0x000A96C8
	private void Update()
	{
		if (!this.fixedUpdate)
		{
			this.Snap();
		}
	}

	// Token: 0x06002D66 RID: 11622 RVA: 0x000AB4DC File Offset: 0x000A96DC
	private void FixedUpdate()
	{
		if (this.fixedUpdate)
		{
			this.Snap();
		}
	}

	// Token: 0x04001750 RID: 5968
	private bool oldCrouch;

	// Token: 0x04001751 RID: 5969
	private CapsuleCollider myCap;

	// Token: 0x04001752 RID: 5970
	public bool fixedUpdate;

	// Token: 0x04001753 RID: 5971
	private Transform parent;

	// Token: 0x04001754 RID: 5972
	private Transform root;

	// Token: 0x04001755 RID: 5973
	private Vector3 offset;

	// Token: 0x04001756 RID: 5974
	public float crouchHeight = 1f;

	// Token: 0x04001757 RID: 5975
	public float standingHeight = 1.85f;

	// Token: 0x04001758 RID: 5976
	private Transform rootTransform;

	// Token: 0x04001759 RID: 5977
	private Transform myTransform;
}
