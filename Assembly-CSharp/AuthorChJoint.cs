using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class AuthorChJoint : AuthorPeice
{
	// Token: 0x17000045 RID: 69
	// (get) Token: 0x0600018C RID: 396 RVA: 0x00007560 File Offset: 0x00005760
	// (set) Token: 0x0600018D RID: 397 RVA: 0x000075AC File Offset: 0x000057AC
	private SoftJointLimit lowTwist
	{
		get
		{
			SoftJointLimit result = default(SoftJointLimit);
			result.limit = this.twistL_limit;
			result.damper = this.twistL_dampler;
			result.spring = this.twistL_spring;
			result.bounciness = this.twistL_bounce;
			return result;
		}
		set
		{
			this.twistL_limit = value.limit;
			this.twistL_dampler = value.damper;
			this.twistL_spring = value.spring;
			this.twistL_bounce = value.bounciness;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600018E RID: 398 RVA: 0x000075F0 File Offset: 0x000057F0
	// (set) Token: 0x0600018F RID: 399 RVA: 0x0000763C File Offset: 0x0000583C
	private SoftJointLimit highTwist
	{
		get
		{
			SoftJointLimit result = default(SoftJointLimit);
			result.limit = this.twistH_limit;
			result.damper = this.twistH_dampler;
			result.spring = this.twistH_spring;
			result.bounciness = this.twistH_bounce;
			return result;
		}
		set
		{
			this.twistH_limit = value.limit;
			this.twistH_dampler = value.damper;
			this.twistH_spring = value.spring;
			this.twistH_bounce = value.bounciness;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x06000190 RID: 400 RVA: 0x00007680 File Offset: 0x00005880
	// (set) Token: 0x06000191 RID: 401 RVA: 0x000076CC File Offset: 0x000058CC
	private SoftJointLimit swing1
	{
		get
		{
			SoftJointLimit result = default(SoftJointLimit);
			result.limit = this.swing1_limit;
			result.damper = this.swing1_dampler;
			result.spring = this.swing1_spring;
			result.bounciness = this.swing1_bounce;
			return result;
		}
		set
		{
			this.swing1_limit = value.limit;
			this.swing1_dampler = value.damper;
			this.swing1_spring = value.spring;
			this.swing1_bounce = value.bounciness;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x06000192 RID: 402 RVA: 0x00007710 File Offset: 0x00005910
	// (set) Token: 0x06000193 RID: 403 RVA: 0x0000775C File Offset: 0x0000595C
	private SoftJointLimit swing2
	{
		get
		{
			SoftJointLimit result = default(SoftJointLimit);
			result.limit = this.swing2_limit;
			result.damper = this.swing2_dampler;
			result.spring = this.swing2_spring;
			result.bounciness = this.swing2_bounce;
			return result;
		}
		set
		{
			this.swing2_limit = value.limit;
			this.swing2_dampler = value.damper;
			this.swing2_spring = value.spring;
			this.swing2_bounce = value.bounciness;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x06000194 RID: 404 RVA: 0x000077A0 File Offset: 0x000059A0
	// (set) Token: 0x06000195 RID: 405 RVA: 0x000077EC File Offset: 0x000059EC
	private JointLimits limit
	{
		get
		{
			JointLimits result = default(JointLimits);
			result.min = this.h_limit_min;
			result.max = this.h_limit_max;
			result.minBounce = this.h_limit_minb;
			result.maxBounce = this.h_limit_maxb;
			return result;
		}
		set
		{
			this.h_limit_min = value.min;
			this.h_limit_max = value.max;
			this.h_limit_minb = value.minBounce;
			this.h_limit_maxb = value.maxBounce;
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x06000196 RID: 406 RVA: 0x00007830 File Offset: 0x00005A30
	// (set) Token: 0x06000197 RID: 407 RVA: 0x00007870 File Offset: 0x00005A70
	private JointSpring spring
	{
		get
		{
			JointSpring result = default(JointSpring);
			result.spring = this.h_spring_s;
			result.damper = this.h_spring_d;
			result.targetPosition = this.h_spring_t;
			return result;
		}
		set
		{
			this.h_spring_s = value.spring;
			this.h_spring_d = value.damper;
			this.h_spring_t = value.targetPosition;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x06000198 RID: 408 RVA: 0x0000789C File Offset: 0x00005A9C
	// (set) Token: 0x06000199 RID: 409 RVA: 0x000078DC File Offset: 0x00005ADC
	private JointMotor motor
	{
		get
		{
			JointMotor result = default(JointMotor);
			result.force = this.h_motor_f;
			result.targetVelocity = this.h_motor_v;
			result.freeSpin = this.h_motor_s;
			return result;
		}
		set
		{
			this.h_motor_f = value.force;
			this.h_motor_v = value.targetVelocity;
			this.h_motor_s = value.freeSpin;
		}
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00007908 File Offset: 0x00005B08
	private void ConfigureJointShared(Joint joint, Transform root, ref AuthorChHit.Rep self)
	{
		if (this.connect)
		{
			AuthorChHit.Rep rep;
			if (self.mirrored)
			{
				rep = this.connect.secondary;
				if (!rep.valid)
				{
					rep = this.connect.primary;
				}
			}
			else
			{
				rep = this.connect.primary;
				if (!rep.valid)
				{
					rep = this.connect.secondary;
				}
			}
			if (rep.valid)
			{
				Transform transform = root.FindChild(rep.path);
				Rigidbody rigidbody = transform.rigidbody;
				if (!rigidbody)
				{
					rigidbody = transform.gameObject.AddComponent<Rigidbody>();
				}
				joint.connectedBody = rigidbody;
			}
			else
			{
				Debug.LogWarning("No means of making/getting rigidbody", this.connect);
			}
		}
		joint.anchor = self.Flip(this.anchor);
		joint.axis = self.AxisFlip(this.axis);
		joint.breakForce = this.breakForce;
		joint.breakTorque = this.breakTorque;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00007A10 File Offset: 0x00005C10
	private TJoint ConfigJoint<TJoint>(TJoint joint, Transform root, ref AuthorChHit.Rep self) where TJoint : Joint
	{
		this.ConfigureJointShared(joint, root, ref self);
		return joint;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00007A24 File Offset: 0x00005C24
	private TJoint CreateJoint<TJoint>(Transform root, ref AuthorChHit.Rep self) where TJoint : Joint
	{
		return this.ConfigJoint<TJoint>(self.bone.gameObject.AddComponent<TJoint>(), root, ref self);
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00007A4C File Offset: 0x00005C4C
	public Joint AddJoint(Transform root, ref AuthorChHit.Rep self)
	{
		Joint result;
		switch (this.kind)
		{
		case AuthorChJoint.Kind.Hinge:
		{
			HingeJoint hingeJoint = this.CreateJoint<HingeJoint>(root, ref self);
			hingeJoint.limits = this.limit;
			hingeJoint.useLimits = this.useLimit;
			hingeJoint.motor = this.motor;
			hingeJoint.useMotor = this.useMotor;
			hingeJoint.spring = this.spring;
			hingeJoint.useSpring = this.useSpring;
			result = hingeJoint;
			break;
		}
		case AuthorChJoint.Kind.Character:
		{
			CharacterJoint characterJoint = this.CreateJoint<CharacterJoint>(root, ref self);
			characterJoint.swingAxis = self.AxisFlip(this.swingAxis);
			characterJoint.lowTwistLimit = this.lowTwist;
			characterJoint.highTwistLimit = this.highTwist;
			characterJoint.lowTwistLimit = this.lowTwist;
			characterJoint.swing1Limit = this.swing1;
			characterJoint.swing2Limit = this.swing2;
			result = characterJoint;
			break;
		}
		case AuthorChJoint.Kind.Fixed:
		{
			FixedJoint fixedJoint = this.CreateJoint<FixedJoint>(root, ref self);
			result = fixedJoint;
			break;
		}
		case AuthorChJoint.Kind.Spring:
		{
			SpringJoint springJoint = this.CreateJoint<SpringJoint>(root, ref self);
			springJoint.spring = this.spring_spring;
			springJoint.damper = this.spring_damper;
			springJoint.minDistance = this.spring_min;
			springJoint.maxDistance = this.spring_max;
			result = springJoint;
			break;
		}
		default:
			return null;
		}
		return result;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00007B90 File Offset: 0x00005D90
	private bool DoTransformHandles(ref AuthorChHit.Rep self, ref AuthorChHit.Rep connect)
	{
		if (!self.valid)
		{
			return false;
		}
		Vector3 v = self.Flip(this.anchor);
		Vector3 vector = self.AxisFlip(this.axis);
		Vector3 vector2 = self.AxisFlip(this.swingAxis);
		Matrix4x4 matrix = AuthorShared.Scene.matrix;
		if (connect.valid)
		{
			AuthorShared.Scene.matrix = connect.bone.localToWorldMatrix;
			Color color = AuthorShared.Scene.color;
			AuthorShared.Scene.color = color * new Color(1f, 1f, 1f, 0.4f);
			Vector3 vector3 = connect.bone.InverseTransformPoint(self.bone.position);
			if (vector3 != Vector3.zero)
			{
				AuthorShared.Scene.DrawBone(Vector3.zero, Quaternion.LookRotation(vector3), vector3.magnitude, 0.02f, new Vector3(0.05f, 0.05f, 0.5f));
			}
			AuthorShared.Scene.color = color;
		}
		AuthorShared.Scene.matrix = self.bone.localToWorldMatrix;
		bool result = false;
		if (AuthorShared.Scene.PivotDrag(ref v, ref vector))
		{
			result = true;
			this.anchor = self.Flip(v);
			this.axis = self.AxisFlip(vector);
		}
		AuthorChJoint.Kind kind = this.kind;
		if (kind != AuthorChJoint.Kind.Hinge)
		{
			if (kind == AuthorChJoint.Kind.Character)
			{
				Color color2 = AuthorShared.Scene.color;
				AuthorShared.Scene.color = color2 * AuthorChJoint.twistColor;
				SoftJointLimit softJointLimit = this.lowTwist;
				SoftJointLimit highTwist = this.highTwist;
				if (AuthorShared.Scene.LimitDrag(v, vector, ref this.twistOffset, ref softJointLimit, ref highTwist))
				{
					result = true;
					this.lowTwist = softJointLimit;
					this.highTwist = highTwist;
				}
				AuthorShared.Scene.color = color2 * AuthorChJoint.swing1Color;
				softJointLimit = this.swing1;
				if (AuthorShared.Scene.LimitDrag(v, vector2, ref this.swingOffset1, ref softJointLimit))
				{
					result = true;
					this.swing1 = softJointLimit;
				}
				AuthorShared.Scene.color = color2 * AuthorChJoint.swing2Color;
				softJointLimit = this.swing2;
				if (AuthorShared.Scene.LimitDrag(v, Vector3.Cross(vector2, vector), ref this.swingOffset2, ref softJointLimit))
				{
					result = true;
					this.swing2 = softJointLimit;
				}
				AuthorShared.Scene.color = color2;
			}
		}
		else if (this.useLimit)
		{
			JointLimits limit = this.limit;
			if (AuthorShared.Scene.LimitDrag(v, vector, ref this.limitOffset, ref limit))
			{
				result = true;
				this.limit = limit;
			}
		}
		AuthorShared.Scene.matrix = matrix;
		return result;
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00007DF4 File Offset: 0x00005FF4
	public override bool OnSceneView()
	{
		bool flag = base.OnSceneView();
		AuthorChHit.Rep primary = this.self.primary;
		AuthorChHit.Rep secondary = this.self.secondary;
		AuthorChHit.Rep rep = default(AuthorChHit.Rep);
		AuthorChHit.Rep rep2 = default(AuthorChHit.Rep);
		if (this.connect)
		{
			rep = this.connect.primary;
			rep2 = this.connect.secondary;
			if (!rep2.valid)
			{
				rep2 = rep;
			}
		}
		if (primary.valid)
		{
			flag |= this.DoTransformHandles(ref primary, ref rep);
		}
		if (secondary.valid)
		{
			flag |= this.DoTransformHandles(ref secondary, ref rep2);
		}
		return flag;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00007EA4 File Offset: 0x000060A4
	private static bool Field(AuthorShared.Content content, ref JointLimits limits, ref bool use, ref float offset)
	{
		GUI.Box(AuthorShared.BeginVertical(new GUILayoutOption[0]), GUIContent.none);
		AuthorShared.PrefixLabel(content);
		bool flag = use;
		bool flag2 = AuthorShared.Change(ref use, GUILayout.Toggle(use, "Use", new GUILayoutOption[0]));
		if (flag)
		{
			float min = limits.min;
			float max = limits.max;
			float minBounce = limits.minBounce;
			float maxBounce = limits.maxBounce;
			flag2 |= AuthorShared.FloatField("Min", ref min, new GUILayoutOption[0]);
			flag2 |= AuthorShared.FloatField("Max", ref max, new GUILayoutOption[0]);
			flag2 |= AuthorShared.FloatField("Min bounciness", ref minBounce, new GUILayoutOption[0]);
			flag2 |= AuthorShared.FloatField("Max bounciness", ref maxBounce, new GUILayoutOption[0]);
			if (use && flag2)
			{
				limits.min = min;
				limits.max = max;
				limits.minBounce = minBounce;
				limits.maxBounce = maxBounce;
			}
			Color contentColor = GUI.contentColor;
			GUI.contentColor = contentColor * new Color(1f, 1f, 1f, 0.3f);
			flag2 |= AuthorShared.FloatField("Offset(visual only)", ref offset, new GUILayoutOption[0]);
			GUI.contentColor = contentColor;
		}
		AuthorShared.EndVertical();
		return flag2;
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00007FF4 File Offset: 0x000061F4
	private static bool Field(AuthorShared.Content content, ref JointMotor motor, ref bool use)
	{
		GUI.Box(AuthorShared.BeginVertical(new GUILayoutOption[0]), GUIContent.none);
		AuthorShared.PrefixLabel(content);
		bool flag = use;
		bool flag2 = AuthorShared.Change(ref use, GUILayout.Toggle(use, "Use", new GUILayoutOption[0]));
		if (flag)
		{
			float force = motor.force;
			float targetVelocity = motor.targetVelocity;
			bool freeSpin = motor.freeSpin;
			flag2 |= AuthorShared.FloatField("Force", ref force, new GUILayoutOption[0]);
			flag2 |= AuthorShared.FloatField("Target Velocity", ref targetVelocity, new GUILayoutOption[0]);
			flag2 |= AuthorShared.Change(ref freeSpin, GUILayout.Toggle(freeSpin, "Free Spin", new GUILayoutOption[0]));
			if (use && flag2)
			{
				motor.force = force;
				motor.targetVelocity = targetVelocity;
				motor.freeSpin = freeSpin;
			}
		}
		AuthorShared.EndVertical();
		return flag2;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x000080D0 File Offset: 0x000062D0
	private static bool Field(AuthorShared.Content content, ref JointSpring spring, ref bool use)
	{
		GUI.Box(AuthorShared.BeginVertical(new GUILayoutOption[0]), GUIContent.none);
		AuthorShared.PrefixLabel(content);
		bool flag = use;
		bool flag2 = AuthorShared.Change(ref use, GUILayout.Toggle(use, "Use", new GUILayoutOption[0]));
		if (flag)
		{
			float spring2 = spring.spring;
			float targetPosition = spring.targetPosition;
			float damper = spring.damper;
			flag2 |= AuthorShared.FloatField("Spring Force", ref spring2, new GUILayoutOption[0]);
			flag2 |= AuthorShared.FloatField("Target Position", ref targetPosition, new GUILayoutOption[0]);
			flag2 |= AuthorShared.FloatField("Damper", ref damper, new GUILayoutOption[0]);
			if (use && flag2)
			{
				spring.spring = spring2;
				spring.targetPosition = targetPosition;
				spring.damper = damper;
			}
		}
		AuthorShared.EndVertical();
		return flag2;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x000081A8 File Offset: 0x000063A8
	private static bool Field(AuthorShared.Content content, ref SoftJointLimit limits, ref float offset)
	{
		GUI.Box(AuthorShared.BeginVertical(new GUILayoutOption[0]), GUIContent.none);
		AuthorShared.PrefixLabel(content);
		float limit = limits.limit;
		float spring = limits.spring;
		float damper = limits.damper;
		float bounciness = limits.bounciness;
		bool flag = AuthorShared.FloatField("Limit", ref limit, new GUILayoutOption[0]);
		flag |= AuthorShared.FloatField("Spring", ref spring, new GUILayoutOption[0]);
		flag |= AuthorShared.FloatField("Damper", ref damper, new GUILayoutOption[0]);
		flag |= AuthorShared.FloatField("Bounciness", ref bounciness, new GUILayoutOption[0]);
		if (flag)
		{
			limits.limit = limit;
			limits.spring = spring;
			limits.damper = damper;
			limits.bounciness = bounciness;
		}
		Color contentColor = GUI.contentColor;
		GUI.contentColor = contentColor * new Color(1f, 1f, 1f, 0.3f);
		flag |= AuthorShared.FloatField("Offset(visual only)", ref offset, new GUILayoutOption[0]);
		GUI.contentColor = contentColor;
		AuthorShared.EndVertical();
		return flag;
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x000082D4 File Offset: 0x000064D4
	protected override void OnPeiceDestroy()
	{
		try
		{
			if (this.self)
			{
				this.self.OnJointDestroy(this);
			}
		}
		finally
		{
			base.OnPeiceDestroy();
		}
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00008328 File Offset: 0x00006528
	public override bool PeiceInspectorGUI()
	{
		bool flag = base.PeiceInspectorGUI();
		string peiceID = base.peiceID;
		if (AuthorShared.StringField("Title", ref peiceID, new GUILayoutOption[0]))
		{
			base.peiceID = peiceID;
			flag = true;
		}
		AuthorShared.EnumField("Kind", this.kind, new GUILayoutOption[0]);
		AuthorShared.PrefixLabel("Self");
		if (GUILayout.Button(AuthorShared.ObjectContent<AuthorChHit>(this.self, typeof(AuthorChHit)), new GUILayoutOption[0]))
		{
			AuthorShared.PingObject(this.self);
		}
		flag |= AuthorShared.PeiceField<AuthorChHit>("Connected", this, ref this.connect, typeof(AuthorChHit), GUI.skin.button, new GUILayoutOption[0]);
		flag |= AuthorShared.Toggle("Reverse Link", ref this.reverseLink, new GUILayoutOption[0]);
		flag |= AuthorShared.Vector3Field("Anchor", ref this.anchor, new GUILayoutOption[0]);
		flag |= AuthorShared.Vector3Field("Axis", ref this.axis, new GUILayoutOption[0]);
		AuthorChJoint.Kind kind = this.kind;
		if (kind != AuthorChJoint.Kind.Hinge)
		{
			if (kind == AuthorChJoint.Kind.Character)
			{
				flag |= AuthorShared.Vector3Field("Swing Axis", ref this.swingAxis, new GUILayoutOption[0]);
				SoftJointLimit softJointLimit = this.lowTwist;
				if (AuthorChJoint.Field("Low Twist", ref softJointLimit, ref this.twistOffset))
				{
					flag = true;
					this.lowTwist = softJointLimit;
				}
				softJointLimit = this.highTwist;
				if (AuthorChJoint.Field("High Twist", ref softJointLimit, ref this.twistOffset))
				{
					flag = true;
					this.highTwist = softJointLimit;
				}
				softJointLimit = this.swing1;
				if (AuthorChJoint.Field("Swing 1", ref softJointLimit, ref this.swingOffset1))
				{
					flag = true;
					this.swing1 = softJointLimit;
				}
				softJointLimit = this.swing2;
				if (AuthorChJoint.Field("Swing 2", ref softJointLimit, ref this.swingOffset2))
				{
					flag = true;
					this.swing2 = softJointLimit;
				}
			}
		}
		else
		{
			JointLimits limit = this.limit;
			if (AuthorChJoint.Field("Limits", ref limit, ref this.useLimit, ref this.limitOffset))
			{
				flag = true;
				this.limit = limit;
			}
		}
		flag |= AuthorShared.FloatField("Break Force", ref this.breakForce, new GUILayoutOption[0]);
		return flag | AuthorShared.FloatField("Break Torque", ref this.breakTorque, new GUILayoutOption[0]);
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x000085C0 File Offset: 0x000067C0
	internal void InitializeFromOwner(AuthorChHit self, AuthorChJoint.Kind kind)
	{
		this.self = self;
		this.kind = kind;
		AuthorShared.SetDirty(this);
	}

	// Token: 0x040000BC RID: 188
	[SerializeField]
	private AuthorChHit self;

	// Token: 0x040000BD RID: 189
	[SerializeField]
	private AuthorChHit connect;

	// Token: 0x040000BE RID: 190
	[SerializeField]
	private AuthorChJoint.Kind kind;

	// Token: 0x040000BF RID: 191
	[SerializeField]
	private bool reverseLink;

	// Token: 0x040000C0 RID: 192
	[SerializeField]
	private Vector3 anchor;

	// Token: 0x040000C1 RID: 193
	[SerializeField]
	private Vector3 axis = Vector3.up;

	// Token: 0x040000C2 RID: 194
	[SerializeField]
	private Vector3 swingAxis = Vector3.forward;

	// Token: 0x040000C3 RID: 195
	[SerializeField]
	private float twistL_limit = -20f;

	// Token: 0x040000C4 RID: 196
	[SerializeField]
	private float twistL_bounce;

	// Token: 0x040000C5 RID: 197
	[SerializeField]
	private float twistL_dampler;

	// Token: 0x040000C6 RID: 198
	[SerializeField]
	private float twistL_spring;

	// Token: 0x040000C7 RID: 199
	[SerializeField]
	private float twistH_limit = 70f;

	// Token: 0x040000C8 RID: 200
	[SerializeField]
	private float twistH_bounce;

	// Token: 0x040000C9 RID: 201
	[SerializeField]
	private float twistH_dampler;

	// Token: 0x040000CA RID: 202
	[SerializeField]
	private float twistH_spring;

	// Token: 0x040000CB RID: 203
	[SerializeField]
	private float swing1_limit = 20f;

	// Token: 0x040000CC RID: 204
	[SerializeField]
	private float swing1_bounce;

	// Token: 0x040000CD RID: 205
	[SerializeField]
	private float swing1_dampler;

	// Token: 0x040000CE RID: 206
	[SerializeField]
	private float swing1_spring;

	// Token: 0x040000CF RID: 207
	[SerializeField]
	private float swing2_limit = 20f;

	// Token: 0x040000D0 RID: 208
	[SerializeField]
	private float swing2_bounce;

	// Token: 0x040000D1 RID: 209
	[SerializeField]
	private float swing2_dampler;

	// Token: 0x040000D2 RID: 210
	[SerializeField]
	private float swing2_spring;

	// Token: 0x040000D3 RID: 211
	[SerializeField]
	private float h_limit_min;

	// Token: 0x040000D4 RID: 212
	[SerializeField]
	private float h_limit_max;

	// Token: 0x040000D5 RID: 213
	[SerializeField]
	private float h_limit_minb;

	// Token: 0x040000D6 RID: 214
	[SerializeField]
	private float h_limit_maxb;

	// Token: 0x040000D7 RID: 215
	[SerializeField]
	private float h_spring_s;

	// Token: 0x040000D8 RID: 216
	[SerializeField]
	private float h_spring_d;

	// Token: 0x040000D9 RID: 217
	[SerializeField]
	private float h_spring_t;

	// Token: 0x040000DA RID: 218
	[SerializeField]
	private float h_motor_f;

	// Token: 0x040000DB RID: 219
	[SerializeField]
	private float h_motor_v;

	// Token: 0x040000DC RID: 220
	[SerializeField]
	private bool h_motor_s;

	// Token: 0x040000DD RID: 221
	[SerializeField]
	private float spring_spring;

	// Token: 0x040000DE RID: 222
	[SerializeField]
	private float spring_min;

	// Token: 0x040000DF RID: 223
	[SerializeField]
	private float spring_max;

	// Token: 0x040000E0 RID: 224
	[SerializeField]
	private float spring_damper;

	// Token: 0x040000E1 RID: 225
	[SerializeField]
	private bool useLimit;

	// Token: 0x040000E2 RID: 226
	[SerializeField]
	private bool useSpring;

	// Token: 0x040000E3 RID: 227
	[SerializeField]
	private bool useMotor;

	// Token: 0x040000E4 RID: 228
	[SerializeField]
	private float limitOffset;

	// Token: 0x040000E5 RID: 229
	[SerializeField]
	private float twistOffset;

	// Token: 0x040000E6 RID: 230
	[SerializeField]
	private float swingOffset1;

	// Token: 0x040000E7 RID: 231
	[SerializeField]
	private float swingOffset2;

	// Token: 0x040000E8 RID: 232
	[SerializeField]
	private float breakForce = float.PositiveInfinity;

	// Token: 0x040000E9 RID: 233
	[SerializeField]
	private float breakTorque = float.PositiveInfinity;

	// Token: 0x040000EA RID: 234
	private static readonly Color twistColor = new Color(1f, 1f, 0.4f, 0.8f);

	// Token: 0x040000EB RID: 235
	private static readonly Color swing1Color = new Color(1f, 0.4f, 1f, 0.8f);

	// Token: 0x040000EC RID: 236
	private static readonly Color swing2Color = new Color(0.4f, 1f, 1f, 0.8f);

	// Token: 0x02000027 RID: 39
	public enum Kind
	{
		// Token: 0x040000EE RID: 238
		None,
		// Token: 0x040000EF RID: 239
		Hinge,
		// Token: 0x040000F0 RID: 240
		Character,
		// Token: 0x040000F1 RID: 241
		Fixed,
		// Token: 0x040000F2 RID: 242
		Spring
	}
}
