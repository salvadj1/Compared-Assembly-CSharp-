using System;
using UnityEngine;

// Token: 0x020006DF RID: 1759
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement_Mecanim : MonoBehaviour
{
	// Token: 0x06003B7B RID: 15227 RVA: 0x000D45D0 File Offset: 0x000D27D0
	private void Start()
	{
		this.playerController = base.GetComponent<CharacterController>();
		this.playerAnimController = base.GetComponent<Animator>();
	}

	// Token: 0x06003B7C RID: 15228 RVA: 0x000D45EC File Offset: 0x000D27EC
	private void Update()
	{
		this.SetUpperBodyAimState();
		if (Input.GetKey(99))
		{
			this.bCrouching = true;
		}
		else
		{
			this.bCrouching = false;
		}
		this.playerAnimController.SetBool("Crouching", this.bCrouching);
		if (Input.GetKey(304) && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f && !this.bCrouching && Input.GetAxis("Vertical") > 0.1f)
		{
			this.bSprinting = true;
		}
		else
		{
			this.bSprinting = false;
		}
		if (Input.GetKey(32) && this.playerController.isGrounded && !this.bCrouching)
		{
			this.bIsInAir = true;
			this.playerAnimController.SetTrigger("Jump");
			this.playerController.Move(Vector3.up * 1.5f);
		}
		if (this.bIsInAir)
		{
			this.CheckLanding();
		}
		if (Input.GetKeyDown(323) && this.flCanAttackAgainTime <= Time.time)
		{
			this.flCanAttackAgainTime = Time.time + this.flAttackTimers[this.iUpperBodyAimState];
			this.playerAnimController.SetTrigger("Attack");
		}
		this.playerAnimController.SetFloat("Move_ForwardBack", Input.GetAxis("Vertical"));
		this.playerAnimController.SetFloat("Move_Strafe", Input.GetAxis("Horizontal"));
		this.flPlayerAimPitch += Input.GetAxis("Mouse Y") * this.flRotateSpeed;
		this.flPlayerAimPitch = Mathf.Clamp(this.flPlayerAimPitch, -55f, 55f);
		this.playerAnimController.SetFloat("Aim_Vertical", this.flPlayerAimPitch);
		if (this.bSprinting)
		{
			this.flUpperBodyAimLayerWeight = Mathf.Lerp(this.flUpperBodyAimLayerWeight, 0f, Time.deltaTime * 6f);
			this.playerAnimController.SetBool("Sprinting", true);
		}
		else
		{
			if (this.iUpperBodyAimState == 0)
			{
				this.flUpperBodyAimLayerWeight = Mathf.Lerp(this.flUpperBodyAimLayerWeight, 0f, Time.deltaTime * 6f);
			}
			else
			{
				this.flUpperBodyAimLayerWeight = Mathf.Lerp(this.flUpperBodyAimLayerWeight, 1f, Time.deltaTime * 6f);
			}
			this.playerAnimController.SetBool("Sprinting", false);
		}
		this.playerAnimController.SetLayerWeight(1, this.flUpperBodyAimLayerWeight);
		Vector3 vector = base.transform.TransformDirection(Vector3.forward);
		vector *= Input.GetAxis("Vertical");
		Vector3 vector2 = base.transform.TransformDirection(Vector3.right);
		vector2 *= Input.GetAxis("Horizontal");
		if (this.bSprinting)
		{
			this.playerController.SimpleMove(vector * this.flSprintSpeed);
		}
		else if (this.bCrouching)
		{
			this.playerController.SimpleMove(vector * this.flCrouchWalkSpeed + vector2 * this.flCrouchWalkSpeed * 0.85f);
		}
		else
		{
			this.playerController.SimpleMove(vector * this.flWalkSpeed + vector2 * this.flWalkSpeed * 0.75f);
		}
	}

	// Token: 0x06003B7D RID: 15229 RVA: 0x000D495C File Offset: 0x000D2B5C
	private void CheckLanding()
	{
		if (this.playerController.isGrounded)
		{
			this.bIsInAir = false;
			this.playerAnimController.SetTrigger("Land");
		}
	}

	// Token: 0x06003B7E RID: 15230 RVA: 0x000D4988 File Offset: 0x000D2B88
	private void SetUpperBodyAimState()
	{
		if (Input.GetKeyDown(49))
		{
			this.iUpperBodyAimState = 1;
		}
		else if (Input.GetKeyDown(50))
		{
			this.iUpperBodyAimState = 2;
		}
		else if (Input.GetKeyDown(51))
		{
			this.iUpperBodyAimState = 3;
		}
		else if (Input.GetKeyDown(52))
		{
			this.iUpperBodyAimState = 4;
		}
		else if (Input.GetKeyDown(53))
		{
			this.iUpperBodyAimState = 5;
		}
		else if (Input.GetKeyDown(48))
		{
			this.iUpperBodyAimState = 0;
		}
		this.playerAnimController.SetInteger("UpperBodyAimState", this.iUpperBodyAimState);
	}

	// Token: 0x04001DA6 RID: 7590
	public float flSprintSpeed = 6.2f;

	// Token: 0x04001DA7 RID: 7591
	public float flWalkSpeed = 2.55f;

	// Token: 0x04001DA8 RID: 7592
	public float flCrouchWalkSpeed = 1.54f;

	// Token: 0x04001DA9 RID: 7593
	public float flRotateSpeed = 9f;

	// Token: 0x04001DAA RID: 7594
	public int iUpperBodyAimState;

	// Token: 0x04001DAB RID: 7595
	private float flPlayerAimPitch;

	// Token: 0x04001DAC RID: 7596
	private float flUpperBodyAimLayerWeight = 1f;

	// Token: 0x04001DAD RID: 7597
	private float[] flAttackTimers = new float[]
	{
		0f,
		1f,
		0.1f,
		0.1f,
		1f,
		1f
	};

	// Token: 0x04001DAE RID: 7598
	private float flCanAttackAgainTime = -1f;

	// Token: 0x04001DAF RID: 7599
	private bool bIsInAir;

	// Token: 0x04001DB0 RID: 7600
	private bool bCrouching;

	// Token: 0x04001DB1 RID: 7601
	private bool bSprinting;

	// Token: 0x04001DB2 RID: 7602
	private CharacterController playerController;

	// Token: 0x04001DB3 RID: 7603
	private Animator playerAnimController;
}
