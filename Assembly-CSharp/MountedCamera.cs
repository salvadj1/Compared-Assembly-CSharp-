using System;
using UnityEngine;

// Token: 0x0200050C RID: 1292
[RequireComponent(typeof(Camera))]
public sealed class MountedCamera : MonoBehaviour
{
	// Token: 0x17000995 RID: 2453
	// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000A54E4 File Offset: 0x000A36E4
	public global::CameraFX cameraFX
	{
		get
		{
			return this._cameraFX;
		}
	}

	// Token: 0x06002C1D RID: 11293 RVA: 0x000A54EC File Offset: 0x000A36EC
	private void Awake()
	{
		this.camera = base.camera;
		global::MountedCamera.singleton = this;
		global::CameraFXPre.mountedCamera = this;
		global::CameraFXPost.mountedCamera = this;
	}

	// Token: 0x06002C1E RID: 11294 RVA: 0x000A550C File Offset: 0x000A370C
	private void OnDestroy()
	{
		if (global::MountedCamera.singleton == this)
		{
			global::MountedCamera.singleton = null;
		}
	}

	// Token: 0x06002C1F RID: 11295 RVA: 0x000A5524 File Offset: 0x000A3724
	public void PreCullBegin()
	{
		global::CameraMount current = global::CameraMount.current;
		if (current != this.mount)
		{
			if (current)
			{
				this._cameraFX = current.cameraFX;
			}
			else
			{
				this._cameraFX = null;
			}
			global::CameraFXPre.cameraFX = this._cameraFX;
			global::CameraFXPost.cameraFX = this._cameraFX;
			this.mount = current;
		}
		if (this.mount)
		{
			Camera camera = this.mount.camera;
			camera.ResetAspect();
			camera.ResetProjectionMatrix();
			camera.ResetWorldToCameraMatrix();
			this.mount.OnPreMount(this);
		}
	}

	// Token: 0x06002C20 RID: 11296 RVA: 0x000A55C4 File Offset: 0x000A37C4
	public void PreCullEnd(bool postCamFX)
	{
		if (this.mount)
		{
			Transform transform = this.mount.transform;
			base.transform.position = transform.position;
			base.transform.rotation = transform.rotation;
			CameraClearFlags clearFlags = this.camera.clearFlags;
			int cullingMask = this.camera.cullingMask;
			DepthTextureMode depthTextureMode = this.camera.depthTextureMode;
			this.camera.ResetProjectionMatrix();
			this.camera.ResetWorldToCameraMatrix();
			this.mount.camera.depthTextureMode = depthTextureMode;
			this.camera.CopyFrom(this.mount.camera);
			if (!postCamFX)
			{
				global::CameraFX.ApplyTransitionAlterations(this.camera, null, false);
			}
			this.camera.clearFlags = clearFlags;
			this.camera.cullingMask = cullingMask;
			if (this.camera.depthTextureMode != depthTextureMode)
			{
				Debug.Log("Yea this is changing depth texture mode!", this.mount);
				this.camera.depthTextureMode = depthTextureMode;
			}
			this.mount.OnPostMount(this);
			this.lastView = this.camera.worldToCameraMatrix;
			this.lastProj = this.camera.projectionMatrix;
			this.once = true;
		}
		else
		{
			if (!this.once)
			{
				this.lastView = this.camera.worldToCameraMatrix;
				this.lastProj = this.camera.projectionMatrix;
				this.once = true;
			}
			this.camera.ResetProjectionMatrix();
			this.camera.ResetWorldToCameraMatrix();
			this.camera.worldToCameraMatrix = this.lastView;
			this.camera.projectionMatrix = this.lastProj;
			if (!postCamFX)
			{
				global::CameraFX.ApplyTransitionAlterations(this.camera, null, false);
			}
		}
		Matrix4x4 cameraToWorldMatrix = this.camera.cameraToWorldMatrix;
		base.transform.position = cameraToWorldMatrix.MultiplyPoint(Vector3.zero);
		base.transform.rotation = Quaternion.LookRotation(cameraToWorldMatrix.MultiplyVector(-Vector3.forward), cameraToWorldMatrix.MultiplyVector(Vector3.up));
		Shader.SetGlobalMatrix("_RUST_MATRIX_CAMERA_TO_WORLD", cameraToWorldMatrix * global::MountedCamera.negateZMatrix);
		Shader.SetGlobalMatrix("_RUST_MATRIX_WORLD_TO_CAMERA", this.camera.worldToCameraMatrix * global::MountedCamera.negateZMatrix);
	}

	// Token: 0x06002C21 RID: 11297 RVA: 0x000A5808 File Offset: 0x000A3A08
	public static bool GetPoint(out Vector3 point)
	{
		if (global::MountedCamera.singleton && global::MountedCamera.singleton.camera && global::MountedCamera.singleton.camera.enabled)
		{
			point = global::MountedCamera.singleton.camera.worldToCameraMatrix.MultiplyPoint(Vector3.zero);
			return true;
		}
		point = default(Vector3);
		return false;
	}

	// Token: 0x17000996 RID: 2454
	// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000A5880 File Offset: 0x000A3A80
	public static global::MountedCamera main
	{
		get
		{
			return global::MountedCamera.singleton;
		}
	}

	// Token: 0x06002C23 RID: 11299 RVA: 0x000A5888 File Offset: 0x000A3A88
	public static bool IsMountedCamera(Camera camera)
	{
		return global::MountedCamera.singleton && (global::MountedCamera.singleton.camera == camera || (global::MountedCamera.singleton.mount && global::MountedCamera.singleton.mount.camera == camera));
	}

	// Token: 0x06002C24 RID: 11300 RVA: 0x000A58EC File Offset: 0x000A3AEC
	public static bool IsCameraBeingUsed(Camera camera)
	{
		return camera && global::MountedCamera.singleton && (global::MountedCamera.singleton.camera && global::MountedCamera.singleton.camera.enabled) && (camera == global::MountedCamera.singleton.camera || (global::MountedCamera.singleton.mount && global::MountedCamera.singleton.mount.camera == camera));
	}

	// Token: 0x0400160A RID: 5642
	public Camera camera;

	// Token: 0x0400160B RID: 5643
	private static global::MountedCamera singleton;

	// Token: 0x0400160C RID: 5644
	private global::CameraFX _cameraFX;

	// Token: 0x0400160D RID: 5645
	private Matrix4x4 transitionView;

	// Token: 0x0400160E RID: 5646
	private Matrix4x4 transitionProj;

	// Token: 0x0400160F RID: 5647
	private float transitionStart;

	// Token: 0x04001610 RID: 5648
	private float transitionEnd;

	// Token: 0x04001611 RID: 5649
	private global::TransitionFunction transitionFunc;

	// Token: 0x04001612 RID: 5650
	private bool once;

	// Token: 0x04001613 RID: 5651
	private Matrix4x4 lastView;

	// Token: 0x04001614 RID: 5652
	private Matrix4x4 lastProj;

	// Token: 0x04001615 RID: 5653
	private global::CameraMount mount;

	// Token: 0x04001616 RID: 5654
	private static readonly Matrix4x4 negateZMatrix = Matrix4x4.Scale(new Vector3(1f, 1f, -1f));
}
