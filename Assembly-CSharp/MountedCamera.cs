using System;
using UnityEngine;

// Token: 0x02000456 RID: 1110
[RequireComponent(typeof(Camera))]
public sealed class MountedCamera : MonoBehaviour
{
	// Token: 0x1700092D RID: 2349
	// (get) Token: 0x0600288C RID: 10380 RVA: 0x0009F564 File Offset: 0x0009D764
	public CameraFX cameraFX
	{
		get
		{
			return this._cameraFX;
		}
	}

	// Token: 0x0600288D RID: 10381 RVA: 0x0009F56C File Offset: 0x0009D76C
	private void Awake()
	{
		this.camera = base.camera;
		MountedCamera.singleton = this;
		CameraFXPre.mountedCamera = this;
		CameraFXPost.mountedCamera = this;
	}

	// Token: 0x0600288E RID: 10382 RVA: 0x0009F58C File Offset: 0x0009D78C
	private void OnDestroy()
	{
		if (MountedCamera.singleton == this)
		{
			MountedCamera.singleton = null;
		}
	}

	// Token: 0x0600288F RID: 10383 RVA: 0x0009F5A4 File Offset: 0x0009D7A4
	public void PreCullBegin()
	{
		CameraMount current = CameraMount.current;
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
			CameraFXPre.cameraFX = this._cameraFX;
			CameraFXPost.cameraFX = this._cameraFX;
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

	// Token: 0x06002890 RID: 10384 RVA: 0x0009F644 File Offset: 0x0009D844
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
				CameraFX.ApplyTransitionAlterations(this.camera, null, false);
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
				CameraFX.ApplyTransitionAlterations(this.camera, null, false);
			}
		}
		Matrix4x4 cameraToWorldMatrix = this.camera.cameraToWorldMatrix;
		base.transform.position = cameraToWorldMatrix.MultiplyPoint(Vector3.zero);
		base.transform.rotation = Quaternion.LookRotation(cameraToWorldMatrix.MultiplyVector(-Vector3.forward), cameraToWorldMatrix.MultiplyVector(Vector3.up));
		Shader.SetGlobalMatrix("_RUST_MATRIX_CAMERA_TO_WORLD", cameraToWorldMatrix * MountedCamera.negateZMatrix);
		Shader.SetGlobalMatrix("_RUST_MATRIX_WORLD_TO_CAMERA", this.camera.worldToCameraMatrix * MountedCamera.negateZMatrix);
	}

	// Token: 0x06002891 RID: 10385 RVA: 0x0009F888 File Offset: 0x0009DA88
	public static bool GetPoint(out Vector3 point)
	{
		if (MountedCamera.singleton && MountedCamera.singleton.camera && MountedCamera.singleton.camera.enabled)
		{
			point = MountedCamera.singleton.camera.worldToCameraMatrix.MultiplyPoint(Vector3.zero);
			return true;
		}
		point = default(Vector3);
		return false;
	}

	// Token: 0x1700092E RID: 2350
	// (get) Token: 0x06002892 RID: 10386 RVA: 0x0009F900 File Offset: 0x0009DB00
	public static MountedCamera main
	{
		get
		{
			return MountedCamera.singleton;
		}
	}

	// Token: 0x06002893 RID: 10387 RVA: 0x0009F908 File Offset: 0x0009DB08
	public static bool IsMountedCamera(Camera camera)
	{
		return MountedCamera.singleton && (MountedCamera.singleton.camera == camera || (MountedCamera.singleton.mount && MountedCamera.singleton.mount.camera == camera));
	}

	// Token: 0x06002894 RID: 10388 RVA: 0x0009F96C File Offset: 0x0009DB6C
	public static bool IsCameraBeingUsed(Camera camera)
	{
		return camera && MountedCamera.singleton && (MountedCamera.singleton.camera && MountedCamera.singleton.camera.enabled) && (camera == MountedCamera.singleton.camera || (MountedCamera.singleton.mount && MountedCamera.singleton.mount.camera == camera));
	}

	// Token: 0x04001487 RID: 5255
	public Camera camera;

	// Token: 0x04001488 RID: 5256
	private static MountedCamera singleton;

	// Token: 0x04001489 RID: 5257
	private CameraFX _cameraFX;

	// Token: 0x0400148A RID: 5258
	private Matrix4x4 transitionView;

	// Token: 0x0400148B RID: 5259
	private Matrix4x4 transitionProj;

	// Token: 0x0400148C RID: 5260
	private float transitionStart;

	// Token: 0x0400148D RID: 5261
	private float transitionEnd;

	// Token: 0x0400148E RID: 5262
	private TransitionFunction transitionFunc;

	// Token: 0x0400148F RID: 5263
	private bool once;

	// Token: 0x04001490 RID: 5264
	private Matrix4x4 lastView;

	// Token: 0x04001491 RID: 5265
	private Matrix4x4 lastProj;

	// Token: 0x04001492 RID: 5266
	private CameraMount mount;

	// Token: 0x04001493 RID: 5267
	private static readonly Matrix4x4 negateZMatrix = Matrix4x4.Scale(new Vector3(1f, 1f, -1f));
}
