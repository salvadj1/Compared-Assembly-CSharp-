using System;
using UnityEngine;

// Token: 0x020006F1 RID: 1777
public static class LocalPrefs
{
	// Token: 0x17000B7F RID: 2943
	// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x000D5438 File Offset: 0x000D3638
	public static global::LocalPrefs.CameraModes CameraMode
	{
		get
		{
			return global::LocalPrefs.h.cameraModes;
		}
	}

	// Token: 0x020006F2 RID: 1778
	public static class FootCameraMode
	{
		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x000D5440 File Offset: 0x000D3640
		// (set) Token: 0x06003BA8 RID: 15272 RVA: 0x000D544C File Offset: 0x000D364C
		public static global::FootCameraMode Current
		{
			get
			{
				return (global::FootCameraMode)global::LocalPrefs.g.GetValue(ref global::LocalPrefs.g.s.footCameraMode);
			}
			set
			{
				global::LocalPrefs.g.SetValue(ref global::LocalPrefs.g.s.footCameraMode, new int?((int)value));
			}
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000D5460 File Offset: 0x000D3660
		public static void Reset()
		{
			global::LocalPrefs.g.s.footCameraMode.Reset();
		}
	}

	// Token: 0x020006F3 RID: 1779
	public static class MountedWeaponCameraMode
	{
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x000D546C File Offset: 0x000D366C
		// (set) Token: 0x06003BAB RID: 15275 RVA: 0x000D5478 File Offset: 0x000D3678
		public static global::MountedWeaponCameraMode Current
		{
			get
			{
				return (global::MountedWeaponCameraMode)global::LocalPrefs.g.GetValue(ref global::LocalPrefs.g.s.mountedWeaponCameraMode);
			}
			set
			{
				global::LocalPrefs.g.SetValue(ref global::LocalPrefs.g.s.mountedWeaponCameraMode, new int?((int)value));
			}
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x000D548C File Offset: 0x000D368C
		public static void Reset()
		{
			global::LocalPrefs.g.s.mountedWeaponCameraMode.Reset();
		}
	}

	// Token: 0x020006F4 RID: 1780
	public static class CarCameraMode
	{
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x000D5498 File Offset: 0x000D3698
		// (set) Token: 0x06003BAE RID: 15278 RVA: 0x000D54A4 File Offset: 0x000D36A4
		public static global::CarCameraMode Current
		{
			get
			{
				return (global::CarCameraMode)global::LocalPrefs.g.GetValue(ref global::LocalPrefs.g.s.carCameraMode);
			}
			set
			{
				global::LocalPrefs.g.SetValue(ref global::LocalPrefs.g.s.carCameraMode, new int?((int)value));
			}
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000D54B8 File Offset: 0x000D36B8
		public static void Reset()
		{
			global::LocalPrefs.g.s.carCameraMode.Reset();
		}
	}

	// Token: 0x020006F5 RID: 1781
	public static class JetCameraMode
	{
		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x000D54C4 File Offset: 0x000D36C4
		// (set) Token: 0x06003BB1 RID: 15281 RVA: 0x000D54D0 File Offset: 0x000D36D0
		public static global::JetCameraMode Current
		{
			get
			{
				return (global::JetCameraMode)global::LocalPrefs.g.GetValue(ref global::LocalPrefs.g.s.jetCameraMode);
			}
			set
			{
				global::LocalPrefs.g.SetValue(ref global::LocalPrefs.g.s.jetCameraMode, new int?((int)value));
			}
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x000D54E4 File Offset: 0x000D36E4
		public static void Reset()
		{
			global::LocalPrefs.g.s.jetCameraMode.Reset();
		}
	}

	// Token: 0x020006F6 RID: 1782
	public static class HeliCameraMode
	{
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x000D54F0 File Offset: 0x000D36F0
		// (set) Token: 0x06003BB4 RID: 15284 RVA: 0x000D54FC File Offset: 0x000D36FC
		public static global::HeliCameraMode Current
		{
			get
			{
				return (global::HeliCameraMode)global::LocalPrefs.g.GetValue(ref global::LocalPrefs.g.s.heliCameraMode);
			}
			set
			{
				global::LocalPrefs.g.SetValue(ref global::LocalPrefs.g.s.heliCameraMode, new int?((int)value));
			}
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x000D5510 File Offset: 0x000D3710
		public static void Reset()
		{
			global::LocalPrefs.g.s.heliCameraMode.Reset();
		}
	}

	// Token: 0x020006F7 RID: 1783
	public static class BoatCameraMode
	{
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x000D551C File Offset: 0x000D371C
		// (set) Token: 0x06003BB7 RID: 15287 RVA: 0x000D5528 File Offset: 0x000D3728
		public static global::BoatCameraMode Current
		{
			get
			{
				return (global::BoatCameraMode)global::LocalPrefs.g.GetValue(ref global::LocalPrefs.g.s.boatCameraMode);
			}
			set
			{
				global::LocalPrefs.g.SetValue(ref global::LocalPrefs.g.s.boatCameraMode, new int?((int)value));
			}
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x000D553C File Offset: 0x000D373C
		public static void Reset()
		{
			global::LocalPrefs.g.s.boatCameraMode.Reset();
		}
	}

	// Token: 0x020006F8 RID: 1784
	public class CameraModes
	{
		// Token: 0x17000B86 RID: 2950
		public global::SharedCameraMode this[global::KindOfCamera kind]
		{
			get
			{
				switch (kind)
				{
				case global::KindOfCamera.Foot:
					return (global::SharedCameraMode)global::LocalPrefs.FootCameraMode.Current;
				case global::KindOfCamera.MountedWeapon:
					return (global::SharedCameraMode)global::LocalPrefs.MountedWeaponCameraMode.Current;
				case global::KindOfCamera.Car:
					return (global::SharedCameraMode)global::LocalPrefs.CarCameraMode.Current;
				case global::KindOfCamera.Boat:
					return (global::SharedCameraMode)global::LocalPrefs.BoatCameraMode.Current;
				case global::KindOfCamera.Jet:
					return (global::SharedCameraMode)global::LocalPrefs.JetCameraMode.Current;
				case global::KindOfCamera.Heli:
					return (global::SharedCameraMode)global::LocalPrefs.HeliCameraMode.Current;
				}
				return global::SharedCameraMode.Undefined;
			}
			set
			{
				switch (kind)
				{
				case global::KindOfCamera.Foot:
					global::LocalPrefs.FootCameraMode.Current = (global::FootCameraMode)value;
					break;
				case global::KindOfCamera.MountedWeapon:
					global::LocalPrefs.MountedWeaponCameraMode.Current = (global::MountedWeaponCameraMode)value;
					break;
				case global::KindOfCamera.Car:
					global::LocalPrefs.CarCameraMode.Current = (global::CarCameraMode)value;
					break;
				case global::KindOfCamera.Boat:
					global::LocalPrefs.BoatCameraMode.Current = (global::BoatCameraMode)value;
					break;
				case global::KindOfCamera.Jet:
					global::LocalPrefs.JetCameraMode.Current = (global::JetCameraMode)value;
					break;
				case global::KindOfCamera.Heli:
					global::LocalPrefs.HeliCameraMode.Current = (global::HeliCameraMode)value;
					break;
				}
			}
		}
	}

	// Token: 0x020006F9 RID: 1785
	private static class h
	{
		// Token: 0x04001DF6 RID: 7670
		public static readonly global::LocalPrefs.CameraModes cameraModes = new global::LocalPrefs.CameraModes();
	}

	// Token: 0x020006FA RID: 1786
	private static class g
	{
		// Token: 0x06003BBD RID: 15293 RVA: 0x000D5630 File Offset: 0x000D3830
		private static bool NeedSetValue<T>(ref global::LocalPrefs.g.KeyDefault<T> k, bool isNull)
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

		// Token: 0x06003BBE RID: 15294 RVA: 0x000D566C File Offset: 0x000D386C
		private static bool NeedSetValue<T>(ref global::LocalPrefs.g.KeyDefault<T> k, T? value) where T : struct
		{
			if (global::LocalPrefs.g.NeedSetValue<T>(ref k, value == null))
			{
				k.value = value.Value;
				return true;
			}
			return false;
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x000D5694 File Offset: 0x000D3894
		private static bool NeedSetValue<T>(ref global::LocalPrefs.g.KeyDefault<T> k, T value) where T : class
		{
			if (global::LocalPrefs.g.NeedSetValue<T>(ref k, value == null))
			{
				k.value = value;
				return true;
			}
			return false;
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x000D56B4 File Offset: 0x000D38B4
		public static int GetValue(ref global::LocalPrefs.g.KeyDefault<int> k)
		{
			if (k.Init())
			{
				k.value = PlayerPrefs.GetInt(k.key);
			}
			return k.value;
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x000D56E4 File Offset: 0x000D38E4
		public static float GetValue(ref global::LocalPrefs.g.KeyDefault<float> k)
		{
			if (k.Init())
			{
				k.value = PlayerPrefs.GetFloat(k.key);
			}
			return k.value;
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x000D5714 File Offset: 0x000D3914
		public static string GetValue(ref global::LocalPrefs.g.KeyDefault<string> k)
		{
			if (k.Init())
			{
				k.value = PlayerPrefs.GetString(k.key);
			}
			return k.value;
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x000D5744 File Offset: 0x000D3944
		public static bool GetValue(ref global::LocalPrefs.g.KeyDefault<bool> k)
		{
			if (k.Init())
			{
				k.value = (PlayerPrefs.GetInt(k.key) != 0);
			}
			return k.value;
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x000D577C File Offset: 0x000D397C
		public static void SetValue(ref global::LocalPrefs.g.KeyDefault<int> k, int? value)
		{
			if ((global::LocalPrefs.g.NeedSetValue<int>(ref k, value) && !k.set) || k.value != value.Value)
			{
				PlayerPrefs.SetInt(k.key, value.Value);
			}
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000D57BC File Offset: 0x000D39BC
		public static void SetValue(ref global::LocalPrefs.g.KeyDefault<float> k, float? value)
		{
			if ((global::LocalPrefs.g.NeedSetValue<float>(ref k, value) && !k.set) || k.value != value.Value)
			{
				PlayerPrefs.SetFloat(k.key, value.Value);
			}
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000D57FC File Offset: 0x000D39FC
		public static void SetValue(ref global::LocalPrefs.g.KeyDefault<string> k, string value)
		{
			if ((global::LocalPrefs.g.NeedSetValue<string>(ref k, value) && !k.set) || k.value != value)
			{
				PlayerPrefs.SetString(k.key, value);
			}
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x000D5840 File Offset: 0x000D3A40
		public static void SetValue(ref global::LocalPrefs.g.KeyDefault<bool> k, bool? value)
		{
			if ((global::LocalPrefs.g.NeedSetValue<bool>(ref k, value) && !k.set) || k.value != value.Value)
			{
				PlayerPrefs.SetInt(k.key, (!value.Value) ? 0 : 1);
			}
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x000D5894 File Offset: 0x000D3A94
		private static global::LocalPrefs.g.KeyDefault<T> New<T>(string key, T @default)
		{
			return new global::LocalPrefs.g.KeyDefault<T>(key, @default);
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x000D58A0 File Offset: 0x000D3AA0
		private static global::LocalPrefs.g.KeyDefault<int> New(string key, Enum @default)
		{
			return new global::LocalPrefs.g.KeyDefault<int>(key, Convert.ToInt32(@default));
		}

		// Token: 0x020006FB RID: 1787
		public struct KeyDefault<T>
		{
			// Token: 0x06003BCA RID: 15306 RVA: 0x000D58B0 File Offset: 0x000D3AB0
			public KeyDefault(string key, T value)
			{
				this.key = key;
				this.@default = value;
				this.value = this.@default;
				this.uninit = false;
				this.set = false;
			}

			// Token: 0x06003BCB RID: 15307 RVA: 0x000D58E8 File Offset: 0x000D3AE8
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

			// Token: 0x06003BCC RID: 15308 RVA: 0x000D5934 File Offset: 0x000D3B34
			public void Reset()
			{
				if (this.set || this.Init())
				{
					PlayerPrefs.DeleteKey(this.key);
					this.set = false;
					this.value = this.@default;
				}
			}

			// Token: 0x04001DF7 RID: 7671
			public readonly string key;

			// Token: 0x04001DF8 RID: 7672
			public readonly T @default;

			// Token: 0x04001DF9 RID: 7673
			public T value;

			// Token: 0x04001DFA RID: 7674
			public bool uninit;

			// Token: 0x04001DFB RID: 7675
			public bool set;
		}

		// Token: 0x020006FC RID: 1788
		public static class s
		{
			// Token: 0x04001DFC RID: 7676
			public static global::LocalPrefs.g.KeyDefault<int> footCameraMode = global::LocalPrefs.g.New<int>("foot_cam", 1);

			// Token: 0x04001DFD RID: 7677
			public static global::LocalPrefs.g.KeyDefault<int> mountedWeaponCameraMode = global::LocalPrefs.g.New<int>("mwep_cam", 1);

			// Token: 0x04001DFE RID: 7678
			public static global::LocalPrefs.g.KeyDefault<int> carCameraMode = global::LocalPrefs.g.New<int>("car_cam", 2);

			// Token: 0x04001DFF RID: 7679
			public static global::LocalPrefs.g.KeyDefault<int> passengerCameraMode = global::LocalPrefs.g.New<int>("pass_cam", 1);

			// Token: 0x04001E00 RID: 7680
			public static global::LocalPrefs.g.KeyDefault<int> jetCameraMode = global::LocalPrefs.g.New<int>("jet_cam", 2);

			// Token: 0x04001E01 RID: 7681
			public static global::LocalPrefs.g.KeyDefault<int> heliCameraMode = global::LocalPrefs.g.New<int>("heli_cam", 2);

			// Token: 0x04001E02 RID: 7682
			public static global::LocalPrefs.g.KeyDefault<int> boatCameraMode = global::LocalPrefs.g.New<int>("boat_cam", 2);
		}
	}
}
