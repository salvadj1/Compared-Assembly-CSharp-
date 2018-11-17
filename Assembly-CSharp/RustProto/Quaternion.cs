using System;
using System.Diagnostics;
using System.IO;
using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using Google.ProtocolBuffers.FieldAccess;
using RustProto.Helpers;
using UnityEngine;

namespace RustProto
{
	// Token: 0x02000256 RID: 598
	[DebuggerNonUserCode]
	public sealed class Quaternion : GeneratedMessage<Quaternion, Quaternion.Builder>
	{
		// Token: 0x060014D8 RID: 5336 RVA: 0x000471F8 File Offset: 0x000453F8
		private Quaternion()
		{
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00047208 File Offset: 0x00045408
		static Quaternion()
		{
			object.ReferenceEquals(Common.Descriptor, null);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x00047274 File Offset: 0x00045474
		public static RustProto.Helpers.Recycler<Quaternion, Quaternion.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<Quaternion, Quaternion.Builder>.Manufacture();
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x0004727C File Offset: 0x0004547C
		public static Quaternion DefaultInstance
		{
			get
			{
				return Quaternion.defaultInstance;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060014DC RID: 5340 RVA: 0x00047284 File Offset: 0x00045484
		public override Quaternion DefaultInstanceForType
		{
			get
			{
				return Quaternion.DefaultInstance;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x0004728C File Offset: 0x0004548C
		protected override Quaternion ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x00047290 File Offset: 0x00045490
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Common.internal__static_RustProto_Quaternion__Descriptor;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00047298 File Offset: 0x00045498
		protected override FieldAccessorTable<Quaternion, Quaternion.Builder> InternalFieldAccessors
		{
			get
			{
				return Common.internal__static_RustProto_Quaternion__FieldAccessorTable;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x000472A0 File Offset: 0x000454A0
		public bool HasX
		{
			get
			{
				return this.hasX;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x000472A8 File Offset: 0x000454A8
		public float X
		{
			get
			{
				return this.x_;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x000472B0 File Offset: 0x000454B0
		public bool HasY
		{
			get
			{
				return this.hasY;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x000472B8 File Offset: 0x000454B8
		public float Y
		{
			get
			{
				return this.y_;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x000472C0 File Offset: 0x000454C0
		public bool HasZ
		{
			get
			{
				return this.hasZ;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000472C8 File Offset: 0x000454C8
		public float Z
		{
			get
			{
				return this.z_;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x000472D0 File Offset: 0x000454D0
		public bool HasW
		{
			get
			{
				return this.hasW;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x000472D8 File Offset: 0x000454D8
		public float W
		{
			get
			{
				return this.w_;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x000472E0 File Offset: 0x000454E0
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x000472E4 File Offset: 0x000454E4
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] quaternionFieldNames = Quaternion._quaternionFieldNames;
			if (this.hasX)
			{
				output.WriteFloat(1, quaternionFieldNames[1], this.X);
			}
			if (this.hasY)
			{
				output.WriteFloat(2, quaternionFieldNames[2], this.Y);
			}
			if (this.hasZ)
			{
				output.WriteFloat(3, quaternionFieldNames[3], this.Z);
			}
			if (this.hasW)
			{
				output.WriteFloat(4, quaternionFieldNames[0], this.W);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00047378 File Offset: 0x00045578
		public override int SerializedSize
		{
			get
			{
				int num = this.memoizedSerializedSize;
				if (num != -1)
				{
					return num;
				}
				num = 0;
				if (this.hasX)
				{
					num += CodedOutputStream.ComputeFloatSize(1, this.X);
				}
				if (this.hasY)
				{
					num += CodedOutputStream.ComputeFloatSize(2, this.Y);
				}
				if (this.hasZ)
				{
					num += CodedOutputStream.ComputeFloatSize(3, this.Z);
				}
				if (this.hasW)
				{
					num += CodedOutputStream.ComputeFloatSize(4, this.W);
				}
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00047418 File Offset: 0x00045618
		public static Quaternion ParseFrom(ByteString data)
		{
			return Quaternion.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0004742C File Offset: 0x0004562C
		public static Quaternion ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return Quaternion.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00047440 File Offset: 0x00045640
		public static Quaternion ParseFrom(byte[] data)
		{
			return Quaternion.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00047454 File Offset: 0x00045654
		public static Quaternion ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return Quaternion.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00047468 File Offset: 0x00045668
		public static Quaternion ParseFrom(Stream input)
		{
			return Quaternion.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0004747C File Offset: 0x0004567C
		public static Quaternion ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Quaternion.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00047490 File Offset: 0x00045690
		public static Quaternion ParseDelimitedFrom(Stream input)
		{
			return Quaternion.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000474A4 File Offset: 0x000456A4
		public static Quaternion ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Quaternion.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x000474B8 File Offset: 0x000456B8
		public static Quaternion ParseFrom(ICodedInputStream input)
		{
			return Quaternion.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x000474CC File Offset: 0x000456CC
		public static Quaternion ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return Quaternion.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x000474E0 File Offset: 0x000456E0
		private Quaternion MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x000474E4 File Offset: 0x000456E4
		public static Quaternion.Builder CreateBuilder()
		{
			return new Quaternion.Builder();
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x000474EC File Offset: 0x000456EC
		public override Quaternion.Builder ToBuilder()
		{
			return Quaternion.CreateBuilder(this);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000474F4 File Offset: 0x000456F4
		public override Quaternion.Builder CreateBuilderForType()
		{
			return new Quaternion.Builder();
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000474FC File Offset: 0x000456FC
		public static Quaternion.Builder CreateBuilder(Quaternion prototype)
		{
			return new Quaternion.Builder(prototype);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00047504 File Offset: 0x00045704
		public static implicit operator Quaternion(Quaternion v)
		{
			Quaternion result;
			using (RustProto.Helpers.Recycler<Quaternion, Quaternion.Builder> recycler = Quaternion.Recycler())
			{
				Quaternion.Builder builder = recycler.OpenBuilder();
				builder.SetX(v.x);
				builder.SetY(v.y);
				builder.SetZ(v.z);
				builder.SetW(v.w);
				result = builder.Build();
			}
			return result;
		}

		// Token: 0x04000B09 RID: 2825
		public const int XFieldNumber = 1;

		// Token: 0x04000B0A RID: 2826
		public const int YFieldNumber = 2;

		// Token: 0x04000B0B RID: 2827
		public const int ZFieldNumber = 3;

		// Token: 0x04000B0C RID: 2828
		public const int WFieldNumber = 4;

		// Token: 0x04000B0D RID: 2829
		private static readonly Quaternion defaultInstance = new Quaternion().MakeReadOnly();

		// Token: 0x04000B0E RID: 2830
		private static readonly string[] _quaternionFieldNames = new string[]
		{
			"w",
			"x",
			"y",
			"z"
		};

		// Token: 0x04000B0F RID: 2831
		private static readonly uint[] _quaternionFieldTags = new uint[]
		{
			37u,
			13u,
			21u,
			29u
		};

		// Token: 0x04000B10 RID: 2832
		private bool hasX;

		// Token: 0x04000B11 RID: 2833
		private float x_;

		// Token: 0x04000B12 RID: 2834
		private bool hasY;

		// Token: 0x04000B13 RID: 2835
		private float y_;

		// Token: 0x04000B14 RID: 2836
		private bool hasZ;

		// Token: 0x04000B15 RID: 2837
		private float z_;

		// Token: 0x04000B16 RID: 2838
		private bool hasW;

		// Token: 0x04000B17 RID: 2839
		private float w_;

		// Token: 0x04000B18 RID: 2840
		private int memoizedSerializedSize = -1;

		// Token: 0x02000257 RID: 599
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<Quaternion, Quaternion.Builder>
		{
			// Token: 0x060014FB RID: 5371 RVA: 0x00047594 File Offset: 0x00045794
			public Builder()
			{
				this.result = Quaternion.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060014FC RID: 5372 RVA: 0x000475B0 File Offset: 0x000457B0
			internal Builder(Quaternion cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060014FD RID: 5373 RVA: 0x000475C8 File Offset: 0x000457C8
			public void Set(Quaternion value)
			{
				this.SetX(value.x);
				this.SetY(value.y);
				this.SetZ(value.z);
				this.SetW(value.w);
			}

			// Token: 0x170005DD RID: 1501
			// (get) Token: 0x060014FE RID: 5374 RVA: 0x00047610 File Offset: 0x00045810
			protected override Quaternion.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060014FF RID: 5375 RVA: 0x00047614 File Offset: 0x00045814
			private Quaternion PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					Quaternion other = this.result;
					this.result = new Quaternion();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170005DE RID: 1502
			// (get) Token: 0x06001500 RID: 5376 RVA: 0x00047654 File Offset: 0x00045854
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170005DF RID: 1503
			// (get) Token: 0x06001501 RID: 5377 RVA: 0x00047664 File Offset: 0x00045864
			protected override Quaternion MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x06001502 RID: 5378 RVA: 0x0004766C File Offset: 0x0004586C
			public override Quaternion.Builder Clear()
			{
				this.result = Quaternion.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x06001503 RID: 5379 RVA: 0x00047684 File Offset: 0x00045884
			public override Quaternion.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new Quaternion.Builder(this.result);
				}
				return new Quaternion.Builder().MergeFrom(this.result);
			}

			// Token: 0x170005E0 RID: 1504
			// (get) Token: 0x06001504 RID: 5380 RVA: 0x000476B0 File Offset: 0x000458B0
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return Quaternion.Descriptor;
				}
			}

			// Token: 0x170005E1 RID: 1505
			// (get) Token: 0x06001505 RID: 5381 RVA: 0x000476B8 File Offset: 0x000458B8
			public override Quaternion DefaultInstanceForType
			{
				get
				{
					return Quaternion.DefaultInstance;
				}
			}

			// Token: 0x06001506 RID: 5382 RVA: 0x000476C0 File Offset: 0x000458C0
			public override Quaternion BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x06001507 RID: 5383 RVA: 0x000476F4 File Offset: 0x000458F4
			public override Quaternion.Builder MergeFrom(IMessage other)
			{
				if (other is Quaternion)
				{
					return this.MergeFrom((Quaternion)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x06001508 RID: 5384 RVA: 0x00047718 File Offset: 0x00045918
			public override Quaternion.Builder MergeFrom(Quaternion other)
			{
				if (other == Quaternion.DefaultInstance)
				{
					return this;
				}
				this.PrepareBuilder();
				if (other.HasX)
				{
					this.X = other.X;
				}
				if (other.HasY)
				{
					this.Y = other.Y;
				}
				if (other.HasZ)
				{
					this.Z = other.Z;
				}
				if (other.HasW)
				{
					this.W = other.W;
				}
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x06001509 RID: 5385 RVA: 0x000477A4 File Offset: 0x000459A4
			public override Quaternion.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x0600150A RID: 5386 RVA: 0x000477B4 File Offset: 0x000459B4
			public override Quaternion.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(Quaternion._quaternionFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = Quaternion._quaternionFieldTags[num2];
					}
					uint num3 = num;
					if (num3 == 0u)
					{
						throw InvalidProtocolBufferException.InvalidTag();
					}
					if (num3 != 13u)
					{
						if (num3 != 21u)
						{
							if (num3 != 29u)
							{
								if (num3 != 37u)
								{
									if (WireFormat.IsEndGroupTag(num))
									{
										if (builder != null)
										{
											this.UnknownFields = builder.Build();
										}
										return this;
									}
									if (builder == null)
									{
										builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
									}
									this.ParseUnknownField(input, builder, extensionRegistry, num, text);
								}
								else
								{
									this.result.hasW = input.ReadFloat(ref this.result.w_);
								}
							}
							else
							{
								this.result.hasZ = input.ReadFloat(ref this.result.z_);
							}
						}
						else
						{
							this.result.hasY = input.ReadFloat(ref this.result.y_);
						}
					}
					else
					{
						this.result.hasX = input.ReadFloat(ref this.result.x_);
					}
				}
				if (builder != null)
				{
					this.UnknownFields = builder.Build();
				}
				return this;
			}

			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x0600150B RID: 5387 RVA: 0x00047948 File Offset: 0x00045B48
			public bool HasX
			{
				get
				{
					return this.result.hasX;
				}
			}

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x0600150C RID: 5388 RVA: 0x00047958 File Offset: 0x00045B58
			// (set) Token: 0x0600150D RID: 5389 RVA: 0x00047968 File Offset: 0x00045B68
			public float X
			{
				get
				{
					return this.result.X;
				}
				set
				{
					this.SetX(value);
				}
			}

			// Token: 0x0600150E RID: 5390 RVA: 0x00047974 File Offset: 0x00045B74
			public Quaternion.Builder SetX(float value)
			{
				this.PrepareBuilder();
				this.result.hasX = true;
				this.result.x_ = value;
				return this;
			}

			// Token: 0x0600150F RID: 5391 RVA: 0x000479A4 File Offset: 0x00045BA4
			public Quaternion.Builder ClearX()
			{
				this.PrepareBuilder();
				this.result.hasX = false;
				this.result.x_ = 0f;
				return this;
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x06001510 RID: 5392 RVA: 0x000479D8 File Offset: 0x00045BD8
			public bool HasY
			{
				get
				{
					return this.result.hasY;
				}
			}

			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x06001511 RID: 5393 RVA: 0x000479E8 File Offset: 0x00045BE8
			// (set) Token: 0x06001512 RID: 5394 RVA: 0x000479F8 File Offset: 0x00045BF8
			public float Y
			{
				get
				{
					return this.result.Y;
				}
				set
				{
					this.SetY(value);
				}
			}

			// Token: 0x06001513 RID: 5395 RVA: 0x00047A04 File Offset: 0x00045C04
			public Quaternion.Builder SetY(float value)
			{
				this.PrepareBuilder();
				this.result.hasY = true;
				this.result.y_ = value;
				return this;
			}

			// Token: 0x06001514 RID: 5396 RVA: 0x00047A34 File Offset: 0x00045C34
			public Quaternion.Builder ClearY()
			{
				this.PrepareBuilder();
				this.result.hasY = false;
				this.result.y_ = 0f;
				return this;
			}

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x06001515 RID: 5397 RVA: 0x00047A68 File Offset: 0x00045C68
			public bool HasZ
			{
				get
				{
					return this.result.hasZ;
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x06001516 RID: 5398 RVA: 0x00047A78 File Offset: 0x00045C78
			// (set) Token: 0x06001517 RID: 5399 RVA: 0x00047A88 File Offset: 0x00045C88
			public float Z
			{
				get
				{
					return this.result.Z;
				}
				set
				{
					this.SetZ(value);
				}
			}

			// Token: 0x06001518 RID: 5400 RVA: 0x00047A94 File Offset: 0x00045C94
			public Quaternion.Builder SetZ(float value)
			{
				this.PrepareBuilder();
				this.result.hasZ = true;
				this.result.z_ = value;
				return this;
			}

			// Token: 0x06001519 RID: 5401 RVA: 0x00047AC4 File Offset: 0x00045CC4
			public Quaternion.Builder ClearZ()
			{
				this.PrepareBuilder();
				this.result.hasZ = false;
				this.result.z_ = 0f;
				return this;
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x0600151A RID: 5402 RVA: 0x00047AF8 File Offset: 0x00045CF8
			public bool HasW
			{
				get
				{
					return this.result.hasW;
				}
			}

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x0600151B RID: 5403 RVA: 0x00047B08 File Offset: 0x00045D08
			// (set) Token: 0x0600151C RID: 5404 RVA: 0x00047B18 File Offset: 0x00045D18
			public float W
			{
				get
				{
					return this.result.W;
				}
				set
				{
					this.SetW(value);
				}
			}

			// Token: 0x0600151D RID: 5405 RVA: 0x00047B24 File Offset: 0x00045D24
			public Quaternion.Builder SetW(float value)
			{
				this.PrepareBuilder();
				this.result.hasW = true;
				this.result.w_ = value;
				return this;
			}

			// Token: 0x0600151E RID: 5406 RVA: 0x00047B54 File Offset: 0x00045D54
			public Quaternion.Builder ClearW()
			{
				this.PrepareBuilder();
				this.result.hasW = false;
				this.result.w_ = 0f;
				return this;
			}

			// Token: 0x04000B19 RID: 2841
			private bool resultIsReadOnly;

			// Token: 0x04000B1A RID: 2842
			private Quaternion result;
		}
	}
}
