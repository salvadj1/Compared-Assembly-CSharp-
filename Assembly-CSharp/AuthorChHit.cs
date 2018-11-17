using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class AuthorChHit : AuthorPeice
{
	// Token: 0x17000042 RID: 66
	// (get) Token: 0x0600016A RID: 362 RVA: 0x00005F10 File Offset: 0x00004110
	// (set) Token: 0x0600016B RID: 363 RVA: 0x00005FAC File Offset: 0x000041AC
	public AuthorChHit.Rep primary
	{
		get
		{
			AuthorChHit.Rep result;
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

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x0600016C RID: 364 RVA: 0x00006008 File Offset: 0x00004208
	// (set) Token: 0x0600016D RID: 365 RVA: 0x000060CC File Offset: 0x000042CC
	public AuthorChHit.Rep secondary
	{
		get
		{
			AuthorChHit.Rep result;
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

	// Token: 0x0600016E RID: 366 RVA: 0x00006130 File Offset: 0x00004330
	private Vector3 GetCenter(bool mirrored)
	{
		if (mirrored)
		{
			return new Vector3((!this.mirrorX) ? this.center.x : (-this.center.x), (!this.mirrorY) ? this.center.y : (-this.center.y), (!this.mirrorZ) ? this.center.z : (-this.center.z));
		}
		return this.center;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x000061C4 File Offset: 0x000043C4
	private bool DoTransformHandles(Transform bone, ref Vector3 center, ref Vector3 size, ref float radius, ref float height, ref int capsuleAxis)
	{
		Matrix4x4 matrix = AuthorShared.Scene.matrix;
		if (bone)
		{
			AuthorShared.Scene.matrix = bone.transform.localToWorldMatrix;
		}
		bool flag = false;
		switch (this.kind)
		{
		case 1:
			flag |= AuthorShared.Scene.SphereDrag(ref center, ref radius);
			break;
		case 2:
			flag |= AuthorShared.Scene.CapsuleDrag(ref center, ref radius, ref height, ref capsuleAxis);
			break;
		case 4:
			flag |= AuthorShared.Scene.BoxDrag(ref center, ref size);
			break;
		}
		AuthorShared.Scene.matrix = matrix;
		return flag;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00006254 File Offset: 0x00004454
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

	// Token: 0x06000171 RID: 369 RVA: 0x000063F4 File Offset: 0x000045F4
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

	// Token: 0x06000172 RID: 370 RVA: 0x0000649C File Offset: 0x0000469C
	private void AddHingeJoint()
	{
		this.AddJoint(AuthorChJoint.Kind.Hinge);
	}

	// Token: 0x06000173 RID: 371 RVA: 0x000064A8 File Offset: 0x000046A8
	private void AddCharacterJoint()
	{
		this.AddJoint(AuthorChJoint.Kind.Character);
	}

	// Token: 0x06000174 RID: 372 RVA: 0x000064B4 File Offset: 0x000046B4
	private void AddSpringJoint()
	{
		this.AddJoint(AuthorChJoint.Kind.Spring);
	}

	// Token: 0x06000175 RID: 373 RVA: 0x000064C0 File Offset: 0x000046C0
	private void AddFixedJoint()
	{
		this.AddJoint(AuthorChJoint.Kind.Fixed);
	}

	// Token: 0x06000176 RID: 374 RVA: 0x000064CC File Offset: 0x000046CC
	private AuthorChJoint AddJoint(AuthorChJoint.Kind kind)
	{
		AuthorChJoint authorChJoint = base.creation.CreatePeice<AuthorChJoint>(kind.ToString(), new Type[0]);
		if (authorChJoint)
		{
			authorChJoint.InitializeFromOwner(this, kind);
			Array.Resize<AuthorChJoint>(ref this.myJoints, (this.myJoints != null) ? (this.myJoints.Length + 1) : 1);
			this.myJoints[this.myJoints.Length - 1] = authorChJoint;
		}
		return authorChJoint;
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00006544 File Offset: 0x00004744
	private void FigureOutDefaultBodyPart(Transform bone, ref BodyPart part)
	{
		if (part == 0)
		{
			AuthorHull authorHull = base.creation as AuthorHull;
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

	// Token: 0x06000178 RID: 376 RVA: 0x000065C0 File Offset: 0x000047C0
	public override bool PeiceInspectorGUI()
	{
		bool flag = base.PeiceInspectorGUI();
		string peiceID = base.peiceID;
		if (AuthorShared.StringField("Title", ref peiceID, new GUILayoutOption[0]))
		{
			base.peiceID = peiceID;
			flag = true;
		}
		bool flag2 = this.mirrored && this.mirrored != this.bone;
		bool flag3 = this.bone;
		BodyPart bodyPart = this.bodyPart;
		if (AuthorShared.ObjectField<Transform>("Bone", ref this.bone, (AuthorShared.ObjectFieldFlags)25, new GUILayoutOption[0]))
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
			bodyPart = (BodyPart)AuthorShared.EnumField("Body Part", bodyPart, new GUILayoutOption[0]);
		}
		GUI.Box(AuthorShared.BeginVertical(new GUILayoutOption[0]), GUIContent.none);
		flag |= AuthorShared.ObjectField<Transform>("Mirrored Bone", ref this.mirrored, (AuthorShared.ObjectFieldFlags)25, new GUILayoutOption[0]);
		if (flag2)
		{
			bodyPart2 = (BodyPart)AuthorShared.EnumField("Body Part", bodyPart2, new GUILayoutOption[0]);
			AuthorShared.BeginHorizontal(new GUILayoutOption[0]);
			bool flag4 = GUILayout.Toggle(this.mirrorX, "Mirror X", new GUILayoutOption[0]);
			bool flag5 = GUILayout.Toggle(this.mirrorY, "Mirror Y", new GUILayoutOption[0]);
			bool flag6 = GUILayout.Toggle(this.mirrorZ, "Mirror Z", new GUILayoutOption[0]);
			AuthorShared.EndHorizontal();
			if (flag4 != this.mirrorX || flag5 != this.mirrorY || flag6 != this.mirrorZ)
			{
				this.mirrorX = flag4;
				this.mirrorY = flag5;
				this.mirrorZ = flag6;
				flag = true;
			}
		}
		AuthorShared.EndVertical();
		Vector3 vector = this.center;
		float num = this.radius;
		float num2 = this.height;
		Vector3 vector2 = this.size;
		int num3 = this.capsuleAxis;
		AuthorShared.BeginSubSection("Shape", new GUILayoutOption[0]);
		HitShapeKind hitShapeKind = (HitShapeKind)AuthorShared.EnumField("Kind", this.kind, new GUILayoutOption[0]);
		switch (this.kind)
		{
		case 1:
			vector = AuthorShared.Vector3Field("Center", this.center, new GUILayoutOption[0]);
			num = Mathf.Max(AuthorShared.FloatField("Radius", this.radius, new GUILayoutOption[0]), 0.001f);
			break;
		case 2:
			vector = AuthorShared.Vector3Field("Center", this.center, new GUILayoutOption[0]);
			num = Mathf.Max(AuthorShared.FloatField("Radius", this.radius, new GUILayoutOption[0]), 0.001f);
			num2 = Mathf.Max(AuthorShared.FloatField("Height", this.height, new GUILayoutOption[0]), 0.001f);
			num3 = Mathf.Clamp(AuthorShared.IntField("Height Axis", this.capsuleAxis, new GUILayoutOption[0]), 0, 2);
			break;
		case 4:
			vector = AuthorShared.Vector3Field("Center", this.center, new GUILayoutOption[0]);
			vector2 = AuthorShared.Vector3Field("Size", this.size, new GUILayoutOption[0]);
			break;
		}
		AuthorShared.EndSubSection();
		AuthorShared.BeginSubSection("Rigidbody", new GUILayoutOption[0]);
		float num4 = Mathf.Max(AuthorShared.FloatField("Mass", this.mass, new GUILayoutOption[0]), 0.001f);
		float num5 = Mathf.Max(AuthorShared.FloatField("Drag", this.drag, new GUILayoutOption[0]), 0f);
		float num6 = Mathf.Max(AuthorShared.FloatField("Angular Drag", this.angularDrag, new GUILayoutOption[0]), 0f);
		AuthorShared.EndSubSection();
		AuthorShared.BeginSubSection("Hit Box", new GUILayoutOption[0]);
		int num7 = this.hitPriority;
		float num8 = this.damageMultiplier;
		if (flag2 || flag3)
		{
			num7 = AuthorShared.IntField("Hit Priority", num7, new GUILayoutOption[0]);
			num8 = AuthorShared.FloatField("Damage Mult.", num8, new GUILayoutOption[0]);
		}
		AuthorShared.EndSubSection();
		bool flag7 = GUILayout.Button("Add Joint", new GUILayoutOption[0]);
		if (Event.current.type == 7)
		{
			this.lastPopupRect = GUILayoutUtility.GetLastRect();
		}
		if (flag7)
		{
			AuthorShared.CustomMenu(this.lastPopupRect, AuthorChHit.JointMenu.options, 0, new AuthorShared.CustomMenuProc(AuthorChHit.JointMenu.Callback), this);
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

	// Token: 0x06000179 RID: 377 RVA: 0x00006BB4 File Offset: 0x00004DB4
	private Collider CreateColliderOn(Transform instanceRoot, Transform root, Transform bone, bool mirrored)
	{
		if (!bone)
		{
			throw new ArgumentException("there was no bone");
		}
		string text = AuthorShared.CalculatePath(bone, root);
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

	// Token: 0x0600017A RID: 378 RVA: 0x00006CCC File Offset: 0x00004ECC
	private void CreatedCollider(Collider created, AuthorChHit.Rep repFormat, bool addJoints, int? layerIndex)
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
				foreach (AuthorChJoint authorChJoint in this.myJoints)
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

	// Token: 0x0600017B RID: 379 RVA: 0x00006DA4 File Offset: 0x00004FA4
	public void CreateColliderOn(Transform instance, Transform root, bool addJoints)
	{
		this.CreateColliderOn(instance, root, addJoints, null);
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00006DC4 File Offset: 0x00004FC4
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

	// Token: 0x0600017D RID: 381 RVA: 0x00006E44 File Offset: 0x00005044
	private HitShape CreateHitBoxOnDo(Transform instanceRoot, Transform root, Transform bone, bool mirrored, int? layerIndex)
	{
		Collider collider = this.CreateColliderOn(instanceRoot, root, bone, mirrored);
		if (layerIndex != null)
		{
			collider.gameObject.layer = layerIndex.Value;
		}
		HitBox hitBox;
		if (base.creation is AuthorHull)
		{
			hitBox = (base.creation as AuthorHull).CreateHitBox(collider.gameObject);
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

	// Token: 0x0600017E RID: 382 RVA: 0x00006EE8 File Offset: 0x000050E8
	public void CreateHitBoxOn(List<HitShape> list, Transform instance, Transform root)
	{
		this.CreateHitBoxOn(list, instance, root, null);
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00006F08 File Offset: 0x00005108
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

	// Token: 0x06000180 RID: 384 RVA: 0x00006F7C File Offset: 0x0000517C
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
				Gizmos2.DrawWireCapsule(this.GetCenter(mirrored), this.radius, this.height, this.capsuleAxis);
				break;
			case 4:
				Gizmos.DrawWireCube(this.GetCenter(mirrored), this.size);
				break;
			}
		}
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00007024 File Offset: 0x00005224
	private void OnDrawGizmos()
	{
		if (this.bone != this.mirrored)
		{
			Gizmos.color = AuthorChHit.mirroredGizmoColor;
			this.DrawGiz(this.mirrored, true);
		}
		Gizmos.color = AuthorChHit.boneGizmoColor;
		this.DrawGiz(this.bone, false);
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00007078 File Offset: 0x00005278
	internal void OnJointDestroy(AuthorChJoint joint)
	{
		if (this.myJoints == null)
		{
			return;
		}
		int num = Array.IndexOf<AuthorChJoint>(this.myJoints, joint);
		if (num != -1)
		{
			for (int i = num; i < this.myJoints.Length - 1; i++)
			{
				this.myJoints[i] = this.myJoints[i + 1];
			}
			Array.Resize<AuthorChJoint>(ref this.myJoints, this.myJoints.Length - 1);
		}
	}

	// Token: 0x06000183 RID: 387 RVA: 0x000070E8 File Offset: 0x000052E8
	protected override void OnPeiceDestroy()
	{
		try
		{
			if (this.myJoints != null)
			{
				AuthorChJoint[] array = this.myJoints;
				this.myJoints = null;
				foreach (AuthorChJoint authorChJoint in array)
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

	// Token: 0x06000184 RID: 388 RVA: 0x00007164 File Offset: 0x00005364
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

	// Token: 0x04000097 RID: 151
	[SerializeField]
	private HitShapeKind kind;

	// Token: 0x04000098 RID: 152
	[SerializeField]
	private Transform bone;

	// Token: 0x04000099 RID: 153
	[SerializeField]
	private Vector3 center;

	// Token: 0x0400009A RID: 154
	[SerializeField]
	private float radius = 0.5f;

	// Token: 0x0400009B RID: 155
	[SerializeField]
	private float height = 2f;

	// Token: 0x0400009C RID: 156
	[SerializeField]
	private float damageMultiplier = 1f;

	// Token: 0x0400009D RID: 157
	[SerializeField]
	private int hitPriority = 128;

	// Token: 0x0400009E RID: 158
	[SerializeField]
	private BodyPart bodyPart;

	// Token: 0x0400009F RID: 159
	[SerializeField]
	private BodyPart mirroredBodyPart;

	// Token: 0x040000A0 RID: 160
	[SerializeField]
	private Vector3 size = Vector3.one;

	// Token: 0x040000A1 RID: 161
	[SerializeField]
	private int capsuleAxis = 1;

	// Token: 0x040000A2 RID: 162
	[SerializeField]
	private float mass = 1f;

	// Token: 0x040000A3 RID: 163
	[SerializeField]
	private float drag;

	// Token: 0x040000A4 RID: 164
	[SerializeField]
	private float angularDrag = 0.05f;

	// Token: 0x040000A5 RID: 165
	[SerializeField]
	private bool isMirror;

	// Token: 0x040000A6 RID: 166
	[SerializeField]
	private Transform mirrored;

	// Token: 0x040000A7 RID: 167
	[SerializeField]
	private bool mirrorX;

	// Token: 0x040000A8 RID: 168
	[SerializeField]
	private bool mirrorY;

	// Token: 0x040000A9 RID: 169
	[SerializeField]
	private bool mirrorZ;

	// Token: 0x040000AA RID: 170
	[SerializeField]
	private AuthorChJoint[] myJoints;

	// Token: 0x040000AB RID: 171
	private Rect lastPopupRect;

	// Token: 0x040000AC RID: 172
	protected static readonly Color boneGizmoColor = new Color(1f, 1f, 1f, 0.3f);

	// Token: 0x040000AD RID: 173
	protected static readonly Color mirroredGizmoColor = new Color(0f, 0f, 0f, 0.3f);

	// Token: 0x02000024 RID: 36
	public struct Rep
	{
		// Token: 0x06000185 RID: 389 RVA: 0x000072D8 File Offset: 0x000054D8
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

		// Token: 0x06000186 RID: 390 RVA: 0x00007334 File Offset: 0x00005534
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000187 RID: 391 RVA: 0x000073A4 File Offset: 0x000055A4
		public string path
		{
			get
			{
				if (!this.valid)
				{
					return null;
				}
				return AuthorShared.CalculatePath(this.bone, this.bone.root);
			}
		}

		// Token: 0x040000AF RID: 175
		public AuthorChHit hit;

		// Token: 0x040000B0 RID: 176
		public Transform bone;

		// Token: 0x040000B1 RID: 177
		public bool mirrored;

		// Token: 0x040000B2 RID: 178
		public bool flipX;

		// Token: 0x040000B3 RID: 179
		public bool flipY;

		// Token: 0x040000B4 RID: 180
		public bool flipZ;

		// Token: 0x040000B5 RID: 181
		public Vector3 center;

		// Token: 0x040000B6 RID: 182
		public Vector3 size;

		// Token: 0x040000B7 RID: 183
		public float radius;

		// Token: 0x040000B8 RID: 184
		public float height;

		// Token: 0x040000B9 RID: 185
		public int capsuleAxis;

		// Token: 0x040000BA RID: 186
		public bool valid;
	}

	// Token: 0x02000025 RID: 37
	private static class JointMenu
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00007428 File Offset: 0x00005628
		public static void Callback(object userData, string[] options, int selected)
		{
			AuthorChHit authorChHit = userData as AuthorChHit;
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

		// Token: 0x040000BB RID: 187
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
