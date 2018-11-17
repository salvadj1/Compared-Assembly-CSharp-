using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
public class SoundEffect : ScriptableObject
{
	// Token: 0x020000D4 RID: 212
	public enum ParentMode
	{
		// Token: 0x04000417 RID: 1047
		None,
		// Token: 0x04000418 RID: 1048
		RetainLocal,
		// Token: 0x04000419 RID: 1049
		RetainWorld = 3,
		// Token: 0x0400041A RID: 1050
		StartLocally = 5,
		// Token: 0x0400041B RID: 1051
		StartWorld,
		// Token: 0x0400041C RID: 1052
		CameraLocally = 9,
		// Token: 0x0400041D RID: 1053
		CameraWorld
	}

	// Token: 0x020000D5 RID: 213
	public struct Parent
	{
		// Token: 0x0400041E RID: 1054
		public Transform transform;

		// Token: 0x0400041F RID: 1055
		public SoundEffect.ParentMode mode;
	}

	// Token: 0x020000D6 RID: 214
	public struct Levels
	{
		// Token: 0x04000420 RID: 1056
		public float volume;

		// Token: 0x04000421 RID: 1057
		public float pitch;

		// Token: 0x04000422 RID: 1058
		public float pan;

		// Token: 0x04000423 RID: 1059
		public float doppler;

		// Token: 0x04000424 RID: 1060
		public float spread;
	}

	// Token: 0x020000D7 RID: 215
	public struct MinMax
	{
		// Token: 0x04000425 RID: 1061
		public float min;

		// Token: 0x04000426 RID: 1062
		public float max;
	}

	// Token: 0x020000D8 RID: 216
	public struct Rolloff
	{
		// Token: 0x04000427 RID: 1063
		public const float kCutoffVolume = 0.001f;

		// Token: 0x04000428 RID: 1064
		public SoundEffect.MinMax distance;

		// Token: 0x04000429 RID: 1065
		public float? manualCutoffDistance;

		// Token: 0x0400042A RID: 1066
		public bool logarithmic;
	}

	// Token: 0x020000D9 RID: 217
	public struct Parameters
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00017090 File Offset: 0x00015290
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x000170D4 File Offset: 0x000152D4
		public Vector3 position
		{
			get
			{
				SoundEffect.ParentMode parentMode = this.parent.mode & SoundEffect.ParentMode.RetainWorld;
				if (parentMode != SoundEffect.ParentMode.RetainLocal)
				{
					return this.positionalValue;
				}
				return this.parent.transform.TransformPoint(this.positionalValue);
			}
			set
			{
				SoundEffect.ParentMode parentMode = this.parent.mode & SoundEffect.ParentMode.RetainWorld;
				if (parentMode != SoundEffect.ParentMode.RetainLocal)
				{
					this.positionalValue = value;
				}
				else
				{
					this.positionalValue = this.parent.transform.InverseTransformPoint(value);
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x00017124 File Offset: 0x00015324
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x00017168 File Offset: 0x00015368
		public Vector3 localPosition
		{
			get
			{
				SoundEffect.ParentMode parentMode = this.parent.mode & SoundEffect.ParentMode.RetainWorld;
				if (parentMode != SoundEffect.ParentMode.RetainWorld)
				{
					return this.positionalValue;
				}
				return this.parent.transform.InverseTransformPoint(this.positionalValue);
			}
			set
			{
				SoundEffect.ParentMode parentMode = this.parent.mode & SoundEffect.ParentMode.RetainWorld;
				if (parentMode != SoundEffect.ParentMode.RetainWorld)
				{
					this.positionalValue = value;
				}
				else
				{
					this.positionalValue = this.parent.transform.TransformPoint(value);
				}
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000171B8 File Offset: 0x000153B8
		private static Quaternion TransformQuaternion(Transform transform, Quaternion rotation)
		{
			return transform.rotation * rotation;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000171C8 File Offset: 0x000153C8
		private static Quaternion InverseTransformQuaternion(Transform transform, Quaternion rotation)
		{
			return rotation * Quaternion.Inverse(transform.rotation);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000171DC File Offset: 0x000153DC
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x00017220 File Offset: 0x00015420
		public Quaternion rotation
		{
			get
			{
				SoundEffect.ParentMode parentMode = this.parent.mode & SoundEffect.ParentMode.RetainWorld;
				if (parentMode != SoundEffect.ParentMode.RetainLocal)
				{
					return this.rotationalValue;
				}
				return SoundEffect.Parameters.TransformQuaternion(this.parent.transform, this.rotationalValue);
			}
			set
			{
				SoundEffect.ParentMode parentMode = this.parent.mode & SoundEffect.ParentMode.RetainWorld;
				if (parentMode != SoundEffect.ParentMode.RetainLocal)
				{
					this.rotationalValue = value;
				}
				else
				{
					this.rotationalValue = SoundEffect.Parameters.InverseTransformQuaternion(this.parent.transform, value);
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x00017270 File Offset: 0x00015470
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x000172B4 File Offset: 0x000154B4
		public Quaternion localRotation
		{
			get
			{
				SoundEffect.ParentMode parentMode = this.parent.mode & SoundEffect.ParentMode.RetainWorld;
				if (parentMode != SoundEffect.ParentMode.RetainWorld)
				{
					return this.rotationalValue;
				}
				return SoundEffect.Parameters.InverseTransformQuaternion(this.parent.transform, this.rotationalValue);
			}
			set
			{
				SoundEffect.ParentMode parentMode = this.parent.mode & SoundEffect.ParentMode.RetainWorld;
				if (parentMode != SoundEffect.ParentMode.RetainWorld)
				{
					this.rotationalValue = value;
				}
				else
				{
					this.rotationalValue = SoundEffect.Parameters.TransformQuaternion(this.parent.transform, value);
				}
			}
		}

		// Token: 0x0400042B RID: 1067
		public AudioClip clip;

		// Token: 0x0400042C RID: 1068
		public SoundEffect.Parent parent;

		// Token: 0x0400042D RID: 1069
		public SoundEffect.Levels levels;

		// Token: 0x0400042E RID: 1070
		public SoundEffect.Rolloff rolloff;

		// Token: 0x0400042F RID: 1071
		public int priority;

		// Token: 0x04000430 RID: 1072
		public bool bypassEffects;

		// Token: 0x04000431 RID: 1073
		public bool bypassListenerVolume;

		// Token: 0x04000432 RID: 1074
		public Vector3 positionalValue;

		// Token: 0x04000433 RID: 1075
		public Quaternion rotationalValue;
	}
}
