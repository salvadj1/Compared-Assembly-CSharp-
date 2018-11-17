using System;
using UnityEngine;

// Token: 0x020002AE RID: 686
public class CCDispatchTest : MonoBehaviour
{
	// Token: 0x06001864 RID: 6244 RVA: 0x0005BC38 File Offset: 0x00059E38
	private void Awake()
	{
		this.currentHeight = this.crouchHeight;
		this.totemPole.OnBindPosition += this.BindPositions;
	}

	// Token: 0x06001865 RID: 6245 RVA: 0x0005BC60 File Offset: 0x00059E60
	private void OnDestroy()
	{
		if (this.totemPole)
		{
			this.totemPole.OnBindPosition -= this.BindPositions;
		}
	}

	// Token: 0x06001866 RID: 6246 RVA: 0x0005BC8C File Offset: 0x00059E8C
	private void BindPositions(ref global::CCTotem.PositionPlacement placement, object Tag)
	{
		base.transform.position = placement.bottom;
		this.fpsCam.transform.position = placement.top - new Vector3(0f, 0.25f, 0f);
	}

	// Token: 0x06001867 RID: 6247 RVA: 0x0005BCDC File Offset: 0x00059EDC
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

	// Token: 0x04000CE3 RID: 3299
	[SerializeField]
	private global::CCTotemPole totemPole;

	// Token: 0x04000CE4 RID: 3300
	[SerializeField]
	private Camera fpsCam;

	// Token: 0x04000CE5 RID: 3301
	public float crouchHeight = 1.3f;

	// Token: 0x04000CE6 RID: 3302
	public float standingHeight = 2f;

	// Token: 0x04000CE7 RID: 3303
	public float crouchSmoothing = 0.02f;

	// Token: 0x04000CE8 RID: 3304
	public float crouchMaxSpeed = 5f;

	// Token: 0x04000CE9 RID: 3305
	public float horizonalScalar = 4f;

	// Token: 0x04000CEA RID: 3306
	public float downwardSpeed = 10f;

	// Token: 0x04000CEB RID: 3307
	[NonSerialized]
	private float crouchVelocity;

	// Token: 0x04000CEC RID: 3308
	[NonSerialized]
	private float currentHeight;
}
