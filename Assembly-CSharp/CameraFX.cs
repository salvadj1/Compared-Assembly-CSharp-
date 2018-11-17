using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x02000448 RID: 1096
[RequireComponent(typeof(Camera))]
public class CameraFX : IDRemote
{
	// Token: 0x1700091E RID: 2334
	// (get) Token: 0x06002812 RID: 10258 RVA: 0x0009C470 File Offset: 0x0009A670
	public Character idMain
	{
		get
		{
			return (Character)base.idMain;
		}
	}

	// Token: 0x1700091F RID: 2335
	// (get) Token: 0x06002813 RID: 10259 RVA: 0x0009C480 File Offset: 0x0009A680
	public Material predrawMaterial
	{
		get
		{
			return this.viewModelPredrawMaterial;
		}
	}

	// Token: 0x17000920 RID: 2336
	// (get) Token: 0x06002814 RID: 10260 RVA: 0x0009C488 File Offset: 0x0009A688
	public Material postdrawMaterial
	{
		get
		{
			return this.viewModelPostdrawMaterial;
		}
	}

	// Token: 0x06002815 RID: 10261 RVA: 0x0009C490 File Offset: 0x0009A690
	public void SetFieldOfView(float fieldOfView, float fraction)
	{
		this.fieldOfViewAdjustment = fieldOfView;
		this.fieldOfViewFraction = fraction;
		this.ApplyFieldOfView();
	}

	// Token: 0x06002816 RID: 10262 RVA: 0x0009C4A8 File Offset: 0x0009A6A8
	private void ApplyFieldOfView()
	{
		float num = Mathf.Lerp((float)render.fov, this.fieldOfViewAdjustment, this.fieldOfViewFraction);
		if (this.camera.fieldOfView != num)
		{
			this.camera.fieldOfView = num;
		}
	}

	// Token: 0x06002817 RID: 10263 RVA: 0x0009C4EC File Offset: 0x0009A6EC
	public Vector3 WorldToViewportPoint(Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		this.projectViewport.Project(ref vector3G, out vector3G2);
		return vector3G2.f;
	}

	// Token: 0x06002818 RID: 10264 RVA: 0x0009C518 File Offset: 0x0009A718
	public bool WorldToViewportPoint(ref Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectViewport.Project(ref vector3G, out vector3G2);
		v = vector3G2.f;
		return result;
	}

	// Token: 0x06002819 RID: 10265 RVA: 0x0009C550 File Offset: 0x0009A750
	public bool WorldToViewportPoint(ref Vector3 v, out Vector3 p)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectViewport.Project(ref vector3G, out vector3G2);
		p = vector3G2.f;
		return result;
	}

	// Token: 0x0600281A RID: 10266 RVA: 0x0009C588 File Offset: 0x0009A788
	public Vector3 WorldToScreenPoint(Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		this.projectScreen.Project(ref vector3G, out vector3G2);
		return vector3G2.f;
	}

	// Token: 0x0600281B RID: 10267 RVA: 0x0009C5B4 File Offset: 0x0009A7B4
	public bool WorldToScreenPoint(ref Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectScreen.Project(ref vector3G, out vector3G2);
		v = vector3G2.f;
		return result;
	}

	// Token: 0x0600281C RID: 10268 RVA: 0x0009C5EC File Offset: 0x0009A7EC
	public bool WorldToScreenPoint(ref Vector3 v, out Vector3 p)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectScreen.Project(ref vector3G, out vector3G2);
		p = vector3G2.f;
		return result;
	}

	// Token: 0x0600281D RID: 10269 RVA: 0x0009C624 File Offset: 0x0009A824
	public Vector3 ViewportToWorldPoint(Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		this.projectViewport.UnProject(ref vector3G, out vector3G2);
		return vector3G2.f;
	}

	// Token: 0x0600281E RID: 10270 RVA: 0x0009C650 File Offset: 0x0009A850
	public bool ViewportToWorldPoint(ref Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectViewport.UnProject(ref vector3G, out vector3G2);
		v = vector3G2.f;
		return result;
	}

	// Token: 0x0600281F RID: 10271 RVA: 0x0009C688 File Offset: 0x0009A888
	public bool ViewportToWorldPoint(ref Vector3 v, out Vector3 p)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectViewport.UnProject(ref vector3G, out vector3G2);
		p = vector3G2.f;
		return result;
	}

	// Token: 0x06002820 RID: 10272 RVA: 0x0009C6C0 File Offset: 0x0009A8C0
	public Vector3 ScreenToWorldPoint(Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		this.projectScreen.UnProject(ref vector3G, out vector3G2);
		return vector3G2.f;
	}

	// Token: 0x06002821 RID: 10273 RVA: 0x0009C6EC File Offset: 0x0009A8EC
	public bool ScreenToWorldPoint(ref Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectScreen.UnProject(ref vector3G, out vector3G2);
		v = vector3G2.f;
		return result;
	}

	// Token: 0x06002822 RID: 10274 RVA: 0x0009C724 File Offset: 0x0009A924
	public bool ScreenToWorldPoint(ref Vector3 v, out Vector3 p)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectScreen.UnProject(ref vector3G, out vector3G2);
		p = vector3G2.f;
		return result;
	}

	// Token: 0x06002823 RID: 10275 RVA: 0x0009C75C File Offset: 0x0009A95C
	public Vector3 ScreenToViewportPoint(Vector3 v)
	{
		return this.camera.ScreenToViewportPoint(v);
	}

	// Token: 0x06002824 RID: 10276 RVA: 0x0009C76C File Offset: 0x0009A96C
	public Vector3 ViewportToScreenPoint(Vector3 v)
	{
		return this.camera.ViewportToScreenPoint(v);
	}

	// Token: 0x06002825 RID: 10277 RVA: 0x0009C77C File Offset: 0x0009A97C
	public Ray ScreenPointToRay(Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		this.projectScreen.UnProject(ref vector3G, out vector3G2);
		vector3G.z += 1.0;
		Vector3G vector3G3;
		this.projectScreen.UnProject(ref vector3G, out vector3G3);
		return new Ray(vector3G2.f, new Vector3((float)(vector3G3.x - vector3G2.x), (float)(vector3G3.y - vector3G2.y), (float)(vector3G3.z - vector3G2.z)));
	}

	// Token: 0x06002826 RID: 10278 RVA: 0x0009C80C File Offset: 0x0009AA0C
	public Ray ViewportPointToRay(Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		this.projectViewport.UnProject(ref vector3G, out vector3G2);
		vector3G.z += 1.0;
		Vector3G vector3G3;
		this.projectViewport.UnProject(ref vector3G, out vector3G3);
		return new Ray(vector3G2.f, new Vector3((float)(vector3G3.x - vector3G2.x), (float)(vector3G3.y - vector3G2.y), (float)(vector3G3.z - vector3G2.z)));
	}

	// Token: 0x06002827 RID: 10279 RVA: 0x0009C89C File Offset: 0x0009AA9C
	public Vector3 TransformPoint(Vector3 v)
	{
		return this.localToWorldMatrix.f.MultiplyPoint3x4(v);
	}

	// Token: 0x06002828 RID: 10280 RVA: 0x0009C8C0 File Offset: 0x0009AAC0
	public Vector3 TransformDirection(Vector3 v)
	{
		return this.localToWorldMatrix.f.MultiplyVector(v);
	}

	// Token: 0x06002829 RID: 10281 RVA: 0x0009C8E4 File Offset: 0x0009AAE4
	public Vector3 InverseTransformPoint(Vector3 v)
	{
		return this.worldToLocalMatrix.f.MultiplyPoint3x4(v);
	}

	// Token: 0x0600282A RID: 10282 RVA: 0x0009C908 File Offset: 0x0009AB08
	public Vector3 InverseTransformDirection(Vector3 v)
	{
		return this.worldToLocalMatrix.f.MultiplyVector(v);
	}

	// Token: 0x0600282B RID: 10283 RVA: 0x0009C92C File Offset: 0x0009AB2C
	public static Vector3? World2Viewport(Vector3 point)
	{
		if (CameraFX.Bind())
		{
			return new Vector3?(CameraFX._mainCameraFX.WorldToViewportPoint(point));
		}
		if (CameraFX._hasMainCamera)
		{
			return new Vector3?(CameraFX._mainCamera.WorldToViewportPoint(point));
		}
		return null;
	}

	// Token: 0x0600282C RID: 10284 RVA: 0x0009C978 File Offset: 0x0009AB78
	public static Vector3? World2Screen(Vector3 point)
	{
		if (CameraFX.Bind())
		{
			return new Vector3?(CameraFX._mainCameraFX.WorldToScreenPoint(point));
		}
		if (CameraFX._hasMainCamera)
		{
			return new Vector3?(CameraFX._mainCamera.WorldToScreenPoint(point));
		}
		return null;
	}

	// Token: 0x0600282D RID: 10285 RVA: 0x0009C9C4 File Offset: 0x0009ABC4
	public static Vector3? Screen2World(Vector3 point)
	{
		if (CameraFX.Bind())
		{
			return new Vector3?(CameraFX._mainCameraFX.ScreenToWorldPoint(point));
		}
		if (CameraFX._hasMainCamera)
		{
			return new Vector3?(CameraFX._mainCamera.ScreenToWorldPoint(point));
		}
		return null;
	}

	// Token: 0x0600282E RID: 10286 RVA: 0x0009CA10 File Offset: 0x0009AC10
	public static Vector3? Viewport2World(Vector3 point)
	{
		if (CameraFX.Bind())
		{
			return new Vector3?(CameraFX._mainCameraFX.ScreenToWorldPoint(point));
		}
		if (CameraFX._hasMainCamera)
		{
			return new Vector3?(CameraFX._mainCamera.ScreenToWorldPoint(point));
		}
		return null;
	}

	// Token: 0x0600282F RID: 10287 RVA: 0x0009CA5C File Offset: 0x0009AC5C
	public static Vector3? Viewport2Screen(Vector3 point)
	{
		if (CameraFX.Bind())
		{
			return new Vector3?(CameraFX._mainCameraFX.ViewportToScreenPoint(point));
		}
		if (CameraFX._hasMainCamera)
		{
			return new Vector3?(CameraFX._mainCamera.ViewportToScreenPoint(point));
		}
		return null;
	}

	// Token: 0x06002830 RID: 10288 RVA: 0x0009CAA8 File Offset: 0x0009ACA8
	public static Vector3? Screen2Viewport(Vector3 point)
	{
		if (CameraFX.Bind())
		{
			return new Vector3?(CameraFX._mainCameraFX.ViewportToScreenPoint(point));
		}
		if (CameraFX._hasMainCamera)
		{
			return new Vector3?(CameraFX._mainCamera.ViewportToScreenPoint(point));
		}
		return null;
	}

	// Token: 0x06002831 RID: 10289 RVA: 0x0009CAF4 File Offset: 0x0009ACF4
	public static Ray? Screen2Ray(Vector3 point)
	{
		if (CameraFX.Bind())
		{
			return new Ray?(CameraFX._mainCameraFX.ScreenPointToRay(point));
		}
		if (CameraFX._hasMainCamera)
		{
			return new Ray?(CameraFX._mainCamera.ScreenPointToRay(point));
		}
		return null;
	}

	// Token: 0x06002832 RID: 10290 RVA: 0x0009CB40 File Offset: 0x0009AD40
	public static Ray? Viewport2Ray(Vector3 point)
	{
		if (CameraFX.Bind())
		{
			return new Ray?(CameraFX._mainCameraFX.ScreenPointToRay(point));
		}
		if (CameraFX._hasMainCamera)
		{
			return new Ray?(CameraFX._mainCamera.ScreenPointToRay(point));
		}
		return null;
	}

	// Token: 0x06002833 RID: 10291 RVA: 0x0009CB8C File Offset: 0x0009AD8C
	private static bool Bind()
	{
		return CameraFX.mainCameraFX;
	}

	// Token: 0x17000921 RID: 2337
	// (get) Token: 0x06002834 RID: 10292 RVA: 0x0009CB98 File Offset: 0x0009AD98
	public static CameraFX mainCameraFX
	{
		get
		{
			Camera main = Camera.main;
			if (CameraFX._mainCamera != main)
			{
				CameraFX._mainCamera = main;
				if (main)
				{
					CameraFX._hasMainCamera = true;
					CameraFX._mainIsMount = MountedCamera.IsMountedCamera(main);
					if (CameraFX._mainIsMount)
					{
						CameraFX._mainMountedCamera = MountedCamera.main;
						CameraFX._hasMainCameraFX = (CameraFX._mainCameraFX = CameraFX._mainMountedCamera.cameraFX);
					}
					else
					{
						CameraFX._mainMountedCamera = null;
						CameraFX._hasMainCameraFX = (CameraFX._mainCameraFX = main.GetComponent<CameraFX>());
					}
				}
				else
				{
					CameraFX._hasMainCamera = false;
					CameraFX._mainIsMount = false;
					CameraFX._hasMainCameraFX = false;
					CameraFX._mainCameraFX = null;
				}
			}
			else if (CameraFX._hasMainCamera && !main)
			{
				CameraFX._hasMainCamera = false;
				CameraFX._mainIsMount = false;
				CameraFX._hasMainCameraFX = false;
				CameraFX._mainCameraFX = null;
			}
			else if (CameraFX._mainIsMount && CameraFX._mainCameraFX != CameraFX._mainMountedCamera.cameraFX)
			{
				CameraFX._mainCameraFX = CameraFX._mainMountedCamera.cameraFX;
				CameraFX._hasMainCameraFX = CameraFX._mainCameraFX;
			}
			return (!CameraFX._hasMainCamera) ? null : ((!CameraFX._mainIsMount) ? CameraFX._mainCameraFX : MountedCamera.main.cameraFX);
		}
	}

	// Token: 0x06002835 RID: 10293 RVA: 0x0009CCF0 File Offset: 0x0009AEF0
	protected void Awake()
	{
		this.camera = base.camera;
		this.adaptiveNearPlane = base.GetComponent<AdaptiveNearPlane>();
		int num = 0;
		if (this._effects != null && this._effects.Length != 0)
		{
			for (int i = 0; i < this._effects.Length; i++)
			{
				if (this._effects[i] && this._effects[i] is ICameraFX)
				{
					this._effects[num++] = this._effects[i];
				}
				else
				{
					Debug.LogWarning("effect at index " + i + " is missing, null or not a ICameraFX", this);
				}
			}
		}
		Array.Resize<MonoBehaviour>(ref this._effects, num);
		Array.Resize<ICameraFX>(ref this.effects, num);
		if (num == 0)
		{
			Debug.LogWarning("There are no effects", this);
		}
		else
		{
			for (int j = 0; j < num; j++)
			{
				this.effects[j] = (ICameraFX)this._effects[j];
			}
		}
		this.awoke = true;
		if (this.viewModel)
		{
			ViewModel vm = this.viewModel;
			this.viewModel = null;
			ItemRepresentation itemRepresentation = this.rep;
			this.rep = null;
			IHeldItem heldItem = this.item;
			this.item = null;
			this.SetViewModel(vm, itemRepresentation, heldItem);
		}
		base.Awake();
	}

	// Token: 0x06002836 RID: 10294 RVA: 0x0009CE4C File Offset: 0x0009B04C
	public void PrePreCull()
	{
		this.ApplyFieldOfView();
		this.camera.ResetProjectionMatrix();
		this.preProjectionMatrix = this.camera.projectionMatrix;
		this.preLocalPosition = base.transform.localPosition;
		this.preRotation = base.transform.rotation;
		for (int i = 0; i < this._effects.Length; i++)
		{
			if (this._effects[i] && this._effects[i].enabled)
			{
				this.effects[i].PreCull();
			}
		}
	}

	// Token: 0x06002837 RID: 10295 RVA: 0x0009CEE8 File Offset: 0x0009B0E8
	public void PostPreCull()
	{
		if (CameraFX.viewModelRootTransform)
		{
			Quaternion localRotation = base.transform.localRotation;
			Vector3 localPosition = base.transform.localPosition;
			if (this.viewModel)
			{
				this.viewModel.ModifyAiming(new Ray(base.transform.parent.position, base.transform.parent.forward), ref localPosition, ref localRotation);
			}
			CameraFX.viewModelRootTransform.localRotation = Quaternion.Inverse(localRotation);
			CameraFX.viewModelRootTransform.localPosition = -localPosition;
		}
		Precise.ExtractLocalToWorldToLocal(this.camera.transform, ref this.localToWorldMatrix, ref this.worldToLocalMatrix);
		if (this.adaptiveNearPlane)
		{
			int num = (this.camera.cullingMask & ~this.adaptiveNearPlane.ignoreLayers.value) | this.adaptiveNearPlane.forceLayers.value;
			Vector3G vector3G = default(Vector3G);
			Vector3G vector3G2;
			this.localToWorldMatrix.MultiplyPoint(ref vector3G, ref vector3G2);
			Collider[] array = Physics.OverlapSphere(vector3G2.f, this.adaptiveNearPlane.minNear + this.adaptiveNearPlane.maxNear, num);
			float num2 = float.PositiveInfinity;
			double num3 = (double)this.camera.fieldOfView;
			double num4 = (double)this.camera.aspect;
			double num5 = (double)this.adaptiveNearPlane.minNear;
			double num6 = (double)(this.adaptiveNearPlane.maxNear + this.adaptiveNearPlane.threshold);
			float minNear = this.adaptiveNearPlane.minNear;
			float num7 = this.adaptiveNearPlane.maxNear + this.adaptiveNearPlane.threshold - minNear;
			Matrix4x4G matrix4x4G;
			Matrix4x4G.Perspective(ref num3, ref num4, ref num5, ref num6, ref matrix4x4G);
			Matrix4x4G matrix4x4G2;
			Matrix4x4G.Inverse(ref matrix4x4G, ref matrix4x4G2);
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					Vector3G vector3G3;
					vector3G3.x = ((double)i - 3.5) / 3.5;
					vector3G3.y = ((double)j - 3.5) / 3.5;
					vector3G3.z = 0.0;
					Vector3G vector3G4;
					matrix4x4G2.MultiplyPoint(ref vector3G3, ref vector3G4);
					vector3G3.z = 1.0;
					Vector3G vector3G5;
					matrix4x4G2.MultiplyPoint(ref vector3G3, ref vector3G5);
					vector3G4.x = -vector3G4.x;
					vector3G4.y = -vector3G4.y;
					vector3G4.z = -vector3G4.z;
					vector3G5.x = -vector3G5.x;
					vector3G5.y = -vector3G5.y;
					vector3G5.z = -vector3G5.z;
					Vector3G vector3G6;
					this.localToWorldMatrix.MultiplyPoint(ref vector3G4, ref vector3G6);
					Vector3G vector3G7;
					this.localToWorldMatrix.MultiplyPoint(ref vector3G5, ref vector3G7);
					Vector3G vector3G8;
					vector3G8.x = vector3G7.x - vector3G6.x;
					vector3G8.y = vector3G7.y - vector3G6.y;
					vector3G8.z = vector3G7.z - vector3G6.z;
					float num8 = (float)Math.Sqrt(vector3G8.x * vector3G8.x + vector3G8.y * vector3G8.y + vector3G8.z * vector3G8.z);
					float num9 = num8;
					Ray ray;
					ray..ctor(vector3G6.f, vector3G8.f);
					foreach (Collider collider in array)
					{
						RaycastHit raycastHit;
						if (collider.Raycast(ray, ref raycastHit, num9))
						{
							float distance = raycastHit.distance;
							if (distance < num9)
							{
								num9 = distance;
								float num10 = minNear + distance / num8 * num7;
								if (num2 > num10)
								{
									num2 = num10;
								}
							}
						}
					}
				}
			}
			if (float.IsInfinity(num2))
			{
				this.camera.nearClipPlane = this.adaptiveNearPlane.maxNear;
			}
			else
			{
				num2 -= this.adaptiveNearPlane.threshold;
				if (num2 >= this.adaptiveNearPlane.maxNear)
				{
					this.camera.nearClipPlane = this.adaptiveNearPlane.maxNear;
				}
				else if (num2 <= this.adaptiveNearPlane.minNear)
				{
					this.camera.nearClipPlane = this.adaptiveNearPlane.minNear;
				}
				else
				{
					this.camera.nearClipPlane = num2;
				}
			}
		}
		PerspectiveMatrixBuilder perspectiveMatrixBuilder;
		perspectiveMatrixBuilder.fieldOfView = (double)this.camera.fieldOfView;
		perspectiveMatrixBuilder.aspectRatio = (double)this.camera.aspect;
		perspectiveMatrixBuilder.nearPlane = (double)this.camera.nearClipPlane;
		perspectiveMatrixBuilder.farPlane = (double)this.camera.farClipPlane;
		PerspectiveMatrixBuilder perspectiveMatrixBuilder2 = perspectiveMatrixBuilder;
		Matrix4x4G matrix4x4G3;
		if (this.camera.isOrthoGraphic)
		{
			this.projectionMatrix.f = this.camera.projectionMatrix;
			matrix4x4G3 = this.projectionMatrix;
		}
		else
		{
			if (this.viewModel)
			{
				this.viewModel.ModifyPerspective(ref perspectiveMatrixBuilder2);
			}
			if (CameraFX.vm_projuse)
			{
				perspectiveMatrixBuilder2.ToProjectionMatrix(out this.projectionMatrix);
			}
			else
			{
				perspectiveMatrixBuilder.ToProjectionMatrix(out this.projectionMatrix);
			}
			this.camera.projectionMatrix = this.projectionMatrix.f;
			perspectiveMatrixBuilder2.ToProjectionMatrix(out matrix4x4G3);
		}
		Vector4 vector;
		vector.y = (float)perspectiveMatrixBuilder2.nearPlane;
		vector.z = (float)perspectiveMatrixBuilder2.farPlane;
		vector.w = (float)(1.0 / perspectiveMatrixBuilder2.farPlane);
		if (CameraFX.vm_flip == CameraFX.PLATFORM_POLL.flipRequired)
		{
			vector.x = 1f;
			Shader.SetGlobalMatrix("V_MUNITY_MATRIX_P", matrix4x4G3.f);
		}
		else
		{
			vector.x = -1f;
			PerspectiveMatrixBuilder perspectiveMatrixBuilder3;
			perspectiveMatrixBuilder3.nearPlane = perspectiveMatrixBuilder2.nearPlane;
			perspectiveMatrixBuilder3.farPlane = perspectiveMatrixBuilder2.farPlane;
			perspectiveMatrixBuilder3.fieldOfView = -perspectiveMatrixBuilder2.fieldOfView;
			perspectiveMatrixBuilder3.aspectRatio = -perspectiveMatrixBuilder2.aspectRatio;
			Matrix4x4G matrix4x4G4;
			perspectiveMatrixBuilder3.ToProjectionMatrix(out matrix4x4G4);
			Shader.SetGlobalMatrix("V_MUNITY_MATRIX_P", matrix4x4G4.f);
		}
		Shader.SetGlobalVector("V_M_ProjectionParams", vector);
		if (this.recalcViewMatrix)
		{
			Vector3G vector3G9;
			QuaternionG quaternionG;
			Vector3G vector3G10;
			Precise.ExtractWorldCoordinates(this.camera.transform, ref vector3G9, ref quaternionG, ref vector3G10);
			vector3G10.x = 1.0;
			vector3G10.y = 1.0;
			vector3G10.z = -1.0;
			Matrix4x4G.TRS(ref vector3G9, ref quaternionG, ref vector3G10, ref this.cameraToWorldMatrix);
			if (Matrix4x4G.Inverse(ref this.cameraToWorldMatrix, ref this.worldToCameraMatrix))
			{
				this.camera.worldToCameraMatrix = this.worldToCameraMatrix.f;
			}
		}
		else
		{
			this.cameraToWorldMatrix.f = this.camera.cameraToWorldMatrix;
			this.worldToCameraMatrix.f = this.camera.worldToCameraMatrix;
		}
		this.worldToCameraMatrixUnAltered = this.worldToCameraMatrix;
		this.cameraToWorldMatrixUnAltered = this.cameraToWorldMatrix;
		this.projectionMatrixUnAltered = this.projectionMatrix;
		CameraFX.ApplyTransitionAlterations(this.camera, this, true);
		this.projectScreen.modelview = (this.projectViewport.modelview = this.worldToCameraMatrix);
		this.projectScreen.projection = (this.projectViewport.projection = this.projectionMatrix);
		Rect rect = this.camera.pixelRect;
		this.projectScreen.offset.x = (double)rect.x;
		this.projectScreen.offset.y = (double)rect.y;
		this.projectScreen.size.x = (double)rect.width;
		this.projectScreen.size.y = (double)rect.height;
		rect = this.camera.rect;
		this.projectViewport.offset.x = (double)rect.x;
		this.projectViewport.offset.y = (double)rect.y;
		this.projectViewport.size.x = (double)rect.width;
		this.projectViewport.size.y = (double)rect.height;
		Matrix4x4G matrix4x4G5;
		Matrix4x4G.Mult(ref this.localToWorldMatrix, ref this.worldToCameraMatrix, ref matrix4x4G5);
		Matrix4x4G matrix4x4G6;
		Matrix4x4G.Mult(ref matrix4x4G5, ref this.projectionMatrix, ref matrix4x4G6);
		Matrix4x4G matrix4x4G7;
		Matrix4x4G.Inverse(ref matrix4x4G5, ref matrix4x4G7);
		Matrix4x4G matrix4x4G8;
		Matrix4x4G.Inverse(ref matrix4x4G6, ref matrix4x4G8);
		Matrix4x4G matrix4x4G9;
		Matrix4x4G.Inverse(ref this.localToWorldMatrix, ref matrix4x4G9);
		Matrix4x4G matrix4x4G10;
		Matrix4x4G.Transpose(ref matrix4x4G8, ref matrix4x4G10);
		Matrix4x4G matrix4x4G11;
		Matrix4x4G.Transpose(ref matrix4x4G7, ref matrix4x4G11);
		Matrix4x4G matrix4x4G12;
		Matrix4x4G.Transpose(ref matrix4x4G9, ref matrix4x4G12);
		if (this.viewModel)
		{
			this.viewModel.UpdateProxies();
		}
		BoundHack.Achieve(base.transform.position);
		ContextSprite.UpdateSpriteFading(this.camera);
		PlayerClient localPlayerClient = PlayerClient.localPlayerClient;
		if (localPlayerClient)
		{
			localPlayerClient.ProcessLocalPlayerPreRender();
		}
		RPOS.BeforeSceneRender_Internal(this.camera);
	}

	// Token: 0x06002838 RID: 10296 RVA: 0x0009D7DC File Offset: 0x0009B9DC
	internal static void ApplyTransitionAlterations(Camera camera, CameraFX fx, bool useFX)
	{
		if (useFX)
		{
			int num = CameraFX.g_trans.Update(ref fx.worldToCameraMatrix, ref fx.projectionMatrix);
			if ((num & 1) == 1)
			{
				camera.worldToCameraMatrix = fx.worldToCameraMatrix.f;
				Matrix4x4G.Inverse(ref fx.worldToCameraMatrix, ref fx.cameraToWorldMatrix);
			}
			if ((num & 2) == 2)
			{
				camera.projectionMatrix = fx.projectionMatrix.f;
			}
		}
		else
		{
			Matrix4x4G matrix4x4G;
			Precise.ExtractCameraMatrixWorldToCamera(camera, ref matrix4x4G);
			Matrix4x4G matrix4x4G2;
			Precise.ExtractCameraMatrixProjection(camera, ref matrix4x4G2);
			int num2 = CameraFX.g_trans.Update(ref matrix4x4G, ref matrix4x4G2);
			if ((num2 & 1) == 1)
			{
				camera.ResetWorldToCameraMatrix();
				camera.worldToCameraMatrix = matrix4x4G.f;
			}
			if ((num2 & 2) == 2)
			{
				camera.ResetProjectionMatrix();
				camera.projectionMatrix = matrix4x4G2.f;
			}
		}
	}

	// Token: 0x06002839 RID: 10297 RVA: 0x0009D8AC File Offset: 0x0009BAAC
	public static void TransitionNow(float duration, TransitionFunction function)
	{
		if (duration <= 0f)
		{
			CameraFX.g_trans.end = (CameraFX.g_trans.start = float.NegativeInfinity);
		}
		else
		{
			CameraFX.g_trans.Set(duration, function);
		}
	}

	// Token: 0x0600283A RID: 10298 RVA: 0x0009D8F4 File Offset: 0x0009BAF4
	public void PrePostRender()
	{
		this.camera.ResetWorldToCameraMatrix();
		for (int i = this._effects.Length - 1; i >= 0; i--)
		{
			if (this._effects[i] && this._effects[i].enabled)
			{
				this.effects[i].PostRender();
			}
		}
	}

	// Token: 0x0600283B RID: 10299 RVA: 0x0009D958 File Offset: 0x0009BB58
	public void PostPostRender()
	{
		base.transform.localPosition = this.preLocalPosition;
		base.transform.rotation = this.preRotation;
		this.camera.projectionMatrix = this.preProjectionMatrix;
	}

	// Token: 0x0600283C RID: 10300 RVA: 0x0009D998 File Offset: 0x0009BB98
	protected void OnDestroy()
	{
		base.OnDestroy();
		if (CameraFX._mainCameraFX == this)
		{
			CameraFX._mainCamera = null;
			CameraFX._mainCameraFX = null;
			CameraFX._hasMainCameraFX = false;
		}
	}

	// Token: 0x0600283D RID: 10301 RVA: 0x0009D9D0 File Offset: 0x0009BBD0
	private void SetViewModel(ViewModel vm)
	{
		this.SetViewModel(vm, null, null);
	}

	// Token: 0x0600283E RID: 10302 RVA: 0x0009D9DC File Offset: 0x0009BBDC
	private void SetViewModel(ViewModel vm, ItemRepresentation rep, IHeldItem item)
	{
		if (!this.awoke)
		{
			this.viewModel = vm;
			this.rep = rep;
			this.item = item;
			return;
		}
		if (this.viewModel != vm)
		{
			bool flag = this.viewModel;
			if (flag)
			{
				if (this.viewModel.itemRep)
				{
					try
					{
						this.viewModel.itemRep.UnBindViewModel(this.viewModel, this.viewModel.item);
					}
					catch (Exception ex)
					{
						Debug.LogException(ex, this.viewModel.itemRep);
					}
				}
				this.viewModel.UnBindTransforms();
				this.viewModel.idMain = null;
			}
			this.viewModel = vm;
			if (vm)
			{
				if (!CameraFX.viewModelRootTransform)
				{
					Transform transform = new GameObject("__View Model Root").transform;
					CameraFX.viewModelRootTransform = new GameObject("Eye Camera Difference").transform;
					CameraFX.viewModelRootTransform.parent = transform;
				}
				vm.idMain = this.idMain;
				vm.transform.parent = CameraFX.viewModelRootTransform;
				if (rep)
				{
					rep.PrepareViewModel(vm, item);
				}
				vm.BindTransforms(CameraFX.viewModelRootTransform, base.transform.parent);
				if (rep)
				{
					rep.BindViewModel(vm, item);
					vm.itemRep = rep;
					vm.item = item;
				}
			}
			for (int i = this._effects.Length - 1; i >= 0; i--)
			{
				if (this._effects[i])
				{
					this.effects[i].OnViewModelChange(vm);
				}
			}
		}
	}

	// Token: 0x0600283F RID: 10303 RVA: 0x0009DBA4 File Offset: 0x0009BDA4
	public static void ReplaceViewModel(ViewModel vm, bool butDontDestroyOld)
	{
		CameraFX.ReplaceViewModel(vm, null, null, butDontDestroyOld);
	}

	// Token: 0x06002840 RID: 10304 RVA: 0x0009DBB0 File Offset: 0x0009BDB0
	public static void ReplaceViewModel(ViewModel vm, ItemRepresentation rep, IHeldItem item, bool butDontDestroyOld)
	{
		CameraFX mainCameraFX = CameraFX.mainCameraFX;
		if (mainCameraFX && mainCameraFX.viewModel != vm)
		{
			ViewModel viewModel = mainCameraFX.viewModel;
			mainCameraFX.SetViewModel(vm, rep, item);
			if (!butDontDestroyOld && viewModel)
			{
				Object.Destroy(viewModel.gameObject);
			}
		}
	}

	// Token: 0x06002841 RID: 10305 RVA: 0x0009DC0C File Offset: 0x0009BE0C
	public static void RemoveViewModel()
	{
		if (CameraFX.mainViewModel)
		{
			CameraFX.ReplaceViewModel(null, false);
		}
	}

	// Token: 0x06002842 RID: 10306 RVA: 0x0009DC24 File Offset: 0x0009BE24
	public static void RemoveViewModel(ref ViewModel vm, bool deleteEvenIfNotCurrent, bool removeCurrentIfNotVM)
	{
		if (!vm)
		{
			if (removeCurrentIfNotVM)
			{
				CameraFX.RemoveViewModel();
			}
			return;
		}
		if (CameraFX.mainViewModel == vm)
		{
			CameraFX.ReplaceViewModel(null, false);
			vm = null;
		}
		else
		{
			if (deleteEvenIfNotCurrent)
			{
				Object.Destroy(vm.gameObject);
				vm = null;
			}
			if (removeCurrentIfNotVM)
			{
				CameraFX.ReplaceViewModel(null, false);
			}
		}
	}

	// Token: 0x17000922 RID: 2338
	// (get) Token: 0x06002843 RID: 10307 RVA: 0x0009DC8C File Offset: 0x0009BE8C
	public static ViewModel mainViewModel
	{
		get
		{
			CameraFX mainCameraFX = CameraFX.mainCameraFX;
			return (!mainCameraFX) ? null : mainCameraFX.viewModel;
		}
	}

	// Token: 0x040013EF RID: 5103
	[NonSerialized]
	public Camera camera;

	// Token: 0x040013F0 RID: 5104
	[SerializeField]
	private MonoBehaviour[] _effects;

	// Token: 0x040013F1 RID: 5105
	[SerializeField]
	private Material viewModelPredrawMaterial;

	// Token: 0x040013F2 RID: 5106
	private Material viewModelPostdrawMaterial;

	// Token: 0x040013F3 RID: 5107
	private AdaptiveNearPlane adaptiveNearPlane;

	// Token: 0x040013F4 RID: 5108
	private float fieldOfViewAdjustment;

	// Token: 0x040013F5 RID: 5109
	private float fieldOfViewFraction;

	// Token: 0x040013F6 RID: 5110
	[SerializeField]
	private bool recalcViewMatrix = true;

	// Token: 0x040013F7 RID: 5111
	private ICameraFX[] effects;

	// Token: 0x040013F8 RID: 5112
	private Quaternion preRotation;

	// Token: 0x040013F9 RID: 5113
	private Vector3 preLocalPosition;

	// Token: 0x040013FA RID: 5114
	private static Transform viewModelRootTransform;

	// Token: 0x040013FB RID: 5115
	private MatrixHelper.ProjectHelperG projectViewport;

	// Token: 0x040013FC RID: 5116
	private MatrixHelper.ProjectHelperG projectScreen;

	// Token: 0x040013FD RID: 5117
	private Matrix4x4G localToWorldMatrix;

	// Token: 0x040013FE RID: 5118
	private Matrix4x4G worldToLocalMatrix;

	// Token: 0x040013FF RID: 5119
	private Matrix4x4G cameraToWorldMatrixUnAltered;

	// Token: 0x04001400 RID: 5120
	private Matrix4x4G worldToCameraMatrixUnAltered;

	// Token: 0x04001401 RID: 5121
	private Matrix4x4G projectionMatrixUnAltered;

	// Token: 0x04001402 RID: 5122
	private Matrix4x4G cameraToWorldMatrix;

	// Token: 0x04001403 RID: 5123
	private Matrix4x4G worldToCameraMatrix;

	// Token: 0x04001404 RID: 5124
	private Matrix4x4G projectionMatrix;

	// Token: 0x04001405 RID: 5125
	private Matrix4x4 preProjectionMatrix;

	// Token: 0x04001406 RID: 5126
	private bool awoke;

	// Token: 0x04001407 RID: 5127
	private static CameraFX _mainCameraFX;

	// Token: 0x04001408 RID: 5128
	private static Camera _mainCamera;

	// Token: 0x04001409 RID: 5129
	private static MountedCamera _mainMountedCamera;

	// Token: 0x0400140A RID: 5130
	private static bool _hasMainCameraFX;

	// Token: 0x0400140B RID: 5131
	private static bool _hasMainCamera;

	// Token: 0x0400140C RID: 5132
	private static bool _mainIsMount;

	// Token: 0x0400140D RID: 5133
	private static bool vm_projuse = false;

	// Token: 0x0400140E RID: 5134
	private static bool vm_flip = false;

	// Token: 0x0400140F RID: 5135
	private static CameraFX.CameraTransitionData g_trans = CameraFX.CameraTransitionData.identity;

	// Token: 0x04001410 RID: 5136
	private ViewModel viewModel;

	// Token: 0x04001411 RID: 5137
	private ItemRepresentation rep;

	// Token: 0x04001412 RID: 5138
	private IHeldItem item;

	// Token: 0x02000449 RID: 1097
	private static class PLATFORM_POLL
	{
		// Token: 0x06002844 RID: 10308 RVA: 0x0009DCB8 File Offset: 0x0009BEB8
		static PLATFORM_POLL()
		{
			string graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;
			if (graphicsDeviceVersion != null)
			{
				if (graphicsDeviceVersion.StartsWith("OpenGL", StringComparison.InvariantCultureIgnoreCase))
				{
					CameraFX.PLATFORM_POLL.flipRequired = false;
					return;
				}
				if (graphicsDeviceVersion.StartsWith("Direct3D", StringComparison.InvariantCultureIgnoreCase))
				{
					CameraFX.PLATFORM_POLL.flipRequired = true;
					return;
				}
			}
			RuntimePlatform platform = Application.platform;
			switch (platform)
			{
			case 2:
			case 5:
			case 7:
				break;
			default:
				switch (platform)
				{
				case 18:
				case 19:
				case 20:
					break;
				default:
					if (platform != 10)
					{
						CameraFX.PLATFORM_POLL.flipRequired = false;
						return;
					}
					break;
				}
				break;
			}
			CameraFX.PLATFORM_POLL.flipRequired = true;
		}

		// Token: 0x04001413 RID: 5139
		public static readonly bool flipRequired;
	}

	// Token: 0x0200044A RID: 1098
	public struct CameraTransitionData
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x0009DD68 File Offset: 0x0009BF68
		public static CameraFX.CameraTransitionData identity
		{
			get
			{
				CameraFX.CameraTransitionData result = default(CameraFX.CameraTransitionData);
				result.view = (result.proj = (result.lastView = (result.lastProj = Matrix4x4G.identity)));
				result.end = (result.start = (result.lastTime = float.NegativeInfinity));
				result.func = TransitionFunction.Linear;
				return result;
			}
		}

		// Token: 0x06002846 RID: 10310 RVA: 0x0009DDD4 File Offset: 0x0009BFD4
		public int Update(ref Matrix4x4G currentView, ref Matrix4x4G currentProj)
		{
			int result;
			try
			{
				float timeSource = CameraFX.CameraTransitionData.timeSource;
				if (this.end > timeSource)
				{
					float num = Mathf.InverseLerp(this.start, this.end, timeSource);
					if (num < 1f)
					{
						num = this.func.Evaluate(num);
						Matrix4x4G matrix4x4G = TransitionFunctions.SlerpWorldToCamera((double)num, this.view, currentView);
						Matrix4x4G matrix4x4G2 = TransitionFunctions.Linear((double)num, this.proj, currentProj);
						this.lastTime = timeSource;
						if (!Matrix4x4G.Equals(ref matrix4x4G, ref currentView))
						{
							currentView = matrix4x4G;
							if (!Matrix4x4G.Equals(ref matrix4x4G2, ref currentProj))
							{
								currentProj = matrix4x4G2;
								return 3;
							}
							return 1;
						}
						else if (!Matrix4x4G.Equals(ref matrix4x4G2, ref currentProj))
						{
							currentProj = matrix4x4G2;
							return 2;
						}
					}
				}
				result = 0;
			}
			finally
			{
				this.lastView = currentView;
				this.lastProj = currentProj;
			}
			return result;
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x0009DEE8 File Offset: 0x0009C0E8
		public static float timeSource
		{
			get
			{
				return Time.time;
			}
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x0009DEF0 File Offset: 0x0009C0F0
		public void Set(float duration, TransitionFunction func)
		{
			this.start = CameraFX.CameraTransitionData.timeSource;
			this.lastTime = this.start;
			this.end = this.start + duration;
			this.view = this.lastView;
			this.proj = this.lastProj;
			this.func = func;
		}

		// Token: 0x04001414 RID: 5140
		public TransitionFunction func;

		// Token: 0x04001415 RID: 5141
		public Matrix4x4G view;

		// Token: 0x04001416 RID: 5142
		public Matrix4x4G proj;

		// Token: 0x04001417 RID: 5143
		private Matrix4x4G lastView;

		// Token: 0x04001418 RID: 5144
		private Matrix4x4G lastProj;

		// Token: 0x04001419 RID: 5145
		public float start;

		// Token: 0x0400141A RID: 5146
		public float end;

		// Token: 0x0400141B RID: 5147
		public float lastTime;
	}
}
