using System;
using UnityEngine;

// Token: 0x0200060C RID: 1548
[RequireComponent(typeof(Camera))]
public class LazyCam : MonoBehaviour, ICameraFX
{
	// Token: 0x06003712 RID: 14098 RVA: 0x000C8058 File Offset: 0x000C6258
	void ICameraFX.OnViewModelChange(ViewModel viewModel)
	{
		if (this.hasViewModel && this.viewModel)
		{
			this.viewModel.lazyCam = null;
		}
		this.viewModel = viewModel;
		this.hasViewModel = this.viewModel;
		if (this.hasViewModel)
		{
			this.viewModel.lazyCam = this;
			this.targetAngle = this.viewModel.lazyAngle;
			this.allow = true;
		}
		else
		{
			this.allow = false;
		}
	}

	// Token: 0x06003713 RID: 14099 RVA: 0x000C80E0 File Offset: 0x000C62E0
	void ICameraFX.PreCull()
	{
		this.aim = this.transform.rotation;
		this.add = (this.sub = Quaternion.identity);
		if (this._allow)
		{
			this.enableFraction += Time.deltaTime / this.enableSeconds;
			if (this.enableFraction >= 1f)
			{
				this.enableFraction = 1f;
			}
		}
		else
		{
			this.enableFraction -= Time.deltaTime / this.disableSeconds;
			if (this.enableFraction <= 0f)
			{
				this.enableFraction = 0f;
			}
		}
		this.maxAngle = Mathf.SmoothDampAngle(this.maxAngle, this.targetAngle * this.enableFraction, ref this.vel, this.damp);
		if (Mathf.Approximately(this.maxAngle, 0f))
		{
			this.view = this.aim;
			if (!this._allow)
			{
				base.enabled = false;
			}
			if (this.hasViewModel)
			{
				this.viewModel.lazyRotation = Quaternion.identity;
			}
		}
		else
		{
			this.isActivelyLazy = true;
			float num = Quaternion.Angle(this.aim, this.view);
			if (num >= this.maxAngle)
			{
				float num2 = 1f - this.maxAngle / num;
				this.view = Quaternion.Slerp(this.view, this.aim, num2);
			}
			this.sub = Quaternion.Inverse(this.add = Quaternion.Inverse(this.aim) * this.view);
			this.transform.rotation = this.view;
			this._world2cam = this.camera.worldToCameraMatrix;
			this._cam2world = this.camera.cameraToWorldMatrix;
			if (this.hasViewModel)
			{
				this.viewModel.lazyRotation = this.sub;
			}
		}
	}

	// Token: 0x06003714 RID: 14100 RVA: 0x000C82CC File Offset: 0x000C64CC
	void ICameraFX.PostRender()
	{
		if (this.wasActivelyLazy = this.isActivelyLazy)
		{
			this.isActivelyLazy = false;
			this.transform.rotation *= this.sub;
		}
	}

	// Token: 0x17000AF2 RID: 2802
	// (get) Token: 0x06003715 RID: 14101 RVA: 0x000C8310 File Offset: 0x000C6510
	// (set) Token: 0x06003716 RID: 14102 RVA: 0x000C8318 File Offset: 0x000C6518
	public bool allow
	{
		get
		{
			return this._allow;
		}
		set
		{
			if (value)
			{
				base.enabled = true;
				this._allow = true;
			}
			else
			{
				this._allow = false;
			}
		}
	}

	// Token: 0x17000AF3 RID: 2803
	// (get) Token: 0x06003717 RID: 14103 RVA: 0x000C8348 File Offset: 0x000C6548
	public Matrix4x4 worldToCameraMatrix
	{
		get
		{
			return (!this.wasActivelyLazy) ? this.camera.worldToCameraMatrix : this._world2cam;
		}
	}

	// Token: 0x17000AF4 RID: 2804
	// (get) Token: 0x06003718 RID: 14104 RVA: 0x000C836C File Offset: 0x000C656C
	public Matrix4x4 cameraToWorldMatrix
	{
		get
		{
			return (!this.wasActivelyLazy) ? this.camera.cameraToWorldMatrix : this._cam2world;
		}
	}

	// Token: 0x06003719 RID: 14105 RVA: 0x000C8390 File Offset: 0x000C6590
	private void Awake()
	{
		this.transform = base.transform;
		this.camera = base.camera;
		if (!this.camera)
		{
			Debug.LogError("No camera detected");
		}
	}

	// Token: 0x0600371A RID: 14106 RVA: 0x000C83D0 File Offset: 0x000C65D0
	private void Start()
	{
		this.view = this.transform.rotation;
	}

	// Token: 0x04001B51 RID: 6993
	private Quaternion aim;

	// Token: 0x04001B52 RID: 6994
	private Quaternion view;

	// Token: 0x04001B53 RID: 6995
	private Quaternion sub;

	// Token: 0x04001B54 RID: 6996
	private Quaternion add;

	// Token: 0x04001B55 RID: 6997
	public float maxAngle = 10f;

	// Token: 0x04001B56 RID: 6998
	public float damp = 0.01f;

	// Token: 0x04001B57 RID: 6999
	public float targetAngle = 10f;

	// Token: 0x04001B58 RID: 7000
	public float enableSeconds = 0.1f;

	// Token: 0x04001B59 RID: 7001
	public float disableSeconds = 0.1f;

	// Token: 0x04001B5A RID: 7002
	private float enableFraction;

	// Token: 0x04001B5B RID: 7003
	[NonSerialized]
	private bool isActivelyLazy;

	// Token: 0x04001B5C RID: 7004
	[NonSerialized]
	private bool wasActivelyLazy;

	// Token: 0x04001B5D RID: 7005
	[NonSerialized]
	private Matrix4x4 _world2cam;

	// Token: 0x04001B5E RID: 7006
	[NonSerialized]
	private Matrix4x4 _cam2world;

	// Token: 0x04001B5F RID: 7007
	[NonSerialized]
	private float vel;

	// Token: 0x04001B60 RID: 7008
	[NonSerialized]
	private Camera camera;

	// Token: 0x04001B61 RID: 7009
	[NonSerialized]
	private Transform transform;

	// Token: 0x04001B62 RID: 7010
	private bool _allow;

	// Token: 0x04001B63 RID: 7011
	private ViewModel viewModel;

	// Token: 0x04001B64 RID: 7012
	private bool hasViewModel;
}
