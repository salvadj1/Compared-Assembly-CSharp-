using System;
using UnityEngine;

// Token: 0x0200061E RID: 1566
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement_Mecanim : MonoBehaviour
{
	// Token: 0x0600379B RID: 14235 RVA: 0x000CBEF8 File Offset: 0x000CA0F8
	private void Start()
	{
		this.playerController = base.GetComponent<CharacterController>();
		this.playerAnimController = base.GetComponent<Animator>();
	}

	// Token: 0x0600379C RID: 14236 RVA: 0x000CBF14 File Offset: 0x000CA114
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

	// Token: 0x0600379D RID: 14237 RVA: 0x000CC284 File Offset: 0x000CA484
	private void CheckLanding()
	{
		if (this.playerController.isGrounded)
		{
			this.bIsInAir = false;
			this.playerAnimController.SetTrigger("Land");
		}
	}

	// Token: 0x0600379E RID: 14238 RVA: 0x000CC2B0 File Offset: 0x000CA4B0
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

	// Token: 0x04001BBB RID: 7099
	public float flSprintSpeed = 6.2f;

	// Token: 0x04001BBC RID: 7100
	public float flWalkSpeed = 2.55f;

	// Token: 0x04001BBD RID: 7101
	public float flCrouchWalkSpeed = 1.54f;

	// Token: 0x04001BBE RID: 7102
	public float flRotateSpeed = 9f;

	// Token: 0x04001BBF RID: 7103
	public int iUpperBodyAimState;

	// Token: 0x04001BC0 RID: 7104
	private float flPlayerAimPitch;

	// Token: 0x04001BC1 RID: 7105
	private float flUpperBodyAimLayerWeight = 1f;

	// Token: 0x04001BC2 RID: 7106
	private float[] flAttackTimers = new float[]
	{
		0f,
		1f,
		0.1f,
		0.1f,
		1f,
		1f
	};

	// Token: 0x04001BC3 RID: 7107
	private float flCanAttackAgainTime = -1f;

	// Token: 0x04001BC4 RID: 7108
	private bool bIsInAir;

	// Token: 0x04001BC5 RID: 7109
	private bool bCrouching;

	// Token: 0x04001BC6 RID: 7110
	private bool bSprinting;

	// Token: 0x04001BC7 RID: 7111
	private CharacterController playerController;

	// Token: 0x04001BC8 RID: 7112
	private Animator playerAnimController;
}
