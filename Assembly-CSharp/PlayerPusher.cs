using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029F RID: 671
public sealed class PlayerPusher : MonoBehaviour
{
	// Token: 0x170006F8 RID: 1784
	// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0005DA98 File Offset: 0x0005BC98
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

	// Token: 0x060017F9 RID: 6137 RVA: 0x0005DACC File Offset: 0x0005BCCC
	private static bool GetCCMotor(Collision collision, out CCMotor ccmotor)
	{
		GameObject gameObject = collision.gameObject;
		if (gameObject.layer == 16)
		{
			ccmotor = gameObject.GetComponent<CCMotor>();
			return ccmotor;
		}
		ccmotor = null;
		return false;
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x0005DB04 File Offset: 0x0005BD04
	private bool AddMotor(CCMotor motor)
	{
		if (this.activeMotors == null)
		{
			this.activeMotors = new HashSet<CCMotor>();
			this.activeMotors.Add(motor);
			return true;
		}
		if (!this.activeMotors.Add(motor))
		{
			Debug.LogWarning("Already added motor?", this);
			return false;
		}
		return true;
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x0005DB58 File Offset: 0x0005BD58
	private bool RemoveMotor(CCMotor motor)
	{
		if (this.activeMotors == null || !this.activeMotors.Remove(motor))
		{
			return false;
		}
		if (this.activeMotors.Count == 0)
		{
			this.activeMotors = null;
		}
		return true;
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x0005DB9C File Offset: 0x0005BD9C
	private bool ContainsMotor(CCMotor motor)
	{
		return this.activeMotors != null && this.activeMotors.Contains(motor);
	}

	// Token: 0x060017FD RID: 6141 RVA: 0x0005DBB8 File Offset: 0x0005BDB8
	private void OnCollisionEnter(Collision collision)
	{
		CCMotor ccmotor;
		if (PlayerPusher.GetCCMotor(collision, out ccmotor) && this.AddMotor(ccmotor))
		{
			try
			{
				ccmotor.OnPushEnter(this.rigidbody, base.collider, collision);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}
	}

	// Token: 0x060017FE RID: 6142 RVA: 0x0005DC20 File Offset: 0x0005BE20
	private void OnCollisionStay(Collision collision)
	{
		CCMotor ccmotor;
		if (PlayerPusher.GetCCMotor(collision, out ccmotor) && this.ContainsMotor(ccmotor))
		{
			try
			{
				ccmotor.OnPushStay(this.rigidbody, base.collider, collision);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}
	}

	// Token: 0x060017FF RID: 6143 RVA: 0x0005DC88 File Offset: 0x0005BE88
	private void OnCollisionExit(Collision collision)
	{
		CCMotor ccmotor;
		if (PlayerPusher.GetCCMotor(collision, out ccmotor) && this.RemoveMotor(ccmotor))
		{
			try
			{
				ccmotor.OnPushExit(this.rigidbody, base.collider, collision);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}
	}

	// Token: 0x04000CB9 RID: 3257
	[NonSerialized]
	private Rigidbody _rigidbody;

	// Token: 0x04000CBA RID: 3258
	[NonSerialized]
	private bool _gotRigidbody;

	// Token: 0x04000CBB RID: 3259
	[NonSerialized]
	private HashSet<CCMotor> activeMotors;
}
