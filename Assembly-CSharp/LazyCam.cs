using System;
using UnityEngine;

// Token: 0x020006CC RID: 1740
[RequireComponent(typeof(Camera))]
public class LazyCam : MonoBehaviour, global::ICameraFX
{
	// Token: 0x06003AEA RID: 15082 RVA: 0x000D0588 File Offset: 0x000CE788
	void global::ICameraFX.OnViewModelChange(global::ViewModel viewModel)
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

	// Token: 0x06003AEB RID: 15083 RVA: 0x000D0610 File Offset: 0x000CE810
	void global::ICameraFX.PreCull()
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

	// Token: 0x06003AEC RID: 15084 RVA: 0x000D07FC File Offset: 0x000CE9FC
	void global::ICameraFX.PostRender()
	{
		if (this.wasActivelyLazy = this.isActivelyLazy)
		{
			this.isActivelyLazy = false;
			this.transform.rotation *= this.sub;
		}
	}

	// Token: 0x17000B6C RID: 2924
	// (get) Token: 0x06003AED RID: 15085 RVA: 0x000D0840 File Offset: 0x000CEA40
	// (set) Token: 0x06003AEE RID: 15086 RVA: 0x000D0848 File Offset: 0x000CEA48
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

	// Token: 0x17000B6D RID: 2925
	// (get) Token: 0x06003AEF RID: 15087 RVA: 0x000D0878 File Offset: 0x000CEA78
	public Matrix4x4 worldToCameraMatrix
	{
		get
		{
			return (!this.wasActivelyLazy) ? this.camera.worldToCameraMatrix : this._world2cam;
		}
	}

	// Token: 0x17000B6E RID: 2926
	// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x000D089C File Offset: 0x000CEA9C
	public Matrix4x4 cameraToWorldMatrix
	{
		get
		{
			return (!this.wasActivelyLazy) ? this.camera.cameraToWorldMatrix : this._cam2world;
		}
	}

	// Token: 0x06003AF1 RID: 15089 RVA: 0x000D08C0 File Offset: 0x000CEAC0
	private void Awake()
	{
		this.transform = base.transform;
		this.camera = base.camera;
		if (!this.camera)
		{
			Debug.LogError("No camera detected");
		}
	}

	// Token: 0x06003AF2 RID: 15090 RVA: 0x000D0900 File Offset: 0x000CEB00
	private void Start()
	{
		this.view = this.transform.rotation;
	}

	// Token: 0x04001D37 RID: 7479
	private Quaternion aim;

	// Token: 0x04001D38 RID: 7480
	private Quaternion view;

	// Token: 0x04001D39 RID: 7481
	private Quaternion sub;

	// Token: 0x04001D3A RID: 7482
	private Quaternion add;

	// Token: 0x04001D3B RID: 7483
	public float maxAngle = 10f;

	// Token: 0x04001D3C RID: 7484
	public float damp = 0.01f;

	// Token: 0x04001D3D RID: 7485
	public float targetAngle = 10f;

	// Token: 0x04001D3E RID: 7486
	public float enableSeconds = 0.1f;

	// Token: 0x04001D3F RID: 7487
	public float disableSeconds = 0.1f;

	// Token: 0x04001D40 RID: 7488
	private float enableFraction;

	// Token: 0x04001D41 RID: 7489
	[NonSerialized]
	private bool isActivelyLazy;

	// Token: 0x04001D42 RID: 7490
	[NonSerialized]
	private bool wasActivelyLazy;

	// Token: 0x04001D43 RID: 7491
	[NonSerialized]
	private Matrix4x4 _world2cam;

	// Token: 0x04001D44 RID: 7492
	[NonSerialized]
	private Matrix4x4 _cam2world;

	// Token: 0x04001D45 RID: 7493
	[NonSerialized]
	private float vel;

	// Token: 0x04001D46 RID: 7494
	[NonSerialized]
	private Camera camera;

	// Token: 0x04001D47 RID: 7495
	[NonSerialized]
	private Transform transform;

	// Token: 0x04001D48 RID: 7496
	private bool _allow;

	// Token: 0x04001D49 RID: 7497
	private global::ViewModel viewModel;

	// Token: 0x04001D4A RID: 7498
	private bool hasViewModel;
}
