using System;
using UnityEngine;

// Token: 0x0200062E RID: 1582
public static class LocalPrefs
{
	// Token: 0x17000AFF RID: 2815
	// (get) Token: 0x060037BA RID: 14266 RVA: 0x000CCB88 File Offset: 0x000CAD88
	public static LocalPrefs.CameraModes CameraMode
	{
		get
		{
			return LocalPrefs.h.cameraModes;
		}
	}

	// Token: 0x0200062F RID: 1583
	public static class FootCameraMode
	{
		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060037BB RID: 14267 RVA: 0x000CCB90 File Offset: 0x000CAD90
		// (set) Token: 0x060037BC RID: 14268 RVA: 0x000CCB9C File Offset: 0x000CAD9C
		public static global::FootCameraMode Current
		{
			get
			{
				return (global::FootCameraMode)LocalPrefs.g.GetValue(ref LocalPrefs.g.s.footCameraMode);
			}
			set
			{
				LocalPrefs.g.SetValue(ref LocalPrefs.g.s.footCameraMode, new int?((int)value));
			}
		}

		// Token: 0x060037BD RID: 14269 RVA: 0x000CCBB0 File Offset: 0x000CADB0
		public static void Reset()
		{
			LocalPrefs.g.s.footCameraMode.Reset();
		}
	}

	// Token: 0x02000630 RID: 1584
	public static class MountedWeaponCameraMode
	{
		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x000CCBBC File Offset: 0x000CADBC
		// (set) Token: 0x060037BF RID: 14271 RVA: 0x000CCBC8 File Offset: 0x000CADC8
		public static global::MountedWeaponCameraMode Current
		{
			get
			{
				return (global::MountedWeaponCameraMode)LocalPrefs.g.GetValue(ref LocalPrefs.g.s.mountedWeaponCameraMode);
			}
			set
			{
				LocalPrefs.g.SetValue(ref LocalPrefs.g.s.mountedWeaponCameraMode, new int?((int)value));
			}
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000CCBDC File Offset: 0x000CADDC
		public static void Reset()
		{
			LocalPrefs.g.s.mountedWeaponCameraMode.Reset();
		}
	}

	// Token: 0x02000631 RID: 1585
	public static class CarCameraMode
	{
		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x000CCBE8 File Offset: 0x000CADE8
		// (set) Token: 0x060037C2 RID: 14274 RVA: 0x000CCBF4 File Offset: 0x000CADF4
		public static global::CarCameraMode Current
		{
			get
			{
				return (global::CarCameraMode)LocalPrefs.g.GetValue(ref LocalPrefs.g.s.carCameraMode);
			}
			set
			{
				LocalPrefs.g.SetValue(ref LocalPrefs.g.s.carCameraMode, new int?((int)value));
			}
		}

		// Token: 0x060037C3 RID: 14275 RVA: 0x000CCC08 File Offset: 0x000CAE08
		public static void Reset()
		{
			LocalPrefs.g.s.carCameraMode.Reset();
		}
	}

	// Token: 0x02000632 RID: 1586
	public static class JetCameraMode
	{
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060037C4 RID: 14276 RVA: 0x000CCC14 File Offset: 0x000CAE14
		// (set) Token: 0x060037C5 RID: 14277 RVA: 0x000CCC20 File Offset: 0x000CAE20
		public static global::JetCameraMode Current
		{
			get
			{
				return (global::JetCameraMode)LocalPrefs.g.GetValue(ref LocalPrefs.g.s.jetCameraMode);
			}
			set
			{
				LocalPrefs.g.SetValue(ref LocalPrefs.g.s.jetCameraMode, new int?((int)value));
			}
		}

		// Token: 0x060037C6 RID: 14278 RVA: 0x000CCC34 File Offset: 0x000CAE34
		public static void Reset()
		{
			LocalPrefs.g.s.jetCameraMode.Reset();
		}
	}

	// Token: 0x02000633 RID: 1587
	public static class HeliCameraMode
	{
		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060037C7 RID: 14279 RVA: 0x000CCC40 File Offset: 0x000CAE40
		// (set) Token: 0x060037C8 RID: 14280 RVA: 0x000CCC4C File Offset: 0x000CAE4C
		public static global::HeliCameraMode Current
		{
			get
			{
				return (global::HeliCameraMode)LocalPrefs.g.GetValue(ref LocalPrefs.g.s.heliCameraMode);
			}
			set
			{
				LocalPrefs.g.SetValue(ref LocalPrefs.g.s.heliCameraMode, new int?((int)value));
			}
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000CCC60 File Offset: 0x000CAE60
		public static void Reset()
		{
			LocalPrefs.g.s.heliCameraMode.Reset();
		}
	}

	// Token: 0x02000634 RID: 1588
	public static class BoatCameraMode
	{
		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060037CA RID: 14282 RVA: 0x000CCC6C File Offset: 0x000CAE6C
		// (set) Token: 0x060037CB RID: 14283 RVA: 0x000CCC78 File Offset: 0x000CAE78
		public static global::BoatCameraMode Current
		{
			get
			{
				return (global::BoatCameraMode)LocalPrefs.g.GetValue(ref LocalPrefs.g.s.boatCameraMode);
			}
			set
			{
				LocalPrefs.g.SetValue(ref LocalPrefs.g.s.boatCameraMode, new int?((int)value));
			}
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000CCC8C File Offset: 0x000CAE8C
		public static void Reset()
		{
			LocalPrefs.g.s.boatCameraMode.Reset();
		}
	}

	// Token: 0x02000635 RID: 1589
	public class CameraModes
	{
		// Token: 0x17000B06 RID: 2822
		public SharedCameraMode this[KindOfCamera kind]
		{
			get
			{
				switch (kind)
				{
				case KindOfCamera.Foot:
					return (SharedCameraMode)LocalPrefs.FootCameraMode.Current;
				case KindOfCamera.MountedWeapon:
					return (SharedCameraMode)LocalPrefs.MountedWeaponCameraMode.Current;
				case KindOfCamera.Car:
					return (SharedCameraMode)LocalPrefs.CarCameraMode.Current;
				case KindOfCamera.Boat:
					return (SharedCameraMode)LocalPrefs.BoatCameraMode.Current;
				case KindOfCamera.Jet:
					return (SharedCameraMode)LocalPrefs.JetCameraMode.Current;
				case KindOfCamera.Heli:
					return (SharedCameraMode)LocalPrefs.HeliCameraMode.Current;
				}
				return SharedCameraMode.Undefined;
			}
			set
			{
				switch (kind)
				{
				case KindOfCamera.Foot:
					LocalPrefs.FootCameraMode.Current = (global::FootCameraMode)value;
					break;
				case KindOfCamera.MountedWeapon:
					LocalPrefs.MountedWeaponCameraMode.Current = (global::MountedWeaponCameraMode)value;
					break;
				case KindOfCamera.Car:
					LocalPrefs.CarCameraMode.Current = (global::CarCameraMode)value;
					break;
				case KindOfCamera.Boat:
					LocalPrefs.BoatCameraMode.Current = (global::BoatCameraMode)value;
					break;
				case KindOfCamera.Jet:
					LocalPrefs.JetCameraMode.Current = (global::JetCameraMode)value;
					break;
				case KindOfCamera.Heli:
					LocalPrefs.HeliCameraMode.Current = (global::HeliCameraMode)value;
					break;
				}
			}
		}
	}

	// Token: 0x02000636 RID: 1590
	private static class h
	{
		// Token: 0x04001C01 RID: 7169
		public static readonly LocalPrefs.CameraModes cameraModes = new LocalPrefs.CameraModes();
	}

	// Token: 0x02000637 RID: 1591
	private static class g
	{
		// Token: 0x060037D1 RID: 14289 RVA: 0x000CCD80 File Offset: 0x000CAF80
		private static bool NeedSetValue<T>(ref LocalPrefs.g.KeyDefault<T> k, bool isNull)
		{
			if (isNull)
			{
				if (k.set || k.Init())
				{
					k.Reset();
				}
				return false;
			}
			k.Init();
			return true;
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000CCDBC File Offset: 0x000CAFBC
		private static bool NeedSetValue<T>(ref LocalPrefs.g.KeyDefault<T> k, T? value) where T : struct
		{
			if (LocalPrefs.g.NeedSetValue<T>(ref k, value == null))
			{
				k.value = value.Value;
				return true;
			}
			return false;
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000CCDE4 File Offset: 0x000CAFE4
		private static bool NeedSetValue<T>(ref LocalPrefs.g.KeyDefault<T> k, T value) where T : class
		{
			if (LocalPrefs.g.NeedSetValue<T>(ref k, value == null))
			{
				k.value = value;
				return true;
			}
			return false;
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000CCE04 File Offset: 0x000CB004
		public static int GetValue(ref LocalPrefs.g.KeyDefault<int> k)
		{
			if (k.Init())
			{
				k.value = PlayerPrefs.GetInt(k.key);
			}
			return k.value;
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000CCE34 File Offset: 0x000CB034
		public static float GetValue(ref LocalPrefs.g.KeyDefault<float> k)
		{
			if (k.Init())
			{
				k.value = PlayerPrefs.GetFloat(k.key);
			}
			return k.value;
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000CCE64 File Offset: 0x000CB064
		public static string GetValue(ref LocalPrefs.g.KeyDefault<string> k)
		{
			if (k.Init())
			{
				k.value = PlayerPrefs.GetString(k.key);
			}
			return k.value;
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000CCE94 File Offset: 0x000CB094
		public static bool GetValue(ref LocalPrefs.g.KeyDefault<bool> k)
		{
			if (k.Init())
			{
				k.value = (PlayerPrefs.GetInt(k.key) != 0);
			}
			return k.value;
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000CCECC File Offset: 0x000CB0CC
		public static void SetValue(ref LocalPrefs.g.KeyDefault<int> k, int? value)
		{
			if ((LocalPrefs.g.NeedSetValue<int>(ref k, value) && !k.set) || k.value != value.Value)
			{
				PlayerPrefs.SetInt(k.key, value.Value);
			}
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000CCF0C File Offset: 0x000CB10C
		public static void SetValue(ref LocalPrefs.g.KeyDefault<float> k, float? value)
		{
			if ((LocalPrefs.g.NeedSetValue<float>(ref k, value) && !k.set) || k.value != value.Value)
			{
				PlayerPrefs.SetFloat(k.key, value.Value);
			}
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000CCF4C File Offset: 0x000CB14C
		public static void SetValue(ref LocalPrefs.g.KeyDefault<string> k, string value)
		{
			if ((LocalPrefs.g.NeedSetValue<string>(ref k, value) && !k.set) || k.value != value)
			{
				PlayerPrefs.SetString(k.key, value);
			}
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000CCF90 File Offset: 0x000CB190
		public static void SetValue(ref LocalPrefs.g.KeyDefault<bool> k, bool? value)
		{
			if ((LocalPrefs.g.NeedSetValue<bool>(ref k, value) && !k.set) || k.value != value.Value)
			{
				PlayerPrefs.SetInt(k.key, (!value.Value) ? 0 : 1);
			}
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000CCFE4 File Offset: 0x000CB1E4
		private static LocalPrefs.g.KeyDefault<T> New<T>(string key, T @default)
		{
			return new LocalPrefs.g.KeyDefault<T>(key, @default);
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000CCFF0 File Offset: 0x000CB1F0
		private static LocalPrefs.g.KeyDefault<int> New(string key, Enum @default)
		{
			return new LocalPrefs.g.KeyDefault<int>(key, Convert.ToInt32(@default));
		}

		// Token: 0x02000638 RID: 1592
		public struct KeyDefault<T>
		{
			// Token: 0x060037DE RID: 14302 RVA: 0x000CD000 File Offset: 0x000CB200
			public KeyDefault(string key, T value)
			{
				this.key = key;
				this.@default = value;
				this.value = this.@default;
				this.uninit = false;
				this.set = false;
			}

			// Token: 0x060037DF RID: 14303 RVA: 0x000CD038 File Offset: 0x000CB238
			public bool Init()
			{
				if (!this.uninit)
				{
					return false;
				}
				this.set = PlayerPrefs.HasKey(this.key);
				this.uninit = false;
				if (this.set)
				{
					return true;
				}
				this.value = this.@default;
				return false;
			}

			// Token: 0x060037E0 RID: 14304 RVA: 0x000CD084 File Offset: 0x000CB284
			public void Reset()
			{
				if (this.set || this.Init())
				{
					PlayerPrefs.DeleteKey(this.key);
					this.set = false;
					this.value = this.@default;
				}
			}

			// Token: 0x04001C02 RID: 7170
			public readonly string key;

			// Token: 0x04001C03 RID: 7171
			public readonly T @default;

			// Token: 0x04001C04 RID: 7172
			public T value;

			// Token: 0x04001C05 RID: 7173
			public bool uninit;

			// Token: 0x04001C06 RID: 7174
			public bool set;
		}

		// Token: 0x02000639 RID: 1593
		public static class s
		{
			// Token: 0x04001C07 RID: 7175
			public static LocalPrefs.g.KeyDefault<int> footCameraMode = LocalPrefs.g.New<int>("foot_cam", 1);

			// Token: 0x04001C08 RID: 7176
			public static LocalPrefs.g.KeyDefault<int> mountedWeaponCameraMode = LocalPrefs.g.New<int>("mwep_cam", 1);

			// Token: 0x04001C09 RID: 7177
			public static LocalPrefs.g.KeyDefault<int> carCameraMode = LocalPrefs.g.New<int>("car_cam", 2);

			// Token: 0x04001C0A RID: 7178
			public static LocalPrefs.g.KeyDefault<int> passengerCameraMode = LocalPrefs.g.New<int>("pass_cam", 1);

			// Token: 0x04001C0B RID: 7179
			public static LocalPrefs.g.KeyDefault<int> jetCameraMode = LocalPrefs.g.New<int>("jet_cam", 2);

			// Token: 0x04001C0C RID: 7180
			public static LocalPrefs.g.KeyDefault<int> heliCameraMode = LocalPrefs.g.New<int>("heli_cam", 2);

			// Token: 0x04001C0D RID: 7181
			public static LocalPrefs.g.KeyDefault<int> boatCameraMode = LocalPrefs.g.New<int>("boat_cam", 2);
		}
	}
}
