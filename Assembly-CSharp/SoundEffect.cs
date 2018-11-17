using System;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class SoundEffect : ScriptableObject
{
	// Token: 0x020000E8 RID: 232
	public enum ParentMode
	{
		// Token: 0x04000486 RID: 1158
		None,
		// Token: 0x04000487 RID: 1159
		RetainLocal,
		// Token: 0x04000488 RID: 1160
		RetainWorld = 3,
		// Token: 0x04000489 RID: 1161
		StartLocally = 5,
		// Token: 0x0400048A RID: 1162
		StartWorld,
		// Token: 0x0400048B RID: 1163
		CameraLocally = 9,
		// Token: 0x0400048C RID: 1164
		CameraWorld
	}

	// Token: 0x020000E9 RID: 233
	public struct Parent
	{
		// Token: 0x0400048D RID: 1165
		public Transform transform;

		// Token: 0x0400048E RID: 1166
		public global::SoundEffect.ParentMode mode;
	}

	// Token: 0x020000EA RID: 234
	public struct Levels
	{
		// Token: 0x0400048F RID: 1167
		public float volume;

		// Token: 0x04000490 RID: 1168
		public float pitch;

		// Token: 0x04000491 RID: 1169
		public float pan;

		// Token: 0x04000492 RID: 1170
		public float doppler;

		// Token: 0x04000493 RID: 1171
		public float spread;
	}

	// Token: 0x020000EB RID: 235
	public struct MinMax
	{
		// Token: 0x04000494 RID: 1172
		public float min;

		// Token: 0x04000495 RID: 1173
		public float max;
	}

	// Token: 0x020000EC RID: 236
	public struct Rolloff
	{
		// Token: 0x04000496 RID: 1174
		public const float kCutoffVolume = 0.001f;

		// Token: 0x04000497 RID: 1175
		public global::SoundEffect.MinMax distance;

		// Token: 0x04000498 RID: 1176
		public float? manualCutoffDistance;

		// Token: 0x04000499 RID: 1177
		public bool logarithmic;
	}

	// Token: 0x020000ED RID: 237
	public struct Parameters
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00018A58 File Offset: 0x00016C58
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00018A9C File Offset: 0x00016C9C
		public Vector3 position
		{
			get
			{
				global::SoundEffect.ParentMode parentMode = this.parent.mode & global::SoundEffect.ParentMode.RetainWorld;
				if (parentMode != global::SoundEffect.ParentMode.RetainLocal)
				{
					return this.positionalValue;
				}
				return this.parent.transform.TransformPoint(this.positionalValue);
			}
			set
			{
				global::SoundEffect.ParentMode parentMode = this.parent.mode & global::SoundEffect.ParentMode.RetainWorld;
				if (parentMode != global::SoundEffect.ParentMode.RetainLocal)
				{
					this.positionalValue = value;
				}
				else
				{
					this.positionalValue = this.parent.transform.InverseTransformPoint(value);
				}
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00018AEC File Offset: 0x00016CEC
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x00018B30 File Offset: 0x00016D30
		public Vector3 localPosition
		{
			get
			{
				global::SoundEffect.ParentMode parentMode = this.parent.mode & global::SoundEffect.ParentMode.RetainWorld;
				if (parentMode != global::SoundEffect.ParentMode.RetainWorld)
				{
					return this.positionalValue;
				}
				return this.parent.transform.InverseTransformPoint(this.positionalValue);
			}
			set
			{
				global::SoundEffect.ParentMode parentMode = this.parent.mode & global::SoundEffect.ParentMode.RetainWorld;
				if (parentMode != global::SoundEffect.ParentMode.RetainWorld)
				{
					this.positionalValue = value;
				}
				else
				{
					this.positionalValue = this.parent.transform.TransformPoint(value);
				}
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00018B80 File Offset: 0x00016D80
		private static Quaternion TransformQuaternion(Transform transform, Quaternion rotation)
		{
			return transform.rotation * rotation;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00018B90 File Offset: 0x00016D90
		private static Quaternion InverseTransformQuaternion(Transform transform, Quaternion rotation)
		{
			return rotation * Quaternion.Inverse(transform.rotation);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x00018BA4 File Offset: 0x00016DA4
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x00018BE8 File Offset: 0x00016DE8
		public Quaternion rotation
		{
			get
			{
				global::SoundEffect.ParentMode parentMode = this.parent.mode & global::SoundEffect.ParentMode.RetainWorld;
				if (parentMode != global::SoundEffect.ParentMode.RetainLocal)
				{
					return this.rotationalValue;
				}
				return global::SoundEffect.Parameters.TransformQuaternion(this.parent.transform, this.rotationalValue);
			}
			set
			{
				global::SoundEffect.ParentMode parentMode = this.parent.mode & global::SoundEffect.ParentMode.RetainWorld;
				if (parentMode != global::SoundEffect.ParentMode.RetainLocal)
				{
					this.rotationalValue = value;
				}
				else
				{
					this.rotationalValue = global::SoundEffect.Parameters.InverseTransformQuaternion(this.parent.transform, value);
				}
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00018C38 File Offset: 0x00016E38
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x00018C7C File Offset: 0x00016E7C
		public Quaternion localRotation
		{
			get
			{
				global::SoundEffect.ParentMode parentMode = this.parent.mode & global::SoundEffect.ParentMode.RetainWorld;
				if (parentMode != global::SoundEffect.ParentMode.RetainWorld)
				{
					return this.rotationalValue;
				}
				return global::SoundEffect.Parameters.InverseTransformQuaternion(this.parent.transform, this.rotationalValue);
			}
			set
			{
				global::SoundEffect.ParentMode parentMode = this.parent.mode & global::SoundEffect.ParentMode.RetainWorld;
				if (parentMode != global::SoundEffect.ParentMode.RetainWorld)
				{
					this.rotationalValue = value;
				}
				else
				{
					this.rotationalValue = global::SoundEffect.Parameters.TransformQuaternion(this.parent.transform, value);
				}
			}
		}

		// Token: 0x0400049A RID: 1178
		public AudioClip clip;

		// Token: 0x0400049B RID: 1179
		public global::SoundEffect.Parent parent;

		// Token: 0x0400049C RID: 1180
		public global::SoundEffect.Levels levels;

		// Token: 0x0400049D RID: 1181
		public global::SoundEffect.Rolloff rolloff;

		// Token: 0x0400049E RID: 1182
		public int priority;

		// Token: 0x0400049F RID: 1183
		public bool bypassEffects;

		// Token: 0x040004A0 RID: 1184
		public bool bypassListenerVolume;

		// Token: 0x040004A1 RID: 1185
		public Vector3 positionalValue;

		// Token: 0x040004A2 RID: 1186
		public Quaternion rotationalValue;
	}
}
