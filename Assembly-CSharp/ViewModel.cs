using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000692 RID: 1682
public class ViewModel : IDRemote, Socket.Source, Socket.Mapped, Socket.Provider
{
	// Token: 0x06003A24 RID: 14884 RVA: 0x000D8680 File Offset: 0x000D6880
	public ViewModel()
	{
		this.socketNames = ViewModel.defaultSocketNames;
		base..ctor();
	}

	// Token: 0x06003A26 RID: 14886 RVA: 0x000D8814 File Offset: 0x000D6A14
	bool Socket.Source.GetSocket(string name, out Socket socket)
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

	// Token: 0x06003A27 RID: 14887 RVA: 0x000D8900 File Offset: 0x000D6B00
	bool Socket.Source.ReplaceSocket(string name, Socket socket)
	{
		Socket.CameraSpace cameraSpace = (Socket.CameraSpace)socket;
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

	// Token: 0x17000B3E RID: 2878
	// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000D89E8 File Offset: 0x000D6BE8
	IEnumerable<string> Socket.Source.SocketNames
	{
		get
		{
			return this.socketNames;
		}
	}

	// Token: 0x17000B3F RID: 2879
	// (get) Token: 0x06003A29 RID: 14889 RVA: 0x000D89F0 File Offset: 0x000D6BF0
	int Socket.Source.SocketsVersion
	{
		get
		{
			return this.socketVersion;
		}
	}

	// Token: 0x06003A2A RID: 14890 RVA: 0x000D89F8 File Offset: 0x000D6BF8
	Socket.CameraConversion Socket.Source.CameraSpaceSetup()
	{
		return new Socket.CameraConversion(this.eye, this.shelf);
	}

	// Token: 0x06003A2B RID: 14891 RVA: 0x000D8A0C File Offset: 0x000D6C0C
	Type Socket.Source.ProxyScriptType(string name)
	{
		return typeof(SocketProxy);
	}

	// Token: 0x17000B40 RID: 2880
	// (get) Token: 0x06003A2C RID: 14892 RVA: 0x000D8A18 File Offset: 0x000D6C18
	public Socket.Map socketMap
	{
		get
		{
			return this._socketMap.Get<ViewModel>(this);
		}
	}

	// Token: 0x06003A2D RID: 14893 RVA: 0x000D8A28 File Offset: 0x000D6C28
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

	// Token: 0x06003A2E RID: 14894 RVA: 0x000D8A78 File Offset: 0x000D6C78
	protected void DeleteSocketMap()
	{
		this._socketMap.DeleteBy<ViewModel>(this);
	}

	// Token: 0x17000B41 RID: 2881
	// (get) Token: 0x06003A2F RID: 14895 RVA: 0x000D8A88 File Offset: 0x000D6C88
	public Character idMain
	{
		get
		{
			return (Character)base.idMain;
		}
	}

	// Token: 0x17000B42 RID: 2882
	// (get) Token: 0x06003A30 RID: 14896 RVA: 0x000D8A98 File Offset: 0x000D6C98
	public bool drawCrosshair
	{
		get
		{
			return this.showCrosshairZoom || this.showCrosshairNotZoomed;
		}
	}

	// Token: 0x06003A31 RID: 14897 RVA: 0x000D8AB0 File Offset: 0x000D6CB0
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
		this.idleMixer = this.idleFrame.Alias(this.animation, new AnimationBlender.ChannelConfig[9].Define(0, "idle", this.idleChannel).Define(1, "move", this.movementIdleChannel).Define(4, "bowi", this.bowChannel).Define(5, "bowm", this.bowMovementChannel).Define(2, "dcki", this.crouchChannel).Define(3, "dckm", this.crouchMovementChannel).Define(6, "fall", this.fallChannel).Define(7, "slip", this.slipChannel).Define(8, "zoom", this.zoomChannel)).Create();
	}

	// Token: 0x06003A32 RID: 14898 RVA: 0x000D8BE4 File Offset: 0x000D6DE4
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

	// Token: 0x17000B43 RID: 2883
	// (get) Token: 0x06003A33 RID: 14899 RVA: 0x000D8C2C File Offset: 0x000D6E2C
	// (set) Token: 0x06003A34 RID: 14900 RVA: 0x000D8C34 File Offset: 0x000D6E34
	public HeadBob headBob
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

	// Token: 0x17000B44 RID: 2884
	// (get) Token: 0x06003A35 RID: 14901 RVA: 0x000D8C40 File Offset: 0x000D6E40
	// (set) Token: 0x06003A36 RID: 14902 RVA: 0x000D8C48 File Offset: 0x000D6E48
	public LazyCam lazyCam
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

	// Token: 0x17000B45 RID: 2885
	// (get) Token: 0x06003A37 RID: 14903 RVA: 0x000D8C54 File Offset: 0x000D6E54
	// (set) Token: 0x06003A38 RID: 14904 RVA: 0x000D8C5C File Offset: 0x000D6E5C
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

	// Token: 0x17000B46 RID: 2886
	// (get) Token: 0x06003A39 RID: 14905 RVA: 0x000D8CBC File Offset: 0x000D6EBC
	public Quaternion muzzleRotation
	{
		get
		{
			return this.muzzle.rotation;
		}
	}

	// Token: 0x17000B47 RID: 2887
	// (get) Token: 0x06003A3A RID: 14906 RVA: 0x000D8CCC File Offset: 0x000D6ECC
	public Vector3 muzzlePosition
	{
		get
		{
			return this.muzzle.position;
		}
	}

	// Token: 0x06003A3B RID: 14907 RVA: 0x000D8CDC File Offset: 0x000D6EDC
	public bool Play(string name)
	{
		return this.idleMixer.Play(name);
	}

	// Token: 0x06003A3C RID: 14908 RVA: 0x000D8CEC File Offset: 0x000D6EEC
	public bool Play(string name, PlayMode playMode)
	{
		return this.idleMixer.Play(name, playMode);
	}

	// Token: 0x06003A3D RID: 14909 RVA: 0x000D8CFC File Offset: 0x000D6EFC
	public bool Play(string name, float speed)
	{
		return this.idleMixer.Play(name, speed);
	}

	// Token: 0x06003A3E RID: 14910 RVA: 0x000D8D0C File Offset: 0x000D6F0C
	public bool Play(string name, float speed, float time)
	{
		return this.idleMixer.Play(name, speed, time);
	}

	// Token: 0x06003A3F RID: 14911 RVA: 0x000D8D1C File Offset: 0x000D6F1C
	public bool Play(string name, PlayMode playMode, float speed)
	{
		return this.idleMixer.Play(name, playMode, speed);
	}

	// Token: 0x06003A40 RID: 14912 RVA: 0x000D8D2C File Offset: 0x000D6F2C
	public bool Play(string name, PlayMode playMode, float speed, float time)
	{
		return this.idleMixer.Play(name, playMode, speed, time);
	}

	// Token: 0x06003A41 RID: 14913 RVA: 0x000D8D40 File Offset: 0x000D6F40
	public bool PlayQueued(string name)
	{
		return this.idleMixer.PlayQueued(name);
	}

	// Token: 0x06003A42 RID: 14914 RVA: 0x000D8D50 File Offset: 0x000D6F50
	public bool PlayQueued(string name, QueueMode queueMode)
	{
		return this.idleMixer.PlayQueued(name, queueMode);
	}

	// Token: 0x06003A43 RID: 14915 RVA: 0x000D8D60 File Offset: 0x000D6F60
	public bool PlayQueued(string name, QueueMode queueMode, PlayMode playMode)
	{
		return this.idleMixer.PlayQueued(name, queueMode, playMode);
	}

	// Token: 0x06003A44 RID: 14916 RVA: 0x000D8D70 File Offset: 0x000D6F70
	public void CrossFade(string name)
	{
		this.idleMixer.CrossFade(name);
	}

	// Token: 0x06003A45 RID: 14917 RVA: 0x000D8D80 File Offset: 0x000D6F80
	public void CrossFade(string name, float fadeLength)
	{
		this.idleMixer.CrossFade(name, fadeLength);
	}

	// Token: 0x06003A46 RID: 14918 RVA: 0x000D8D90 File Offset: 0x000D6F90
	public void CrossFade(string name, float fadeLength, PlayMode playMode)
	{
		this.idleMixer.CrossFade(name, fadeLength, playMode);
	}

	// Token: 0x06003A47 RID: 14919 RVA: 0x000D8DA4 File Offset: 0x000D6FA4
	public void CrossFade(string name, float fadeLength, PlayMode playMode, float speed)
	{
		this.idleMixer.CrossFade(name, fadeLength, playMode, speed);
	}

	// Token: 0x06003A48 RID: 14920 RVA: 0x000D8DB8 File Offset: 0x000D6FB8
	public void PlayFireAnimation(float speed)
	{
		this.Play(this.fireAnimName, speed);
		this.punchTime = Time.time;
	}

	// Token: 0x06003A49 RID: 14921 RVA: 0x000D8DD4 File Offset: 0x000D6FD4
	public void PlayFireAnimation()
	{
		this.PlayFireAnimation(this.fireAnimScaleSpeed);
	}

	// Token: 0x06003A4A RID: 14922 RVA: 0x000D8DE4 File Offset: 0x000D6FE4
	public void PlayDeployAnimation()
	{
		this.Play(this.deployAnimName);
	}

	// Token: 0x06003A4B RID: 14923 RVA: 0x000D8DF4 File Offset: 0x000D6FF4
	public void PlayReloadAnimation()
	{
		this.Play(this.reloadAnimName);
	}

	// Token: 0x06003A4C RID: 14924 RVA: 0x000D8E04 File Offset: 0x000D7004
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

	// Token: 0x06003A4D RID: 14925 RVA: 0x000D8E40 File Offset: 0x000D7040
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

	// Token: 0x06003A4E RID: 14926 RVA: 0x000D8E7C File Offset: 0x000D707C
	public void BindTransforms(Transform shelf, Transform eye)
	{
		this.punchTime = Time.time - 20f;
		this.BindCameraSpaceTransforms(shelf, eye);
	}

	// Token: 0x06003A4F RID: 14927 RVA: 0x000D8E98 File Offset: 0x000D7098
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

	// Token: 0x06003A50 RID: 14928 RVA: 0x000D8F20 File Offset: 0x000D7120
	public void UnBindTransforms()
	{
		this.ClearProxies();
		if (CameraFX.mainViewModel == this)
		{
			CameraFX mainCameraFX = CameraFX.mainCameraFX;
			if (mainCameraFX)
			{
				mainCameraFX.SetFieldOfView(320432f, 0f);
			}
		}
	}

	// Token: 0x06003A51 RID: 14929 RVA: 0x000D8F64 File Offset: 0x000D7164
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

	// Token: 0x06003A52 RID: 14930 RVA: 0x000D9008 File Offset: 0x000D7208
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

	// Token: 0x06003A53 RID: 14931 RVA: 0x000D9080 File Offset: 0x000D7280
	private ViewModel.BarrelTransform BarrelAim(Vector3 offset, ref ViewModel.BarrelParameters barrel)
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
		ViewModel.SolveTriangleSSA(barrel.a, barrel.bc, barrel.ca, out barrel.ab, out barrel.c, out barrel.b);
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
		ViewModel.BarrelTransform result;
		result.origin = quaternion3 * -vector4 + vector4;
		result.angles = quaternion3.eulerAngles;
		result.origin += vector3;
		result.angles.x = Mathf.DeltaAngle(0f, result.angles.x);
		result.angles.y = Mathf.DeltaAngle(0f, result.angles.y);
		result.angles.z = Mathf.DeltaAngle(0f, result.angles.z);
		return result;
	}

	// Token: 0x06003A54 RID: 14932 RVA: 0x000D947C File Offset: 0x000D767C
	protected void LateUpdate()
	{
		Character idMain = this.idMain;
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
		Angle2 eyesAngles;
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
			num = Angle2.AngleDistance(this.lastLook, eyesAngles) / deltaTime;
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
			CameraFX mainCameraFX = CameraFX.mainCameraFX;
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

	// Token: 0x06003A55 RID: 14933 RVA: 0x000D9CC4 File Offset: 0x000D7EC4
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
		Vector3 vector = Angle2.Direction(this.barrelRotation.x, this.barrelRotation.y);
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

	// Token: 0x06003A56 RID: 14934 RVA: 0x000DA008 File Offset: 0x000D8208
	[ContextMenu("Set as current view model")]
	private void SetAsCurrentViewModel()
	{
		if (base.enabled)
		{
			CameraFX.ReplaceViewModel(this, this.itemRep, this.item, false);
		}
	}

	// Token: 0x06003A57 RID: 14935 RVA: 0x000DA028 File Offset: 0x000D8228
	public void UpdateProxies()
	{
		Socket.Map socketMap = this.socketMap;
		if (!object.ReferenceEquals(socketMap, null))
		{
			socketMap.SnapProxies();
		}
	}

	// Token: 0x06003A58 RID: 14936 RVA: 0x000DA050 File Offset: 0x000D8250
	protected void Update()
	{
		this.idleMixer.Update(1f, Time.deltaTime);
	}

	// Token: 0x06003A59 RID: 14937 RVA: 0x000DA068 File Offset: 0x000D8268
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

	// Token: 0x06003A5A RID: 14938 RVA: 0x000DA308 File Offset: 0x000D8508
	public void ModifyAiming(Ray ray, ref Vector3 p, ref Quaternion q)
	{
		if (ViewModel.modifyAiming)
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

	// Token: 0x06003A5B RID: 14939 RVA: 0x000DA3FC File Offset: 0x000D85FC
	protected void OnDestroy()
	{
		base.OnDestroy();
		this.UnBindTransforms();
		this.meshInstances.Dispose();
	}

	// Token: 0x06003A5C RID: 14940 RVA: 0x000DA418 File Offset: 0x000D8618
	private void OnGUI()
	{
		if (Event.current.type != 7 || RPOS.IsOpen || !this.drawCrosshair || !this.crosshairTexture || !this.dotTexture)
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
		if (camera && (camera.enabled || MountedCamera.IsCameraBeingUsed(camera)))
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
			Vector3? vector = CameraFX.World2Screen(point);
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
				vector = CameraFX.World2Screen(raycastHit.point);
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

	// Token: 0x06003A5D RID: 14941 RVA: 0x000DA72C File Offset: 0x000D892C
	public void ModifyPerspective(ref PerspectiveMatrixBuilder perspective)
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

	// Token: 0x04001E42 RID: 7746
	public const int kCap_PerspectiveNear = 1;

	// Token: 0x04001E43 RID: 7747
	public const int kCap_PerspectiveFar = 2;

	// Token: 0x04001E44 RID: 7748
	public const int kCap_PerspectiveFOV = 4;

	// Token: 0x04001E45 RID: 7749
	public const int kCap_PerspectiveAspect = 8;

	// Token: 0x04001E46 RID: 7750
	protected const int kIdleChannel_Idle = 0;

	// Token: 0x04001E47 RID: 7751
	protected const int kIdleChannel_IdleMovement = 1;

	// Token: 0x04001E48 RID: 7752
	protected const int kIdleChannel_Crouch = 2;

	// Token: 0x04001E49 RID: 7753
	protected const int kIdleChannel_CrouchMovement = 3;

	// Token: 0x04001E4A RID: 7754
	protected const int kIdleChannel_Bow = 4;

	// Token: 0x04001E4B RID: 7755
	protected const int kIdleChannel_BowMovement = 5;

	// Token: 0x04001E4C RID: 7756
	protected const int kIdleChannel_Fall = 6;

	// Token: 0x04001E4D RID: 7757
	protected const int kIdleChannel_Slip = 7;

	// Token: 0x04001E4E RID: 7758
	protected const int kIdleChannel_Zoom = 8;

	// Token: 0x04001E4F RID: 7759
	protected const int kIdleChannelCount = 9;

	// Token: 0x04001E50 RID: 7760
	protected const string kIdleChannel_Idle_Name = "idle";

	// Token: 0x04001E51 RID: 7761
	protected const string kIdleChannel_IdleMovement_Name = "move";

	// Token: 0x04001E52 RID: 7762
	protected const string kIdleChannel_Bow_Name = "bowi";

	// Token: 0x04001E53 RID: 7763
	protected const string kIdleChannel_BowMovement_Name = "bowm";

	// Token: 0x04001E54 RID: 7764
	protected const string kIdleChannel_Crouch_Name = "dcki";

	// Token: 0x04001E55 RID: 7765
	protected const string kIdleChannel_CrouchMovement_Name = "dckm";

	// Token: 0x04001E56 RID: 7766
	protected const string kIdleChannel_Fall_Name = "fall";

	// Token: 0x04001E57 RID: 7767
	protected const string kIdleChannel_Slip_Name = "slip";

	// Token: 0x04001E58 RID: 7768
	protected const string kIdleChannel_Zoom_Name = "zoom";

	// Token: 0x04001E59 RID: 7769
	[SerializeField]
	public Socket.CameraSpace pivot;

	// Token: 0x04001E5A RID: 7770
	[SerializeField]
	public Socket.CameraSpace pivot2;

	// Token: 0x04001E5B RID: 7771
	[SerializeField]
	public Socket.CameraSpace muzzle;

	// Token: 0x04001E5C RID: 7772
	[SerializeField]
	public Socket.CameraSpace sight;

	// Token: 0x04001E5D RID: 7773
	[SerializeField]
	public Socket.CameraSpace optics;

	// Token: 0x04001E5E RID: 7774
	[SerializeField]
	public Socket.CameraSpace bowPivot;

	// Token: 0x04001E5F RID: 7775
	protected static readonly string[] defaultSocketNames = new string[]
	{
		"muzzle",
		"sight",
		"optics",
		"pivot1",
		"pivot2",
		"bowPivot"
	};

	// Token: 0x04001E60 RID: 7776
	[NonSerialized]
	protected IEnumerable<string> socketNames;

	// Token: 0x04001E61 RID: 7777
	[NonSerialized]
	protected int socketVersion;

	// Token: 0x04001E62 RID: 7778
	[NonSerialized]
	private Socket.Map.Member _socketMap;

	// Token: 0x04001E63 RID: 7779
	private Vector3 originalRootOffset;

	// Token: 0x04001E64 RID: 7780
	private Quaternion originalRootRotation;

	// Token: 0x04001E65 RID: 7781
	private bool flipped;

	// Token: 0x04001E66 RID: 7782
	private Dictionary<Socket, Transform> proxies;

	// Token: 0x04001E67 RID: 7783
	private bool madeProxyDict;

	// Token: 0x04001E68 RID: 7784
	public int caps;

	// Token: 0x04001E69 RID: 7785
	public float perspectiveNearOverride = 0.1f;

	// Token: 0x04001E6A RID: 7786
	public float perspectiveFarOverride = 25f;

	// Token: 0x04001E6B RID: 7787
	public float perspectiveFOVOverride = 60f;

	// Token: 0x04001E6C RID: 7788
	public float perspectiveAspectOverride = 1f;

	// Token: 0x04001E6D RID: 7789
	public float lazyAngle = 5f;

	// Token: 0x04001E6E RID: 7790
	public float zoomFieldOfView = 40f;

	// Token: 0x04001E6F RID: 7791
	public AnimationCurve zoomCurve;

	// Token: 0x04001E70 RID: 7792
	public Vector3 zoomOffset;

	// Token: 0x04001E71 RID: 7793
	public Vector3 zoomRotate;

	// Token: 0x04001E72 RID: 7794
	public Vector3 offset;

	// Token: 0x04001E73 RID: 7795
	public Vector3 rotate;

	// Token: 0x04001E74 RID: 7796
	public Transform root;

	// Token: 0x04001E75 RID: 7797
	public Animation animation;

	// Token: 0x04001E76 RID: 7798
	public Texture crosshairTexture;

	// Token: 0x04001E77 RID: 7799
	public Texture dotTexture;

	// Token: 0x04001E78 RID: 7800
	public float zoomInDuration = 0.5f;

	// Token: 0x04001E79 RID: 7801
	public float zoomOutDuration = 0.4f;

	// Token: 0x04001E7A RID: 7802
	public bool showCrosshairZoom;

	// Token: 0x04001E7B RID: 7803
	public bool showCrosshairNotZoomed = true;

	// Token: 0x04001E7C RID: 7804
	public Color crosshairColor = Color.white;

	// Token: 0x04001E7D RID: 7805
	public Color crosshairOutline = Color.black;

	// Token: 0x04001E7E RID: 7806
	public LayerMask aimMask;

	// Token: 0x04001E7F RID: 7807
	public AnimationCurve headBobOffsetScale;

	// Token: 0x04001E80 RID: 7808
	public AnimationCurve headBobRotationScale;

	// Token: 0x04001E81 RID: 7809
	public bool barrelAiming = true;

	// Token: 0x04001E82 RID: 7810
	public bool barrelWhileZoom;

	// Token: 0x04001E83 RID: 7811
	public bool barrelWhileBowing;

	// Token: 0x04001E84 RID: 7812
	public Vector3 barrelPivot;

	// Token: 0x04001E85 RID: 7813
	public Vector2 barrelRotation;

	// Token: 0x04001E86 RID: 7814
	public float barrelLimit;

	// Token: 0x04001E87 RID: 7815
	public float noHitPlane = 20f;

	// Token: 0x04001E88 RID: 7816
	public float barrelAngleSmoothDamp = 0.01f;

	// Token: 0x04001E89 RID: 7817
	public float barrelAngleMaxSpeed = float.PositiveInfinity;

	// Token: 0x04001E8A RID: 7818
	public float barrelLimitOffsetFactor = 1f;

	// Token: 0x04001E8B RID: 7819
	public float barrelLimitPivotFactor;

	// Token: 0x04001E8C RID: 7820
	public bool bowAllowed;

	// Token: 0x04001E8D RID: 7821
	public Vector3 bowOffsetPoint;

	// Token: 0x04001E8E RID: 7822
	public Vector3 bowOffsetAngles;

	// Token: 0x04001E8F RID: 7823
	public AnimationCurve bowCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04001E90 RID: 7824
	public bool bowCurveIs01Fraction;

	// Token: 0x04001E91 RID: 7825
	public float bowEnterDuration = 1f;

	// Token: 0x04001E92 RID: 7826
	public float bowExitDuration = 1f;

	// Token: 0x04001E93 RID: 7827
	private float bowTime;

	// Token: 0x04001E94 RID: 7828
	public AnimationCurve zoomPunch;

	// Token: 0x04001E95 RID: 7829
	public float punchScalar = 1f;

	// Token: 0x04001E96 RID: 7830
	private float punchTime = -2000f;

	// Token: 0x04001E97 RID: 7831
	private float zoomPunchValue;

	// Token: 0x04001E98 RID: 7832
	public string fireAnimName = "fire_1";

	// Token: 0x04001E99 RID: 7833
	public string deployAnimName = "deploy";

	// Token: 0x04001E9A RID: 7834
	public string reloadAnimName = "reload";

	// Token: 0x04001E9B RID: 7835
	public float fireAnimScaleSpeed = 1f;

	// Token: 0x04001E9C RID: 7836
	[SerializeField]
	protected AnimationBlender.ResidualField idleFrame;

	// Token: 0x04001E9D RID: 7837
	[SerializeField]
	protected AnimationBlender.ChannelField idleChannel;

	// Token: 0x04001E9E RID: 7838
	[SerializeField]
	protected AnimationBlender.ChannelField movementIdleChannel;

	// Token: 0x04001E9F RID: 7839
	[SerializeField]
	protected AnimationBlender.ChannelField bowChannel;

	// Token: 0x04001EA0 RID: 7840
	[SerializeField]
	protected AnimationBlender.ChannelField bowMovementChannel;

	// Token: 0x04001EA1 RID: 7841
	[SerializeField]
	protected AnimationBlender.ChannelField crouchChannel;

	// Token: 0x04001EA2 RID: 7842
	[SerializeField]
	protected AnimationBlender.ChannelField crouchMovementChannel;

	// Token: 0x04001EA3 RID: 7843
	[SerializeField]
	protected AnimationBlender.ChannelField fallChannel;

	// Token: 0x04001EA4 RID: 7844
	[SerializeField]
	protected AnimationBlender.ChannelField slipChannel;

	// Token: 0x04001EA5 RID: 7845
	[SerializeField]
	protected AnimationBlender.ChannelField zoomChannel;

	// Token: 0x04001EA6 RID: 7846
	[NonSerialized]
	protected AnimationBlender.Mixer idleMixer;

	// Token: 0x04001EA7 RID: 7847
	[NonSerialized]
	public ItemRepresentation itemRep;

	// Token: 0x04001EA8 RID: 7848
	[NonSerialized]
	public IHeldItem item;

	// Token: 0x04001EA9 RID: 7849
	[NonSerialized]
	private Angle2 lastLook;

	// Token: 0x04001EAA RID: 7850
	[SerializeField]
	private SkinnedMeshRenderer[] builtinRenderers;

	// Token: 0x04001EAB RID: 7851
	[NonSerialized]
	private ViewModel.MeshInstance.Holder meshInstances;

	// Token: 0x04001EAC RID: 7852
	[NonSerialized]
	private ViewModel.BarrelParameters bpHip;

	// Token: 0x04001EAD RID: 7853
	[NonSerialized]
	private ViewModel.BarrelParameters bpZoom;

	// Token: 0x04001EAE RID: 7854
	[NonSerialized]
	private ViewModel.BarrelParameters bpBow;

	// Token: 0x04001EAF RID: 7855
	private static bool force_legacy_fallback;

	// Token: 0x04001EB0 RID: 7856
	private HeadBob _headBob;

	// Token: 0x04001EB1 RID: 7857
	private LazyCam _lazyCam;

	// Token: 0x04001EB2 RID: 7858
	private Quaternion _additiveRotation = Quaternion.identity;

	// Token: 0x04001EB3 RID: 7859
	private float zoomTime;

	// Token: 0x04001EB4 RID: 7860
	private float headBobLinearTime;

	// Token: 0x04001EB5 RID: 7861
	private float headBobAngularTime;

	// Token: 0x04001EB6 RID: 7862
	private float lastZoomFraction = float.NaN;

	// Token: 0x04001EB7 RID: 7863
	private float lastHeadBobLinearFraction;

	// Token: 0x04001EB8 RID: 7864
	private float lastHeadBobAngular;

	// Token: 0x04001EB9 RID: 7865
	private Vector3 lastLocalPositionOffset;

	// Token: 0x04001EBA RID: 7866
	private Vector3 lastLocalRotationOffset;

	// Token: 0x04001EBB RID: 7867
	private Vector3 lastSightRotation;

	// Token: 0x04001EBC RID: 7868
	private Transform eye;

	// Token: 0x04001EBD RID: 7869
	private Transform shelf;

	// Token: 0x04001EBE RID: 7870
	private List<GameObject> destroyOnUnbind;

	// Token: 0x04001EBF RID: 7871
	private static bool modifyAiming;

	// Token: 0x02000693 RID: 1683
	private class MeshInstance
	{
		// Token: 0x06003A5E RID: 14942 RVA: 0x000DA7A8 File Offset: 0x000D89A8
		private MeshInstance()
		{
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x000DA7B0 File Offset: 0x000D89B0
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
				if (ViewModel.MeshInstance.dumpCount < 8)
				{
					this.next = ViewModel.MeshInstance.dump;
					ViewModel.MeshInstance.dump = this;
					this.hasNext = (ViewModel.MeshInstance.dumpCount++ > 0);
				}
				else
				{
					this.next = null;
					this.hasNext = false;
				}
			}
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x000DA854 File Offset: 0x000D8A54
		private static bool New(ViewModel.MeshInstance ptr, SkinnedMeshRenderer renderer, out ViewModel.MeshInstance newInstance)
		{
			if (!renderer)
			{
				newInstance = null;
				return false;
			}
			if (ViewModel.MeshInstance.dumpCount > 0)
			{
				newInstance = ViewModel.MeshInstance.dump;
				if (--ViewModel.MeshInstance.dumpCount > 0)
				{
					ViewModel.MeshInstance.dump = newInstance.next;
				}
				else
				{
					ViewModel.MeshInstance.dump = null;
				}
				newInstance.next = null;
				newInstance.hasNext = false;
				newInstance.disposed = false;
				newInstance.renderer = null;
			}
			else
			{
				newInstance = new ViewModel.MeshInstance();
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

		// Token: 0x06003A61 RID: 14945 RVA: 0x000DA97C File Offset: 0x000D8B7C
		private void SetReplacementRenderMaterial(ref ViewModel.MeshInstance.ReplacementRenderer rr, int itsa, Material mat)
		{
			if (!this.disposed)
			{
				if (!rr.initialized)
				{
					this.legacy = (ViewModel.force_legacy_fallback || this.renderer.sharedMesh.subMeshCount > 1);
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

		// Token: 0x06003A62 RID: 14946 RVA: 0x000DAB08 File Offset: 0x000D8D08
		public void SetPredrawMaterial(Material mat)
		{
			this.SetReplacementRenderMaterial(ref this.predraw, 1, mat);
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x000DAB18 File Offset: 0x000D8D18
		public void SetPostdrawMaterial(Material mat)
		{
			this.SetReplacementRenderMaterial(ref this.postdraw, 2, mat);
		}

		// Token: 0x04001EC2 RID: 7874
		private const int kMaxDumpCount = 8;

		// Token: 0x04001EC3 RID: 7875
		public ViewModel.MeshInstance next;

		// Token: 0x04001EC4 RID: 7876
		public SkinnedMeshRenderer renderer;

		// Token: 0x04001EC5 RID: 7877
		public bool legacy;

		// Token: 0x04001EC6 RID: 7878
		public ViewModel.MeshInstance.ReplacementRenderer predraw;

		// Token: 0x04001EC7 RID: 7879
		public ViewModel.MeshInstance.ReplacementRenderer postdraw;

		// Token: 0x04001EC8 RID: 7880
		public bool disposed;

		// Token: 0x04001EC9 RID: 7881
		public bool hasNext;

		// Token: 0x04001ECA RID: 7882
		private Material[] originalMaterials;

		// Token: 0x04001ECB RID: 7883
		private Material[] modifiedMaterials;

		// Token: 0x04001ECC RID: 7884
		private static ViewModel.MeshInstance dump;

		// Token: 0x04001ECD RID: 7885
		private static int dumpCount;

		// Token: 0x02000694 RID: 1684
		public struct Holder : IDisposable
		{
			// Token: 0x06003A64 RID: 14948 RVA: 0x000DAB28 File Offset: 0x000D8D28
			private void IterDelete(ViewModel.MeshInstance iter)
			{
				ViewModel.MeshInstance next = iter.next;
				iter.hasNext = next.hasNext;
				iter.next = next.next;
				this.InstanceDeleteShared(next);
			}

			// Token: 0x06003A65 RID: 14949 RVA: 0x000DAB5C File Offset: 0x000D8D5C
			private void FirstDelete()
			{
				ViewModel.MeshInstance instance = this.first;
				this.first = this.first.next;
				this.InstanceDeleteShared(instance);
			}

			// Token: 0x06003A66 RID: 14950 RVA: 0x000DAB88 File Offset: 0x000D8D88
			private void InstanceDeleteShared(ViewModel.MeshInstance instance)
			{
				this.count--;
				instance.Delete();
			}

			// Token: 0x06003A67 RID: 14951 RVA: 0x000DABA0 File Offset: 0x000D8DA0
			public bool Delete(ViewModel.MeshInstance instance)
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
					ViewModel.MeshInstance meshInstance = this.first;
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

			// Token: 0x06003A68 RID: 14952 RVA: 0x000DAC24 File Offset: 0x000D8E24
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
					ViewModel.MeshInstance next = this.first;
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

			// Token: 0x06003A69 RID: 14953 RVA: 0x000DACB4 File Offset: 0x000D8EB4
			private bool AddShared(bool didIt, ViewModel.MeshInstance meshInstance)
			{
				if (didIt && this.count++ == 0)
				{
					this.first = meshInstance;
				}
				CameraFX mainCameraFX = CameraFX.mainCameraFX;
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

			// Token: 0x06003A6A RID: 14954 RVA: 0x000DAD28 File Offset: 0x000D8F28
			public bool Add(SkinnedMeshRenderer renderer)
			{
				ViewModel.MeshInstance meshInstance;
				return renderer && this.Add(renderer, out meshInstance);
			}

			// Token: 0x06003A6B RID: 14955 RVA: 0x000DAD4C File Offset: 0x000D8F4C
			public bool Add(SkinnedMeshRenderer renderer, out ViewModel.MeshInstance newOrExistingInstance)
			{
				if (this.disposed)
				{
					newOrExistingInstance = null;
					return false;
				}
				if (this.count == 0)
				{
					return this.AddShared(ViewModel.MeshInstance.New(null, renderer, out newOrExistingInstance), newOrExistingInstance);
				}
				if (object.ReferenceEquals(this.first.renderer, renderer))
				{
					newOrExistingInstance = this.first;
					return false;
				}
				int num = this.count - 1;
				ViewModel.MeshInstance next = this.first;
				for (int i = 0; i < num; i++)
				{
					if (object.ReferenceEquals(next.next.renderer, renderer))
					{
						newOrExistingInstance = next.next;
						return false;
					}
					next = next.next;
				}
				return this.AddShared(ViewModel.MeshInstance.New(next, renderer, out newOrExistingInstance), newOrExistingInstance);
			}

			// Token: 0x06003A6C RID: 14956 RVA: 0x000DAE00 File Offset: 0x000D9000
			public void Clear()
			{
				while (this.count > 0)
				{
					this.FirstDelete();
				}
			}

			// Token: 0x06003A6D RID: 14957 RVA: 0x000DAE1C File Offset: 0x000D901C
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

			// Token: 0x04001ECE RID: 7886
			public ViewModel.MeshInstance first;

			// Token: 0x04001ECF RID: 7887
			public int count;

			// Token: 0x04001ED0 RID: 7888
			public bool disposed;
		}

		// Token: 0x02000695 RID: 1685
		public struct ReplacementRenderer
		{
			// Token: 0x06003A6E RID: 14958 RVA: 0x000DAE64 File Offset: 0x000D9064
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

			// Token: 0x06003A6F RID: 14959 RVA: 0x000DAEAC File Offset: 0x000D90AC
			public void Shutdown()
			{
				if (this.initialized)
				{
					if (this.renderer)
					{
						Object.Destroy(this.renderer.gameObject);
					}
					this = default(ViewModel.MeshInstance.ReplacementRenderer);
				}
			}

			// Token: 0x06003A70 RID: 14960 RVA: 0x000DAEF4 File Offset: 0x000D90F4
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

			// Token: 0x06003A71 RID: 14961 RVA: 0x000DAFC4 File Offset: 0x000D91C4
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

			// Token: 0x04001ED1 RID: 7889
			public const int kItsaPreDraw = 1;

			// Token: 0x04001ED2 RID: 7890
			public const int kItsaPostDraw = 2;

			// Token: 0x04001ED3 RID: 7891
			public Material[] materials;

			// Token: 0x04001ED4 RID: 7892
			public SkinnedMeshRenderer renderer;

			// Token: 0x04001ED5 RID: 7893
			public bool initialized;

			// Token: 0x04001ED6 RID: 7894
			public int offset;
		}
	}

	// Token: 0x02000696 RID: 1686
	private struct BarrelTransform
	{
		// Token: 0x06003A72 RID: 14962 RVA: 0x000DB0EC File Offset: 0x000D92EC
		public void Get(out Vector3 origin, out Vector3 angles)
		{
			origin = this.origin;
			angles = this.angles;
		}

		// Token: 0x04001ED7 RID: 7895
		public Vector3 origin;

		// Token: 0x04001ED8 RID: 7896
		public Vector3 angles;
	}

	// Token: 0x02000697 RID: 1687
	private struct BarrelParameters
	{
		// Token: 0x04001ED9 RID: 7897
		public float a;

		// Token: 0x04001EDA RID: 7898
		public float b;

		// Token: 0x04001EDB RID: 7899
		public float c;

		// Token: 0x04001EDC RID: 7900
		public float bc;

		// Token: 0x04001EDD RID: 7901
		public float ca;

		// Token: 0x04001EDE RID: 7902
		public float ab;

		// Token: 0x04001EDF RID: 7903
		public bool once;

		// Token: 0x04001EE0 RID: 7904
		public bool ir;

		// Token: 0x04001EE1 RID: 7905
		public float angle;

		// Token: 0x04001EE2 RID: 7906
		public float angularVelocity;
	}
}
