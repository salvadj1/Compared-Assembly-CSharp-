using System;
using Facepunch.Precision;
using UnityEngine;

// Token: 0x020004FE RID: 1278
[RequireComponent(typeof(Camera))]
public class CameraFX : IDRemote
{
	// Token: 0x17000986 RID: 2438
	// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x000A23F0 File Offset: 0x000A05F0
	public global::Character idMain
	{
		get
		{
			return (global::Character)base.idMain;
		}
	}

	// Token: 0x17000987 RID: 2439
	// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x000A2400 File Offset: 0x000A0600
	public Material predrawMaterial
	{
		get
		{
			return this.viewModelPredrawMaterial;
		}
	}

	// Token: 0x17000988 RID: 2440
	// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x000A2408 File Offset: 0x000A0608
	public Material postdrawMaterial
	{
		get
		{
			return this.viewModelPostdrawMaterial;
		}
	}

	// Token: 0x06002BA5 RID: 11173 RVA: 0x000A2410 File Offset: 0x000A0610
	public void SetFieldOfView(float fieldOfView, float fraction)
	{
		this.fieldOfViewAdjustment = fieldOfView;
		this.fieldOfViewFraction = fraction;
		this.ApplyFieldOfView();
	}

	// Token: 0x06002BA6 RID: 11174 RVA: 0x000A2428 File Offset: 0x000A0628
	private void ApplyFieldOfView()
	{
		float num = Mathf.Lerp((float)global::render.fov, this.fieldOfViewAdjustment, this.fieldOfViewFraction);
		if (this.camera.fieldOfView != num)
		{
			this.camera.fieldOfView = num;
		}
	}

	// Token: 0x06002BA7 RID: 11175 RVA: 0x000A246C File Offset: 0x000A066C
	public Vector3 WorldToViewportPoint(Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		this.projectViewport.Project(ref vector3G, out vector3G2);
		return vector3G2.f;
	}

	// Token: 0x06002BA8 RID: 11176 RVA: 0x000A2498 File Offset: 0x000A0698
	public bool WorldToViewportPoint(ref Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectViewport.Project(ref vector3G, out vector3G2);
		v = vector3G2.f;
		return result;
	}

	// Token: 0x06002BA9 RID: 11177 RVA: 0x000A24D0 File Offset: 0x000A06D0
	public bool WorldToViewportPoint(ref Vector3 v, out Vector3 p)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectViewport.Project(ref vector3G, out vector3G2);
		p = vector3G2.f;
		return result;
	}

	// Token: 0x06002BAA RID: 11178 RVA: 0x000A2508 File Offset: 0x000A0708
	public Vector3 WorldToScreenPoint(Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		this.projectScreen.Project(ref vector3G, out vector3G2);
		return vector3G2.f;
	}

	// Token: 0x06002BAB RID: 11179 RVA: 0x000A2534 File Offset: 0x000A0734
	public bool WorldToScreenPoint(ref Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectScreen.Project(ref vector3G, out vector3G2);
		v = vector3G2.f;
		return result;
	}

	// Token: 0x06002BAC RID: 11180 RVA: 0x000A256C File Offset: 0x000A076C
	public bool WorldToScreenPoint(ref Vector3 v, out Vector3 p)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectScreen.Project(ref vector3G, out vector3G2);
		p = vector3G2.f;
		return result;
	}

	// Token: 0x06002BAD RID: 11181 RVA: 0x000A25A4 File Offset: 0x000A07A4
	public Vector3 ViewportToWorldPoint(Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		this.projectViewport.UnProject(ref vector3G, out vector3G2);
		return vector3G2.f;
	}

	// Token: 0x06002BAE RID: 11182 RVA: 0x000A25D0 File Offset: 0x000A07D0
	public bool ViewportToWorldPoint(ref Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectViewport.UnProject(ref vector3G, out vector3G2);
		v = vector3G2.f;
		return result;
	}

	// Token: 0x06002BAF RID: 11183 RVA: 0x000A2608 File Offset: 0x000A0808
	public bool ViewportToWorldPoint(ref Vector3 v, out Vector3 p)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectViewport.UnProject(ref vector3G, out vector3G2);
		p = vector3G2.f;
		return result;
	}

	// Token: 0x06002BB0 RID: 11184 RVA: 0x000A2640 File Offset: 0x000A0840
	public Vector3 ScreenToWorldPoint(Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		this.projectScreen.UnProject(ref vector3G, out vector3G2);
		return vector3G2.f;
	}

	// Token: 0x06002BB1 RID: 11185 RVA: 0x000A266C File Offset: 0x000A086C
	public bool ScreenToWorldPoint(ref Vector3 v)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectScreen.UnProject(ref vector3G, out vector3G2);
		v = vector3G2.f;
		return result;
	}

	// Token: 0x06002BB2 RID: 11186 RVA: 0x000A26A4 File Offset: 0x000A08A4
	public bool ScreenToWorldPoint(ref Vector3 v, out Vector3 p)
	{
		Vector3G vector3G;
		vector3G..ctor(v);
		Vector3G vector3G2;
		bool result = this.projectScreen.UnProject(ref vector3G, out vector3G2);
		p = vector3G2.f;
		return result;
	}

	// Token: 0x06002BB3 RID: 11187 RVA: 0x000A26DC File Offset: 0x000A08DC
	public Vector3 ScreenToViewportPoint(Vector3 v)
	{
		return this.camera.ScreenToViewportPoint(v);
	}

	// Token: 0x06002BB4 RID: 11188 RVA: 0x000A26EC File Offset: 0x000A08EC
	public Vector3 ViewportToScreenPoint(Vector3 v)
	{
		return this.camera.ViewportToScreenPoint(v);
	}

	// Token: 0x06002BB5 RID: 11189 RVA: 0x000A26FC File Offset: 0x000A08FC
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

	// Token: 0x06002BB6 RID: 11190 RVA: 0x000A278C File Offset: 0x000A098C
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

	// Token: 0x06002BB7 RID: 11191 RVA: 0x000A281C File Offset: 0x000A0A1C
	public Vector3 TransformPoint(Vector3 v)
	{
		return this.localToWorldMatrix.f.MultiplyPoint3x4(v);
	}

	// Token: 0x06002BB8 RID: 11192 RVA: 0x000A2840 File Offset: 0x000A0A40
	public Vector3 TransformDirection(Vector3 v)
	{
		return this.localToWorldMatrix.f.MultiplyVector(v);
	}

	// Token: 0x06002BB9 RID: 11193 RVA: 0x000A2864 File Offset: 0x000A0A64
	public Vector3 InverseTransformPoint(Vector3 v)
	{
		return this.worldToLocalMatrix.f.MultiplyPoint3x4(v);
	}

	// Token: 0x06002BBA RID: 11194 RVA: 0x000A2888 File Offset: 0x000A0A88
	public Vector3 InverseTransformDirection(Vector3 v)
	{
		return this.worldToLocalMatrix.f.MultiplyVector(v);
	}

	// Token: 0x06002BBB RID: 11195 RVA: 0x000A28AC File Offset: 0x000A0AAC
	public static Vector3? World2Viewport(Vector3 point)
	{
		if (global::CameraFX.Bind())
		{
			return new Vector3?(global::CameraFX._mainCameraFX.WorldToViewportPoint(point));
		}
		if (global::CameraFX._hasMainCamera)
		{
			return new Vector3?(global::CameraFX._mainCamera.WorldToViewportPoint(point));
		}
		return null;
	}

	// Token: 0x06002BBC RID: 11196 RVA: 0x000A28F8 File Offset: 0x000A0AF8
	public static Vector3? World2Screen(Vector3 point)
	{
		if (global::CameraFX.Bind())
		{
			return new Vector3?(global::CameraFX._mainCameraFX.WorldToScreenPoint(point));
		}
		if (global::CameraFX._hasMainCamera)
		{
			return new Vector3?(global::CameraFX._mainCamera.WorldToScreenPoint(point));
		}
		return null;
	}

	// Token: 0x06002BBD RID: 11197 RVA: 0x000A2944 File Offset: 0x000A0B44
	public static Vector3? Screen2World(Vector3 point)
	{
		if (global::CameraFX.Bind())
		{
			return new Vector3?(global::CameraFX._mainCameraFX.ScreenToWorldPoint(point));
		}
		if (global::CameraFX._hasMainCamera)
		{
			return new Vector3?(global::CameraFX._mainCamera.ScreenToWorldPoint(point));
		}
		return null;
	}

	// Token: 0x06002BBE RID: 11198 RVA: 0x000A2990 File Offset: 0x000A0B90
	public static Vector3? Viewport2World(Vector3 point)
	{
		if (global::CameraFX.Bind())
		{
			return new Vector3?(global::CameraFX._mainCameraFX.ScreenToWorldPoint(point));
		}
		if (global::CameraFX._hasMainCamera)
		{
			return new Vector3?(global::CameraFX._mainCamera.ScreenToWorldPoint(point));
		}
		return null;
	}

	// Token: 0x06002BBF RID: 11199 RVA: 0x000A29DC File Offset: 0x000A0BDC
	public static Vector3? Viewport2Screen(Vector3 point)
	{
		if (global::CameraFX.Bind())
		{
			return new Vector3?(global::CameraFX._mainCameraFX.ViewportToScreenPoint(point));
		}
		if (global::CameraFX._hasMainCamera)
		{
			return new Vector3?(global::CameraFX._mainCamera.ViewportToScreenPoint(point));
		}
		return null;
	}

	// Token: 0x06002BC0 RID: 11200 RVA: 0x000A2A28 File Offset: 0x000A0C28
	public static Vector3? Screen2Viewport(Vector3 point)
	{
		if (global::CameraFX.Bind())
		{
			return new Vector3?(global::CameraFX._mainCameraFX.ViewportToScreenPoint(point));
		}
		if (global::CameraFX._hasMainCamera)
		{
			return new Vector3?(global::CameraFX._mainCamera.ViewportToScreenPoint(point));
		}
		return null;
	}

	// Token: 0x06002BC1 RID: 11201 RVA: 0x000A2A74 File Offset: 0x000A0C74
	public static Ray? Screen2Ray(Vector3 point)
	{
		if (global::CameraFX.Bind())
		{
			return new Ray?(global::CameraFX._mainCameraFX.ScreenPointToRay(point));
		}
		if (global::CameraFX._hasMainCamera)
		{
			return new Ray?(global::CameraFX._mainCamera.ScreenPointToRay(point));
		}
		return null;
	}

	// Token: 0x06002BC2 RID: 11202 RVA: 0x000A2AC0 File Offset: 0x000A0CC0
	public static Ray? Viewport2Ray(Vector3 point)
	{
		if (global::CameraFX.Bind())
		{
			return new Ray?(global::CameraFX._mainCameraFX.ScreenPointToRay(point));
		}
		if (global::CameraFX._hasMainCamera)
		{
			return new Ray?(global::CameraFX._mainCamera.ScreenPointToRay(point));
		}
		return null;
	}

	// Token: 0x06002BC3 RID: 11203 RVA: 0x000A2B0C File Offset: 0x000A0D0C
	private static bool Bind()
	{
		return global::CameraFX.mainCameraFX;
	}

	// Token: 0x17000989 RID: 2441
	// (get) Token: 0x06002BC4 RID: 11204 RVA: 0x000A2B18 File Offset: 0x000A0D18
	public static global::CameraFX mainCameraFX
	{
		get
		{
			Camera main = Camera.main;
			if (global::CameraFX._mainCamera != main)
			{
				global::CameraFX._mainCamera = main;
				if (main)
				{
					global::CameraFX._hasMainCamera = true;
					global::CameraFX._mainIsMount = global::MountedCamera.IsMountedCamera(main);
					if (global::CameraFX._mainIsMount)
					{
						global::CameraFX._mainMountedCamera = global::MountedCamera.main;
						global::CameraFX._hasMainCameraFX = (global::CameraFX._mainCameraFX = global::CameraFX._mainMountedCamera.cameraFX);
					}
					else
					{
						global::CameraFX._mainMountedCamera = null;
						global::CameraFX._hasMainCameraFX = (global::CameraFX._mainCameraFX = main.GetComponent<global::CameraFX>());
					}
				}
				else
				{
					global::CameraFX._hasMainCamera = false;
					global::CameraFX._mainIsMount = false;
					global::CameraFX._hasMainCameraFX = false;
					global::CameraFX._mainCameraFX = null;
				}
			}
			else if (global::CameraFX._hasMainCamera && !main)
			{
				global::CameraFX._hasMainCamera = false;
				global::CameraFX._mainIsMount = false;
				global::CameraFX._hasMainCameraFX = false;
				global::CameraFX._mainCameraFX = null;
			}
			else if (global::CameraFX._mainIsMount && global::CameraFX._mainCameraFX != global::CameraFX._mainMountedCamera.cameraFX)
			{
				global::CameraFX._mainCameraFX = global::CameraFX._mainMountedCamera.cameraFX;
				global::CameraFX._hasMainCameraFX = global::CameraFX._mainCameraFX;
			}
			return (!global::CameraFX._hasMainCamera) ? null : ((!global::CameraFX._mainIsMount) ? global::CameraFX._mainCameraFX : global::MountedCamera.main.cameraFX);
		}
	}

	// Token: 0x06002BC5 RID: 11205 RVA: 0x000A2C70 File Offset: 0x000A0E70
	protected void Awake()
	{
		this.camera = base.camera;
		this.adaptiveNearPlane = base.GetComponent<global::AdaptiveNearPlane>();
		int num = 0;
		if (this._effects != null && this._effects.Length != 0)
		{
			for (int i = 0; i < this._effects.Length; i++)
			{
				if (this._effects[i] && this._effects[i] is global::ICameraFX)
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
		Array.Resize<global::ICameraFX>(ref this.effects, num);
		if (num == 0)
		{
			Debug.LogWarning("There are no effects", this);
		}
		else
		{
			for (int j = 0; j < num; j++)
			{
				this.effects[j] = (global::ICameraFX)this._effects[j];
			}
		}
		this.awoke = true;
		if (this.viewModel)
		{
			global::ViewModel vm = this.viewModel;
			this.viewModel = null;
			global::ItemRepresentation itemRepresentation = this.rep;
			this.rep = null;
			global::IHeldItem heldItem = this.item;
			this.item = null;
			this.SetViewModel(vm, itemRepresentation, heldItem);
		}
		base.Awake();
	}

	// Token: 0x06002BC6 RID: 11206 RVA: 0x000A2DCC File Offset: 0x000A0FCC
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

	// Token: 0x06002BC7 RID: 11207 RVA: 0x000A2E68 File Offset: 0x000A1068
	public void PostPreCull()
	{
		if (global::CameraFX.viewModelRootTransform)
		{
			Quaternion localRotation = base.transform.localRotation;
			Vector3 localPosition = base.transform.localPosition;
			if (this.viewModel)
			{
				this.viewModel.ModifyAiming(new Ray(base.transform.parent.position, base.transform.parent.forward), ref localPosition, ref localRotation);
			}
			global::CameraFX.viewModelRootTransform.localRotation = Quaternion.Inverse(localRotation);
			global::CameraFX.viewModelRootTransform.localPosition = -localPosition;
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
		global::PerspectiveMatrixBuilder perspectiveMatrixBuilder;
		perspectiveMatrixBuilder.fieldOfView = (double)this.camera.fieldOfView;
		perspectiveMatrixBuilder.aspectRatio = (double)this.camera.aspect;
		perspectiveMatrixBuilder.nearPlane = (double)this.camera.nearClipPlane;
		perspectiveMatrixBuilder.farPlane = (double)this.camera.farClipPlane;
		global::PerspectiveMatrixBuilder perspectiveMatrixBuilder2 = perspectiveMatrixBuilder;
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
			if (global::CameraFX.vm_projuse)
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
		if (global::CameraFX.vm_flip == global::CameraFX.PLATFORM_POLL.flipRequired)
		{
			vector.x = 1f;
			Shader.SetGlobalMatrix("V_MUNITY_MATRIX_P", matrix4x4G3.f);
		}
		else
		{
			vector.x = -1f;
			global::PerspectiveMatrixBuilder perspectiveMatrixBuilder3;
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
		global::CameraFX.ApplyTransitionAlterations(this.camera, this, true);
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
		global::BoundHack.Achieve(base.transform.position);
		global::ContextSprite.UpdateSpriteFading(this.camera);
		global::PlayerClient localPlayerClient = global::PlayerClient.localPlayerClient;
		if (localPlayerClient)
		{
			localPlayerClient.ProcessLocalPlayerPreRender();
		}
		global::RPOS.BeforeSceneRender_Internal(this.camera);
	}

	// Token: 0x06002BC8 RID: 11208 RVA: 0x000A375C File Offset: 0x000A195C
	internal static void ApplyTransitionAlterations(Camera camera, global::CameraFX fx, bool useFX)
	{
		if (useFX)
		{
			int num = global::CameraFX.g_trans.Update(ref fx.worldToCameraMatrix, ref fx.projectionMatrix);
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
			int num2 = global::CameraFX.g_trans.Update(ref matrix4x4G, ref matrix4x4G2);
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

	// Token: 0x06002BC9 RID: 11209 RVA: 0x000A382C File Offset: 0x000A1A2C
	public static void TransitionNow(float duration, global::TransitionFunction function)
	{
		if (duration <= 0f)
		{
			global::CameraFX.g_trans.end = (global::CameraFX.g_trans.start = float.NegativeInfinity);
		}
		else
		{
			global::CameraFX.g_trans.Set(duration, function);
		}
	}

	// Token: 0x06002BCA RID: 11210 RVA: 0x000A3874 File Offset: 0x000A1A74
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

	// Token: 0x06002BCB RID: 11211 RVA: 0x000A38D8 File Offset: 0x000A1AD8
	public void PostPostRender()
	{
		base.transform.localPosition = this.preLocalPosition;
		base.transform.rotation = this.preRotation;
		this.camera.projectionMatrix = this.preProjectionMatrix;
	}

	// Token: 0x06002BCC RID: 11212 RVA: 0x000A3918 File Offset: 0x000A1B18
	protected void OnDestroy()
	{
		base.OnDestroy();
		if (global::CameraFX._mainCameraFX == this)
		{
			global::CameraFX._mainCamera = null;
			global::CameraFX._mainCameraFX = null;
			global::CameraFX._hasMainCameraFX = false;
		}
	}

	// Token: 0x06002BCD RID: 11213 RVA: 0x000A3950 File Offset: 0x000A1B50
	private void SetViewModel(global::ViewModel vm)
	{
		this.SetViewModel(vm, null, null);
	}

	// Token: 0x06002BCE RID: 11214 RVA: 0x000A395C File Offset: 0x000A1B5C
	private void SetViewModel(global::ViewModel vm, global::ItemRepresentation rep, global::IHeldItem item)
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
				if (!global::CameraFX.viewModelRootTransform)
				{
					Transform transform = new GameObject("__View Model Root").transform;
					global::CameraFX.viewModelRootTransform = new GameObject("Eye Camera Difference").transform;
					global::CameraFX.viewModelRootTransform.parent = transform;
				}
				vm.idMain = this.idMain;
				vm.transform.parent = global::CameraFX.viewModelRootTransform;
				if (rep)
				{
					rep.PrepareViewModel(vm, item);
				}
				vm.BindTransforms(global::CameraFX.viewModelRootTransform, base.transform.parent);
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

	// Token: 0x06002BCF RID: 11215 RVA: 0x000A3B24 File Offset: 0x000A1D24
	public static void ReplaceViewModel(global::ViewModel vm, bool butDontDestroyOld)
	{
		global::CameraFX.ReplaceViewModel(vm, null, null, butDontDestroyOld);
	}

	// Token: 0x06002BD0 RID: 11216 RVA: 0x000A3B30 File Offset: 0x000A1D30
	public static void ReplaceViewModel(global::ViewModel vm, global::ItemRepresentation rep, global::IHeldItem item, bool butDontDestroyOld)
	{
		global::CameraFX mainCameraFX = global::CameraFX.mainCameraFX;
		if (mainCameraFX && mainCameraFX.viewModel != vm)
		{
			global::ViewModel viewModel = mainCameraFX.viewModel;
			mainCameraFX.SetViewModel(vm, rep, item);
			if (!butDontDestroyOld && viewModel)
			{
				Object.Destroy(viewModel.gameObject);
			}
		}
	}

	// Token: 0x06002BD1 RID: 11217 RVA: 0x000A3B8C File Offset: 0x000A1D8C
	public static void RemoveViewModel()
	{
		if (global::CameraFX.mainViewModel)
		{
			global::CameraFX.ReplaceViewModel(null, false);
		}
	}

	// Token: 0x06002BD2 RID: 11218 RVA: 0x000A3BA4 File Offset: 0x000A1DA4
	public static void RemoveViewModel(ref global::ViewModel vm, bool deleteEvenIfNotCurrent, bool removeCurrentIfNotVM)
	{
		if (!vm)
		{
			if (removeCurrentIfNotVM)
			{
				global::CameraFX.RemoveViewModel();
			}
			return;
		}
		if (global::CameraFX.mainViewModel == vm)
		{
			global::CameraFX.ReplaceViewModel(null, false);
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
				global::CameraFX.ReplaceViewModel(null, false);
			}
		}
	}

	// Token: 0x1700098A RID: 2442
	// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x000A3C0C File Offset: 0x000A1E0C
	public static global::ViewModel mainViewModel
	{
		get
		{
			global::CameraFX mainCameraFX = global::CameraFX.mainCameraFX;
			return (!mainCameraFX) ? null : mainCameraFX.viewModel;
		}
	}

	// Token: 0x04001572 RID: 5490
	[NonSerialized]
	public Camera camera;

	// Token: 0x04001573 RID: 5491
	[SerializeField]
	private MonoBehaviour[] _effects;

	// Token: 0x04001574 RID: 5492
	[SerializeField]
	private Material viewModelPredrawMaterial;

	// Token: 0x04001575 RID: 5493
	private Material viewModelPostdrawMaterial;

	// Token: 0x04001576 RID: 5494
	private global::AdaptiveNearPlane adaptiveNearPlane;

	// Token: 0x04001577 RID: 5495
	private float fieldOfViewAdjustment;

	// Token: 0x04001578 RID: 5496
	private float fieldOfViewFraction;

	// Token: 0x04001579 RID: 5497
	[SerializeField]
	private bool recalcViewMatrix = true;

	// Token: 0x0400157A RID: 5498
	private global::ICameraFX[] effects;

	// Token: 0x0400157B RID: 5499
	private Quaternion preRotation;

	// Token: 0x0400157C RID: 5500
	private Vector3 preLocalPosition;

	// Token: 0x0400157D RID: 5501
	private static Transform viewModelRootTransform;

	// Token: 0x0400157E RID: 5502
	private global::MatrixHelper.ProjectHelperG projectViewport;

	// Token: 0x0400157F RID: 5503
	private global::MatrixHelper.ProjectHelperG projectScreen;

	// Token: 0x04001580 RID: 5504
	private Matrix4x4G localToWorldMatrix;

	// Token: 0x04001581 RID: 5505
	private Matrix4x4G worldToLocalMatrix;

	// Token: 0x04001582 RID: 5506
	private Matrix4x4G cameraToWorldMatrixUnAltered;

	// Token: 0x04001583 RID: 5507
	private Matrix4x4G worldToCameraMatrixUnAltered;

	// Token: 0x04001584 RID: 5508
	private Matrix4x4G projectionMatrixUnAltered;

	// Token: 0x04001585 RID: 5509
	private Matrix4x4G cameraToWorldMatrix;

	// Token: 0x04001586 RID: 5510
	private Matrix4x4G worldToCameraMatrix;

	// Token: 0x04001587 RID: 5511
	private Matrix4x4G projectionMatrix;

	// Token: 0x04001588 RID: 5512
	private Matrix4x4 preProjectionMatrix;

	// Token: 0x04001589 RID: 5513
	private bool awoke;

	// Token: 0x0400158A RID: 5514
	private static global::CameraFX _mainCameraFX;

	// Token: 0x0400158B RID: 5515
	private static Camera _mainCamera;

	// Token: 0x0400158C RID: 5516
	private static global::MountedCamera _mainMountedCamera;

	// Token: 0x0400158D RID: 5517
	private static bool _hasMainCameraFX;

	// Token: 0x0400158E RID: 5518
	private static bool _hasMainCamera;

	// Token: 0x0400158F RID: 5519
	private static bool _mainIsMount;

	// Token: 0x04001590 RID: 5520
	private static bool vm_projuse = false;

	// Token: 0x04001591 RID: 5521
	private static bool vm_flip = false;

	// Token: 0x04001592 RID: 5522
	private static global::CameraFX.CameraTransitionData g_trans = global::CameraFX.CameraTransitionData.identity;

	// Token: 0x04001593 RID: 5523
	private global::ViewModel viewModel;

	// Token: 0x04001594 RID: 5524
	private global::ItemRepresentation rep;

	// Token: 0x04001595 RID: 5525
	private global::IHeldItem item;

	// Token: 0x020004FF RID: 1279
	private static class PLATFORM_POLL
	{
		// Token: 0x06002BD4 RID: 11220 RVA: 0x000A3C38 File Offset: 0x000A1E38
		static PLATFORM_POLL()
		{
			string graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion;
			if (graphicsDeviceVersion != null)
			{
				if (graphicsDeviceVersion.StartsWith("OpenGL", StringComparison.InvariantCultureIgnoreCase))
				{
					global::CameraFX.PLATFORM_POLL.flipRequired = false;
					return;
				}
				if (graphicsDeviceVersion.StartsWith("Direct3D", StringComparison.InvariantCultureIgnoreCase))
				{
					global::CameraFX.PLATFORM_POLL.flipRequired = true;
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
						global::CameraFX.PLATFORM_POLL.flipRequired = false;
						return;
					}
					break;
				}
				break;
			}
			global::CameraFX.PLATFORM_POLL.flipRequired = true;
		}

		// Token: 0x04001596 RID: 5526
		public static readonly bool flipRequired;
	}

	// Token: 0x02000500 RID: 1280
	public struct CameraTransitionData
	{
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000A3CE8 File Offset: 0x000A1EE8
		public static global::CameraFX.CameraTransitionData identity
		{
			get
			{
				global::CameraFX.CameraTransitionData result = default(global::CameraFX.CameraTransitionData);
				result.view = (result.proj = (result.lastView = (result.lastProj = Matrix4x4G.identity)));
				result.end = (result.start = (result.lastTime = float.NegativeInfinity));
				result.func = global::TransitionFunction.Linear;
				return result;
			}
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000A3D54 File Offset: 0x000A1F54
		public int Update(ref Matrix4x4G currentView, ref Matrix4x4G currentProj)
		{
			int result;
			try
			{
				float timeSource = global::CameraFX.CameraTransitionData.timeSource;
				if (this.end > timeSource)
				{
					float num = Mathf.InverseLerp(this.start, this.end, timeSource);
					if (num < 1f)
					{
						num = this.func.Evaluate(num);
						Matrix4x4G matrix4x4G = global::TransitionFunctions.SlerpWorldToCamera((double)num, this.view, currentView);
						Matrix4x4G matrix4x4G2 = global::TransitionFunctions.Linear((double)num, this.proj, currentProj);
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

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x000A3E68 File Offset: 0x000A2068
		public static float timeSource
		{
			get
			{
				return Time.time;
			}
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000A3E70 File Offset: 0x000A2070
		public void Set(float duration, global::TransitionFunction func)
		{
			this.start = global::CameraFX.CameraTransitionData.timeSource;
			this.lastTime = this.start;
			this.end = this.start + duration;
			this.view = this.lastView;
			this.proj = this.lastProj;
			this.func = func;
		}

		// Token: 0x04001597 RID: 5527
		public global::TransitionFunction func;

		// Token: 0x04001598 RID: 5528
		public Matrix4x4G view;

		// Token: 0x04001599 RID: 5529
		public Matrix4x4G proj;

		// Token: 0x0400159A RID: 5530
		private Matrix4x4G lastView;

		// Token: 0x0400159B RID: 5531
		private Matrix4x4G lastProj;

		// Token: 0x0400159C RID: 5532
		public float start;

		// Token: 0x0400159D RID: 5533
		public float end;

		// Token: 0x0400159E RID: 5534
		public float lastTime;
	}
}
