using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002DC RID: 732
public sealed class PlayerPusher : MonoBehaviour
{
	// Token: 0x1700074C RID: 1868
	// (get) Token: 0x06001988 RID: 6536 RVA: 0x0006240C File Offset: 0x0006060C
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

	// Token: 0x06001989 RID: 6537 RVA: 0x00062440 File Offset: 0x00060640
	private static bool GetCCMotor(Collision collision, out global::CCMotor ccmotor)
	{
		GameObject gameObject = collision.gameObject;
		if (gameObject.layer == 16)
		{
			ccmotor = gameObject.GetComponent<global::CCMotor>();
			return ccmotor;
		}
		ccmotor = null;
		return false;
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x00062478 File Offset: 0x00060678
	private bool AddMotor(global::CCMotor motor)
	{
		if (this.activeMotors == null)
		{
			this.activeMotors = new HashSet<global::CCMotor>();
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

	// Token: 0x0600198B RID: 6539 RVA: 0x000624CC File Offset: 0x000606CC
	private bool RemoveMotor(global::CCMotor motor)
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

	// Token: 0x0600198C RID: 6540 RVA: 0x00062510 File Offset: 0x00060710
	private bool ContainsMotor(global::CCMotor motor)
	{
		return this.activeMotors != null && this.activeMotors.Contains(motor);
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x0006252C File Offset: 0x0006072C
	private void OnCollisionEnter(Collision collision)
	{
		global::CCMotor ccmotor;
		if (global::PlayerPusher.GetCCMotor(collision, out ccmotor) && this.AddMotor(ccmotor))
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

	// Token: 0x0600198E RID: 6542 RVA: 0x00062594 File Offset: 0x00060794
	private void OnCollisionStay(Collision collision)
	{
		global::CCMotor ccmotor;
		if (global::PlayerPusher.GetCCMotor(collision, out ccmotor) && this.ContainsMotor(ccmotor))
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

	// Token: 0x0600198F RID: 6543 RVA: 0x000625FC File Offset: 0x000607FC
	private void OnCollisionExit(Collision collision)
	{
		global::CCMotor ccmotor;
		if (global::PlayerPusher.GetCCMotor(collision, out ccmotor) && this.RemoveMotor(ccmotor))
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

	// Token: 0x04000DF4 RID: 3572
	[NonSerialized]
	private Rigidbody _rigidbody;

	// Token: 0x04000DF5 RID: 3573
	[NonSerialized]
	private bool _gotRigidbody;

	// Token: 0x04000DF6 RID: 3574
	[NonSerialized]
	private HashSet<global::CCMotor> activeMotors;
}
