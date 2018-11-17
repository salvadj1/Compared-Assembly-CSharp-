using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class AuthorChHit : global::AuthorPeice
{
	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060001B6 RID: 438 RVA: 0x00006C70 File Offset: 0x00004E70
	// (set) Token: 0x060001B7 RID: 439 RVA: 0x00006D0C File Offset: 0x00004F0C
	public global::AuthorChHit.Rep primary
	{
		get
		{
			global::AuthorChHit.Rep result;
			result.hit = this;
			result.bone = this.bone;
			result.mirrored = false;
			result.flipX = (result.flipY = (result.flipZ = false));
			result.center = this.center;
			result.size = this.size;
			result.radius = this.radius;
			result.height = this.height;
			result.capsuleAxis = this.capsuleAxis;
			result.valid = result.bone;
			return result;
		}
		set
		{
			this.bone = value.bone;
			this.center = value.center;
			this.size = value.size;
			this.radius = value.radius;
			this.height = value.height;
			this.capsuleAxis = value.capsuleAxis;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060001B8 RID: 440 RVA: 0x00006D68 File Offset: 0x00004F68
	// (set) Token: 0x060001B9 RID: 441 RVA: 0x00006E2C File Offset: 0x0000502C
	public global::AuthorChHit.Rep secondary
	{
		get
		{
			global::AuthorChHit.Rep result;
			result.hit = this;
			result.bone = ((!(this.mirrored == this.bone)) ? this.mirrored : null);
			result.mirrored = true;
			result.flipX = this.mirrorX;
			result.flipY = this.mirrorY;
			result.flipZ = this.mirrorZ;
			result.center = this.GetCenter(true);
			result.size = this.size;
			result.radius = this.radius;
			result.height = this.height;
			result.capsuleAxis = this.capsuleAxis;
			result.valid = result.bone;
			return result;
		}
		set
		{
			this.mirrored = value.bone;
			this.center = value.Flip(value.center);
			this.size = value.size;
			this.radius = value.radius;
			this.height = value.height;
			this.capsuleAxis = value.capsuleAxis;
		}
	}

	// Token: 0x060001BA RID: 442 RVA: 0x00006E90 File Offset: 0x00005090
	private Vector3 GetCenter(bool mirrored)
	{
		if (mirrored)
		{
			return new Vector3((!this.mirrorX) ? this.center.x : (-this.center.x), (!this.mirrorY) ? this.center.y : (-this.center.y), (!this.mirrorZ) ? this.center.z : (-this.center.z));
		}
		return this.center;
	}

	// Token: 0x060001BB RID: 443 RVA: 0x00006F24 File Offset: 0x00005124
	private bool DoTransformHandles(Transform bone, ref Vector3 center, ref Vector3 size, ref float radius, ref float height, ref int capsuleAxis)
	{
		Matrix4x4 matrix = global::AuthorShared.Scene.matrix;
		if (bone)
		{
			global::AuthorShared.Scene.matrix = bone.transform.localToWorldMatrix;
		}
		bool flag = false;
		switch (this.kind)
		{
		case 1:
			flag |= global::AuthorShared.Scene.SphereDrag(ref center, ref radius);
			break;
		case 2:
			flag |= global::AuthorShared.Scene.CapsuleDrag(ref center, ref radius, ref height, ref capsuleAxis);
			break;
		case 4:
			flag |= global::AuthorShared.Scene.BoxDrag(ref center, ref size);
			break;
		}
		global::AuthorShared.Scene.matrix = matrix;
		return flag;
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00006FB4 File Offset: 0x000051B4
	public override bool OnSceneView()
	{
		bool flag = base.OnSceneView();
		flag |= this.DoTransformHandles(this.bone, ref this.center, ref this.size, ref this.radius, ref this.height, ref this.capsuleAxis);
		if (this.mirrored && this.mirrored != this.bone)
		{
			Vector3 vector;
			vector.x = ((!this.mirrorX) ? this.center.x : (-this.center.x));
			vector.y = ((!this.mirrorY) ? this.center.y : (-this.center.y));
			vector.z = ((!this.mirrorZ) ? this.center.z : (-this.center.z));
			if (this.DoTransformHandles(this.mirrored, ref vector, ref this.size, ref this.radius, ref this.height, ref this.capsuleAxis))
			{
				this.center.x = ((!this.mirrorX) ? vector.x : (-vector.x));
				this.center.y = ((!this.mirrorY) ? vector.y : (-vector.y));
				this.center.z = ((!this.mirrorZ) ? vector.z : (-vector.z));
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00007154 File Offset: 0x00005354
	protected override void OnRegistered()
	{
		string peiceID = base.peiceID;
		switch (peiceID)
		{
		case "Sphere":
			this.kind = 1;
			break;
		case "Box":
			this.kind = 4;
			break;
		case "Capsule":
			this.kind = 2;
			break;
		}
		base.OnRegistered();
	}

	// Token: 0x060001BE RID: 446 RVA: 0x000071FC File Offset: 0x000053FC
	private void AddHingeJoint()
	{
		this.AddJoint(global::AuthorChJoint.Kind.Hinge);
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00007208 File Offset: 0x00005408
	private void AddCharacterJoint()
	{
		this.AddJoint(global::AuthorChJoint.Kind.Character);
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x00007214 File Offset: 0x00005414
	private void AddSpringJoint()
	{
		this.AddJoint(global::AuthorChJoint.Kind.Spring);
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00007220 File Offset: 0x00005420
	private void AddFixedJoint()
	{
		this.AddJoint(global::AuthorChJoint.Kind.Fixed);
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000722C File Offset: 0x0000542C
	private global::AuthorChJoint AddJoint(global::AuthorChJoint.Kind kind)
	{
		global::AuthorChJoint authorChJoint = base.creation.CreatePeice<global::AuthorChJoint>(kind.ToString(), new Type[0]);
		if (authorChJoint)
		{
			authorChJoint.InitializeFromOwner(this, kind);
			Array.Resize<global::AuthorChJoint>(ref this.myJoints, (this.myJoints != null) ? (this.myJoints.Length + 1) : 1);
			this.myJoints[this.myJoints.Length - 1] = authorChJoint;
		}
		return authorChJoint;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x000072A4 File Offset: 0x000054A4
	private void FigureOutDefaultBodyPart(Transform bone, ref BodyPart part)
	{
		if (part == 0)
		{
			global::AuthorHull authorHull = base.creation as global::AuthorHull;
			if (authorHull)
			{
				authorHull.FigureOutDefaultBodyPart(ref bone, ref part, ref this.mirrored, ref this.mirroredBodyPart);
				Debug.Log(string.Format("[{0}:{1}][{2}:{3}]", new object[]
				{
					bone,
					part,
					this.mirrored,
					this.mirroredBodyPart
				}), this);
			}
		}
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x00007320 File Offset: 0x00005520
	public override bool PeiceInspectorGUI()
	{
		bool flag = base.PeiceInspectorGUI();
		string peiceID = base.peiceID;
		if (global::AuthorShared.StringField("Title", ref peiceID, new GUILayoutOption[0]))
		{
			base.peiceID = peiceID;
			flag = true;
		}
		bool flag2 = this.mirrored && this.mirrored != this.bone;
		bool flag3 = this.bone;
		BodyPart bodyPart = this.bodyPart;
		if (global::AuthorShared.ObjectField<Transform>("Bone", ref this.bone, (global::AuthorShared.ObjectFieldFlags)25, new GUILayoutOption[0]))
		{
			if (!flag3)
			{
				this.FigureOutDefaultBodyPart(this.bone, ref bodyPart);
			}
			flag = true;
		}
		BodyPart bodyPart2 = this.mirroredBodyPart;
		if (flag3)
		{
			bodyPart = (BodyPart)global::AuthorShared.EnumField("Body Part", bodyPart, new GUILayoutOption[0]);
		}
		GUI.Box(global::AuthorShared.BeginVertical(new GUILayoutOption[0]), GUIContent.none);
		flag |= global::AuthorShared.ObjectField<Transform>("Mirrored Bone", ref this.mirrored, (global::AuthorShared.ObjectFieldFlags)25, new GUILayoutOption[0]);
		if (flag2)
		{
			bodyPart2 = (BodyPart)global::AuthorShared.EnumField("Body Part", bodyPart2, new GUILayoutOption[0]);
			global::AuthorShared.BeginHorizontal(new GUILayoutOption[0]);
			bool flag4 = GUILayout.Toggle(this.mirrorX, "Mirror X", new GUILayoutOption[0]);
			bool flag5 = GUILayout.Toggle(this.mirrorY, "Mirror Y", new GUILayoutOption[0]);
			bool flag6 = GUILayout.Toggle(this.mirrorZ, "Mirror Z", new GUILayoutOption[0]);
			global::AuthorShared.EndHorizontal();
			if (flag4 != this.mirrorX || flag5 != this.mirrorY || flag6 != this.mirrorZ)
			{
				this.mirrorX = flag4;
				this.mirrorY = flag5;
				this.mirrorZ = flag6;
				flag = true;
			}
		}
		global::AuthorShared.EndVertical();
		Vector3 vector = this.center;
		float num = this.radius;
		float num2 = this.height;
		Vector3 vector2 = this.size;
		int num3 = this.capsuleAxis;
		global::AuthorShared.BeginSubSection("Shape", new GUILayoutOption[0]);
		HitShapeKind hitShapeKind = (HitShapeKind)global::AuthorShared.EnumField("Kind", this.kind, new GUILayoutOption[0]);
		switch (this.kind)
		{
		case 1:
			vector = global::AuthorShared.Vector3Field("Center", this.center, new GUILayoutOption[0]);
			num = Mathf.Max(global::AuthorShared.FloatField("Radius", this.radius, new GUILayoutOption[0]), 0.001f);
			break;
		case 2:
			vector = global::AuthorShared.Vector3Field("Center", this.center, new GUILayoutOption[0]);
			num = Mathf.Max(global::AuthorShared.FloatField("Radius", this.radius, new GUILayoutOption[0]), 0.001f);
			num2 = Mathf.Max(global::AuthorShared.FloatField("Height", this.height, new GUILayoutOption[0]), 0.001f);
			num3 = Mathf.Clamp(global::AuthorShared.IntField("Height Axis", this.capsuleAxis, new GUILayoutOption[0]), 0, 2);
			break;
		case 4:
			vector = global::AuthorShared.Vector3Field("Center", this.center, new GUILayoutOption[0]);
			vector2 = global::AuthorShared.Vector3Field("Size", this.size, new GUILayoutOption[0]);
			break;
		}
		global::AuthorShared.EndSubSection();
		global::AuthorShared.BeginSubSection("Rigidbody", new GUILayoutOption[0]);
		float num4 = Mathf.Max(global::AuthorShared.FloatField("Mass", this.mass, new GUILayoutOption[0]), 0.001f);
		float num5 = Mathf.Max(global::AuthorShared.FloatField("Drag", this.drag, new GUILayoutOption[0]), 0f);
		float num6 = Mathf.Max(global::AuthorShared.FloatField("Angular Drag", this.angularDrag, new GUILayoutOption[0]), 0f);
		global::AuthorShared.EndSubSection();
		global::AuthorShared.BeginSubSection("Hit Box", new GUILayoutOption[0]);
		int num7 = this.hitPriority;
		float num8 = this.damageMultiplier;
		if (flag2 || flag3)
		{
			num7 = global::AuthorShared.IntField("Hit Priority", num7, new GUILayoutOption[0]);
			num8 = global::AuthorShared.FloatField("Damage Mult.", num8, new GUILayoutOption[0]);
		}
		global::AuthorShared.EndSubSection();
		bool flag7 = GUILayout.Button("Add Joint", new GUILayoutOption[0]);
		if (Event.current.type == 7)
		{
			this.lastPopupRect = GUILayoutUtility.GetLastRect();
		}
		if (flag7)
		{
			global::AuthorShared.CustomMenu(this.lastPopupRect, global::AuthorChHit.JointMenu.options, 0, new global::AuthorShared.CustomMenuProc(global::AuthorChHit.JointMenu.Callback), this);
		}
		if (hitShapeKind != this.kind || vector != this.center || vector2 != this.size || num != this.radius || num2 != this.height || num3 != this.capsuleAxis || num4 != this.mass || num5 != this.drag || num6 != this.angularDrag || bodyPart != this.bodyPart || bodyPart2 != this.mirroredBodyPart || this.hitPriority != num7 || num8 != this.damageMultiplier)
		{
			flag = true;
			this.kind = hitShapeKind;
			this.center = vector;
			this.size = vector2;
			this.radius = num;
			this.height = num2;
			this.capsuleAxis = num3;
			this.mass = num4;
			this.drag = num5;
			this.angularDrag = num6;
			this.bodyPart = bodyPart;
			this.mirroredBodyPart = bodyPart2;
			this.hitPriority = num7;
			this.damageMultiplier = num8;
		}
		return flag;
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00007914 File Offset: 0x00005B14
	private Collider CreateColliderOn(Transform instanceRoot, Transform root, Transform bone, bool mirrored)
	{
		if (!bone)
		{
			throw new ArgumentException("there was no bone");
		}
		string text = global::AuthorShared.CalculatePath(bone, root);
		Transform transform = instanceRoot.FindChild(text);
		if (!transform)
		{
			throw new MissingReferenceException(text);
		}
		switch (this.kind)
		{
		case 1:
		{
			SphereCollider sphereCollider = transform.gameObject.AddComponent<SphereCollider>();
			sphereCollider.center = this.GetCenter(mirrored);
			sphereCollider.radius = this.radius;
			goto IL_F7;
		}
		case 2:
		{
			CapsuleCollider capsuleCollider = transform.gameObject.AddComponent<CapsuleCollider>();
			capsuleCollider.center = this.GetCenter(mirrored);
			capsuleCollider.radius = this.radius;
			capsuleCollider.height = this.height;
			capsuleCollider.direction = this.capsuleAxis;
			goto IL_F7;
		}
		case 4:
		{
			BoxCollider boxCollider = transform.gameObject.AddComponent<BoxCollider>();
			boxCollider.center = this.GetCenter(mirrored);
			boxCollider.size = this.size;
			goto IL_F7;
		}
		}
		throw new NotSupportedException();
		IL_F7:
		return transform.collider;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x00007A2C File Offset: 0x00005C2C
	private void CreatedCollider(Collider created, global::AuthorChHit.Rep repFormat, bool addJoints, int? layerIndex)
	{
		if (!created)
		{
			return;
		}
		repFormat.bone = created.transform;
		if (addJoints)
		{
			Rigidbody rigidbody = created.rigidbody;
			if (!rigidbody)
			{
				rigidbody = created.gameObject.AddComponent<Rigidbody>();
			}
			rigidbody.mass = this.mass;
			rigidbody.drag = this.drag;
			rigidbody.angularDrag = this.angularDrag;
			if (this.myJoints != null)
			{
				foreach (global::AuthorChJoint authorChJoint in this.myJoints)
				{
					if (authorChJoint)
					{
						authorChJoint.AddJoint(repFormat.bone.root, ref repFormat);
					}
				}
			}
		}
		if (layerIndex != null)
		{
			created.gameObject.layer = layerIndex.Value;
		}
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x00007B04 File Offset: 0x00005D04
	public void CreateColliderOn(Transform instance, Transform root, bool addJoints)
	{
		this.CreateColliderOn(instance, root, addJoints, null);
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x00007B24 File Offset: 0x00005D24
	public void CreateColliderOn(Transform instance, Transform root, bool addJoints, int? layerIndex)
	{
		if (this.bone)
		{
			this.CreatedCollider(this.CreateColliderOn(instance, root, this.bone, false), this.primary, addJoints, layerIndex);
		}
		if (this.mirrored && this.mirrored != this.bone)
		{
			this.CreatedCollider(this.CreateColliderOn(instance, root, this.mirrored, true), this.secondary, addJoints, layerIndex);
		}
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x00007BA4 File Offset: 0x00005DA4
	private HitShape CreateHitBoxOnDo(Transform instanceRoot, Transform root, Transform bone, bool mirrored, int? layerIndex)
	{
		Collider collider = this.CreateColliderOn(instanceRoot, root, bone, mirrored);
		if (layerIndex != null)
		{
			collider.gameObject.layer = layerIndex.Value;
		}
		global::HitBox hitBox;
		if (base.creation is global::AuthorHull)
		{
			hitBox = (base.creation as global::AuthorHull).CreateHitBox(collider.gameObject);
		}
		else
		{
			hitBox = null;
		}
		hitBox.bodyPart = ((!mirrored) ? this.bodyPart : this.mirroredBodyPart);
		hitBox.priority = this.hitPriority;
		hitBox.damageFactor = this.damageMultiplier;
		return new HitShape(collider);
	}

	// Token: 0x060001CA RID: 458 RVA: 0x00007C48 File Offset: 0x00005E48
	public void CreateHitBoxOn(List<HitShape> list, Transform instance, Transform root)
	{
		this.CreateHitBoxOn(list, instance, root, null);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x00007C68 File Offset: 0x00005E68
	public void CreateHitBoxOn(List<HitShape> list, Transform instance, Transform root, int? layerIndex)
	{
		if (this.bone)
		{
			list.Add(this.CreateHitBoxOnDo(instance, root, this.bone, false, layerIndex));
		}
		if (this.mirrored && this.mirrored != this.bone)
		{
			list.Add(this.CreateHitBoxOnDo(instance, root, this.mirrored, true, layerIndex));
		}
	}

	// Token: 0x060001CC RID: 460 RVA: 0x00007CDC File Offset: 0x00005EDC
	private void DrawGiz(Transform bone, bool mirrored)
	{
		if (Event.current.shift && bone)
		{
			Gizmos.matrix = bone.localToWorldMatrix;
			switch (this.kind)
			{
			case 1:
				Gizmos.DrawWireSphere(this.GetCenter(mirrored), this.radius);
				break;
			case 2:
				global::Gizmos2.DrawWireCapsule(this.GetCenter(mirrored), this.radius, this.height, this.capsuleAxis);
				break;
			case 4:
				Gizmos.DrawWireCube(this.GetCenter(mirrored), this.size);
				break;
			}
		}
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00007D84 File Offset: 0x00005F84
	private void OnDrawGizmos()
	{
		if (this.bone != this.mirrored)
		{
			Gizmos.color = global::AuthorChHit.mirroredGizmoColor;
			this.DrawGiz(this.mirrored, true);
		}
		Gizmos.color = global::AuthorChHit.boneGizmoColor;
		this.DrawGiz(this.bone, false);
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00007DD8 File Offset: 0x00005FD8
	internal void OnJointDestroy(global::AuthorChJoint joint)
	{
		if (this.myJoints == null)
		{
			return;
		}
		int num = Array.IndexOf<global::AuthorChJoint>(this.myJoints, joint);
		if (num != -1)
		{
			for (int i = num; i < this.myJoints.Length - 1; i++)
			{
				this.myJoints[i] = this.myJoints[i + 1];
			}
			Array.Resize<global::AuthorChJoint>(ref this.myJoints, this.myJoints.Length - 1);
		}
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00007E48 File Offset: 0x00006048
	protected override void OnPeiceDestroy()
	{
		try
		{
			if (this.myJoints != null)
			{
				global::AuthorChJoint[] array = this.myJoints;
				this.myJoints = null;
				foreach (global::AuthorChJoint authorChJoint in array)
				{
					if (authorChJoint)
					{
						Object.Destroy(authorChJoint);
					}
				}
			}
		}
		finally
		{
			base.OnPeiceDestroy();
		}
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00007EC4 File Offset: 0x000060C4
	public override void SaveJsonProperties(JSONStream stream)
	{
		base.SaveJsonProperties(stream);
		stream.WriteText("bone", base.FromRootBonePath(this.bone));
		stream.WriteEnum("bonepart", this.bodyPart);
		stream.WriteBoolean("mirror", this.isMirror);
		stream.WriteText("mirrorbone", base.FromRootBonePath(this.mirrored));
		stream.WriteEnum("mirrorbonepart", this.mirroredBodyPart);
		stream.WriteArrayStart("mirrorboneflip");
		stream.WriteBoolean(this.mirrorX);
		stream.WriteBoolean(this.mirrorY);
		stream.WriteBoolean(this.mirrorZ);
		stream.WriteArrayEnd();
		stream.WriteEnum("kind", this.kind);
		stream.WriteVector3("center", this.center);
		stream.WriteVector3("size", this.size);
		stream.WriteNumber("radius", this.radius);
		stream.WriteNumber("height", this.height);
		stream.WriteInteger("capsuleaxis", this.capsuleAxis);
		stream.WriteNumber("damagemul", this.damageMultiplier);
		stream.WriteInteger("hitpriority", this.hitPriority);
		stream.WriteNumber("mass", this.mass);
		stream.WriteNumber("drag", this.drag);
		stream.WriteNumber("adrag", this.angularDrag);
	}

	// Token: 0x040000D7 RID: 215
	[SerializeField]
	private HitShapeKind kind;

	// Token: 0x040000D8 RID: 216
	[SerializeField]
	private Transform bone;

	// Token: 0x040000D9 RID: 217
	[SerializeField]
	private Vector3 center;

	// Token: 0x040000DA RID: 218
	[SerializeField]
	private float radius = 0.5f;

	// Token: 0x040000DB RID: 219
	[SerializeField]
	private float height = 2f;

	// Token: 0x040000DC RID: 220
	[SerializeField]
	private float damageMultiplier = 1f;

	// Token: 0x040000DD RID: 221
	[SerializeField]
	private int hitPriority = 128;

	// Token: 0x040000DE RID: 222
	[SerializeField]
	private BodyPart bodyPart;

	// Token: 0x040000DF RID: 223
	[SerializeField]
	private BodyPart mirroredBodyPart;

	// Token: 0x040000E0 RID: 224
	[SerializeField]
	private Vector3 size = Vector3.one;

	// Token: 0x040000E1 RID: 225
	[SerializeField]
	private int capsuleAxis = 1;

	// Token: 0x040000E2 RID: 226
	[SerializeField]
	private float mass = 1f;

	// Token: 0x040000E3 RID: 227
	[SerializeField]
	private float drag;

	// Token: 0x040000E4 RID: 228
	[SerializeField]
	private float angularDrag = 0.05f;

	// Token: 0x040000E5 RID: 229
	[SerializeField]
	private bool isMirror;

	// Token: 0x040000E6 RID: 230
	[SerializeField]
	private Transform mirrored;

	// Token: 0x040000E7 RID: 231
	[SerializeField]
	private bool mirrorX;

	// Token: 0x040000E8 RID: 232
	[SerializeField]
	private bool mirrorY;

	// Token: 0x040000E9 RID: 233
	[SerializeField]
	private bool mirrorZ;

	// Token: 0x040000EA RID: 234
	[SerializeField]
	private global::AuthorChJoint[] myJoints;

	// Token: 0x040000EB RID: 235
	private Rect lastPopupRect;

	// Token: 0x040000EC RID: 236
	protected static readonly Color boneGizmoColor = new Color(1f, 1f, 1f, 0.3f);

	// Token: 0x040000ED RID: 237
	protected static readonly Color mirroredGizmoColor = new Color(0f, 0f, 0f, 0.3f);

	// Token: 0x02000031 RID: 49
	public struct Rep
	{
		// Token: 0x060001D1 RID: 465 RVA: 0x00008038 File Offset: 0x00006238
		public Vector3 Flip(Vector3 v)
		{
			if (this.flipX)
			{
				v.x = -v.x;
			}
			if (this.flipY)
			{
				v.y = -v.y;
			}
			if (this.flipZ)
			{
				v.z = -v.z;
			}
			return v;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008094 File Offset: 0x00006294
		public Vector3 AxisFlip(Vector3 v)
		{
			if (this.flipX == this.mirrored)
			{
				v.x = -v.x;
			}
			if (this.flipY == this.mirrored)
			{
				v.y = -v.y;
			}
			if (this.flipZ == this.mirrored)
			{
				v.z = -v.z;
			}
			return v;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008104 File Offset: 0x00006304
		public string path
		{
			get
			{
				if (!this.valid)
				{
					return null;
				}
				return global::AuthorShared.CalculatePath(this.bone, this.bone.root);
			}
		}

		// Token: 0x040000EF RID: 239
		public global::AuthorChHit hit;

		// Token: 0x040000F0 RID: 240
		public Transform bone;

		// Token: 0x040000F1 RID: 241
		public bool mirrored;

		// Token: 0x040000F2 RID: 242
		public bool flipX;

		// Token: 0x040000F3 RID: 243
		public bool flipY;

		// Token: 0x040000F4 RID: 244
		public bool flipZ;

		// Token: 0x040000F5 RID: 245
		public Vector3 center;

		// Token: 0x040000F6 RID: 246
		public Vector3 size;

		// Token: 0x040000F7 RID: 247
		public float radius;

		// Token: 0x040000F8 RID: 248
		public float height;

		// Token: 0x040000F9 RID: 249
		public int capsuleAxis;

		// Token: 0x040000FA RID: 250
		public bool valid;
	}

	// Token: 0x02000032 RID: 50
	private static class JointMenu
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00008188 File Offset: 0x00006388
		public static void Callback(object userData, string[] options, int selected)
		{
			global::AuthorChHit authorChHit = userData as global::AuthorChHit;
			switch (selected)
			{
			case 1:
				authorChHit.AddHingeJoint();
				break;
			case 2:
				authorChHit.AddCharacterJoint();
				break;
			case 3:
				authorChHit.AddFixedJoint();
				break;
			case 4:
				authorChHit.AddSpringJoint();
				break;
			}
		}

		// Token: 0x040000FB RID: 251
		public static readonly GUIContent[] options = new GUIContent[]
		{
			new GUIContent("Nevermind"),
			new GUIContent("Add Hinge Joint"),
			new GUIContent("Add Character Joint"),
			new GUIContent("Add Fixed Joint"),
			new GUIContent("Add Spring Joint")
		};
	}
}
