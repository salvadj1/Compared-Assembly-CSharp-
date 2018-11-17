using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000757 RID: 1879
public class ViewModel : IDRemote, global::Socket.Source, global::Socket.Mapped, global::Socket.Provider
{
	// Token: 0x06003E1C RID: 15900 RVA: 0x000E1060 File Offset: 0x000DF260
	public ViewModel()
	{
		this.socketNames = global::ViewModel.defaultSocketNames;
		base..ctor();
	}

	// Token: 0x06003E1E RID: 15902 RVA: 0x000E11F4 File Offset: 0x000DF3F4
	bool global::Socket.Source.GetSocket(string name, out global::Socket socket)
	{
		switch (name)
		{
		case "muzzle":
			socket = this.muzzle;
			return true;
		case "sight":
			socket = this.sight;
			return true;
		case "optics":
			socket = this.optics;
			return true;
		case "pivot1":
			socket = this.pivot;
			return true;
		case "pivot2":
			socket = this.pivot2;
			return true;
		case "bowPivot":
			socket = this.bowPivot;
			return true;
		}
		socket = null;
		return false;
	}

	// Token: 0x06003E1F RID: 15903 RVA: 0x000E12E0 File Offset: 0x000DF4E0
	bool global::Socket.Source.ReplaceSocket(string name, global::Socket socket)
	{
		global::Socket.CameraSpace cameraSpace = (global::Socket.CameraSpace)socket;
		switch (name)
		{
		case "muzzle":
			this.muzzle = cameraSpace;
			return true;
		case "sight":
			this.sight = cameraSpace;
			return true;
		case "optics":
			this.optics = cameraSpace;
			return true;
		case "pivot1":
			this.pivot = cameraSpace;
			return true;
		case "pivot2":
			this.pivot2 = cameraSpace;
			return true;
		case "bowPivot":
			this.bowPivot = cameraSpace;
			return true;
		}
		return false;
	}

	// Token: 0x17000BC0 RID: 3008
	// (get) Token: 0x06003E20 RID: 15904 RVA: 0x000E13C8 File Offset: 0x000DF5C8
	IEnumerable<string> global::Socket.Source.SocketNames
	{
		get
		{
			return this.socketNames;
		}
	}

	// Token: 0x17000BC1 RID: 3009
	// (get) Token: 0x06003E21 RID: 15905 RVA: 0x000E13D0 File Offset: 0x000DF5D0
	int global::Socket.Source.SocketsVersion
	{
		get
		{
			return this.socketVersion;
		}
	}

	// Token: 0x06003E22 RID: 15906 RVA: 0x000E13D8 File Offset: 0x000DF5D8
	global::Socket.CameraConversion global::Socket.Source.CameraSpaceSetup()
	{
		return new global::Socket.CameraConversion(this.eye, this.shelf);
	}

	// Token: 0x06003E23 RID: 15907 RVA: 0x000E13EC File Offset: 0x000DF5EC
	Type global::Socket.Source.ProxyScriptType(string name)
	{
		return typeof(global::SocketProxy);
	}

	// Token: 0x17000BC2 RID: 3010
	// (get) Token: 0x06003E24 RID: 15908 RVA: 0x000E13F8 File Offset: 0x000DF5F8
	public global::Socket.Map socketMap
	{
		get
		{
			return this._socketMap.Get<global::ViewModel>(this);
		}
	}

	// Token: 0x06003E25 RID: 15909 RVA: 0x000E1408 File Offset: 0x000DF608
	protected void BindCameraSpaceTransforms(Transform newShelf, Transform newEye)
	{
		Transform transform = this.eye;
		Transform transform2 = this.shelf;
		this.eye = newEye;
		this.shelf = newShelf;
		if (transform != newEye || transform2 != newShelf)
		{
			this.socketVersion++;
		}
	}

	// Token: 0x06003E26 RID: 15910 RVA: 0x000E1458 File Offset: 0x000DF658
	protected void DeleteSocketMap()
	{
		this._socketMap.DeleteBy<global::ViewModel>(this);
	}

	// Token: 0x17000BC3 RID: 3011
	// (get) Token: 0x06003E27 RID: 15911 RVA: 0x000E1468 File Offset: 0x000DF668
	public global::Character idMain
	{
		get
		{
			return (global::Character)base.idMain;
		}
	}

	// Token: 0x17000BC4 RID: 3012
	// (get) Token: 0x06003E28 RID: 15912 RVA: 0x000E1478 File Offset: 0x000DF678
	public bool drawCrosshair
	{
		get
		{
			return this.showCrosshairZoom || this.showCrosshairNotZoomed;
		}
	}

	// Token: 0x06003E29 RID: 15913 RVA: 0x000E1490 File Offset: 0x000DF690
	protected void Awake()
	{
		this.originalRootOffset = this.root.localPosition;
		this.originalRootRotation = this.root.localRotation;
		base.Awake();
		if (this.builtinRenderers != null)
		{
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in this.builtinRenderers)
			{
				if (skinnedMeshRenderer)
				{
					this.meshInstances.Add(skinnedMeshRenderer);
				}
			}
		}
		this.idleMixer = this.idleFrame.Alias(this.animation, new global::AnimationBlender.ChannelConfig[9].Define(0, "idle", this.idleChannel).Define(1, "move", this.movementIdleChannel).Define(4, "bowi", this.bowChannel).Define(5, "bowm", this.bowMovementChannel).Define(2, "dcki", this.crouchChannel).Define(3, "dckm", this.crouchMovementChannel).Define(6, "fall", this.fallChannel).Define(7, "slip", this.slipChannel).Define(8, "zoom", this.zoomChannel)).Create();
	}

	// Token: 0x06003E2A RID: 15914 RVA: 0x000E15C4 File Offset: 0x000DF7C4
	public void Flip()
	{
		if (!this.flipped)
		{
			Vector3 localScale = base.transform.localScale;
			localScale.z = -localScale.z;
			base.transform.localScale = localScale;
			this.flipped = true;
		}
	}

	// Token: 0x17000BC5 RID: 3013
	// (get) Token: 0x06003E2B RID: 15915 RVA: 0x000E160C File Offset: 0x000DF80C
	// (set) Token: 0x06003E2C RID: 15916 RVA: 0x000E1614 File Offset: 0x000DF814
	public global::HeadBob headBob
	{
		get
		{
			return this._headBob;
		}
		set
		{
			this._headBob = value;
		}
	}

	// Token: 0x17000BC6 RID: 3014
	// (get) Token: 0x06003E2D RID: 15917 RVA: 0x000E1620 File Offset: 0x000DF820
	// (set) Token: 0x06003E2E RID: 15918 RVA: 0x000E1628 File Offset: 0x000DF828
	public global::LazyCam lazyCam
	{
		get
		{
			return this._lazyCam;
		}
		set
		{
			this._lazyCam = value;
		}
	}

	// Token: 0x17000BC7 RID: 3015
	// (get) Token: 0x06003E2F RID: 15919 RVA: 0x000E1634 File Offset: 0x000DF834
	// (set) Token: 0x06003E30 RID: 15920 RVA: 0x000E163C File Offset: 0x000DF83C
	public Quaternion lazyRotation
	{
		get
		{
			return this._additiveRotation;
		}
		set
		{
			if (this._additiveRotation != value)
			{
				this.pivot2.Rotate(this._additiveRotation);
				this.pivot.UnRotate(this._additiveRotation);
				this.pivot.Rotate(value);
				this.pivot2.UnRotate(value);
				this._additiveRotation = value;
			}
		}
	}

	// Token: 0x17000BC8 RID: 3016
	// (get) Token: 0x06003E31 RID: 15921 RVA: 0x000E169C File Offset: 0x000DF89C
	public Quaternion muzzleRotation
	{
		get
		{
			return this.muzzle.rotation;
		}
	}

	// Token: 0x17000BC9 RID: 3017
	// (get) Token: 0x06003E32 RID: 15922 RVA: 0x000E16AC File Offset: 0x000DF8AC
	public Vector3 muzzlePosition
	{
		get
		{
			return this.muzzle.position;
		}
	}

	// Token: 0x06003E33 RID: 15923 RVA: 0x000E16BC File Offset: 0x000DF8BC
	public bool Play(string name)
	{
		return this.idleMixer.Play(name);
	}

	// Token: 0x06003E34 RID: 15924 RVA: 0x000E16CC File Offset: 0x000DF8CC
	public bool Play(string name, PlayMode playMode)
	{
		return this.idleMixer.Play(name, playMode);
	}

	// Token: 0x06003E35 RID: 15925 RVA: 0x000E16DC File Offset: 0x000DF8DC
	public bool Play(string name, float speed)
	{
		return this.idleMixer.Play(name, speed);
	}

	// Token: 0x06003E36 RID: 15926 RVA: 0x000E16EC File Offset: 0x000DF8EC
	public bool Play(string name, float speed, float time)
	{
		return this.idleMixer.Play(name, speed, time);
	}

	// Token: 0x06003E37 RID: 15927 RVA: 0x000E16FC File Offset: 0x000DF8FC
	public bool Play(string name, PlayMode playMode, float speed)
	{
		return this.idleMixer.Play(name, playMode, speed);
	}

	// Token: 0x06003E38 RID: 15928 RVA: 0x000E170C File Offset: 0x000DF90C
	public bool Play(string name, PlayMode playMode, float speed, float time)
	{
		return this.idleMixer.Play(name, playMode, speed, time);
	}

	// Token: 0x06003E39 RID: 15929 RVA: 0x000E1720 File Offset: 0x000DF920
	public bool PlayQueued(string name)
	{
		return this.idleMixer.PlayQueued(name);
	}

	// Token: 0x06003E3A RID: 15930 RVA: 0x000E1730 File Offset: 0x000DF930
	public bool PlayQueued(string name, QueueMode queueMode)
	{
		return this.idleMixer.PlayQueued(name, queueMode);
	}

	// Token: 0x06003E3B RID: 15931 RVA: 0x000E1740 File Offset: 0x000DF940
	public bool PlayQueued(string name, QueueMode queueMode, PlayMode playMode)
	{
		return this.idleMixer.PlayQueued(name, queueMode, playMode);
	}

	// Token: 0x06003E3C RID: 15932 RVA: 0x000E1750 File Offset: 0x000DF950
	public void CrossFade(string name)
	{
		this.idleMixer.CrossFade(name);
	}

	// Token: 0x06003E3D RID: 15933 RVA: 0x000E1760 File Offset: 0x000DF960
	public void CrossFade(string name, float fadeLength)
	{
		this.idleMixer.CrossFade(name, fadeLength);
	}

	// Token: 0x06003E3E RID: 15934 RVA: 0x000E1770 File Offset: 0x000DF970
	public void CrossFade(string name, float fadeLength, PlayMode playMode)
	{
		this.idleMixer.CrossFade(name, fadeLength, playMode);
	}

	// Token: 0x06003E3F RID: 15935 RVA: 0x000E1784 File Offset: 0x000DF984
	public void CrossFade(string name, float fadeLength, PlayMode playMode, float speed)
	{
		this.idleMixer.CrossFade(name, fadeLength, playMode, speed);
	}

	// Token: 0x06003E40 RID: 15936 RVA: 0x000E1798 File Offset: 0x000DF998
	public void PlayFireAnimation(float speed)
	{
		this.Play(this.fireAnimName, speed);
		this.punchTime = Time.time;
	}

	// Token: 0x06003E41 RID: 15937 RVA: 0x000E17B4 File Offset: 0x000DF9B4
	public void PlayFireAnimation()
	{
		this.PlayFireAnimation(this.fireAnimScaleSpeed);
	}

	// Token: 0x06003E42 RID: 15938 RVA: 0x000E17C4 File Offset: 0x000DF9C4
	public void PlayDeployAnimation()
	{
		this.Play(this.deployAnimName);
	}

	// Token: 0x06003E43 RID: 15939 RVA: 0x000E17D4 File Offset: 0x000DF9D4
	public void PlayReloadAnimation()
	{
		this.Play(this.reloadAnimName);
	}

	// Token: 0x06003E44 RID: 15940 RVA: 0x000E17E4 File Offset: 0x000DF9E4
	public void AddRenderers(SkinnedMeshRenderer[] renderers)
	{
		if (renderers != null)
		{
			foreach (SkinnedMeshRenderer renderer in renderers)
			{
				this.meshInstances.Add(renderer);
			}
		}
	}

	// Token: 0x06003E45 RID: 15941 RVA: 0x000E1820 File Offset: 0x000DFA20
	public void RemoveRenderers(SkinnedMeshRenderer[] renderers)
	{
		if (renderers != null)
		{
			foreach (SkinnedMeshRenderer renderer in renderers)
			{
				this.meshInstances.Delete(renderer);
			}
		}
	}

	// Token: 0x06003E46 RID: 15942 RVA: 0x000E185C File Offset: 0x000DFA5C
	public void BindTransforms(Transform shelf, Transform eye)
	{
		this.punchTime = Time.time - 20f;
		this.BindCameraSpaceTransforms(shelf, eye);
	}

	// Token: 0x06003E47 RID: 15943 RVA: 0x000E1878 File Offset: 0x000DFA78
	private void ClearProxies()
	{
		this.DeleteSocketMap();
		if (this.destroyOnUnbind != null)
		{
			foreach (GameObject gameObject in this.destroyOnUnbind)
			{
				if (gameObject)
				{
					Object.Destroy(gameObject);
				}
			}
		}
		this.destroyOnUnbind = null;
	}

	// Token: 0x06003E48 RID: 15944 RVA: 0x000E1900 File Offset: 0x000DFB00
	public void UnBindTransforms()
	{
		this.ClearProxies();
		if (global::CameraFX.mainViewModel == this)
		{
			global::CameraFX mainCameraFX = global::CameraFX.mainCameraFX;
			if (mainCameraFX)
			{
				mainCameraFX.SetFieldOfView(320432f, 0f);
			}
		}
	}

	// Token: 0x06003E49 RID: 15945 RVA: 0x000E1944 File Offset: 0x000DFB44
	private static void SolveTriangleSAS(float angleA, float lengthB, float lengthC, out float lengthA, out float angleB, out float angleC)
	{
		lengthA = Mathf.Sqrt(lengthB * lengthB + lengthC * lengthC - 2f * lengthB * lengthC * Mathf.Cos(angleA * 0.0174532924f));
		if (angleA >= 90f || lengthB < lengthC)
		{
			angleB = Mathf.Asin(Mathf.Sin(angleA * 0.0174532924f) * lengthB / lengthA) * 57.29578f;
			angleC = 180f - (angleA + angleB);
		}
		else
		{
			angleC = Mathf.Asin(Mathf.Sin(angleA * 0.0174532924f) * lengthC / lengthA) * 57.29578f;
			angleB = 180f - (angleA + angleC);
		}
	}

	// Token: 0x06003E4A RID: 15946 RVA: 0x000E19E8 File Offset: 0x000DFBE8
	private static void SolveTriangleSSA(float angleB, float lengthB, float lengthC, out float lengthA, out float angleA, out float angleC)
	{
		float num = Mathf.Sin(angleB * 0.0174532924f);
		angleC = Mathf.Asin(num * lengthC / lengthB) * 57.29578f;
		angleA = 180f - angleC - angleB;
		if (angleA < 0f || angleA > 180f)
		{
			angleA += 180f;
		}
		lengthA = Mathf.Sin(angleA * 0.0174532924f) * lengthB / num;
	}

	// Token: 0x06003E4B RID: 15947 RVA: 0x000E1A60 File Offset: 0x000DFC60
	private global::ViewModel.BarrelTransform BarrelAim(Vector3 offset, ref global::ViewModel.BarrelParameters barrel)
	{
		Ray eyesRay = this.idMain.eyesRay;
		RaycastHit2 raycastHit;
		float num;
		Vector3 vector;
		if (Physics2.Raycast2(eyesRay, ref raycastHit, this.noHitPlane, this.aimMask.value))
		{
			num = raycastHit.distance;
			vector = raycastHit.point;
		}
		else
		{
			num = this.noHitPlane;
			vector = eyesRay.GetPoint(this.noHitPlane);
		}
		vector = this.idMain.eyesTransformReadOnly.InverseTransformPoint(vector);
		num = vector.magnitude;
		Vector3 vector2 = Vector3.Scale(offset + this.barrelPivot, base.transform.localScale);
		Plane plane;
		plane..ctor(this.idMain.eyesTransformReadOnly.InverseTransformDirection(eyesRay.direction), vector2);
		Ray ray;
		ray..ctor(vector, -vector);
		float num2;
		plane.Raycast(ray, ref num2);
		Vector3 point = ray.GetPoint(num2);
		float ca = Vector3.Distance(point, vector2);
		float num3 = Vector3.Distance(vector, vector2);
		if (Mathf.Approximately(0f, num3) && barrel.ir)
		{
			barrel.ir = false;
		}
		barrel.bc = num3;
		barrel.ca = ca;
		barrel.a = 90f;
		global::ViewModel.SolveTriangleSSA(barrel.a, barrel.bc, barrel.ca, out barrel.ab, out barrel.c, out barrel.b);
		barrel.ir = true;
		float num4 = -(90f - barrel.c);
		if (!barrel.once)
		{
			barrel.once = true;
			barrel.angle = num4;
		}
		else if (this.barrelAngleSmoothDamp <= 0f)
		{
			if (this.barrelAngleMaxSpeed <= 0f || this.barrelAngleMaxSpeed == float.PositiveInfinity)
			{
				barrel.angle = num4;
			}
			else
			{
				barrel.angle = Mathf.MoveTowardsAngle(barrel.angle, num4, this.barrelAngleMaxSpeed * Time.deltaTime);
			}
		}
		else if (this.barrelAngleMaxSpeed <= 0f)
		{
			barrel.angle = Mathf.SmoothDampAngle(barrel.angle, num4, ref barrel.angularVelocity, this.barrelAngleSmoothDamp);
		}
		else
		{
			barrel.angle = Mathf.SmoothDampAngle(barrel.angle, num4, ref barrel.angularVelocity, this.barrelAngleSmoothDamp, this.barrelAngleMaxSpeed);
		}
		Quaternion quaternion = Quaternion.Euler(-this.barrelRotation.x, this.barrelRotation.y, 0f);
		Quaternion quaternion2 = Quaternion.Inverse(quaternion);
		float angle = barrel.angle;
		Plane plane2;
		plane2..ctor(vector, vector2, Vector3.zero);
		Quaternion quaternion3 = quaternion2 * Quaternion.AngleAxis(angle, plane2.normal);
		Vector3 vector3;
		Vector3 vector4;
		if (barrel.bc < this.barrelLimit)
		{
			if (this.barrelLimitOffsetFactor != 0f)
			{
				vector3 = offset - quaternion3 * (Vector3.forward * ((this.barrelLimit - barrel.bc) * this.barrelLimitOffsetFactor));
			}
			else
			{
				vector3 = offset;
			}
			if (this.barrelLimitPivotFactor != 0f)
			{
				vector4 = this.barrelPivot + quaternion * (Vector3.back * ((this.barrelLimit - barrel.bc) * this.barrelLimitPivotFactor));
			}
			else
			{
				vector4 = this.barrelPivot;
			}
		}
		else
		{
			vector4 = this.barrelPivot;
			vector3 = offset;
		}
		global::ViewModel.BarrelTransform result;
		result.origin = quaternion3 * -vector4 + vector4;
		result.angles = quaternion3.eulerAngles;
		result.origin += vector3;
		result.angles.x = Mathf.DeltaAngle(0f, result.angles.x);
		result.angles.y = Mathf.DeltaAngle(0f, result.angles.y);
		result.angles.z = Mathf.DeltaAngle(0f, result.angles.z);
		return result;
	}

	// Token: 0x06003E4C RID: 15948 RVA: 0x000E1E5C File Offset: 0x000E005C
	protected void LateUpdate()
	{
		global::Character idMain = this.idMain;
		if (!idMain)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		bool flag;
		bool flag2;
		bool flag3;
		bool flag4;
		bool flag5;
		bool flag6;
		global::Angle2 eyesAngles;
		bool flag7;
		if (idMain)
		{
			flag = idMain.stateFlags.aim;
			flag2 = !idMain.stateFlags.grounded;
			flag3 = idMain.stateFlags.slipping;
			flag4 = idMain.stateFlags.movement;
			flag5 = idMain.stateFlags.aim;
			flag6 = idMain.stateFlags.crouch;
			eyesAngles = idMain.eyesAngles;
			flag7 = (this.bowAllowed && idMain.stateFlags.sprint && flag4);
		}
		else
		{
			flag = false;
			flag2 = false;
			flag3 = false;
			flag6 = false;
			flag4 = false;
			flag5 = false;
			flag7 = false;
			eyesAngles = this.lastLook;
		}
		float num;
		if (eyesAngles == this.lastLook)
		{
			num = 0f;
		}
		else
		{
			num = global::Angle2.AngleDistance(this.lastLook, eyesAngles) / deltaTime;
			this.lastLook = eyesAngles;
		}
		if (flag2)
		{
			this.idleMixer.SetSolo(6);
		}
		else if (flag3)
		{
			this.idleMixer.SetSolo(7);
		}
		else if (flag)
		{
			this.idleMixer.SetSolo(8);
		}
		else if (flag7)
		{
			if (flag4)
			{
				this.idleMixer.SetSolo(5);
			}
			else
			{
				this.idleMixer.SetSolo(4);
			}
		}
		else if (flag6)
		{
			if (flag4)
			{
				this.idleMixer.SetSolo(3);
			}
			else
			{
				this.idleMixer.SetSolo(2);
			}
		}
		else if (flag4)
		{
			this.idleMixer.SetSolo(1);
		}
		else
		{
			this.idleMixer.SetSolo(0);
			if (num < -2f || num > 2f)
			{
				this.idleMixer.SetActive(0, false);
			}
		}
		float num2 = Time.deltaTime / ((!flag5) ? (-this.zoomOutDuration) : this.zoomInDuration);
		float num3 = this.zoomCurve.EvaluateClampedTime(ref this.zoomTime, num2);
		float num4 = Time.deltaTime / ((!flag7) ? (-this.bowExitDuration) : this.bowEnterDuration);
		float num5;
		if (float.IsInfinity(num4))
		{
			num5 = ((!flag7) ? 0f : 1f);
		}
		else
		{
			if (this.bowCurveIs01Fraction)
			{
				num4 *= this.bowCurve[0].time - this.bowCurve[this.bowCurve.length].time;
			}
			num5 = this.bowCurve.EvaluateClampedTime(ref this.bowTime, num4);
		}
		if (flag7 == flag5)
		{
			if (this.bowAllowed)
			{
				if (flag7)
				{
					float num6 = Mathf.Max(num5, num2);
				}
				else
				{
					float num6 = Mathf.Min(num5, num2);
				}
			}
		}
		else if (flag5 || !this.bowAllowed)
		{
		}
		this.root.localPosition = this.originalRootOffset;
		this.root.localRotation = this.originalRootRotation;
		Vector3 preEyePosition = this.sight.preEyePosition;
		Vector3 preEyePosition2 = this.bowPivot.preEyePosition;
		Vector3 vector = -this.root.InverseTransformPoint(preEyePosition);
		Vector3 vector2 = -this.root.InverseTransformPoint(preEyePosition2);
		Quaternion quaternion = this.sight.preEyeRotation;
		Vector3 vector3 = this.root.InverseTransformDirection(quaternion * Vector3.forward);
		Vector3 vector4 = this.root.InverseTransformDirection(quaternion * Vector3.up);
		quaternion = Quaternion.Inverse(Quaternion.LookRotation(vector3, vector4));
		vector = quaternion * vector;
		Vector3 eulerAngles = quaternion.eulerAngles;
		Quaternion quaternion2 = this.bowPivot.preEyeRotation;
		Vector3 vector5 = this.root.InverseTransformPoint(quaternion2 * Vector3.forward);
		Vector3 vector6 = this.root.InverseTransformDirection(quaternion2 * Vector3.up);
		quaternion2 = Quaternion.Inverse(Quaternion.LookRotation(vector5, vector6));
		vector2 = quaternion2 * vector2;
		Vector3 eulerAngles2 = quaternion2.eulerAngles;
		Vector3 vector7;
		Vector3 vector8;
		if (this.barrelAiming)
		{
			this.BarrelAim(this.offset, ref this.bpHip).Get(out vector7, out vector8);
		}
		else
		{
			vector7 = this.offset;
			vector8 = this.rotate;
		}
		if (this.barrelWhileZoom)
		{
			this.BarrelAim(vector, ref this.bpZoom).Get(out vector, out eulerAngles);
		}
		if (this.barrelWhileBowing)
		{
			this.BarrelAim(vector2, ref this.bpBow).Get(out vector2, out eulerAngles2);
		}
		float num7 = 1f - num3;
		float num8 = this.zoomPunch.Evaluate(Time.time - this.punchTime) * this.punchScalar;
		float num9 = 1f - num5;
		Vector3 vector9;
		vector9.x = (vector2.x + this.bowOffsetPoint.x) * num5 + ((vector.x + this.zoomOffset.x) * num3 + vector7.x * num7) * num9;
		vector9.y = (vector2.y + this.bowOffsetPoint.y) * num5 + ((vector.y + this.zoomOffset.y) * num3 + vector7.y * num7 * num9);
		vector9.z = (vector2.z + this.bowOffsetPoint.z) * num5 + ((vector.z + (this.zoomOffset.z - num8)) * num3 + vector7.z * num7) * num9;
		Vector3 vector10;
		vector10.x = Mathf.DeltaAngle(0f, ((Mathf.DeltaAngle(this.zoomRotate.x, eulerAngles.x) + this.zoomRotate.x) * num3 + vector8.x * num7) * num9 + (Mathf.DeltaAngle(this.bowOffsetAngles.x, eulerAngles2.x) + this.bowOffsetAngles.x) * num5);
		vector10.y = Mathf.DeltaAngle(0f, ((Mathf.DeltaAngle(this.zoomRotate.y, eulerAngles.y) + this.zoomRotate.y) * num3 + vector8.y * num7) * num9 + (Mathf.DeltaAngle(this.bowOffsetAngles.y, eulerAngles2.y) + this.bowOffsetAngles.y) * num5);
		vector10.z = Mathf.DeltaAngle(0f, ((Mathf.DeltaAngle(this.zoomRotate.z, eulerAngles.z) + this.zoomRotate.z) * num3 + vector8.z * num7) * num9 + (Mathf.DeltaAngle(this.bowOffsetAngles.z, eulerAngles2.z) + this.bowOffsetAngles.z) * num5);
		this.lastLocalPositionOffset = vector9;
		this.lastLocalRotationOffset = vector10;
		this.root.localEulerAngles += this.lastLocalRotationOffset;
		this.root.localPosition += this.lastLocalPositionOffset;
		this.lastZoomFraction = num3;
		if (this._headBob)
		{
			global::CameraFX mainCameraFX = global::CameraFX.mainCameraFX;
			if (mainCameraFX)
			{
				mainCameraFX.SetFieldOfView(this.zoomFieldOfView, num3);
			}
			else
			{
				Debug.Log("No CamFX");
			}
		}
		this.pivot.Rotate(this._additiveRotation);
		this.pivot2.UnRotate(this._additiveRotation);
		if (this._lazyCam)
		{
			this._lazyCam.allow = (!flag5 && !flag7);
		}
		if (this._headBob)
		{
			this._headBob.viewModelPositionScalar = this.headBobOffsetScale.EvaluateClampedTime(ref this.headBobLinearTime, num2);
			this._headBob.viewModelRotationScalar = this.headBobRotationScale.EvaluateClampedTime(ref this.headBobAngularTime, num2);
		}
	}

	// Token: 0x06003E4D RID: 15949 RVA: 0x000E26A4 File Offset: 0x000E08A4
	private void OnDrawGizmosSelected()
	{
		if (!this.root)
		{
			return;
		}
		this.pivot.DrawGizmos("pivot1");
		this.pivot2.DrawGizmos("pivot2");
		this.muzzle.DrawGizmos("muzzle");
		this.sight.DrawGizmos("sights");
		this.bowPivot.DrawGizmos("bow");
		Gizmos.matrix = this.root.localToWorldMatrix;
		Vector3 vector = global::Angle2.Direction(this.barrelRotation.x, this.barrelRotation.y);
		Gizmos.DrawSphere(this.barrelPivot, 0.001f);
		Gizmos.DrawLine(this.barrelPivot, this.barrelPivot + vector);
		Gizmos.matrix *= Matrix4x4.TRS(this.barrelPivot, Quaternion.Euler(-this.barrelRotation.x, this.barrelRotation.y, 0f), Vector3.one);
		Gizmos.DrawWireCube(Vector3.forward * (this.barrelLimit * 0.5f), new Vector3(0.02f, 0.02f, this.barrelLimit));
		float a = this.bpHip.a;
		float b = this.bpHip.b;
		float c = this.bpHip.c;
		float ab = this.bpHip.ab;
		float bc = this.bpHip.bc;
		float ca = this.bpHip.ca;
		Quaternion quaternion = Quaternion.Euler(0f, 0f, a);
		Quaternion quaternion2 = Quaternion.Euler(0f, 0f, a + b);
		Quaternion quaternion3 = Quaternion.Euler(0f, 0f, a + b + c);
		Vector3 vector2 = quaternion * (Vector3.up * ab);
		Vector3 vector3 = vector2 + quaternion2 * (Vector3.up * bc);
		Vector3 vector4 = vector3 + quaternion3 * (Vector3.up * ca);
		Bounds bounds = default(Bounds);
		bounds.Encapsulate(vector4);
		bounds.Encapsulate(vector2);
		bounds.Encapsulate(vector3);
		Gizmos.matrix = Matrix4x4.TRS(-vector3, Quaternion.identity, Vector3.one);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(vector4, 0.01f);
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(vector4, vector2);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(vector2, 0.01f);
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(vector2, vector3);
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(vector3, 0.01f);
		Gizmos.color = Color.magenta;
		Gizmos.DrawLine(vector3, vector4);
		Gizmos.color = Color.black;
		float num = ca;
		float num2 = a * 0.0174532924f;
		float num3 = num * Mathf.Sin(num2);
		float num4 = Mathf.Sqrt(num3 * num3 + num * num);
		Gizmos.color = Color.black;
		Vector3 vector5 = quaternion2 * (Vector3.up * num3);
		Gizmos.DrawLine(vector5, vector3);
		Gizmos.color = Color.gray;
		vector5 = quaternion * (Vector3.up * num4);
		Gizmos.DrawLine(vector5, vector3);
	}

	// Token: 0x06003E4E RID: 15950 RVA: 0x000E29E8 File Offset: 0x000E0BE8
	[ContextMenu("Set as current view model")]
	private void SetAsCurrentViewModel()
	{
		if (base.enabled)
		{
			global::CameraFX.ReplaceViewModel(this, this.itemRep, this.item, false);
		}
	}

	// Token: 0x06003E4F RID: 15951 RVA: 0x000E2A08 File Offset: 0x000E0C08
	public void UpdateProxies()
	{
		global::Socket.Map socketMap = this.socketMap;
		if (!object.ReferenceEquals(socketMap, null))
		{
			socketMap.SnapProxies();
		}
	}

	// Token: 0x06003E50 RID: 15952 RVA: 0x000E2A30 File Offset: 0x000E0C30
	protected void Update()
	{
		this.idleMixer.Update(1f, Time.deltaTime);
	}

	// Token: 0x06003E51 RID: 15953 RVA: 0x000E2A48 File Offset: 0x000E0C48
	private void DrawShadowed(ref Rect r, Texture texture)
	{
		Color color = GUI.color;
		if (color.a > 0.5f)
		{
			Rect rect = r;
			rect.x += 1f;
			rect.y -= 1f;
			Color color2;
			color2.a = (color.a - 0.5f) * 2f;
			color2.a = this.crosshairOutline.a * (color2.a * color2.a);
			color2.r = this.crosshairOutline.r;
			color2.g = this.crosshairOutline.g;
			color2.b = this.crosshairOutline.b;
			GUI.color = color2;
			GUI.DrawTexture(rect, texture);
			rect.x -= 2f;
			GUI.DrawTexture(rect, texture);
			rect.y += 2f;
			GUI.DrawTexture(rect, texture);
			rect.x += 2f;
			GUI.DrawTexture(rect, texture);
			float num = 1f - color2.a;
			color2.r = this.crosshairColor.r * color2.a + this.crosshairOutline.r * num;
			color2.g = this.crosshairColor.g * color2.a + this.crosshairOutline.g * num;
			color2.b = this.crosshairColor.b * color2.a + this.crosshairOutline.b * num;
			color2.a = this.crosshairColor.a * color2.a + this.crosshairOutline.a * num;
			GUI.color = color2;
			GUI.DrawTexture(r, texture);
		}
		else if (color.a > 0f)
		{
			float num2 = color.a * 2f;
			float num3 = num2 + (num2 - num2 * num2);
			float num4 = 1f - num3;
			GUI.color = new Color(this.crosshairOutline.r * num3 + this.crosshairColor.r * num4, this.crosshairOutline.g * num3 + this.crosshairColor.g * num4, this.crosshairOutline.b * num3 + this.crosshairColor.b * num4, this.crosshairOutline.a * (num2 * num2));
			GUI.DrawTexture(r, texture);
		}
		GUI.color = color;
	}

	// Token: 0x06003E52 RID: 15954 RVA: 0x000E2CE8 File Offset: 0x000E0EE8
	public void ModifyAiming(Ray ray, ref Vector3 p, ref Quaternion q)
	{
		if (global::ViewModel.modifyAiming)
		{
			RaycastHit2 raycastHit;
			float distance;
			if (Physics2.Raycast2(ray, ref raycastHit, this.noHitPlane))
			{
				distance = raycastHit.distance;
			}
			else
			{
				distance = this.noHitPlane;
			}
			Vector3 vector = this.shelf.InverseTransformPoint((this.pivot.position + this.pivot2.position) / 2f);
			Vector3 vector2 = this.shelf.InverseTransformPoint(ray.GetPoint(distance));
			float num = Vector3.Angle(vector, this.shelf.InverseTransformPoint(ray.origin));
			float num2 = distance * Mathf.Cos(num * 0.0174532924f);
			float num3 = Mathf.Atan2(num2, vector2.magnitude);
			q *= new Quaternion(0f, Mathf.Sin(num3), 0f, Mathf.Cos(num3));
		}
	}

	// Token: 0x06003E53 RID: 15955 RVA: 0x000E2DDC File Offset: 0x000E0FDC
	protected void OnDestroy()
	{
		base.OnDestroy();
		this.UnBindTransforms();
		this.meshInstances.Dispose();
	}

	// Token: 0x06003E54 RID: 15956 RVA: 0x000E2DF8 File Offset: 0x000E0FF8
	private void OnGUI()
	{
		if (Event.current.type != 7 || global::RPOS.IsOpen || !this.drawCrosshair || !this.crosshairTexture || !this.dotTexture)
		{
			return;
		}
		Camera camera;
		if (this._headBob)
		{
			camera = this._headBob.camera;
		}
		else
		{
			if (!this._lazyCam)
			{
				return;
			}
			camera = this._lazyCam.camera;
		}
		if (camera && (camera.enabled || global::MountedCamera.IsCameraBeingUsed(camera)))
		{
			Color color;
			color.r = 1f;
			color.g = 1f;
			color.b = 1f;
			if (this.showCrosshairNotZoomed)
			{
				if (this.showCrosshairZoom)
				{
					color.a = 1f;
				}
				else
				{
					color.a = Mathf.Clamp01(1f - this.lastZoomFraction);
				}
			}
			else if (this.showCrosshairZoom)
			{
				color.a = this.lastZoomFraction;
			}
			else
			{
				color.a = 1f;
			}
			if (color.a == 0f)
			{
				return;
			}
			GUI.color = color;
			Ray ray = camera.ViewportPointToRay(Vector3.one * 0.5f);
			Plane plane;
			plane..ctor(-camera.transform.forward, camera.transform.position + camera.transform.forward * this.noHitPlane);
			float num;
			plane.Raycast(ray, ref num);
			Vector3 point = ray.GetPoint(num);
			Vector3? vector = global::CameraFX.World2Screen(point);
			if (vector != null)
			{
				Vector3 value = vector.Value;
				value.y = (float)Screen.height - (value.y + 1f);
				Rect rect;
				rect..ctor(value.x - (float)this.crosshairTexture.width / 2f, value.y - (float)this.crosshairTexture.height / 2f, (float)this.crosshairTexture.width, (float)this.crosshairTexture.height);
				this.DrawShadowed(ref rect, this.crosshairTexture);
			}
			RaycastHit2 raycastHit;
			if (Physics2.Raycast2(ray, ref raycastHit))
			{
				vector = global::CameraFX.World2Screen(raycastHit.point);
				if (vector != null)
				{
					Vector3 value2 = vector.Value;
					value2.y = (float)Screen.height - (value2.y + 1f);
					Rect rect2;
					rect2..ctor(value2.x - (float)this.dotTexture.width / 2f, value2.y - (float)this.dotTexture.height / 2f, (float)this.dotTexture.width, (float)this.dotTexture.height);
					this.DrawShadowed(ref rect2, this.dotTexture);
				}
			}
		}
	}

	// Token: 0x06003E55 RID: 15957 RVA: 0x000E310C File Offset: 0x000E130C
	public void ModifyPerspective(ref global::PerspectiveMatrixBuilder perspective)
	{
		if ((this.caps & 1) == 1)
		{
			perspective.nearPlane = (double)this.perspectiveNearOverride;
		}
		if ((this.caps & 2) == 2)
		{
			perspective.farPlane = (double)this.perspectiveFarOverride;
		}
		if ((this.caps & 4) == 4)
		{
			perspective.fieldOfView = (double)this.perspectiveFOVOverride;
		}
		if ((this.caps & 8) == 8)
		{
			perspective.aspectRatio = (double)this.perspectiveAspectOverride;
		}
	}

	// Token: 0x0400203A RID: 8250
	public const int kCap_PerspectiveNear = 1;

	// Token: 0x0400203B RID: 8251
	public const int kCap_PerspectiveFar = 2;

	// Token: 0x0400203C RID: 8252
	public const int kCap_PerspectiveFOV = 4;

	// Token: 0x0400203D RID: 8253
	public const int kCap_PerspectiveAspect = 8;

	// Token: 0x0400203E RID: 8254
	protected const int kIdleChannel_Idle = 0;

	// Token: 0x0400203F RID: 8255
	protected const int kIdleChannel_IdleMovement = 1;

	// Token: 0x04002040 RID: 8256
	protected const int kIdleChannel_Crouch = 2;

	// Token: 0x04002041 RID: 8257
	protected const int kIdleChannel_CrouchMovement = 3;

	// Token: 0x04002042 RID: 8258
	protected const int kIdleChannel_Bow = 4;

	// Token: 0x04002043 RID: 8259
	protected const int kIdleChannel_BowMovement = 5;

	// Token: 0x04002044 RID: 8260
	protected const int kIdleChannel_Fall = 6;

	// Token: 0x04002045 RID: 8261
	protected const int kIdleChannel_Slip = 7;

	// Token: 0x04002046 RID: 8262
	protected const int kIdleChannel_Zoom = 8;

	// Token: 0x04002047 RID: 8263
	protected const int kIdleChannelCount = 9;

	// Token: 0x04002048 RID: 8264
	protected const string kIdleChannel_Idle_Name = "idle";

	// Token: 0x04002049 RID: 8265
	protected const string kIdleChannel_IdleMovement_Name = "move";

	// Token: 0x0400204A RID: 8266
	protected const string kIdleChannel_Bow_Name = "bowi";

	// Token: 0x0400204B RID: 8267
	protected const string kIdleChannel_BowMovement_Name = "bowm";

	// Token: 0x0400204C RID: 8268
	protected const string kIdleChannel_Crouch_Name = "dcki";

	// Token: 0x0400204D RID: 8269
	protected const string kIdleChannel_CrouchMovement_Name = "dckm";

	// Token: 0x0400204E RID: 8270
	protected const string kIdleChannel_Fall_Name = "fall";

	// Token: 0x0400204F RID: 8271
	protected const string kIdleChannel_Slip_Name = "slip";

	// Token: 0x04002050 RID: 8272
	protected const string kIdleChannel_Zoom_Name = "zoom";

	// Token: 0x04002051 RID: 8273
	[SerializeField]
	public global::Socket.CameraSpace pivot;

	// Token: 0x04002052 RID: 8274
	[SerializeField]
	public global::Socket.CameraSpace pivot2;

	// Token: 0x04002053 RID: 8275
	[SerializeField]
	public global::Socket.CameraSpace muzzle;

	// Token: 0x04002054 RID: 8276
	[SerializeField]
	public global::Socket.CameraSpace sight;

	// Token: 0x04002055 RID: 8277
	[SerializeField]
	public global::Socket.CameraSpace optics;

	// Token: 0x04002056 RID: 8278
	[SerializeField]
	public global::Socket.CameraSpace bowPivot;

	// Token: 0x04002057 RID: 8279
	protected static readonly string[] defaultSocketNames = new string[]
	{
		"muzzle",
		"sight",
		"optics",
		"pivot1",
		"pivot2",
		"bowPivot"
	};

	// Token: 0x04002058 RID: 8280
	[NonSerialized]
	protected IEnumerable<string> socketNames;

	// Token: 0x04002059 RID: 8281
	[NonSerialized]
	protected int socketVersion;

	// Token: 0x0400205A RID: 8282
	[NonSerialized]
	private global::Socket.Map.Member _socketMap;

	// Token: 0x0400205B RID: 8283
	private Vector3 originalRootOffset;

	// Token: 0x0400205C RID: 8284
	private Quaternion originalRootRotation;

	// Token: 0x0400205D RID: 8285
	private bool flipped;

	// Token: 0x0400205E RID: 8286
	private Dictionary<global::Socket, Transform> proxies;

	// Token: 0x0400205F RID: 8287
	private bool madeProxyDict;

	// Token: 0x04002060 RID: 8288
	public int caps;

	// Token: 0x04002061 RID: 8289
	public float perspectiveNearOverride = 0.1f;

	// Token: 0x04002062 RID: 8290
	public float perspectiveFarOverride = 25f;

	// Token: 0x04002063 RID: 8291
	public float perspectiveFOVOverride = 60f;

	// Token: 0x04002064 RID: 8292
	public float perspectiveAspectOverride = 1f;

	// Token: 0x04002065 RID: 8293
	public float lazyAngle = 5f;

	// Token: 0x04002066 RID: 8294
	public float zoomFieldOfView = 40f;

	// Token: 0x04002067 RID: 8295
	public AnimationCurve zoomCurve;

	// Token: 0x04002068 RID: 8296
	public Vector3 zoomOffset;

	// Token: 0x04002069 RID: 8297
	public Vector3 zoomRotate;

	// Token: 0x0400206A RID: 8298
	public Vector3 offset;

	// Token: 0x0400206B RID: 8299
	public Vector3 rotate;

	// Token: 0x0400206C RID: 8300
	public Transform root;

	// Token: 0x0400206D RID: 8301
	public Animation animation;

	// Token: 0x0400206E RID: 8302
	public Texture crosshairTexture;

	// Token: 0x0400206F RID: 8303
	public Texture dotTexture;

	// Token: 0x04002070 RID: 8304
	public float zoomInDuration = 0.5f;

	// Token: 0x04002071 RID: 8305
	public float zoomOutDuration = 0.4f;

	// Token: 0x04002072 RID: 8306
	public bool showCrosshairZoom;

	// Token: 0x04002073 RID: 8307
	public bool showCrosshairNotZoomed = true;

	// Token: 0x04002074 RID: 8308
	public Color crosshairColor = Color.white;

	// Token: 0x04002075 RID: 8309
	public Color crosshairOutline = Color.black;

	// Token: 0x04002076 RID: 8310
	public LayerMask aimMask;

	// Token: 0x04002077 RID: 8311
	public AnimationCurve headBobOffsetScale;

	// Token: 0x04002078 RID: 8312
	public AnimationCurve headBobRotationScale;

	// Token: 0x04002079 RID: 8313
	public bool barrelAiming = true;

	// Token: 0x0400207A RID: 8314
	public bool barrelWhileZoom;

	// Token: 0x0400207B RID: 8315
	public bool barrelWhileBowing;

	// Token: 0x0400207C RID: 8316
	public Vector3 barrelPivot;

	// Token: 0x0400207D RID: 8317
	public Vector2 barrelRotation;

	// Token: 0x0400207E RID: 8318
	public float barrelLimit;

	// Token: 0x0400207F RID: 8319
	public float noHitPlane = 20f;

	// Token: 0x04002080 RID: 8320
	public float barrelAngleSmoothDamp = 0.01f;

	// Token: 0x04002081 RID: 8321
	public float barrelAngleMaxSpeed = float.PositiveInfinity;

	// Token: 0x04002082 RID: 8322
	public float barrelLimitOffsetFactor = 1f;

	// Token: 0x04002083 RID: 8323
	public float barrelLimitPivotFactor;

	// Token: 0x04002084 RID: 8324
	public bool bowAllowed;

	// Token: 0x04002085 RID: 8325
	public Vector3 bowOffsetPoint;

	// Token: 0x04002086 RID: 8326
	public Vector3 bowOffsetAngles;

	// Token: 0x04002087 RID: 8327
	public AnimationCurve bowCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04002088 RID: 8328
	public bool bowCurveIs01Fraction;

	// Token: 0x04002089 RID: 8329
	public float bowEnterDuration = 1f;

	// Token: 0x0400208A RID: 8330
	public float bowExitDuration = 1f;

	// Token: 0x0400208B RID: 8331
	private float bowTime;

	// Token: 0x0400208C RID: 8332
	public AnimationCurve zoomPunch;

	// Token: 0x0400208D RID: 8333
	public float punchScalar = 1f;

	// Token: 0x0400208E RID: 8334
	private float punchTime = -2000f;

	// Token: 0x0400208F RID: 8335
	private float zoomPunchValue;

	// Token: 0x04002090 RID: 8336
	public string fireAnimName = "fire_1";

	// Token: 0x04002091 RID: 8337
	public string deployAnimName = "deploy";

	// Token: 0x04002092 RID: 8338
	public string reloadAnimName = "reload";

	// Token: 0x04002093 RID: 8339
	public float fireAnimScaleSpeed = 1f;

	// Token: 0x04002094 RID: 8340
	[SerializeField]
	protected global::AnimationBlender.ResidualField idleFrame;

	// Token: 0x04002095 RID: 8341
	[SerializeField]
	protected global::AnimationBlender.ChannelField idleChannel;

	// Token: 0x04002096 RID: 8342
	[SerializeField]
	protected global::AnimationBlender.ChannelField movementIdleChannel;

	// Token: 0x04002097 RID: 8343
	[SerializeField]
	protected global::AnimationBlender.ChannelField bowChannel;

	// Token: 0x04002098 RID: 8344
	[SerializeField]
	protected global::AnimationBlender.ChannelField bowMovementChannel;

	// Token: 0x04002099 RID: 8345
	[SerializeField]
	protected global::AnimationBlender.ChannelField crouchChannel;

	// Token: 0x0400209A RID: 8346
	[SerializeField]
	protected global::AnimationBlender.ChannelField crouchMovementChannel;

	// Token: 0x0400209B RID: 8347
	[SerializeField]
	protected global::AnimationBlender.ChannelField fallChannel;

	// Token: 0x0400209C RID: 8348
	[SerializeField]
	protected global::AnimationBlender.ChannelField slipChannel;

	// Token: 0x0400209D RID: 8349
	[SerializeField]
	protected global::AnimationBlender.ChannelField zoomChannel;

	// Token: 0x0400209E RID: 8350
	[NonSerialized]
	protected global::AnimationBlender.Mixer idleMixer;

	// Token: 0x0400209F RID: 8351
	[NonSerialized]
	public global::ItemRepresentation itemRep;

	// Token: 0x040020A0 RID: 8352
	[NonSerialized]
	public global::IHeldItem item;

	// Token: 0x040020A1 RID: 8353
	[NonSerialized]
	private global::Angle2 lastLook;

	// Token: 0x040020A2 RID: 8354
	[SerializeField]
	private SkinnedMeshRenderer[] builtinRenderers;

	// Token: 0x040020A3 RID: 8355
	[NonSerialized]
	private global::ViewModel.MeshInstance.Holder meshInstances;

	// Token: 0x040020A4 RID: 8356
	[NonSerialized]
	private global::ViewModel.BarrelParameters bpHip;

	// Token: 0x040020A5 RID: 8357
	[NonSerialized]
	private global::ViewModel.BarrelParameters bpZoom;

	// Token: 0x040020A6 RID: 8358
	[NonSerialized]
	private global::ViewModel.BarrelParameters bpBow;

	// Token: 0x040020A7 RID: 8359
	private static bool force_legacy_fallback;

	// Token: 0x040020A8 RID: 8360
	private global::HeadBob _headBob;

	// Token: 0x040020A9 RID: 8361
	private global::LazyCam _lazyCam;

	// Token: 0x040020AA RID: 8362
	private Quaternion _additiveRotation = Quaternion.identity;

	// Token: 0x040020AB RID: 8363
	private float zoomTime;

	// Token: 0x040020AC RID: 8364
	private float headBobLinearTime;

	// Token: 0x040020AD RID: 8365
	private float headBobAngularTime;

	// Token: 0x040020AE RID: 8366
	private float lastZoomFraction = float.NaN;

	// Token: 0x040020AF RID: 8367
	private float lastHeadBobLinearFraction;

	// Token: 0x040020B0 RID: 8368
	private float lastHeadBobAngular;

	// Token: 0x040020B1 RID: 8369
	private Vector3 lastLocalPositionOffset;

	// Token: 0x040020B2 RID: 8370
	private Vector3 lastLocalRotationOffset;

	// Token: 0x040020B3 RID: 8371
	private Vector3 lastSightRotation;

	// Token: 0x040020B4 RID: 8372
	private Transform eye;

	// Token: 0x040020B5 RID: 8373
	private Transform shelf;

	// Token: 0x040020B6 RID: 8374
	private List<GameObject> destroyOnUnbind;

	// Token: 0x040020B7 RID: 8375
	private static bool modifyAiming;

	// Token: 0x02000758 RID: 1880
	private class MeshInstance
	{
		// Token: 0x06003E56 RID: 15958 RVA: 0x000E3188 File Offset: 0x000E1388
		private MeshInstance()
		{
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x000E3190 File Offset: 0x000E1390
		private void Delete()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.predraw.Shutdown();
				this.postdraw.Shutdown();
				if (this.renderer)
				{
					this.renderer.sharedMaterials = this.originalMaterials;
				}
				this.renderer = null;
				if (global::ViewModel.MeshInstance.dumpCount < 8)
				{
					this.next = global::ViewModel.MeshInstance.dump;
					global::ViewModel.MeshInstance.dump = this;
					this.hasNext = (global::ViewModel.MeshInstance.dumpCount++ > 0);
				}
				else
				{
					this.next = null;
					this.hasNext = false;
				}
			}
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x000E3234 File Offset: 0x000E1434
		private static bool New(global::ViewModel.MeshInstance ptr, SkinnedMeshRenderer renderer, out global::ViewModel.MeshInstance newInstance)
		{
			if (!renderer)
			{
				newInstance = null;
				return false;
			}
			if (global::ViewModel.MeshInstance.dumpCount > 0)
			{
				newInstance = global::ViewModel.MeshInstance.dump;
				if (--global::ViewModel.MeshInstance.dumpCount > 0)
				{
					global::ViewModel.MeshInstance.dump = newInstance.next;
				}
				else
				{
					global::ViewModel.MeshInstance.dump = null;
				}
				newInstance.next = null;
				newInstance.hasNext = false;
				newInstance.disposed = false;
				newInstance.renderer = null;
			}
			else
			{
				newInstance = new global::ViewModel.MeshInstance();
			}
			if (ptr != null)
			{
				newInstance.hasNext = ptr.hasNext;
				newInstance.next = ptr.next;
				ptr.hasNext = true;
				ptr.next = newInstance;
			}
			else
			{
				newInstance.hasNext = false;
				newInstance.next = null;
			}
			newInstance.renderer = renderer;
			newInstance.originalMaterials = renderer.sharedMaterials;
			int subMeshCount = renderer.sharedMesh.subMeshCount;
			int num = newInstance.originalMaterials.Length % subMeshCount;
			if (num != 0)
			{
				Array.Resize<Material>(ref newInstance.originalMaterials, (newInstance.originalMaterials.Length / subMeshCount + 1) * subMeshCount);
			}
			newInstance.modifiedMaterials = newInstance.originalMaterials;
			return true;
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x000E335C File Offset: 0x000E155C
		private void SetReplacementRenderMaterial(ref global::ViewModel.MeshInstance.ReplacementRenderer rr, int itsa, Material mat)
		{
			if (!this.disposed)
			{
				if (!rr.initialized)
				{
					this.legacy = (global::ViewModel.force_legacy_fallback || this.renderer.sharedMesh.subMeshCount > 1);
					rr.Initialize(this.renderer, this.renderer, this.originalMaterials, mat, itsa, this.legacy);
				}
				else
				{
					rr.SetOverride(this.originalMaterials, mat, itsa);
				}
				Material[] array = rr.UpdateMaterials(this.legacy);
				if (!this.legacy)
				{
					if (array == null)
					{
						if (rr.offset != 0)
						{
							int num = rr.offset;
							for (int i = rr.offset + this.originalMaterials.Length; i < this.modifiedMaterials.Length; i++)
							{
								this.modifiedMaterials[num] = this.modifiedMaterials[i];
								num++;
							}
							Array.Resize<Material>(ref this.modifiedMaterials, this.modifiedMaterials.Length - this.originalMaterials.Length);
							rr.offset = 0;
						}
					}
					else
					{
						if (rr.offset == 0)
						{
							rr.offset = this.modifiedMaterials.Length;
							Array.Resize<Material>(ref this.modifiedMaterials, this.modifiedMaterials.Length + this.originalMaterials.Length);
						}
						int num2 = rr.offset;
						for (int j = 0; j < this.originalMaterials.Length; j++)
						{
							this.modifiedMaterials[num2] = array[j];
							num2++;
						}
					}
					this.renderer.sharedMaterials = this.modifiedMaterials;
				}
			}
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x000E34E8 File Offset: 0x000E16E8
		public void SetPredrawMaterial(Material mat)
		{
			this.SetReplacementRenderMaterial(ref this.predraw, 1, mat);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x000E34F8 File Offset: 0x000E16F8
		public void SetPostdrawMaterial(Material mat)
		{
			this.SetReplacementRenderMaterial(ref this.postdraw, 2, mat);
		}

		// Token: 0x040020BA RID: 8378
		private const int kMaxDumpCount = 8;

		// Token: 0x040020BB RID: 8379
		public global::ViewModel.MeshInstance next;

		// Token: 0x040020BC RID: 8380
		public SkinnedMeshRenderer renderer;

		// Token: 0x040020BD RID: 8381
		public bool legacy;

		// Token: 0x040020BE RID: 8382
		public global::ViewModel.MeshInstance.ReplacementRenderer predraw;

		// Token: 0x040020BF RID: 8383
		public global::ViewModel.MeshInstance.ReplacementRenderer postdraw;

		// Token: 0x040020C0 RID: 8384
		public bool disposed;

		// Token: 0x040020C1 RID: 8385
		public bool hasNext;

		// Token: 0x040020C2 RID: 8386
		private Material[] originalMaterials;

		// Token: 0x040020C3 RID: 8387
		private Material[] modifiedMaterials;

		// Token: 0x040020C4 RID: 8388
		private static global::ViewModel.MeshInstance dump;

		// Token: 0x040020C5 RID: 8389
		private static int dumpCount;

		// Token: 0x02000759 RID: 1881
		public struct Holder : IDisposable
		{
			// Token: 0x06003E5C RID: 15964 RVA: 0x000E3508 File Offset: 0x000E1708
			private void IterDelete(global::ViewModel.MeshInstance iter)
			{
				global::ViewModel.MeshInstance next = iter.next;
				iter.hasNext = next.hasNext;
				iter.next = next.next;
				this.InstanceDeleteShared(next);
			}

			// Token: 0x06003E5D RID: 15965 RVA: 0x000E353C File Offset: 0x000E173C
			private void FirstDelete()
			{
				global::ViewModel.MeshInstance instance = this.first;
				this.first = this.first.next;
				this.InstanceDeleteShared(instance);
			}

			// Token: 0x06003E5E RID: 15966 RVA: 0x000E3568 File Offset: 0x000E1768
			private void InstanceDeleteShared(global::ViewModel.MeshInstance instance)
			{
				this.count--;
				instance.Delete();
			}

			// Token: 0x06003E5F RID: 15967 RVA: 0x000E3580 File Offset: 0x000E1780
			public bool Delete(global::ViewModel.MeshInstance instance)
			{
				if (this.disposed)
				{
					return false;
				}
				if (this.count > 0 && instance != null && !instance.disposed)
				{
					if (instance == this.first)
					{
						this.FirstDelete();
						return true;
					}
					int num = this.count - 1;
					global::ViewModel.MeshInstance meshInstance = this.first;
					for (int i = 0; i < num; i++)
					{
						if (meshInstance.next == instance)
						{
							this.IterDelete(meshInstance);
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06003E60 RID: 15968 RVA: 0x000E3604 File Offset: 0x000E1804
			public bool Delete(SkinnedMeshRenderer renderer)
			{
				if (this.disposed)
				{
					return false;
				}
				if (this.count > 0)
				{
					if (object.ReferenceEquals(renderer, this.first.renderer))
					{
						this.FirstDelete();
						return true;
					}
					int num = this.count - 1;
					global::ViewModel.MeshInstance next = this.first;
					for (int i = 0; i < num; i++)
					{
						if (object.ReferenceEquals(next.next.renderer, renderer))
						{
							this.IterDelete(next);
							return true;
						}
						next = next.next;
					}
				}
				return false;
			}

			// Token: 0x06003E61 RID: 15969 RVA: 0x000E3694 File Offset: 0x000E1894
			private bool AddShared(bool didIt, global::ViewModel.MeshInstance meshInstance)
			{
				if (didIt && this.count++ == 0)
				{
					this.first = meshInstance;
				}
				global::CameraFX mainCameraFX = global::CameraFX.mainCameraFX;
				if (mainCameraFX)
				{
					Material material;
					if (material = mainCameraFX.predrawMaterial)
					{
						meshInstance.SetPredrawMaterial(material);
					}
					if (material = mainCameraFX.postdrawMaterial)
					{
						meshInstance.SetPostdrawMaterial(material);
					}
				}
				return didIt;
			}

			// Token: 0x06003E62 RID: 15970 RVA: 0x000E3708 File Offset: 0x000E1908
			public bool Add(SkinnedMeshRenderer renderer)
			{
				global::ViewModel.MeshInstance meshInstance;
				return renderer && this.Add(renderer, out meshInstance);
			}

			// Token: 0x06003E63 RID: 15971 RVA: 0x000E372C File Offset: 0x000E192C
			public bool Add(SkinnedMeshRenderer renderer, out global::ViewModel.MeshInstance newOrExistingInstance)
			{
				if (this.disposed)
				{
					newOrExistingInstance = null;
					return false;
				}
				if (this.count == 0)
				{
					return this.AddShared(global::ViewModel.MeshInstance.New(null, renderer, out newOrExistingInstance), newOrExistingInstance);
				}
				if (object.ReferenceEquals(this.first.renderer, renderer))
				{
					newOrExistingInstance = this.first;
					return false;
				}
				int num = this.count - 1;
				global::ViewModel.MeshInstance next = this.first;
				for (int i = 0; i < num; i++)
				{
					if (object.ReferenceEquals(next.next.renderer, renderer))
					{
						newOrExistingInstance = next.next;
						return false;
					}
					next = next.next;
				}
				return this.AddShared(global::ViewModel.MeshInstance.New(next, renderer, out newOrExistingInstance), newOrExistingInstance);
			}

			// Token: 0x06003E64 RID: 15972 RVA: 0x000E37E0 File Offset: 0x000E19E0
			public void Clear()
			{
				while (this.count > 0)
				{
					this.FirstDelete();
				}
			}

			// Token: 0x06003E65 RID: 15973 RVA: 0x000E37FC File Offset: 0x000E19FC
			public void Dispose()
			{
				if (!this.disposed)
				{
					try
					{
						this.Clear();
					}
					finally
					{
						this.disposed = true;
					}
				}
			}

			// Token: 0x040020C6 RID: 8390
			public global::ViewModel.MeshInstance first;

			// Token: 0x040020C7 RID: 8391
			public int count;

			// Token: 0x040020C8 RID: 8392
			public bool disposed;
		}

		// Token: 0x0200075A RID: 1882
		public struct ReplacementRenderer
		{
			// Token: 0x06003E66 RID: 15974 RVA: 0x000E3844 File Offset: 0x000E1A44
			public Material[] UpdateMaterials(bool legacy)
			{
				if (this.initialized)
				{
					if (legacy && this.renderer)
					{
						this.renderer.sharedMaterials = this.materials;
					}
					return this.materials;
				}
				return null;
			}

			// Token: 0x06003E67 RID: 15975 RVA: 0x000E388C File Offset: 0x000E1A8C
			public void Shutdown()
			{
				if (this.initialized)
				{
					if (this.renderer)
					{
						Object.Destroy(this.renderer.gameObject);
					}
					this = default(global::ViewModel.MeshInstance.ReplacementRenderer);
				}
			}

			// Token: 0x06003E68 RID: 15976 RVA: 0x000E38D4 File Offset: 0x000E1AD4
			public void Initialize(SkinnedMeshRenderer owner, SkinnedMeshRenderer source, Material[] originalMaterials, Material overrideMaterial, int itsa, bool legacy)
			{
				this.Shutdown();
				if (legacy)
				{
					Transform transform = owner.transform;
					this.renderer = (SkinnedMeshRenderer)Object.Instantiate(source);
					Transform transform2 = this.renderer.transform;
					transform2.parent = transform.parent;
					transform2.localPosition = transform.localPosition;
					transform2.localRotation = transform.localRotation;
					transform2.localScale = transform.localScale;
					this.materials = (Material[])originalMaterials.Clone();
					this.initialized = true;
					this.SetOverride(originalMaterials, overrideMaterial, itsa);
					this.UpdateMaterials(true);
				}
				else
				{
					this.materials = (Material[])originalMaterials.Clone();
					this.initialized = true;
					if (!this.SetOverride(originalMaterials, overrideMaterial, itsa))
					{
						this.materials = null;
					}
				}
			}

			// Token: 0x06003E69 RID: 15977 RVA: 0x000E39A4 File Offset: 0x000E1BA4
			public bool SetOverride(Material[] originals, Material material, int itsa)
			{
				bool result = false;
				if (this.initialized)
				{
					if (itsa != 1)
					{
						if (itsa != 2)
						{
							for (int i = 0; i < originals.Length; i++)
							{
								if (originals[i])
								{
									this.materials[i] = material;
								}
								result = true;
							}
						}
						else
						{
							for (int j = 0; j < originals.Length; j++)
							{
								if (originals[j])
								{
									if (originals[j].GetTag("SkipViewModelPostdraw", false, "False") == "True")
									{
										this.materials[j] = null;
									}
									else
									{
										this.materials[j] = material;
										result = true;
									}
								}
							}
						}
					}
					else
					{
						for (int k = 0; k < originals.Length; k++)
						{
							if (originals[k])
							{
								if (originals[k].GetTag("SkipViewModelPredraw", false, "False") == "True")
								{
									this.materials[k] = null;
								}
								else
								{
									this.materials[k] = material;
									result = true;
								}
							}
						}
					}
				}
				return result;
			}

			// Token: 0x040020C9 RID: 8393
			public const int kItsaPreDraw = 1;

			// Token: 0x040020CA RID: 8394
			public const int kItsaPostDraw = 2;

			// Token: 0x040020CB RID: 8395
			public Material[] materials;

			// Token: 0x040020CC RID: 8396
			public SkinnedMeshRenderer renderer;

			// Token: 0x040020CD RID: 8397
			public bool initialized;

			// Token: 0x040020CE RID: 8398
			public int offset;
		}
	}

	// Token: 0x0200075B RID: 1883
	private struct BarrelTransform
	{
		// Token: 0x06003E6A RID: 15978 RVA: 0x000E3ACC File Offset: 0x000E1CCC
		public void Get(out Vector3 origin, out Vector3 angles)
		{
			origin = this.origin;
			angles = this.angles;
		}

		// Token: 0x040020CF RID: 8399
		public Vector3 origin;

		// Token: 0x040020D0 RID: 8400
		public Vector3 angles;
	}

	// Token: 0x0200075C RID: 1884
	private struct BarrelParameters
	{
		// Token: 0x040020D1 RID: 8401
		public float a;

		// Token: 0x040020D2 RID: 8402
		public float b;

		// Token: 0x040020D3 RID: 8403
		public float c;

		// Token: 0x040020D4 RID: 8404
		public float bc;

		// Token: 0x040020D5 RID: 8405
		public float ca;

		// Token: 0x040020D6 RID: 8406
		public float ab;

		// Token: 0x040020D7 RID: 8407
		public bool once;

		// Token: 0x040020D8 RID: 8408
		public bool ir;

		// Token: 0x040020D9 RID: 8409
		public float angle;

		// Token: 0x040020DA RID: 8410
		public float angularVelocity;
	}
}
