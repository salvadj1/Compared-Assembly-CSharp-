using System;
using UnityEngine;

// Token: 0x02000278 RID: 632
public class CCDispatchTest : MonoBehaviour
{
	// Token: 0x06001702 RID: 5890 RVA: 0x000577F0 File Offset: 0x000559F0
	private void Awake()
	{
		this.currentHeight = this.crouchHeight;
		this.totemPole.OnBindPosition += this.BindPositions;
	}

	// Token: 0x06001703 RID: 5891 RVA: 0x00057818 File Offset: 0x00055A18
	private void OnDestroy()
	{
		if (this.totemPole)
		{
			this.totemPole.OnBindPosition -= this.BindPositions;
		}
	}

	// Token: 0x06001704 RID: 5892 RVA: 0x00057844 File Offset: 0x00055A44
	private void BindPositions(ref CCTotem.PositionPlacement placement, object Tag)
	{
		base.transform.position = placement.bottom;
		this.fpsCam.transform.position = placement.top - new Vector3(0f, 0.25f, 0f);
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x00057894 File Offset: 0x00055A94
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		Vector3 motion;
		motion..ctor(Input.GetAxis("Horizontal") * this.horizonalScalar, -this.downwardSpeed, Input.GetAxis("Vertical") * this.horizonalScalar);
		float num = motion.x * motion.x + motion.z * motion.z;
		float num2;
		if (num > this.horizonalScalar * this.horizonalScalar)
		{
			num2 = this.horizonalScalar / Mathf.Sqrt(num) * deltaTime;
		}
		else
		{
			num2 = deltaTime;
		}
		motion.x *= num2;
		motion.z *= num2;
		motion.y *= deltaTime;
		float num3 = (!Input.GetButton("Crouch")) ? this.standingHeight : this.crouchHeight;
		this.currentHeight = Mathf.SmoothDamp(this.currentHeight, num3, ref this.crouchVelocity, this.crouchSmoothing, this.crouchMaxSpeed, deltaTime);
		this.totemPole.Move(motion, this.currentHeight);
	}

	// Token: 0x04000BBD RID: 3005
	[SerializeField]
	private CCTotemPole totemPole;

	// Token: 0x04000BBE RID: 3006
	[SerializeField]
	private Camera fpsCam;

	// Token: 0x04000BBF RID: 3007
	public float crouchHeight = 1.3f;

	// Token: 0x04000BC0 RID: 3008
	public float standingHeight = 2f;

	// Token: 0x04000BC1 RID: 3009
	public float crouchSmoothing = 0.02f;

	// Token: 0x04000BC2 RID: 3010
	public float crouchMaxSpeed = 5f;

	// Token: 0x04000BC3 RID: 3011
	public float horizonalScalar = 4f;

	// Token: 0x04000BC4 RID: 3012
	public float downwardSpeed = 10f;

	// Token: 0x04000BC5 RID: 3013
	[NonSerialized]
	private float crouchVelocity;

	// Token: 0x04000BC6 RID: 3014
	[NonSerialized]
	private float currentHeight;
}
