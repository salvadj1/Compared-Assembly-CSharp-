using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200032A RID: 810
public abstract class Socket
{
	// Token: 0x06001EF7 RID: 7927 RVA: 0x0007A0BC File Offset: 0x000782BC
	protected Socket(bool is_vm)
	{
		this.is_vm = is_vm;
	}

	// Token: 0x170007B1 RID: 1969
	// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x0007A0CC File Offset: 0x000782CC
	public Transform attachParent
	{
		get
		{
			if (this.is_vm)
			{
				return ((Socket.CameraSpace)this).attachParent;
			}
			return this.parent;
		}
	}

	// Token: 0x170007B2 RID: 1970
	// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x0007A0EC File Offset: 0x000782EC
	public Vector3 position
	{
		get
		{
			if (this.is_vm)
			{
				return ((Socket.CameraSpace)this).position;
			}
			return ((Socket.LocalSpace)this).position;
		}
	}

	// Token: 0x170007B3 RID: 1971
	// (get) Token: 0x06001EFA RID: 7930 RVA: 0x0007A11C File Offset: 0x0007831C
	public Quaternion rotation
	{
		get
		{
			if (this.is_vm)
			{
				return ((Socket.CameraSpace)this).rotation;
			}
			return ((Socket.LocalSpace)this).rotation;
		}
	}

	// Token: 0x170007B4 RID: 1972
	// (get) Token: 0x06001EFB RID: 7931 RVA: 0x0007A14C File Offset: 0x0007834C
	public Vector3 localPosition
	{
		get
		{
			return this.offset;
		}
	}

	// Token: 0x170007B5 RID: 1973
	// (get) Token: 0x06001EFC RID: 7932 RVA: 0x0007A154 File Offset: 0x00078354
	public Quaternion localRotation
	{
		get
		{
			return this.rotate;
		}
	}

	// Token: 0x06001EFD RID: 7933 RVA: 0x0007A15C File Offset: 0x0007835C
	public bool AddChild(Transform transform, bool snap)
	{
		if (this.is_vm)
		{
			return ((Socket.CameraSpace)this).AddChild(transform, snap);
		}
		return ((Socket.LocalSpace)this).AddChild(transform, snap);
	}

	// Token: 0x06001EFE RID: 7934 RVA: 0x0007A190 File Offset: 0x00078390
	public bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket)
	{
		if (this.is_vm)
		{
			return ((Socket.CameraSpace)this).AddChildWithCoords(transform, offsetFromThisSocket);
		}
		return ((Socket.LocalSpace)this).AddChildWithCoords(transform, offsetFromThisSocket);
	}

	// Token: 0x06001EFF RID: 7935 RVA: 0x0007A1C4 File Offset: 0x000783C4
	public bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket, Vector3 eulerOffsetFromThisSocket)
	{
		if (this.is_vm)
		{
			return ((Socket.CameraSpace)this).AddChildWithCoords(transform, offsetFromThisSocket, eulerOffsetFromThisSocket);
		}
		return ((Socket.LocalSpace)this).AddChildWithCoords(transform, offsetFromThisSocket, eulerOffsetFromThisSocket);
	}

	// Token: 0x06001F00 RID: 7936 RVA: 0x0007A1FC File Offset: 0x000783FC
	public bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket, Quaternion rotationalOffsetFromThisSocket)
	{
		if (this.is_vm)
		{
			return ((Socket.CameraSpace)this).AddChildWithCoords(transform, offsetFromThisSocket, rotationalOffsetFromThisSocket);
		}
		return ((Socket.LocalSpace)this).AddChildWithCoords(transform, offsetFromThisSocket, rotationalOffsetFromThisSocket);
	}

	// Token: 0x170007B6 RID: 1974
	// (get) Token: 0x06001F01 RID: 7937 RVA: 0x0007A234 File Offset: 0x00078434
	public Quaternion rotate
	{
		get
		{
			if (!this.got_last || this.rotate_last != this.eulerRotate)
			{
				this.rotate_last = this.eulerRotate;
				this.quat_last = Quaternion.Euler(this.eulerRotate);
				this.got_last = true;
			}
			return this.quat_last;
		}
	}

	// Token: 0x06001F02 RID: 7938 RVA: 0x0007A28C File Offset: 0x0007848C
	public void Rotate(Quaternion rotation)
	{
		if (this.is_vm)
		{
			((Socket.CameraSpace)this).Rotate(rotation);
		}
		else
		{
			((Socket.LocalSpace)this).Rotate(rotation);
		}
	}

	// Token: 0x06001F03 RID: 7939 RVA: 0x0007A2C4 File Offset: 0x000784C4
	public void UnRotate(Quaternion rotation)
	{
		if (this.is_vm)
		{
			((Socket.CameraSpace)this).UnRotate(rotation);
		}
		else
		{
			((Socket.LocalSpace)this).UnRotate(rotation);
		}
	}

	// Token: 0x06001F04 RID: 7940 RVA: 0x0007A2FC File Offset: 0x000784FC
	public void DrawGizmos(string icon)
	{
		Matrix4x4 matrix = Gizmos.matrix;
		if (this.parent)
		{
			Gizmos.matrix = this.parent.localToWorldMatrix;
		}
		Gizmos.matrix *= Matrix4x4.TRS(this.offset, this.rotate, Vector3.one);
		Color color = Gizmos.color;
		Gizmos.color = color * Color.red;
		Gizmos.DrawLine(Vector3.zero, Vector3.right * 0.1f);
		if (icon != null)
		{
			Gizmos.DrawIcon(Vector3.left, icon);
		}
		Gizmos.color = color * Color.green;
		Gizmos.DrawLine(Vector3.zero, Vector3.up * 0.1f);
		if (icon != null)
		{
			Gizmos.DrawIcon(Vector3.down, icon);
		}
		Gizmos.color = color * Color.blue;
		Gizmos.DrawLine(Vector3.zero, Vector3.forward * 0.1f);
		Gizmos.matrix = matrix;
		Gizmos.color = color;
	}

	// Token: 0x06001F05 RID: 7941 RVA: 0x0007A404 File Offset: 0x00078604
	private void AddInstanceChild(Transform tr, bool snap)
	{
		if (!this.AddChild(tr, snap))
		{
			Debug.LogWarning("Could not add child!", tr);
		}
	}

	// Token: 0x06001F06 RID: 7942 RVA: 0x0007A420 File Offset: 0x00078620
	public Transform InstantiateAsChild(Transform prefab, bool snap)
	{
		Transform transform = (Transform)Object.Instantiate(prefab, this.position, this.rotation);
		this.AddInstanceChild(transform, snap);
		return transform;
	}

	// Token: 0x06001F07 RID: 7943 RVA: 0x0007A450 File Offset: 0x00078650
	public GameObject InstantiateAsChild(GameObject prefab, bool snap)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(prefab, this.position, this.rotation);
		this.AddInstanceChild(gameObject.transform, snap);
		return gameObject;
	}

	// Token: 0x06001F08 RID: 7944 RVA: 0x0007A484 File Offset: 0x00078684
	public TComponent InstantiateAsChild<TComponent>(TComponent prefab, bool snap) where TComponent : Component
	{
		TComponent result = (TComponent)((object)Object.Instantiate(prefab, this.position, this.rotation));
		this.AddInstanceChild(result.transform, snap);
		return result;
	}

	// Token: 0x06001F09 RID: 7945 RVA: 0x0007A4C4 File Offset: 0x000786C4
	public TObject Instantiate<TObject>(TObject prefab) where TObject : Object
	{
		return (TObject)((object)Object.Instantiate(prefab, this.position, this.rotation));
	}

	// Token: 0x06001F0A RID: 7946 RVA: 0x0007A4E4 File Offset: 0x000786E4
	public void Snap()
	{
		if (this.is_vm)
		{
			((Socket.CameraSpace)this).Snap();
		}
	}

	// Token: 0x04000EEE RID: 3822
	public Transform parent;

	// Token: 0x04000EEF RID: 3823
	public Vector3 offset;

	// Token: 0x04000EF0 RID: 3824
	public Vector3 eulerRotate;

	// Token: 0x04000EF1 RID: 3825
	private readonly bool is_vm;

	// Token: 0x04000EF2 RID: 3826
	private Vector3 rotate_last;

	// Token: 0x04000EF3 RID: 3827
	private Quaternion quat_last;

	// Token: 0x04000EF4 RID: 3828
	private bool got_last;

	// Token: 0x0200032B RID: 811
	public struct CameraConversion : IEquatable<Socket.CameraConversion>
	{
		// Token: 0x06001F0B RID: 7947 RVA: 0x0007A4FC File Offset: 0x000786FC
		public CameraConversion(Transform World, Transform Camera)
		{
			this.Eye = World;
			this.Shelf = Camera;
			this.Provided = (World != Camera && World && Camera);
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x0007A540 File Offset: 0x00078740
		public bool Valid
		{
			get
			{
				return this.Provided && this.Eye && this.Shelf;
			}
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0007A56C File Offset: 0x0007876C
		public bool Equals(Socket.CameraConversion other)
		{
			return (!this.Provided) ? (!other.Provided) : (other.Provided && this.Eye == other.Eye && this.Shelf == other.Shelf);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0007A5CC File Offset: 0x000787CC
		public override bool Equals(object obj)
		{
			return obj is Socket.CameraConversion && this.Equals((Socket.CameraConversion)obj);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0007A5E8 File Offset: 0x000787E8
		public override string ToString()
		{
			return (!this.Valid) ? ((!this.Provided) ? "[CameraConversion:NotProvided]" : "[CameraConversion:Invalid]") : "[CameraConversion:Valid]";
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0007A61C File Offset: 0x0007881C
		public override int GetHashCode()
		{
			return (!this.Provided) ? 0 : (this.Eye.GetHashCode() ^ this.Shelf.GetHashCode());
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x0007A654 File Offset: 0x00078854
		public static Socket.CameraConversion None
		{
			get
			{
				return default(Socket.CameraConversion);
			}
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0007A66C File Offset: 0x0007886C
		public static implicit operator bool(Socket.CameraConversion cc)
		{
			return cc.Valid;
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0007A678 File Offset: 0x00078878
		public static bool operator true(Socket.CameraConversion cc)
		{
			return cc.Valid;
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0007A684 File Offset: 0x00078884
		public static bool operator false(Socket.CameraConversion cc)
		{
			return !cc.Valid;
		}

		// Token: 0x04000EF5 RID: 3829
		public readonly Transform Eye;

		// Token: 0x04000EF6 RID: 3830
		public readonly Transform Shelf;

		// Token: 0x04000EF7 RID: 3831
		public readonly bool Provided;
	}

	// Token: 0x0200032C RID: 812
	[Serializable]
	public sealed class CameraSpace : Socket
	{
		// Token: 0x06001F15 RID: 7957 RVA: 0x0007A690 File Offset: 0x00078890
		public CameraSpace() : base(true)
		{
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0007A69C File Offset: 0x0007889C
		public static void UpdateProxy(Transform key, Transform value, Transform shelf, Transform eye)
		{
			value.position = eye.TransformPoint(shelf.InverseTransformPoint(key.position));
			Vector3 vector = eye.TransformDirection(shelf.InverseTransformDirection(key.forward));
			Vector3 vector2 = eye.TransformDirection(shelf.InverseTransformDirection(key.up));
			value.rotation = Quaternion.LookRotation(vector, vector2);
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x0007A6F4 File Offset: 0x000788F4
		public new Vector3 position
		{
			get
			{
				Vector3 vector;
				if (this.root)
				{
					if (this.parent && this.parent != this.root)
					{
						vector = this.root.InverseTransformPoint(this.parent.TransformPoint(this.offset));
					}
					else
					{
						vector = this.offset;
					}
				}
				else if (this.parent)
				{
					vector = this.parent.TransformPoint(this.offset);
				}
				else
				{
					vector = this.offset;
				}
				return (!this.eye) ? vector : this.eye.TransformPoint(vector);
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x0007A7B8 File Offset: 0x000789B8
		public new Quaternion rotation
		{
			get
			{
				Quaternion quaternion;
				if (this.root)
				{
					if (this.parent && this.parent != this.root)
					{
						quaternion = Quaternion.Inverse(this.root.rotation) * (base.rotate * this.parent.rotation);
					}
					else
					{
						quaternion = base.rotate;
					}
				}
				else if (this.parent)
				{
					quaternion = base.rotate * this.parent.rotation;
				}
				else
				{
					quaternion = base.rotate;
				}
				if (this.eye)
				{
					return this.eye.rotation * quaternion;
				}
				return quaternion;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0007A890 File Offset: 0x00078A90
		public new Transform attachParent
		{
			get
			{
				if (this.proxy)
				{
					return this.proxyTransform;
				}
				return this.eye;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0007A8AC File Offset: 0x00078AAC
		public Vector3 preEyePosition
		{
			get
			{
				return (!this.parent) ? this.offset : this.parent.TransformPoint(this.offset);
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001F1B RID: 7963 RVA: 0x0007A8E8 File Offset: 0x00078AE8
		public Quaternion preEyeRotation
		{
			get
			{
				return (!this.parent) ? base.rotate : (this.parent.rotation * base.rotate);
			}
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0007A928 File Offset: 0x00078B28
		public new void Rotate(Quaternion rotation)
		{
			float num;
			Vector3 vector;
			rotation.ToAngleAxis(ref num, ref vector);
			vector = this.parent.TransformDirection(vector);
			this.parent.RotateAround(this.preEyePosition, vector, num);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0007A960 File Offset: 0x00078B60
		public new void UnRotate(Quaternion rotation)
		{
			float num;
			Vector3 vector;
			rotation.ToAngleAxis(ref num, ref vector);
			vector = this.parent.TransformDirection(vector);
			this.parent.RotateAround(this.preEyePosition, -vector, num);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0007A9A0 File Offset: 0x00078BA0
		public new bool AddChild(Transform transform, bool snap)
		{
			if (this.proxy && this.proxyTransform)
			{
				if (snap)
				{
					transform.parent = this.proxyTransform;
					transform.localPosition = this.offset;
					transform.localEulerAngles = this.eulerRotate;
				}
				else
				{
					Vector3 vector = transform.position;
					Vector3 vector2 = transform.forward;
					Vector3 vector3 = transform.up;
					if (this.eye)
					{
						vector = this.eye.InverseTransformPoint(vector);
						vector3 = this.eye.InverseTransformDirection(vector3);
						vector2 = this.eye.InverseTransformDirection(vector2);
					}
					if (this.root)
					{
						vector = this.root.TransformPoint(vector);
						vector3 = this.root.TransformDirection(vector3);
						vector2 = this.root.TransformDirection(vector2);
					}
					if (this.parent)
					{
						vector = this.parent.InverseTransformPoint(vector);
						vector3 = this.parent.InverseTransformDirection(vector3);
						vector2 = this.parent.InverseTransformDirection(vector2);
					}
					transform.parent = this.proxyTransform;
					transform.localPosition = vector;
					transform.localRotation = Quaternion.LookRotation(vector2, vector3);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0007AAD4 File Offset: 0x00078CD4
		public new bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket)
		{
			if (this.AddChild(transform, false))
			{
				transform.localPosition = this.offset + base.rotate * offsetFromThisSocket;
				return true;
			}
			return false;
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x0007AB10 File Offset: 0x00078D10
		public new bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket, Vector3 eulerOffsetFromThisSocket)
		{
			if (this.AddChild(transform, false))
			{
				Quaternion rotate = base.rotate;
				transform.localPosition = this.offset + rotate * offsetFromThisSocket;
				transform.localRotation = rotate * Quaternion.Euler(eulerOffsetFromThisSocket);
				return true;
			}
			return false;
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x0007AB60 File Offset: 0x00078D60
		public new bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket, Quaternion rotationOffsetFromThisSocket)
		{
			if (this.AddChild(transform, false))
			{
				Quaternion rotate = base.rotate;
				transform.localPosition = this.offset + rotate * offsetFromThisSocket;
				transform.localRotation = rotate * rotationOffsetFromThisSocket;
				return true;
			}
			return false;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0007ABAC File Offset: 0x00078DAC
		public new void Snap()
		{
			if (this.proxy && this.proxyTransform && this.root && this.eye)
			{
				Socket.CameraSpace.UpdateProxy(this.parent, this.proxyTransform, this.root, this.eye);
			}
		}

		// Token: 0x04000EF8 RID: 3832
		[NonSerialized]
		public Transform eye;

		// Token: 0x04000EF9 RID: 3833
		[NonSerialized]
		public Transform root;

		// Token: 0x04000EFA RID: 3834
		public bool proxy;

		// Token: 0x04000EFB RID: 3835
		[NonSerialized]
		internal Transform proxyTransform;
	}

	// Token: 0x0200032D RID: 813
	[Serializable]
	public sealed class ConfigBodyPart
	{
		// Token: 0x06001F24 RID: 7972 RVA: 0x0007AC1C File Offset: 0x00078E1C
		public static Socket.ConfigBodyPart Create(BodyPart parent, Vector3 offset, Vector3 eulerRotate)
		{
			return new Socket.ConfigBodyPart
			{
				parent = parent,
				offset = offset,
				eulerRotate = eulerRotate
			};
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0007AC48 File Offset: 0x00078E48
		private bool Find(HitBoxSystem system, out Transform parent)
		{
			if (!system)
			{
				parent = null;
				return false;
			}
			IDRemoteBodyPart idremoteBodyPart;
			if (!system.bodyParts.TryGetValue(this.parent, ref idremoteBodyPart))
			{
				parent = null;
				return false;
			}
			parent = idremoteBodyPart.transform;
			return true;
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0007AC8C File Offset: 0x00078E8C
		public bool Extract(ref Socket.LocalSpace space, HitBoxSystem system)
		{
			Transform transform;
			if (this.Find(system, out transform))
			{
				if (space == null)
				{
					space = new Socket.LocalSpace
					{
						parent = transform,
						eulerRotate = this.eulerRotate,
						offset = this.offset
					};
				}
				else if (space.parent != transform)
				{
					space.parent = transform;
					space.eulerRotate = this.eulerRotate;
					space.offset = this.offset;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0007AD14 File Offset: 0x00078F14
		public bool Extract(ref Socket.CameraSpace space, HitBoxSystem system)
		{
			Transform transform;
			if (this.Find(system, out transform))
			{
				if (space == null)
				{
					space = new Socket.CameraSpace
					{
						parent = transform,
						eulerRotate = this.eulerRotate,
						offset = this.offset
					};
				}
				else if (space.parent != transform)
				{
					space.parent = transform;
					space.eulerRotate = this.eulerRotate;
					space.offset = this.offset;
				}
				return true;
			}
			return false;
		}

		// Token: 0x04000EFC RID: 3836
		public BodyPart parent;

		// Token: 0x04000EFD RID: 3837
		public Vector3 offset;

		// Token: 0x04000EFE RID: 3838
		public Vector3 eulerRotate;
	}

	// Token: 0x0200032E RID: 814
	[Serializable]
	public sealed class LocalSpace : Socket
	{
		// Token: 0x06001F28 RID: 7976 RVA: 0x0007AD9C File Offset: 0x00078F9C
		public LocalSpace() : base(false)
		{
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x0007ADA8 File Offset: 0x00078FA8
		public new Vector3 position
		{
			get
			{
				return (!this.parent) ? this.offset : this.parent.TransformPoint(this.offset);
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x0007ADE4 File Offset: 0x00078FE4
		public new Quaternion rotation
		{
			get
			{
				return (!this.parent) ? base.rotate : (this.parent.rotation * base.rotate);
			}
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x0007AE24 File Offset: 0x00079024
		public new void Rotate(Quaternion rotation)
		{
			float num;
			Vector3 vector;
			rotation.ToAngleAxis(ref num, ref vector);
			vector = this.parent.TransformDirection(vector);
			this.parent.RotateAround(this.position, vector, num);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0007AE5C File Offset: 0x0007905C
		public new void UnRotate(Quaternion rotation)
		{
			float num;
			Vector3 vector;
			rotation.ToAngleAxis(ref num, ref vector);
			vector = this.parent.TransformDirection(vector);
			this.parent.RotateAround(this.position, -vector, num);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0007AE9C File Offset: 0x0007909C
		public new bool AddChild(Transform transform, bool snap)
		{
			if (transform)
			{
				transform.parent = this.parent;
				if (snap)
				{
					transform.localPosition = this.offset;
					transform.localEulerAngles = this.eulerRotate;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0007AEE4 File Offset: 0x000790E4
		public new bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket)
		{
			if (this.AddChild(transform, false))
			{
				transform.localPosition = this.offset + base.rotate * offsetFromThisSocket;
				return true;
			}
			return false;
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0007AF20 File Offset: 0x00079120
		public new bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket, Vector3 eulerOffsetFromThisSocket)
		{
			if (this.AddChild(transform, false))
			{
				Quaternion rotate = base.rotate;
				transform.localPosition = this.offset + rotate * offsetFromThisSocket;
				transform.localRotation = rotate * Quaternion.Euler(eulerOffsetFromThisSocket);
				return true;
			}
			return false;
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0007AF70 File Offset: 0x00079170
		public new bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket, Quaternion rotationOffsetFromThisSocket)
		{
			if (this.AddChild(transform, false))
			{
				Quaternion rotate = base.rotate;
				transform.localPosition = this.offset + rotate * offsetFromThisSocket;
				transform.localRotation = rotate * rotationOffsetFromThisSocket;
				return true;
			}
			return false;
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0007AFBC File Offset: 0x000791BC
		public new void Snap()
		{
		}
	}

	// Token: 0x0200032F RID: 815
	public interface Source
	{
		// Token: 0x06001F32 RID: 7986
		bool GetSocket(string name, out Socket socket);

		// Token: 0x06001F33 RID: 7987
		bool ReplaceSocket(string name, Socket newValue);

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001F34 RID: 7988
		IEnumerable<string> SocketNames { get; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001F35 RID: 7989
		int SocketsVersion { get; }

		// Token: 0x06001F36 RID: 7990
		Socket.CameraConversion CameraSpaceSetup();

		// Token: 0x06001F37 RID: 7991
		Type ProxyScriptType(string name);
	}

	// Token: 0x02000330 RID: 816
	public interface Mapped
	{
		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001F38 RID: 7992
		Socket.Map socketMap { get; }
	}

	// Token: 0x02000331 RID: 817
	public interface Provider : Socket.Source, Socket.Mapped
	{
	}

	// Token: 0x02000332 RID: 818
	public abstract class Proxy : MonoBehaviour, Socket.Mapped
	{
		// Token: 0x06001F39 RID: 7993 RVA: 0x0007AFC0 File Offset: 0x000791C0
		public Proxy()
		{
			this.link = Socket.ProxyLink.Pop();
			this.link.proxy = this;
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x0007AFE0 File Offset: 0x000791E0
		public Transform transform
		{
			get
			{
				return this._transform;
			}
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x0007AFE8 File Offset: 0x000791E8
		public bool GetSocketMap(out Socket.Map map)
		{
			return this.link.map.Try(out map);
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x0007AFFC File Offset: 0x000791FC
		public Socket.Map socketMap
		{
			get
			{
				return this.link.map.Map;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x0007B010 File Offset: 0x00079210
		public int socketIndex
		{
			get
			{
				return (!this.link.linked || !this.link.map.Exists) ? -1 : this.link.index;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0007B054 File Offset: 0x00079254
		public Socket.CameraSpace socket
		{
			get
			{
				Socket.CameraSpace cameraSpace;
				return (!this.link.linked || !this.link.map.Socket<Socket.CameraSpace>(this.link.index, out cameraSpace)) ? null : cameraSpace;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x0007B09C File Offset: 0x0007929C
		public string socketName
		{
			get
			{
				string text;
				return (!this.link.linked || !this.link.map.Name(this.link.index, out text)) ? null : text;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x0007B0E4 File Offset: 0x000792E4
		public bool socketExists
		{
			get
			{
				return this.link.linked && this.link.map.Exists;
			}
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0007B10C File Offset: 0x0007930C
		protected virtual void InitializeProxy()
		{
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0007B110 File Offset: 0x00079310
		protected virtual void UninitializeProxy()
		{
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x0007B114 File Offset: 0x00079314
		protected void Awake()
		{
			this._transform = base.transform;
			this.link.scriptAlive = true;
			try
			{
				this.InitializeProxy();
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this);
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x0007B170 File Offset: 0x00079370
		protected void OnDestroy()
		{
			if (this.link.scriptAlive)
			{
				this.link.scriptAlive = false;
				try
				{
					this.UninitializeProxy();
				}
				finally
				{
					Socket.Map map;
					if (this.GetSocketMap(out map))
					{
						map.OnProxyDestroyed(this.link);
					}
					this.link.proxy = null;
				}
			}
		}

		// Token: 0x04000EFF RID: 3839
		[NonSerialized]
		private readonly Socket.ProxyLink link;

		// Token: 0x04000F00 RID: 3840
		[NonSerialized]
		private Transform _transform;
	}

	// Token: 0x02000333 RID: 819
	internal sealed class ProxyLink
	{
		// Token: 0x06001F46 RID: 8006 RVA: 0x0007B1F8 File Offset: 0x000793F8
		public static Socket.ProxyLink Pop()
		{
			return Socket.ProxyLink.Usage.Stack.Pop();
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x0007B204 File Offset: 0x00079404
		public static void Push(Socket.ProxyLink top)
		{
			Socket.ProxyLink.Usage.Stack.Push(top);
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0007B214 File Offset: 0x00079414
		public static void EnsurePopped(Socket.ProxyLink top)
		{
			if (Socket.ProxyLink.Usage.Stack.Count > 0 && Socket.ProxyLink.Usage.Stack.Peek() == top)
			{
				Socket.ProxyLink.Usage.Stack.Pop();
			}
		}

		// Token: 0x04000F01 RID: 3841
		[NonSerialized]
		public Socket.Map.Reference map;

		// Token: 0x04000F02 RID: 3842
		[NonSerialized]
		public Socket.Proxy proxy;

		// Token: 0x04000F03 RID: 3843
		[NonSerialized]
		public GameObject gameObject;

		// Token: 0x04000F04 RID: 3844
		[NonSerialized]
		public bool scriptAlive;

		// Token: 0x04000F05 RID: 3845
		[NonSerialized]
		public bool linked;

		// Token: 0x04000F06 RID: 3846
		[NonSerialized]
		public int index = -1;

		// Token: 0x02000334 RID: 820
		private static class Usage
		{
			// Token: 0x04000F07 RID: 3847
			public static readonly Stack<Socket.ProxyLink> Stack = new Stack<Socket.ProxyLink>();
		}
	}

	// Token: 0x02000335 RID: 821
	public struct Slot
	{
		// Token: 0x06001F4A RID: 8010 RVA: 0x0007B250 File Offset: 0x00079450
		internal Slot(Socket.Map.Reference map, int index)
		{
			this.m = map;
			this.index = index;
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x0007B260 File Offset: 0x00079460
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x0007B274 File Offset: 0x00079474
		public Socket socket
		{
			get
			{
				return this.m.Socket(this.index);
			}
			set
			{
				if (!this.ReplaceSocket(value))
				{
					throw new InvalidOperationException("could not replace socket");
				}
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x0007B290 File Offset: 0x00079490
		public Transform proxy
		{
			get
			{
				Socket.ProxyLink proxyLink;
				return (!this.m.Proxy(this.index, out proxyLink) || !proxyLink.proxy) ? null : proxyLink.proxy.transform;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x0007B2D8 File Offset: 0x000794D8
		public string name
		{
			get
			{
				return this.m.Name(this.index);
			}
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0007B2EC File Offset: 0x000794EC
		public bool BelongsTo(Socket.Map map)
		{
			return this.m.Is(map);
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0007B2FC File Offset: 0x000794FC
		public bool ReplaceSocket(Socket newSocketValue)
		{
			Socket.Map map;
			return this.m.Try(out map) && map.ReplaceSocket(this.index, newSocketValue);
		}

		// Token: 0x04000F08 RID: 3848
		private Socket.Map.Reference m;

		// Token: 0x04000F09 RID: 3849
		public readonly int index;
	}

	// Token: 0x02000336 RID: 822
	public sealed class Map : Socket.Mapped
	{
		// Token: 0x06001F51 RID: 8017 RVA: 0x0007B32C File Offset: 0x0007952C
		private Map(Socket.Source source, Object script)
		{
			this.source = source;
			this.script = script;
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x0007B344 File Offset: 0x00079544
		Socket.Map Socket.Mapped.socketMap
		{
			get
			{
				return this.EnsureMap();
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x0007B34C File Offset: 0x0007954C
		public int socketCount
		{
			get
			{
				return (!this.EnsureState()) ? 0 : this.array.Length;
			}
		}

		// Token: 0x170007CE RID: 1998
		public Socket.Slot this[int index]
		{
			get
			{
				if (index < 0 || !this.EnsureState() || index >= this.array.Length)
				{
					throw new IndexOutOfRangeException();
				}
				return new Socket.Slot(this, index);
			}
		}

		// Token: 0x170007CF RID: 1999
		public Socket.Slot this[string name]
		{
			get
			{
				if (!this.EnsureState())
				{
					throw new KeyNotFoundException(name);
				}
				return new Socket.Slot(this, this.dict[name]);
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x0007B3D4 File Offset: 0x000795D4
		public Socket.CameraConversion cameraConversion
		{
			get
			{
				Socket.CameraConversion result;
				this.GetCameraSpace(out result);
				return result;
			}
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x0007B3EC File Offset: 0x000795EC
		public bool ReplaceSocket(string name, Socket value)
		{
			int index;
			return this.EnsureState() && this.dict.TryGetValue(name, out index) && this.ValidSlotReplace(index, value);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0007B424 File Offset: 0x00079624
		public bool ReplaceSocket(int index, Socket value)
		{
			return index >= 0 && (this.EnsureState() && index < this.array.Length) && this.ValidSlotReplace(index, value);
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x0007B454 File Offset: 0x00079654
		public bool ReplaceSocket(Socket.Slot slot, Socket value)
		{
			return slot.index >= 0 && (slot.BelongsTo(this) && slot.index < this.array.Length) && this.ValidSlotReplace(slot.index, value);
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x0007B4A4 File Offset: 0x000796A4
		public void SnapProxies()
		{
			if (this.EnsureState())
			{
				Socket.CameraConversion cameraConversion;
				bool flag = this.GetCameraSpace(out cameraConversion);
				for (int i = 0; i < this.array.Length; i++)
				{
					if (this.array[i].madeLink && this.array[i].link.scriptAlive && this.array[i].link.linked)
					{
						try
						{
							Socket.CameraSpace cameraSpace = (Socket.CameraSpace)this.array[i].socket;
							cameraSpace.proxyTransform = this.array[i].link.proxy.transform;
							cameraSpace.eye = cameraConversion.Eye;
							cameraSpace.root = cameraConversion.Shelf;
							if (flag)
							{
								cameraSpace.Snap();
							}
						}
						catch (Exception ex)
						{
							Debug.LogException(ex, this.array[i].link.proxy);
						}
					}
				}
			}
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x0007B5D0 File Offset: 0x000797D0
		private static bool Of(ref Socket.Map member, out Socket.Map value)
		{
			if (object.ReferenceEquals(member, null))
			{
				value = null;
				return false;
			}
			Socket.Map map = member.EnsureMap();
			member = map;
			value = map;
			return !object.ReferenceEquals(map, null);
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x0007B608 File Offset: 0x00079808
		internal static Socket.Map Of(ref Socket.Map member)
		{
			Socket.Map result;
			Socket.Map.Of(ref member, out result);
			return result;
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0007B620 File Offset: 0x00079820
		private static Socket.Map Get<TSource>(TSource source, ref Socket.Map member) where TSource : Object, Socket.Source
		{
			if (object.ReferenceEquals(source, null))
			{
				throw new ArgumentNullException("source");
			}
			if (!source)
			{
				return Socket.Map.NullMap;
			}
			Socket.Map map = member;
			if (object.ReferenceEquals(map, null))
			{
				map = new Socket.Map(source, source);
			}
			Socket.Map result;
			member = (result = map.EnsureMap());
			return result;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0007B68C File Offset: 0x0007988C
		private void CleanTransforms()
		{
			this.checkTransforms = true;
			this.cameraSpace = Socket.CameraConversion.None;
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0007B6A0 File Offset: 0x000798A0
		private Socket.Map EnsureMap()
		{
			return (!this.EnsureState()) ? Socket.Map.NullMap : this;
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x0007B6B8 File Offset: 0x000798B8
		private bool EnsureState()
		{
			if (!this.script || this.deleted)
			{
				return false;
			}
			if (this.securing)
			{
				return true;
			}
			try
			{
				this.securing = true;
				this.SecureState();
			}
			finally
			{
				this.securing = false;
			}
			return true;
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x0007B728 File Offset: 0x00079928
		private static Socket.Map NullMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0007B72C File Offset: 0x0007992C
		private Socket.Map.Result SecureState()
		{
			Socket.Map.Result result = this.PollState();
			switch (result)
			{
			case Socket.Map.Result.Initialized:
				this.initialized = true;
				this.CleanTransforms();
				break;
			case Socket.Map.Result.Version:
				this.CleanTransforms();
				break;
			case Socket.Map.Result.Forced:
				break;
			default:
				return result;
			}
			this.OnState(result);
			return result;
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x0007B788 File Offset: 0x00079988
		private Socket.Map.Result PollState()
		{
			if (!this.initialized)
			{
				this.Initialize();
				return Socket.Map.Result.Initialized;
			}
			int socketsVersion = this.source.SocketsVersion;
			Socket.Map.Result result;
			if (this.version != socketsVersion)
			{
				this.version = socketsVersion;
				result = Socket.Map.Result.Version;
			}
			else
			{
				if (!this.forceUpdate)
				{
					return Socket.Map.Result.Nothing;
				}
				result = Socket.Map.Result.Forced;
			}
			this.forceUpdate = false;
			this.Update(result);
			return result;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x0007B7F4 File Offset: 0x000799F4
		private void Initialize()
		{
			IEnumerable<string> socketNames = this.source.SocketNames;
			ICollection<string> collection;
			if (object.ReferenceEquals(socketNames, null))
			{
				collection = new string[0];
			}
			else
			{
				collection = (socketNames as ICollection<string>);
				if (collection == null)
				{
					collection = new HashSet<string>(socketNames, StringComparer.InvariantCultureIgnoreCase);
				}
			}
			int count = collection.Count;
			this.array = new Socket.Map.Element[count];
			this.dict = new Dictionary<string, int>(count, StringComparer.InvariantCultureIgnoreCase);
			int num = 0;
			foreach (string text in collection)
			{
				if (this.source.GetSocket(text, out this.array[num].socket))
				{
					try
					{
						this.dict.Add(this.array[num].name = text, num);
					}
					catch (ArgumentException ex)
					{
						Debug.LogException(ex, this.script);
						Debug.Log(text);
						continue;
					}
					num++;
				}
			}
			Array.Resize<Socket.Map.Element>(ref this.array, num);
			this.version = this.source.SocketsVersion;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0007B95C File Offset: 0x00079B5C
		private void ElementUpdate(int srcIndex, ref Socket.Map.Element src, int dstIndex, ref Socket.Map.Element dst, Socket newSocket)
		{
			if (srcIndex != dstIndex)
			{
				dst.name = src.name;
				dst.link = src.link;
				dst.socket = src.socket;
				dst.madeLink = src.madeLink;
				if (dst.madeLink)
				{
					dst.link.index = dstIndex;
				}
				this.dict[dst.name] = dstIndex;
			}
			this.SocketUpdate(ref dst.socket, newSocket);
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0007B9E0 File Offset: 0x00079BE0
		private void SocketUpdate(ref Socket socket, Socket newSocket)
		{
			Socket socket2 = socket;
			if (!object.ReferenceEquals(socket2, newSocket))
			{
				socket = newSocket;
				if (socket2 is Socket.CameraSpace && newSocket is Socket.CameraSpace)
				{
					Socket.CameraSpace cameraSpace = (Socket.CameraSpace)socket2;
					Socket.CameraSpace cameraSpace2 = (Socket.CameraSpace)newSocket;
					cameraSpace2.root = cameraSpace.root;
					cameraSpace2.eye = cameraSpace.eye;
					cameraSpace2.proxyTransform = cameraSpace.proxyTransform;
				}
			}
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0007BA48 File Offset: 0x00079C48
		private void ElementRemove(ref Socket.Map.Element element, ref Socket.Map.RemoveList<Socket.ProxyLink> removeList)
		{
			if (element.madeLink)
			{
				if (element.link.scriptAlive)
				{
					removeList.Add(element.link);
				}
				element.link = null;
				element.madeLink = false;
			}
			this.dict.Remove(element.name);
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0007BA9C File Offset: 0x00079C9C
		private void Update(Socket.Map.Result Because)
		{
			if (Because == Socket.Map.Result.Version)
			{
				this.CleanTransforms();
			}
			int newSize = 0;
			Socket.Map.RemoveList<Socket.ProxyLink> removeList = default(Socket.Map.RemoveList<Socket.ProxyLink>);
			for (int i = 0; i < this.array.Length; i++)
			{
				Socket newSocket;
				if (this.source.GetSocket(this.array[i].name, out newSocket))
				{
					int num = newSize++;
					this.ElementUpdate(i, ref this.array[i], num, ref this.array[num], newSocket);
				}
				else
				{
					this.ElementRemove(ref this.array[i], ref removeList);
				}
			}
			Array.Resize<Socket.Map.Element>(ref this.array, newSize);
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0007BB50 File Offset: 0x00079D50
		private void OnState(Socket.Map.Result State)
		{
			bool flag = false;
			Socket.CameraConversion cameraConversion = default(Socket.CameraConversion);
			for (int i = 0; i < this.array.Length; i++)
			{
				Socket.Map.ProxyCheck proxyCheck;
				this.CheckProxyIndex(i, out proxyCheck);
				if (proxyCheck.isCameraSpace)
				{
					if (!flag)
					{
						bool flag2 = this.GetCameraSpace(out cameraConversion);
						flag = true;
					}
					proxyCheck.cameraSpace.eye = cameraConversion.Eye;
					proxyCheck.cameraSpace.root = cameraConversion.Shelf;
					proxyCheck.cameraSpace.proxyTransform = proxyCheck.proxyTransform;
				}
			}
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0007BBEC File Offset: 0x00079DEC
		private bool ValidSlotReplace(int index, Socket value)
		{
			Socket socket = this.array[index].socket;
			if (object.ReferenceEquals(value, socket))
			{
				return true;
			}
			if ((!object.ReferenceEquals(value, null) && value.GetType() != socket.GetType()) || !this.source.ReplaceSocket(this.array[index].name, value))
			{
				return false;
			}
			this.forceUpdate = true;
			return this.EnsureState();
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0007BC68 File Offset: 0x00079E68
		private bool GetCameraSpace(out Socket.CameraConversion cameraSpace)
		{
			if (!this.EnsureState())
			{
				this.checkTransforms = false;
				this.cameraSpace = Socket.CameraConversion.None;
			}
			else if (this.checkTransforms)
			{
				this.checkTransforms = false;
				this.cameraSpace = this.source.CameraSpaceSetup();
			}
			cameraSpace = this.cameraSpace;
			return cameraSpace.Valid;
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0007BCCC File Offset: 0x00079ECC
		private void DestroyProxyLink(Socket.ProxyLink link)
		{
			if (link.linked)
			{
				link.linked = false;
				if (link.scriptAlive && link.proxy)
				{
					Object.Destroy(link.proxy);
				}
				link.proxy = null;
				if (link.gameObject)
				{
					Object.Destroy(link.gameObject);
				}
				link.gameObject = null;
				link.proxy = null;
			}
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0007BD44 File Offset: 0x00079F44
		internal void OnProxyDestroyed(object link)
		{
			this.DestroyProxyLink((Socket.ProxyLink)link);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0007BD54 File Offset: 0x00079F54
		private Socket.ProxyLink MakeProxy(Socket.CameraSpace socket, int index)
		{
			Type type = this.source.ProxyScriptType(this.array[index].name);
			if (object.ReferenceEquals(type, null))
			{
				return null;
			}
			if (!typeof(Socket.Proxy).IsAssignableFrom(type))
			{
				throw new InvalidProgramException("SocketSource returned a type that did not extend SocketMap.Proxy");
			}
			Socket.ProxyLink proxyLink = new Socket.ProxyLink();
			proxyLink.map = this;
			proxyLink.index = index;
			Socket.ProxyLink.Push(proxyLink);
			Vector3 position = Vector3.zero;
			Quaternion rotation = Quaternion.identity;
			Socket.CameraConversion cameraConversion;
			if (this.GetCameraSpace(out cameraConversion))
			{
				socket.root = cameraConversion.Shelf;
				socket.eye = cameraConversion.Eye;
			}
			else
			{
				socket.eye = null;
				socket.root = null;
			}
			try
			{
				position = socket.position;
				rotation = socket.rotation;
			}
			catch (Exception ex)
			{
				Debug.LogException(ex, this.script);
			}
			try
			{
				proxyLink.gameObject = new GameObject(this.array[index].name, new Type[]
				{
					type
				})
				{
					transform = 
					{
						position = position,
						rotation = rotation
					}
				};
			}
			catch
			{
				proxyLink.linked = false;
				if (proxyLink.gameObject)
				{
					Object.Destroy(proxyLink.gameObject);
				}
				throw;
			}
			finally
			{
				Socket.ProxyLink.EnsurePopped(proxyLink);
			}
			proxyLink.linked = true;
			socket.proxyTransform = proxyLink.proxy.transform;
			return proxyLink;
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0007BF18 File Offset: 0x0007A118
		private void CheckProxyIndex(int index, out Socket.Map.ProxyCheck o)
		{
			if (o.isProxy = ((o.isCameraSpace = !object.ReferenceEquals(o.cameraSpace = (this.array[index].socket as Socket.CameraSpace), null)) && o.cameraSpace.proxy))
			{
				if (!this.array[index].madeLink)
				{
					o.proxyLink = (this.array[index].link = this.MakeProxy(o.cameraSpace, index));
					this.array[index].madeLink = true;
				}
				else
				{
					o.proxyLink = this.array[index].link;
				}
				o.parentOrProxy = o.proxyLink.proxy.transform;
			}
			else
			{
				o.proxyLink = null;
				o.parentOrProxy = this.array[index].socket.parent;
			}
			o.index = index;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x0007C028 File Offset: 0x0007A228
		private void Delete()
		{
			if (!this.initialized || this.deleted)
			{
				return;
			}
			this.deleted = true;
			for (int i = this.array.Length - 1; i >= 0; i--)
			{
				if (this.array[i].madeLink)
				{
					this.DestroyProxyLink(this.array[i].link);
				}
			}
		}

		// Token: 0x04000F0A RID: 3850
		[NonSerialized]
		private readonly Object script;

		// Token: 0x04000F0B RID: 3851
		[NonSerialized]
		private readonly Socket.Source source;

		// Token: 0x04000F0C RID: 3852
		[NonSerialized]
		private Dictionary<string, int> dict;

		// Token: 0x04000F0D RID: 3853
		[NonSerialized]
		private bool initialized;

		// Token: 0x04000F0E RID: 3854
		[NonSerialized]
		private bool checkTransforms;

		// Token: 0x04000F0F RID: 3855
		[NonSerialized]
		private bool securing;

		// Token: 0x04000F10 RID: 3856
		[NonSerialized]
		private bool forceUpdate;

		// Token: 0x04000F11 RID: 3857
		[NonSerialized]
		private bool deleted;

		// Token: 0x04000F12 RID: 3858
		[NonSerialized]
		private Socket.Map.Element[] array;

		// Token: 0x04000F13 RID: 3859
		[NonSerialized]
		private int version;

		// Token: 0x04000F14 RID: 3860
		[NonSerialized]
		private Socket.CameraConversion cameraSpace;

		// Token: 0x02000337 RID: 823
		public struct Member
		{
			// Token: 0x06001F71 RID: 8049 RVA: 0x0007C09C File Offset: 0x0007A29C
			public Socket.Map Get<T>(T outerInstance) where T : Object, Socket.Source
			{
				if (this.deleted)
				{
					return null;
				}
				return Socket.Map.Get<T>(outerInstance, ref this.reference);
			}

			// Token: 0x06001F72 RID: 8050 RVA: 0x0007C0B8 File Offset: 0x0007A2B8
			public bool Get<T>(T outerInstance, out Socket.Map map) where T : Object, Socket.Source
			{
				map = this.Get<T>(outerInstance);
				return !object.ReferenceEquals(map, null);
			}

			// Token: 0x06001F73 RID: 8051 RVA: 0x0007C0D0 File Offset: 0x0007A2D0
			public bool DeleteBy<T>(T outerInstance) where T : Object, Socket.Source
			{
				if (!this.deleted)
				{
					if (object.ReferenceEquals(this.reference, null))
					{
						this.deleted = true;
					}
					else
					{
						if (!object.ReferenceEquals(outerInstance, this.reference.source))
						{
							throw new ArgumentException("instance did not match that of which created the map", "outerInstance");
						}
						this.deleted = true;
						try
						{
							this.reference.Delete();
						}
						catch (Exception ex)
						{
							Debug.LogException(ex, outerInstance);
						}
						finally
						{
							this.reference = null;
						}
					}
					return true;
				}
				return false;
			}

			// Token: 0x04000F15 RID: 3861
			private Socket.Map reference;

			// Token: 0x04000F16 RID: 3862
			private bool deleted;
		}

		// Token: 0x02000338 RID: 824
		private struct Element
		{
			// Token: 0x04000F17 RID: 3863
			public Socket socket;

			// Token: 0x04000F18 RID: 3864
			public string name;

			// Token: 0x04000F19 RID: 3865
			public Socket.ProxyLink link;

			// Token: 0x04000F1A RID: 3866
			public bool madeLink;
		}

		// Token: 0x02000339 RID: 825
		private enum Result
		{
			// Token: 0x04000F1C RID: 3868
			Nothing,
			// Token: 0x04000F1D RID: 3869
			Initialized,
			// Token: 0x04000F1E RID: 3870
			Version,
			// Token: 0x04000F1F RID: 3871
			Forced
		}

		// Token: 0x0200033A RID: 826
		private struct RemoveList<T>
		{
			// Token: 0x06001F74 RID: 8052 RVA: 0x0007C198 File Offset: 0x0007A398
			public void Add(T item)
			{
				if (!this.exists)
				{
					this.list = new List<T>();
				}
				this.list.Add(item);
			}

			// Token: 0x04000F20 RID: 3872
			public bool exists;

			// Token: 0x04000F21 RID: 3873
			public List<T> list;
		}

		// Token: 0x0200033B RID: 827
		private struct ProxyCheck
		{
			// Token: 0x170007D2 RID: 2002
			// (get) Token: 0x06001F75 RID: 8053 RVA: 0x0007C1C8 File Offset: 0x0007A3C8
			public Transform proxyTransform
			{
				get
				{
					return (!this.isProxy) ? null : this.parentOrProxy;
				}
			}

			// Token: 0x170007D3 RID: 2003
			// (get) Token: 0x06001F76 RID: 8054 RVA: 0x0007C1E4 File Offset: 0x0007A3E4
			public Transform parent
			{
				get
				{
					return (!this.isProxy) ? this.parentOrProxy : this.cameraSpace.parent;
				}
			}

			// Token: 0x04000F22 RID: 3874
			public Transform parentOrProxy;

			// Token: 0x04000F23 RID: 3875
			public Socket.CameraSpace cameraSpace;

			// Token: 0x04000F24 RID: 3876
			public Socket.ProxyLink proxyLink;

			// Token: 0x04000F25 RID: 3877
			public int index;

			// Token: 0x04000F26 RID: 3878
			public bool isCameraSpace;

			// Token: 0x04000F27 RID: 3879
			public bool isProxy;
		}

		// Token: 0x0200033C RID: 828
		internal struct Reference
		{
			// Token: 0x06001F77 RID: 8055 RVA: 0x0007C208 File Offset: 0x0007A408
			private Reference(Socket.Map reference)
			{
				this.reference = reference;
			}

			// Token: 0x06001F78 RID: 8056 RVA: 0x0007C214 File Offset: 0x0007A414
			public bool Try(out Socket.Map map)
			{
				return global::Socket.Map.Of(ref this.reference, out map);
			}

			// Token: 0x06001F79 RID: 8057 RVA: 0x0007C224 File Offset: 0x0007A424
			private bool ByIndex(int index, out Socket.Map map)
			{
				if (index < 0)
				{
					map = null;
				}
				else if (this.Try(out map) && index < map.array.Length)
				{
					return true;
				}
				return false;
			}

			// Token: 0x06001F7A RID: 8058 RVA: 0x0007C254 File Offset: 0x0007A454
			private bool ByKey(string name, out Socket.Map map, out int index)
			{
				if (object.ReferenceEquals(name, null))
				{
					map = null;
				}
				else if (this.Try(out map))
				{
					return map.dict.TryGetValue(name, out index);
				}
				index = -1;
				return false;
			}

			// Token: 0x06001F7B RID: 8059 RVA: 0x0007C28C File Offset: 0x0007A48C
			private static bool Socket(bool valid, int index, Socket.Map map, out Socket socket)
			{
				if (valid)
				{
					socket = map.array[index].socket;
					return true;
				}
				socket = null;
				return false;
			}

			// Token: 0x06001F7C RID: 8060 RVA: 0x0007C2B0 File Offset: 0x0007A4B0
			private static bool Name(bool valid, int index, Socket.Map map, out string name)
			{
				if (valid)
				{
					name = map.array[index].name;
					return true;
				}
				name = null;
				return false;
			}

			// Token: 0x06001F7D RID: 8061 RVA: 0x0007C2D4 File Offset: 0x0007A4D4
			private static bool Proxy(bool valid, int index, Socket.Map map, out Socket.ProxyLink proxyLink)
			{
				if (valid)
				{
					proxyLink = map.array[index].link;
					return map.array[index].madeLink;
				}
				proxyLink = null;
				return false;
			}

			// Token: 0x06001F7E RID: 8062 RVA: 0x0007C308 File Offset: 0x0007A508
			public bool Socket(int index, out Socket socket)
			{
				Socket.Map map;
				return global::Socket.Map.Reference.Socket(this.ByIndex(index, out map), index, map, out socket);
			}

			// Token: 0x06001F7F RID: 8063 RVA: 0x0007C328 File Offset: 0x0007A528
			public Socket Socket(int index)
			{
				Socket.Map map = this.Map;
				return map.array[index].socket;
			}

			// Token: 0x06001F80 RID: 8064 RVA: 0x0007C350 File Offset: 0x0007A550
			public bool Name(int index, out string name)
			{
				Socket.Map map;
				return global::Socket.Map.Reference.Name(this.ByIndex(index, out map), index, map, out name);
			}

			// Token: 0x06001F81 RID: 8065 RVA: 0x0007C370 File Offset: 0x0007A570
			public string Name(int index)
			{
				Socket.Map map = this.Map;
				return map.array[index].name;
			}

			// Token: 0x06001F82 RID: 8066 RVA: 0x0007C398 File Offset: 0x0007A598
			public bool Proxy(int index, out Socket.ProxyLink link)
			{
				Socket.Map map;
				return global::Socket.Map.Reference.Proxy(this.ByIndex(index, out map), index, map, out link);
			}

			// Token: 0x06001F83 RID: 8067 RVA: 0x0007C3B8 File Offset: 0x0007A5B8
			internal Socket.ProxyLink Proxy(int index)
			{
				Socket.Map map = this.Map;
				return map.array[index].link;
			}

			// Token: 0x06001F84 RID: 8068 RVA: 0x0007C3E0 File Offset: 0x0007A5E0
			public bool Socket(string key, out Socket socket)
			{
				Socket.Map map;
				int index;
				return global::Socket.Map.Reference.Socket(this.ByKey(key, out map, out index), index, map, out socket);
			}

			// Token: 0x06001F85 RID: 8069 RVA: 0x0007C400 File Offset: 0x0007A600
			public Socket Socket(string key)
			{
				Socket.Map map = this.Map;
				return map.array[map.dict[key]].socket;
			}

			// Token: 0x06001F86 RID: 8070 RVA: 0x0007C430 File Offset: 0x0007A630
			public bool Name(string key, out string name)
			{
				Socket.Map map;
				int index;
				return global::Socket.Map.Reference.Name(this.ByKey(key, out map, out index), index, map, out name);
			}

			// Token: 0x06001F87 RID: 8071 RVA: 0x0007C450 File Offset: 0x0007A650
			public string Name(string key)
			{
				Socket.Map map = this.Map;
				return map.array[map.dict[key]].name;
			}

			// Token: 0x06001F88 RID: 8072 RVA: 0x0007C480 File Offset: 0x0007A680
			internal bool Proxy(string key, out Socket.ProxyLink link)
			{
				Socket.Map map;
				int index;
				return global::Socket.Map.Reference.Proxy(this.ByKey(key, out map, out index), index, map, out link);
			}

			// Token: 0x06001F89 RID: 8073 RVA: 0x0007C4A0 File Offset: 0x0007A6A0
			internal Socket.ProxyLink Proxy(string key)
			{
				Socket.Map map = this.Map;
				return map.array[map.dict[key]].link;
			}

			// Token: 0x170007D4 RID: 2004
			// (get) Token: 0x06001F8A RID: 8074 RVA: 0x0007C4D0 File Offset: 0x0007A6D0
			public Socket.Map Map
			{
				get
				{
					return global::Socket.Map.Of(ref this.reference);
				}
			}

			// Token: 0x170007D5 RID: 2005
			// (get) Token: 0x06001F8B RID: 8075 RVA: 0x0007C4E0 File Offset: 0x0007A6E0
			public bool Exists
			{
				get
				{
					Socket.Map map;
					return global::Socket.Map.Of(ref this.reference, out map);
				}
			}

			// Token: 0x06001F8C RID: 8076 RVA: 0x0007C4FC File Offset: 0x0007A6FC
			public bool RefEquals(Socket.Map map)
			{
				return object.ReferenceEquals(this.reference, map);
			}

			// Token: 0x06001F8D RID: 8077 RVA: 0x0007C50C File Offset: 0x0007A70C
			public bool Is(Socket.Map map)
			{
				return object.ReferenceEquals(this.Map, map);
			}

			// Token: 0x06001F8E RID: 8078 RVA: 0x0007C51C File Offset: 0x0007A71C
			public bool Socket<TSocket>(int index, out TSocket socket) where TSocket : Socket, new()
			{
				Socket socket2;
				bool flag = this.Socket(index, out socket2);
				socket = ((!flag) ? ((TSocket)((object)null)) : (socket2 as TSocket));
				return flag && socket2 != null;
			}

			// Token: 0x06001F8F RID: 8079 RVA: 0x0007C568 File Offset: 0x0007A768
			public bool Socket<TSocket>(string name, out TSocket socket) where TSocket : Socket, new()
			{
				Socket socket2;
				bool flag = this.Socket(name, out socket2);
				socket = ((!flag) ? ((TSocket)((object)null)) : (socket2 as TSocket));
				return flag && socket2 != null;
			}

			// Token: 0x06001F90 RID: 8080 RVA: 0x0007C5B4 File Offset: 0x0007A7B4
			public TSocket Socket<TSocket>(int index) where TSocket : Socket, new()
			{
				return (TSocket)((object)this.Socket(index));
			}

			// Token: 0x06001F91 RID: 8081 RVA: 0x0007C5C4 File Offset: 0x0007A7C4
			public TSocket Socket<TSocket>(string name) where TSocket : Socket, new()
			{
				return (TSocket)((object)this.Socket(name));
			}

			// Token: 0x06001F92 RID: 8082 RVA: 0x0007C5D4 File Offset: 0x0007A7D4
			public bool SocketIndex(string name, out int index)
			{
				Socket.Map map;
				if (this.Try(out map))
				{
					return map.dict.TryGetValue(name, out index);
				}
				index = -1;
				return false;
			}

			// Token: 0x06001F93 RID: 8083 RVA: 0x0007C600 File Offset: 0x0007A800
			public int SocketIndex(string name)
			{
				return this.Map.dict[name];
			}

			// Token: 0x06001F94 RID: 8084 RVA: 0x0007C614 File Offset: 0x0007A814
			public static implicit operator Socket.Map.Reference(Socket.Map reference)
			{
				return new Socket.Map.Reference(reference);
			}

			// Token: 0x04000F28 RID: 3880
			private Socket.Map reference;
		}
	}
}
