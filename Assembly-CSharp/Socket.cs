using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003D7 RID: 983
public abstract class Socket
{
	// Token: 0x06002259 RID: 8793 RVA: 0x0007F4B8 File Offset: 0x0007D6B8
	protected Socket(bool is_vm)
	{
		this.is_vm = is_vm;
	}

	// Token: 0x1700080F RID: 2063
	// (get) Token: 0x0600225A RID: 8794 RVA: 0x0007F4C8 File Offset: 0x0007D6C8
	public Transform attachParent
	{
		get
		{
			if (this.is_vm)
			{
				return ((global::Socket.CameraSpace)this).attachParent;
			}
			return this.parent;
		}
	}

	// Token: 0x17000810 RID: 2064
	// (get) Token: 0x0600225B RID: 8795 RVA: 0x0007F4E8 File Offset: 0x0007D6E8
	public Vector3 position
	{
		get
		{
			if (this.is_vm)
			{
				return ((global::Socket.CameraSpace)this).position;
			}
			return ((global::Socket.LocalSpace)this).position;
		}
	}

	// Token: 0x17000811 RID: 2065
	// (get) Token: 0x0600225C RID: 8796 RVA: 0x0007F518 File Offset: 0x0007D718
	public Quaternion rotation
	{
		get
		{
			if (this.is_vm)
			{
				return ((global::Socket.CameraSpace)this).rotation;
			}
			return ((global::Socket.LocalSpace)this).rotation;
		}
	}

	// Token: 0x17000812 RID: 2066
	// (get) Token: 0x0600225D RID: 8797 RVA: 0x0007F548 File Offset: 0x0007D748
	public Vector3 localPosition
	{
		get
		{
			return this.offset;
		}
	}

	// Token: 0x17000813 RID: 2067
	// (get) Token: 0x0600225E RID: 8798 RVA: 0x0007F550 File Offset: 0x0007D750
	public Quaternion localRotation
	{
		get
		{
			return this.rotate;
		}
	}

	// Token: 0x0600225F RID: 8799 RVA: 0x0007F558 File Offset: 0x0007D758
	public bool AddChild(Transform transform, bool snap)
	{
		if (this.is_vm)
		{
			return ((global::Socket.CameraSpace)this).AddChild(transform, snap);
		}
		return ((global::Socket.LocalSpace)this).AddChild(transform, snap);
	}

	// Token: 0x06002260 RID: 8800 RVA: 0x0007F58C File Offset: 0x0007D78C
	public bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket)
	{
		if (this.is_vm)
		{
			return ((global::Socket.CameraSpace)this).AddChildWithCoords(transform, offsetFromThisSocket);
		}
		return ((global::Socket.LocalSpace)this).AddChildWithCoords(transform, offsetFromThisSocket);
	}

	// Token: 0x06002261 RID: 8801 RVA: 0x0007F5C0 File Offset: 0x0007D7C0
	public bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket, Vector3 eulerOffsetFromThisSocket)
	{
		if (this.is_vm)
		{
			return ((global::Socket.CameraSpace)this).AddChildWithCoords(transform, offsetFromThisSocket, eulerOffsetFromThisSocket);
		}
		return ((global::Socket.LocalSpace)this).AddChildWithCoords(transform, offsetFromThisSocket, eulerOffsetFromThisSocket);
	}

	// Token: 0x06002262 RID: 8802 RVA: 0x0007F5F8 File Offset: 0x0007D7F8
	public bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket, Quaternion rotationalOffsetFromThisSocket)
	{
		if (this.is_vm)
		{
			return ((global::Socket.CameraSpace)this).AddChildWithCoords(transform, offsetFromThisSocket, rotationalOffsetFromThisSocket);
		}
		return ((global::Socket.LocalSpace)this).AddChildWithCoords(transform, offsetFromThisSocket, rotationalOffsetFromThisSocket);
	}

	// Token: 0x17000814 RID: 2068
	// (get) Token: 0x06002263 RID: 8803 RVA: 0x0007F630 File Offset: 0x0007D830
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

	// Token: 0x06002264 RID: 8804 RVA: 0x0007F688 File Offset: 0x0007D888
	public void Rotate(Quaternion rotation)
	{
		if (this.is_vm)
		{
			((global::Socket.CameraSpace)this).Rotate(rotation);
		}
		else
		{
			((global::Socket.LocalSpace)this).Rotate(rotation);
		}
	}

	// Token: 0x06002265 RID: 8805 RVA: 0x0007F6C0 File Offset: 0x0007D8C0
	public void UnRotate(Quaternion rotation)
	{
		if (this.is_vm)
		{
			((global::Socket.CameraSpace)this).UnRotate(rotation);
		}
		else
		{
			((global::Socket.LocalSpace)this).UnRotate(rotation);
		}
	}

	// Token: 0x06002266 RID: 8806 RVA: 0x0007F6F8 File Offset: 0x0007D8F8
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

	// Token: 0x06002267 RID: 8807 RVA: 0x0007F800 File Offset: 0x0007DA00
	private void AddInstanceChild(Transform tr, bool snap)
	{
		if (!this.AddChild(tr, snap))
		{
			Debug.LogWarning("Could not add child!", tr);
		}
	}

	// Token: 0x06002268 RID: 8808 RVA: 0x0007F81C File Offset: 0x0007DA1C
	public Transform InstantiateAsChild(Transform prefab, bool snap)
	{
		Transform transform = (Transform)Object.Instantiate(prefab, this.position, this.rotation);
		this.AddInstanceChild(transform, snap);
		return transform;
	}

	// Token: 0x06002269 RID: 8809 RVA: 0x0007F84C File Offset: 0x0007DA4C
	public GameObject InstantiateAsChild(GameObject prefab, bool snap)
	{
		GameObject gameObject = (GameObject)Object.Instantiate(prefab, this.position, this.rotation);
		this.AddInstanceChild(gameObject.transform, snap);
		return gameObject;
	}

	// Token: 0x0600226A RID: 8810 RVA: 0x0007F880 File Offset: 0x0007DA80
	public TComponent InstantiateAsChild<TComponent>(TComponent prefab, bool snap) where TComponent : Component
	{
		TComponent result = (TComponent)((object)Object.Instantiate(prefab, this.position, this.rotation));
		this.AddInstanceChild(result.transform, snap);
		return result;
	}

	// Token: 0x0600226B RID: 8811 RVA: 0x0007F8C0 File Offset: 0x0007DAC0
	public TObject Instantiate<TObject>(TObject prefab) where TObject : Object
	{
		return (TObject)((object)Object.Instantiate(prefab, this.position, this.rotation));
	}

	// Token: 0x0600226C RID: 8812 RVA: 0x0007F8E0 File Offset: 0x0007DAE0
	public void Snap()
	{
		if (this.is_vm)
		{
			((global::Socket.CameraSpace)this).Snap();
		}
	}

	// Token: 0x04001054 RID: 4180
	public Transform parent;

	// Token: 0x04001055 RID: 4181
	public Vector3 offset;

	// Token: 0x04001056 RID: 4182
	public Vector3 eulerRotate;

	// Token: 0x04001057 RID: 4183
	private readonly bool is_vm;

	// Token: 0x04001058 RID: 4184
	private Vector3 rotate_last;

	// Token: 0x04001059 RID: 4185
	private Quaternion quat_last;

	// Token: 0x0400105A RID: 4186
	private bool got_last;

	// Token: 0x020003D8 RID: 984
	public struct CameraConversion : IEquatable<global::Socket.CameraConversion>
	{
		// Token: 0x0600226D RID: 8813 RVA: 0x0007F8F8 File Offset: 0x0007DAF8
		public CameraConversion(Transform World, Transform Camera)
		{
			this.Eye = World;
			this.Shelf = Camera;
			this.Provided = (World != Camera && World && Camera);
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x0007F93C File Offset: 0x0007DB3C
		public bool Valid
		{
			get
			{
				return this.Provided && this.Eye && this.Shelf;
			}
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x0007F968 File Offset: 0x0007DB68
		public bool Equals(global::Socket.CameraConversion other)
		{
			return (!this.Provided) ? (!other.Provided) : (other.Provided && this.Eye == other.Eye && this.Shelf == other.Shelf);
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0007F9C8 File Offset: 0x0007DBC8
		public override bool Equals(object obj)
		{
			return obj is global::Socket.CameraConversion && this.Equals((global::Socket.CameraConversion)obj);
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x0007F9E4 File Offset: 0x0007DBE4
		public override string ToString()
		{
			return (!this.Valid) ? ((!this.Provided) ? "[CameraConversion:NotProvided]" : "[CameraConversion:Invalid]") : "[CameraConversion:Valid]";
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x0007FA18 File Offset: 0x0007DC18
		public override int GetHashCode()
		{
			return (!this.Provided) ? 0 : (this.Eye.GetHashCode() ^ this.Shelf.GetHashCode());
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x0007FA50 File Offset: 0x0007DC50
		public static global::Socket.CameraConversion None
		{
			get
			{
				return default(global::Socket.CameraConversion);
			}
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0007FA68 File Offset: 0x0007DC68
		public static implicit operator bool(global::Socket.CameraConversion cc)
		{
			return cc.Valid;
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x0007FA74 File Offset: 0x0007DC74
		public static bool operator true(global::Socket.CameraConversion cc)
		{
			return cc.Valid;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0007FA80 File Offset: 0x0007DC80
		public static bool operator false(global::Socket.CameraConversion cc)
		{
			return !cc.Valid;
		}

		// Token: 0x0400105B RID: 4187
		public readonly Transform Eye;

		// Token: 0x0400105C RID: 4188
		public readonly Transform Shelf;

		// Token: 0x0400105D RID: 4189
		public readonly bool Provided;
	}

	// Token: 0x020003D9 RID: 985
	[Serializable]
	public sealed class CameraSpace : global::Socket
	{
		// Token: 0x06002277 RID: 8823 RVA: 0x0007FA8C File Offset: 0x0007DC8C
		public CameraSpace() : base(true)
		{
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x0007FA98 File Offset: 0x0007DC98
		public static void UpdateProxy(Transform key, Transform value, Transform shelf, Transform eye)
		{
			value.position = eye.TransformPoint(shelf.InverseTransformPoint(key.position));
			Vector3 vector = eye.TransformDirection(shelf.InverseTransformDirection(key.forward));
			Vector3 vector2 = eye.TransformDirection(shelf.InverseTransformDirection(key.up));
			value.rotation = Quaternion.LookRotation(vector, vector2);
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x0007FAF0 File Offset: 0x0007DCF0
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

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x0007FBB4 File Offset: 0x0007DDB4
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

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x0007FC8C File Offset: 0x0007DE8C
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

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x0007FCA8 File Offset: 0x0007DEA8
		public Vector3 preEyePosition
		{
			get
			{
				return (!this.parent) ? this.offset : this.parent.TransformPoint(this.offset);
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x0007FCE4 File Offset: 0x0007DEE4
		public Quaternion preEyeRotation
		{
			get
			{
				return (!this.parent) ? base.rotate : (this.parent.rotation * base.rotate);
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0007FD24 File Offset: 0x0007DF24
		public new void Rotate(Quaternion rotation)
		{
			float num;
			Vector3 vector;
			rotation.ToAngleAxis(ref num, ref vector);
			vector = this.parent.TransformDirection(vector);
			this.parent.RotateAround(this.preEyePosition, vector, num);
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x0007FD5C File Offset: 0x0007DF5C
		public new void UnRotate(Quaternion rotation)
		{
			float num;
			Vector3 vector;
			rotation.ToAngleAxis(ref num, ref vector);
			vector = this.parent.TransformDirection(vector);
			this.parent.RotateAround(this.preEyePosition, -vector, num);
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0007FD9C File Offset: 0x0007DF9C
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

		// Token: 0x06002281 RID: 8833 RVA: 0x0007FED0 File Offset: 0x0007E0D0
		public new bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket)
		{
			if (this.AddChild(transform, false))
			{
				transform.localPosition = this.offset + base.rotate * offsetFromThisSocket;
				return true;
			}
			return false;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0007FF0C File Offset: 0x0007E10C
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

		// Token: 0x06002283 RID: 8835 RVA: 0x0007FF5C File Offset: 0x0007E15C
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

		// Token: 0x06002284 RID: 8836 RVA: 0x0007FFA8 File Offset: 0x0007E1A8
		public new void Snap()
		{
			if (this.proxy && this.proxyTransform && this.root && this.eye)
			{
				global::Socket.CameraSpace.UpdateProxy(this.parent, this.proxyTransform, this.root, this.eye);
			}
		}

		// Token: 0x0400105E RID: 4190
		[NonSerialized]
		public Transform eye;

		// Token: 0x0400105F RID: 4191
		[NonSerialized]
		public Transform root;

		// Token: 0x04001060 RID: 4192
		public bool proxy;

		// Token: 0x04001061 RID: 4193
		[NonSerialized]
		internal Transform proxyTransform;
	}

	// Token: 0x020003DA RID: 986
	[Serializable]
	public sealed class ConfigBodyPart
	{
		// Token: 0x06002286 RID: 8838 RVA: 0x00080018 File Offset: 0x0007E218
		public static global::Socket.ConfigBodyPart Create(BodyPart parent, Vector3 offset, Vector3 eulerRotate)
		{
			return new global::Socket.ConfigBodyPart
			{
				parent = parent,
				offset = offset,
				eulerRotate = eulerRotate
			};
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00080044 File Offset: 0x0007E244
		private bool Find(global::HitBoxSystem system, out Transform parent)
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

		// Token: 0x06002288 RID: 8840 RVA: 0x00080088 File Offset: 0x0007E288
		public bool Extract(ref global::Socket.LocalSpace space, global::HitBoxSystem system)
		{
			Transform transform;
			if (this.Find(system, out transform))
			{
				if (space == null)
				{
					space = new global::Socket.LocalSpace
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

		// Token: 0x06002289 RID: 8841 RVA: 0x00080110 File Offset: 0x0007E310
		public bool Extract(ref global::Socket.CameraSpace space, global::HitBoxSystem system)
		{
			Transform transform;
			if (this.Find(system, out transform))
			{
				if (space == null)
				{
					space = new global::Socket.CameraSpace
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

		// Token: 0x04001062 RID: 4194
		public BodyPart parent;

		// Token: 0x04001063 RID: 4195
		public Vector3 offset;

		// Token: 0x04001064 RID: 4196
		public Vector3 eulerRotate;
	}

	// Token: 0x020003DB RID: 987
	[Serializable]
	public sealed class LocalSpace : global::Socket
	{
		// Token: 0x0600228A RID: 8842 RVA: 0x00080198 File Offset: 0x0007E398
		public LocalSpace() : base(false)
		{
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x000801A4 File Offset: 0x0007E3A4
		public new Vector3 position
		{
			get
			{
				return (!this.parent) ? this.offset : this.parent.TransformPoint(this.offset);
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x000801E0 File Offset: 0x0007E3E0
		public new Quaternion rotation
		{
			get
			{
				return (!this.parent) ? base.rotate : (this.parent.rotation * base.rotate);
			}
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x00080220 File Offset: 0x0007E420
		public new void Rotate(Quaternion rotation)
		{
			float num;
			Vector3 vector;
			rotation.ToAngleAxis(ref num, ref vector);
			vector = this.parent.TransformDirection(vector);
			this.parent.RotateAround(this.position, vector, num);
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x00080258 File Offset: 0x0007E458
		public new void UnRotate(Quaternion rotation)
		{
			float num;
			Vector3 vector;
			rotation.ToAngleAxis(ref num, ref vector);
			vector = this.parent.TransformDirection(vector);
			this.parent.RotateAround(this.position, -vector, num);
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00080298 File Offset: 0x0007E498
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

		// Token: 0x06002290 RID: 8848 RVA: 0x000802E0 File Offset: 0x0007E4E0
		public new bool AddChildWithCoords(Transform transform, Vector3 offsetFromThisSocket)
		{
			if (this.AddChild(transform, false))
			{
				transform.localPosition = this.offset + base.rotate * offsetFromThisSocket;
				return true;
			}
			return false;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0008031C File Offset: 0x0007E51C
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

		// Token: 0x06002292 RID: 8850 RVA: 0x0008036C File Offset: 0x0007E56C
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

		// Token: 0x06002293 RID: 8851 RVA: 0x000803B8 File Offset: 0x0007E5B8
		public new void Snap()
		{
		}
	}

	// Token: 0x020003DC RID: 988
	public interface Source
	{
		// Token: 0x06002294 RID: 8852
		bool GetSocket(string name, out global::Socket socket);

		// Token: 0x06002295 RID: 8853
		bool ReplaceSocket(string name, global::Socket newValue);

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002296 RID: 8854
		IEnumerable<string> SocketNames { get; }

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002297 RID: 8855
		int SocketsVersion { get; }

		// Token: 0x06002298 RID: 8856
		global::Socket.CameraConversion CameraSpaceSetup();

		// Token: 0x06002299 RID: 8857
		Type ProxyScriptType(string name);
	}

	// Token: 0x020003DD RID: 989
	public interface Mapped
	{
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x0600229A RID: 8858
		global::Socket.Map socketMap { get; }
	}

	// Token: 0x020003DE RID: 990
	public interface Provider : global::Socket.Source, global::Socket.Mapped
	{
	}

	// Token: 0x020003DF RID: 991
	public abstract class Proxy : MonoBehaviour, global::Socket.Mapped
	{
		// Token: 0x0600229B RID: 8859 RVA: 0x000803BC File Offset: 0x0007E5BC
		public Proxy()
		{
			this.link = global::Socket.ProxyLink.Pop();
			this.link.proxy = this;
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x000803DC File Offset: 0x0007E5DC
		public Transform transform
		{
			get
			{
				return this._transform;
			}
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x000803E4 File Offset: 0x0007E5E4
		public bool GetSocketMap(out global::Socket.Map map)
		{
			return this.link.map.Try(out map);
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x000803F8 File Offset: 0x0007E5F8
		public global::Socket.Map socketMap
		{
			get
			{
				return this.link.map.Map;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x0008040C File Offset: 0x0007E60C
		public int socketIndex
		{
			get
			{
				return (!this.link.linked || !this.link.map.Exists) ? -1 : this.link.index;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x00080450 File Offset: 0x0007E650
		public global::Socket.CameraSpace socket
		{
			get
			{
				global::Socket.CameraSpace cameraSpace;
				return (!this.link.linked || !this.link.map.Socket<global::Socket.CameraSpace>(this.link.index, out cameraSpace)) ? null : cameraSpace;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x00080498 File Offset: 0x0007E698
		public string socketName
		{
			get
			{
				string text;
				return (!this.link.linked || !this.link.map.Name(this.link.index, out text)) ? null : text;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000804E0 File Offset: 0x0007E6E0
		public bool socketExists
		{
			get
			{
				return this.link.linked && this.link.map.Exists;
			}
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x00080508 File Offset: 0x0007E708
		protected virtual void InitializeProxy()
		{
		}

		// Token: 0x060022A4 RID: 8868 RVA: 0x0008050C File Offset: 0x0007E70C
		protected virtual void UninitializeProxy()
		{
		}

		// Token: 0x060022A5 RID: 8869 RVA: 0x00080510 File Offset: 0x0007E710
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

		// Token: 0x060022A6 RID: 8870 RVA: 0x0008056C File Offset: 0x0007E76C
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
					global::Socket.Map map;
					if (this.GetSocketMap(out map))
					{
						map.OnProxyDestroyed(this.link);
					}
					this.link.proxy = null;
				}
			}
		}

		// Token: 0x04001065 RID: 4197
		[NonSerialized]
		private readonly global::Socket.ProxyLink link;

		// Token: 0x04001066 RID: 4198
		[NonSerialized]
		private Transform _transform;
	}

	// Token: 0x020003E0 RID: 992
	internal sealed class ProxyLink
	{
		// Token: 0x060022A8 RID: 8872 RVA: 0x000805F4 File Offset: 0x0007E7F4
		public static global::Socket.ProxyLink Pop()
		{
			return global::Socket.ProxyLink.Usage.Stack.Pop();
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x00080600 File Offset: 0x0007E800
		public static void Push(global::Socket.ProxyLink top)
		{
			global::Socket.ProxyLink.Usage.Stack.Push(top);
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x00080610 File Offset: 0x0007E810
		public static void EnsurePopped(global::Socket.ProxyLink top)
		{
			if (global::Socket.ProxyLink.Usage.Stack.Count > 0 && global::Socket.ProxyLink.Usage.Stack.Peek() == top)
			{
				global::Socket.ProxyLink.Usage.Stack.Pop();
			}
		}

		// Token: 0x04001067 RID: 4199
		[NonSerialized]
		public global::Socket.Map.Reference map;

		// Token: 0x04001068 RID: 4200
		[NonSerialized]
		public global::Socket.Proxy proxy;

		// Token: 0x04001069 RID: 4201
		[NonSerialized]
		public GameObject gameObject;

		// Token: 0x0400106A RID: 4202
		[NonSerialized]
		public bool scriptAlive;

		// Token: 0x0400106B RID: 4203
		[NonSerialized]
		public bool linked;

		// Token: 0x0400106C RID: 4204
		[NonSerialized]
		public int index = -1;

		// Token: 0x020003E1 RID: 993
		private static class Usage
		{
			// Token: 0x0400106D RID: 4205
			public static readonly Stack<global::Socket.ProxyLink> Stack = new Stack<global::Socket.ProxyLink>();
		}
	}

	// Token: 0x020003E2 RID: 994
	public struct Slot
	{
		// Token: 0x060022AC RID: 8876 RVA: 0x0008064C File Offset: 0x0007E84C
		internal Slot(global::Socket.Map.Reference map, int index)
		{
			this.m = map;
			this.index = index;
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x0008065C File Offset: 0x0007E85C
		// (set) Token: 0x060022AE RID: 8878 RVA: 0x00080670 File Offset: 0x0007E870
		public global::Socket socket
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

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x0008068C File Offset: 0x0007E88C
		public Transform proxy
		{
			get
			{
				global::Socket.ProxyLink proxyLink;
				return (!this.m.Proxy(this.index, out proxyLink) || !proxyLink.proxy) ? null : proxyLink.proxy.transform;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060022B0 RID: 8880 RVA: 0x000806D4 File Offset: 0x0007E8D4
		public string name
		{
			get
			{
				return this.m.Name(this.index);
			}
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000806E8 File Offset: 0x0007E8E8
		public bool BelongsTo(global::Socket.Map map)
		{
			return this.m.Is(map);
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000806F8 File Offset: 0x0007E8F8
		public bool ReplaceSocket(global::Socket newSocketValue)
		{
			global::Socket.Map map;
			return this.m.Try(out map) && map.ReplaceSocket(this.index, newSocketValue);
		}

		// Token: 0x0400106E RID: 4206
		private global::Socket.Map.Reference m;

		// Token: 0x0400106F RID: 4207
		public readonly int index;
	}

	// Token: 0x020003E3 RID: 995
	public sealed class Map : global::Socket.Mapped
	{
		// Token: 0x060022B3 RID: 8883 RVA: 0x00080728 File Offset: 0x0007E928
		private Map(global::Socket.Source source, Object script)
		{
			this.source = source;
			this.script = script;
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x00080740 File Offset: 0x0007E940
		global::Socket.Map global::Socket.Mapped.socketMap
		{
			get
			{
				return this.EnsureMap();
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x00080748 File Offset: 0x0007E948
		public int socketCount
		{
			get
			{
				return (!this.EnsureState()) ? 0 : this.array.Length;
			}
		}

		// Token: 0x1700082C RID: 2092
		public global::Socket.Slot this[int index]
		{
			get
			{
				if (index < 0 || !this.EnsureState() || index >= this.array.Length)
				{
					throw new IndexOutOfRangeException();
				}
				return new global::Socket.Slot(this, index);
			}
		}

		// Token: 0x1700082D RID: 2093
		public global::Socket.Slot this[string name]
		{
			get
			{
				if (!this.EnsureState())
				{
					throw new KeyNotFoundException(name);
				}
				return new global::Socket.Slot(this, this.dict[name]);
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x000807D0 File Offset: 0x0007E9D0
		public global::Socket.CameraConversion cameraConversion
		{
			get
			{
				global::Socket.CameraConversion result;
				this.GetCameraSpace(out result);
				return result;
			}
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000807E8 File Offset: 0x0007E9E8
		public bool ReplaceSocket(string name, global::Socket value)
		{
			int index;
			return this.EnsureState() && this.dict.TryGetValue(name, out index) && this.ValidSlotReplace(index, value);
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00080820 File Offset: 0x0007EA20
		public bool ReplaceSocket(int index, global::Socket value)
		{
			return index >= 0 && (this.EnsureState() && index < this.array.Length) && this.ValidSlotReplace(index, value);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00080850 File Offset: 0x0007EA50
		public bool ReplaceSocket(global::Socket.Slot slot, global::Socket value)
		{
			return slot.index >= 0 && (slot.BelongsTo(this) && slot.index < this.array.Length) && this.ValidSlotReplace(slot.index, value);
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000808A0 File Offset: 0x0007EAA0
		public void SnapProxies()
		{
			if (this.EnsureState())
			{
				global::Socket.CameraConversion cameraConversion;
				bool flag = this.GetCameraSpace(out cameraConversion);
				for (int i = 0; i < this.array.Length; i++)
				{
					if (this.array[i].madeLink && this.array[i].link.scriptAlive && this.array[i].link.linked)
					{
						try
						{
							global::Socket.CameraSpace cameraSpace = (global::Socket.CameraSpace)this.array[i].socket;
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

		// Token: 0x060022BD RID: 8893 RVA: 0x000809CC File Offset: 0x0007EBCC
		private static bool Of(ref global::Socket.Map member, out global::Socket.Map value)
		{
			if (object.ReferenceEquals(member, null))
			{
				value = null;
				return false;
			}
			global::Socket.Map map = member.EnsureMap();
			member = map;
			value = map;
			return !object.ReferenceEquals(map, null);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x00080A04 File Offset: 0x0007EC04
		internal static global::Socket.Map Of(ref global::Socket.Map member)
		{
			global::Socket.Map result;
			global::Socket.Map.Of(ref member, out result);
			return result;
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x00080A1C File Offset: 0x0007EC1C
		private static global::Socket.Map Get<TSource>(TSource source, ref global::Socket.Map member) where TSource : Object, global::Socket.Source
		{
			if (object.ReferenceEquals(source, null))
			{
				throw new ArgumentNullException("source");
			}
			if (!source)
			{
				return global::Socket.Map.NullMap;
			}
			global::Socket.Map map = member;
			if (object.ReferenceEquals(map, null))
			{
				map = new global::Socket.Map(source, source);
			}
			global::Socket.Map result;
			member = (result = map.EnsureMap());
			return result;
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x00080A88 File Offset: 0x0007EC88
		private void CleanTransforms()
		{
			this.checkTransforms = true;
			this.cameraSpace = global::Socket.CameraConversion.None;
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x00080A9C File Offset: 0x0007EC9C
		private global::Socket.Map EnsureMap()
		{
			return (!this.EnsureState()) ? global::Socket.Map.NullMap : this;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x00080AB4 File Offset: 0x0007ECB4
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

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00080B24 File Offset: 0x0007ED24
		private static global::Socket.Map NullMap
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x00080B28 File Offset: 0x0007ED28
		private global::Socket.Map.Result SecureState()
		{
			global::Socket.Map.Result result = this.PollState();
			switch (result)
			{
			case global::Socket.Map.Result.Initialized:
				this.initialized = true;
				this.CleanTransforms();
				break;
			case global::Socket.Map.Result.Version:
				this.CleanTransforms();
				break;
			case global::Socket.Map.Result.Forced:
				break;
			default:
				return result;
			}
			this.OnState(result);
			return result;
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x00080B84 File Offset: 0x0007ED84
		private global::Socket.Map.Result PollState()
		{
			if (!this.initialized)
			{
				this.Initialize();
				return global::Socket.Map.Result.Initialized;
			}
			int socketsVersion = this.source.SocketsVersion;
			global::Socket.Map.Result result;
			if (this.version != socketsVersion)
			{
				this.version = socketsVersion;
				result = global::Socket.Map.Result.Version;
			}
			else
			{
				if (!this.forceUpdate)
				{
					return global::Socket.Map.Result.Nothing;
				}
				result = global::Socket.Map.Result.Forced;
			}
			this.forceUpdate = false;
			this.Update(result);
			return result;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x00080BF0 File Offset: 0x0007EDF0
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
			this.array = new global::Socket.Map.Element[count];
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
			Array.Resize<global::Socket.Map.Element>(ref this.array, num);
			this.version = this.source.SocketsVersion;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x00080D58 File Offset: 0x0007EF58
		private void ElementUpdate(int srcIndex, ref global::Socket.Map.Element src, int dstIndex, ref global::Socket.Map.Element dst, global::Socket newSocket)
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

		// Token: 0x060022C8 RID: 8904 RVA: 0x00080DDC File Offset: 0x0007EFDC
		private void SocketUpdate(ref global::Socket socket, global::Socket newSocket)
		{
			global::Socket socket2 = socket;
			if (!object.ReferenceEquals(socket2, newSocket))
			{
				socket = newSocket;
				if (socket2 is global::Socket.CameraSpace && newSocket is global::Socket.CameraSpace)
				{
					global::Socket.CameraSpace cameraSpace = (global::Socket.CameraSpace)socket2;
					global::Socket.CameraSpace cameraSpace2 = (global::Socket.CameraSpace)newSocket;
					cameraSpace2.root = cameraSpace.root;
					cameraSpace2.eye = cameraSpace.eye;
					cameraSpace2.proxyTransform = cameraSpace.proxyTransform;
				}
			}
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x00080E44 File Offset: 0x0007F044
		private void ElementRemove(ref global::Socket.Map.Element element, ref global::Socket.Map.RemoveList<global::Socket.ProxyLink> removeList)
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

		// Token: 0x060022CA RID: 8906 RVA: 0x00080E98 File Offset: 0x0007F098
		private void Update(global::Socket.Map.Result Because)
		{
			if (Because == global::Socket.Map.Result.Version)
			{
				this.CleanTransforms();
			}
			int newSize = 0;
			global::Socket.Map.RemoveList<global::Socket.ProxyLink> removeList = default(global::Socket.Map.RemoveList<global::Socket.ProxyLink>);
			for (int i = 0; i < this.array.Length; i++)
			{
				global::Socket newSocket;
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
			Array.Resize<global::Socket.Map.Element>(ref this.array, newSize);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x00080F4C File Offset: 0x0007F14C
		private void OnState(global::Socket.Map.Result State)
		{
			bool flag = false;
			global::Socket.CameraConversion cameraConversion = default(global::Socket.CameraConversion);
			for (int i = 0; i < this.array.Length; i++)
			{
				global::Socket.Map.ProxyCheck proxyCheck;
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

		// Token: 0x060022CC RID: 8908 RVA: 0x00080FE8 File Offset: 0x0007F1E8
		private bool ValidSlotReplace(int index, global::Socket value)
		{
			global::Socket socket = this.array[index].socket;
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

		// Token: 0x060022CD RID: 8909 RVA: 0x00081064 File Offset: 0x0007F264
		private bool GetCameraSpace(out global::Socket.CameraConversion cameraSpace)
		{
			if (!this.EnsureState())
			{
				this.checkTransforms = false;
				this.cameraSpace = global::Socket.CameraConversion.None;
			}
			else if (this.checkTransforms)
			{
				this.checkTransforms = false;
				this.cameraSpace = this.source.CameraSpaceSetup();
			}
			cameraSpace = this.cameraSpace;
			return cameraSpace.Valid;
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000810C8 File Offset: 0x0007F2C8
		private void DestroyProxyLink(global::Socket.ProxyLink link)
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

		// Token: 0x060022CF RID: 8911 RVA: 0x00081140 File Offset: 0x0007F340
		internal void OnProxyDestroyed(object link)
		{
			this.DestroyProxyLink((global::Socket.ProxyLink)link);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x00081150 File Offset: 0x0007F350
		private global::Socket.ProxyLink MakeProxy(global::Socket.CameraSpace socket, int index)
		{
			Type type = this.source.ProxyScriptType(this.array[index].name);
			if (object.ReferenceEquals(type, null))
			{
				return null;
			}
			if (!typeof(global::Socket.Proxy).IsAssignableFrom(type))
			{
				throw new InvalidProgramException("SocketSource returned a type that did not extend SocketMap.Proxy");
			}
			global::Socket.ProxyLink proxyLink = new global::Socket.ProxyLink();
			proxyLink.map = this;
			proxyLink.index = index;
			global::Socket.ProxyLink.Push(proxyLink);
			Vector3 position = Vector3.zero;
			Quaternion rotation = Quaternion.identity;
			global::Socket.CameraConversion cameraConversion;
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
				global::Socket.ProxyLink.EnsurePopped(proxyLink);
			}
			proxyLink.linked = true;
			socket.proxyTransform = proxyLink.proxy.transform;
			return proxyLink;
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x00081314 File Offset: 0x0007F514
		private void CheckProxyIndex(int index, out global::Socket.Map.ProxyCheck o)
		{
			if (o.isProxy = ((o.isCameraSpace = !object.ReferenceEquals(o.cameraSpace = (this.array[index].socket as global::Socket.CameraSpace), null)) && o.cameraSpace.proxy))
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

		// Token: 0x060022D2 RID: 8914 RVA: 0x00081424 File Offset: 0x0007F624
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

		// Token: 0x04001070 RID: 4208
		[NonSerialized]
		private readonly Object script;

		// Token: 0x04001071 RID: 4209
		[NonSerialized]
		private readonly global::Socket.Source source;

		// Token: 0x04001072 RID: 4210
		[NonSerialized]
		private Dictionary<string, int> dict;

		// Token: 0x04001073 RID: 4211
		[NonSerialized]
		private bool initialized;

		// Token: 0x04001074 RID: 4212
		[NonSerialized]
		private bool checkTransforms;

		// Token: 0x04001075 RID: 4213
		[NonSerialized]
		private bool securing;

		// Token: 0x04001076 RID: 4214
		[NonSerialized]
		private bool forceUpdate;

		// Token: 0x04001077 RID: 4215
		[NonSerialized]
		private bool deleted;

		// Token: 0x04001078 RID: 4216
		[NonSerialized]
		private global::Socket.Map.Element[] array;

		// Token: 0x04001079 RID: 4217
		[NonSerialized]
		private int version;

		// Token: 0x0400107A RID: 4218
		[NonSerialized]
		private global::Socket.CameraConversion cameraSpace;

		// Token: 0x020003E4 RID: 996
		public struct Member
		{
			// Token: 0x060022D3 RID: 8915 RVA: 0x00081498 File Offset: 0x0007F698
			public global::Socket.Map Get<T>(T outerInstance) where T : Object, global::Socket.Source
			{
				if (this.deleted)
				{
					return null;
				}
				return global::Socket.Map.Get<T>(outerInstance, ref this.reference);
			}

			// Token: 0x060022D4 RID: 8916 RVA: 0x000814B4 File Offset: 0x0007F6B4
			public bool Get<T>(T outerInstance, out global::Socket.Map map) where T : Object, global::Socket.Source
			{
				map = this.Get<T>(outerInstance);
				return !object.ReferenceEquals(map, null);
			}

			// Token: 0x060022D5 RID: 8917 RVA: 0x000814CC File Offset: 0x0007F6CC
			public bool DeleteBy<T>(T outerInstance) where T : Object, global::Socket.Source
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

			// Token: 0x0400107B RID: 4219
			private global::Socket.Map reference;

			// Token: 0x0400107C RID: 4220
			private bool deleted;
		}

		// Token: 0x020003E5 RID: 997
		private struct Element
		{
			// Token: 0x0400107D RID: 4221
			public global::Socket socket;

			// Token: 0x0400107E RID: 4222
			public string name;

			// Token: 0x0400107F RID: 4223
			public global::Socket.ProxyLink link;

			// Token: 0x04001080 RID: 4224
			public bool madeLink;
		}

		// Token: 0x020003E6 RID: 998
		private enum Result
		{
			// Token: 0x04001082 RID: 4226
			Nothing,
			// Token: 0x04001083 RID: 4227
			Initialized,
			// Token: 0x04001084 RID: 4228
			Version,
			// Token: 0x04001085 RID: 4229
			Forced
		}

		// Token: 0x020003E7 RID: 999
		private struct RemoveList<T>
		{
			// Token: 0x060022D6 RID: 8918 RVA: 0x00081594 File Offset: 0x0007F794
			public void Add(T item)
			{
				if (!this.exists)
				{
					this.list = new List<T>();
				}
				this.list.Add(item);
			}

			// Token: 0x04001086 RID: 4230
			public bool exists;

			// Token: 0x04001087 RID: 4231
			public List<T> list;
		}

		// Token: 0x020003E8 RID: 1000
		private struct ProxyCheck
		{
			// Token: 0x17000830 RID: 2096
			// (get) Token: 0x060022D7 RID: 8919 RVA: 0x000815C4 File Offset: 0x0007F7C4
			public Transform proxyTransform
			{
				get
				{
					return (!this.isProxy) ? null : this.parentOrProxy;
				}
			}

			// Token: 0x17000831 RID: 2097
			// (get) Token: 0x060022D8 RID: 8920 RVA: 0x000815E0 File Offset: 0x0007F7E0
			public Transform parent
			{
				get
				{
					return (!this.isProxy) ? this.parentOrProxy : this.cameraSpace.parent;
				}
			}

			// Token: 0x04001088 RID: 4232
			public Transform parentOrProxy;

			// Token: 0x04001089 RID: 4233
			public global::Socket.CameraSpace cameraSpace;

			// Token: 0x0400108A RID: 4234
			public global::Socket.ProxyLink proxyLink;

			// Token: 0x0400108B RID: 4235
			public int index;

			// Token: 0x0400108C RID: 4236
			public bool isCameraSpace;

			// Token: 0x0400108D RID: 4237
			public bool isProxy;
		}

		// Token: 0x020003E9 RID: 1001
		internal struct Reference
		{
			// Token: 0x060022D9 RID: 8921 RVA: 0x00081604 File Offset: 0x0007F804
			private Reference(global::Socket.Map reference)
			{
				this.reference = reference;
			}

			// Token: 0x060022DA RID: 8922 RVA: 0x00081610 File Offset: 0x0007F810
			public bool Try(out global::Socket.Map map)
			{
				return global::Socket.Map.Of(ref this.reference, out map);
			}

			// Token: 0x060022DB RID: 8923 RVA: 0x00081620 File Offset: 0x0007F820
			private bool ByIndex(int index, out global::Socket.Map map)
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

			// Token: 0x060022DC RID: 8924 RVA: 0x00081650 File Offset: 0x0007F850
			private bool ByKey(string name, out global::Socket.Map map, out int index)
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

			// Token: 0x060022DD RID: 8925 RVA: 0x00081688 File Offset: 0x0007F888
			private static bool Socket(bool valid, int index, global::Socket.Map map, out global::Socket socket)
			{
				if (valid)
				{
					socket = map.array[index].socket;
					return true;
				}
				socket = null;
				return false;
			}

			// Token: 0x060022DE RID: 8926 RVA: 0x000816AC File Offset: 0x0007F8AC
			private static bool Name(bool valid, int index, global::Socket.Map map, out string name)
			{
				if (valid)
				{
					name = map.array[index].name;
					return true;
				}
				name = null;
				return false;
			}

			// Token: 0x060022DF RID: 8927 RVA: 0x000816D0 File Offset: 0x0007F8D0
			private static bool Proxy(bool valid, int index, global::Socket.Map map, out global::Socket.ProxyLink proxyLink)
			{
				if (valid)
				{
					proxyLink = map.array[index].link;
					return map.array[index].madeLink;
				}
				proxyLink = null;
				return false;
			}

			// Token: 0x060022E0 RID: 8928 RVA: 0x00081704 File Offset: 0x0007F904
			public bool Socket(int index, out global::Socket socket)
			{
				global::Socket.Map map;
				return global::Socket.Map.Reference.Socket(this.ByIndex(index, out map), index, map, out socket);
			}

			// Token: 0x060022E1 RID: 8929 RVA: 0x00081724 File Offset: 0x0007F924
			public global::Socket Socket(int index)
			{
				global::Socket.Map map = this.Map;
				return map.array[index].socket;
			}

			// Token: 0x060022E2 RID: 8930 RVA: 0x0008174C File Offset: 0x0007F94C
			public bool Name(int index, out string name)
			{
				global::Socket.Map map;
				return global::Socket.Map.Reference.Name(this.ByIndex(index, out map), index, map, out name);
			}

			// Token: 0x060022E3 RID: 8931 RVA: 0x0008176C File Offset: 0x0007F96C
			public string Name(int index)
			{
				global::Socket.Map map = this.Map;
				return map.array[index].name;
			}

			// Token: 0x060022E4 RID: 8932 RVA: 0x00081794 File Offset: 0x0007F994
			public bool Proxy(int index, out global::Socket.ProxyLink link)
			{
				global::Socket.Map map;
				return global::Socket.Map.Reference.Proxy(this.ByIndex(index, out map), index, map, out link);
			}

			// Token: 0x060022E5 RID: 8933 RVA: 0x000817B4 File Offset: 0x0007F9B4
			internal global::Socket.ProxyLink Proxy(int index)
			{
				global::Socket.Map map = this.Map;
				return map.array[index].link;
			}

			// Token: 0x060022E6 RID: 8934 RVA: 0x000817DC File Offset: 0x0007F9DC
			public bool Socket(string key, out global::Socket socket)
			{
				global::Socket.Map map;
				int index;
				return global::Socket.Map.Reference.Socket(this.ByKey(key, out map, out index), index, map, out socket);
			}

			// Token: 0x060022E7 RID: 8935 RVA: 0x000817FC File Offset: 0x0007F9FC
			public global::Socket Socket(string key)
			{
				global::Socket.Map map = this.Map;
				return map.array[map.dict[key]].socket;
			}

			// Token: 0x060022E8 RID: 8936 RVA: 0x0008182C File Offset: 0x0007FA2C
			public bool Name(string key, out string name)
			{
				global::Socket.Map map;
				int index;
				return global::Socket.Map.Reference.Name(this.ByKey(key, out map, out index), index, map, out name);
			}

			// Token: 0x060022E9 RID: 8937 RVA: 0x0008184C File Offset: 0x0007FA4C
			public string Name(string key)
			{
				global::Socket.Map map = this.Map;
				return map.array[map.dict[key]].name;
			}

			// Token: 0x060022EA RID: 8938 RVA: 0x0008187C File Offset: 0x0007FA7C
			internal bool Proxy(string key, out global::Socket.ProxyLink link)
			{
				global::Socket.Map map;
				int index;
				return global::Socket.Map.Reference.Proxy(this.ByKey(key, out map, out index), index, map, out link);
			}

			// Token: 0x060022EB RID: 8939 RVA: 0x0008189C File Offset: 0x0007FA9C
			internal global::Socket.ProxyLink Proxy(string key)
			{
				global::Socket.Map map = this.Map;
				return map.array[map.dict[key]].link;
			}

			// Token: 0x17000832 RID: 2098
			// (get) Token: 0x060022EC RID: 8940 RVA: 0x000818CC File Offset: 0x0007FACC
			public global::Socket.Map Map
			{
				get
				{
					return global::Socket.Map.Of(ref this.reference);
				}
			}

			// Token: 0x17000833 RID: 2099
			// (get) Token: 0x060022ED RID: 8941 RVA: 0x000818DC File Offset: 0x0007FADC
			public bool Exists
			{
				get
				{
					global::Socket.Map map;
					return global::Socket.Map.Of(ref this.reference, out map);
				}
			}

			// Token: 0x060022EE RID: 8942 RVA: 0x000818F8 File Offset: 0x0007FAF8
			public bool RefEquals(global::Socket.Map map)
			{
				return object.ReferenceEquals(this.reference, map);
			}

			// Token: 0x060022EF RID: 8943 RVA: 0x00081908 File Offset: 0x0007FB08
			public bool Is(global::Socket.Map map)
			{
				return object.ReferenceEquals(this.Map, map);
			}

			// Token: 0x060022F0 RID: 8944 RVA: 0x00081918 File Offset: 0x0007FB18
			public bool Socket<TSocket>(int index, out TSocket socket) where TSocket : global::Socket, new()
			{
				global::Socket socket2;
				bool flag = this.Socket(index, out socket2);
				socket = ((!flag) ? ((TSocket)((object)null)) : (socket2 as TSocket));
				return flag && socket2 != null;
			}

			// Token: 0x060022F1 RID: 8945 RVA: 0x00081964 File Offset: 0x0007FB64
			public bool Socket<TSocket>(string name, out TSocket socket) where TSocket : global::Socket, new()
			{
				global::Socket socket2;
				bool flag = this.Socket(name, out socket2);
				socket = ((!flag) ? ((TSocket)((object)null)) : (socket2 as TSocket));
				return flag && socket2 != null;
			}

			// Token: 0x060022F2 RID: 8946 RVA: 0x000819B0 File Offset: 0x0007FBB0
			public TSocket Socket<TSocket>(int index) where TSocket : global::Socket, new()
			{
				return (TSocket)((object)this.Socket(index));
			}

			// Token: 0x060022F3 RID: 8947 RVA: 0x000819C0 File Offset: 0x0007FBC0
			public TSocket Socket<TSocket>(string name) where TSocket : global::Socket, new()
			{
				return (TSocket)((object)this.Socket(name));
			}

			// Token: 0x060022F4 RID: 8948 RVA: 0x000819D0 File Offset: 0x0007FBD0
			public bool SocketIndex(string name, out int index)
			{
				global::Socket.Map map;
				if (this.Try(out map))
				{
					return map.dict.TryGetValue(name, out index);
				}
				index = -1;
				return false;
			}

			// Token: 0x060022F5 RID: 8949 RVA: 0x000819FC File Offset: 0x0007FBFC
			public int SocketIndex(string name)
			{
				return this.Map.dict[name];
			}

			// Token: 0x060022F6 RID: 8950 RVA: 0x00081A10 File Offset: 0x0007FC10
			public static implicit operator global::Socket.Map.Reference(global::Socket.Map reference)
			{
				return new global::Socket.Map.Reference(reference);
			}

			// Token: 0x0400108E RID: 4238
			private global::Socket.Map reference;
		}
	}
}
