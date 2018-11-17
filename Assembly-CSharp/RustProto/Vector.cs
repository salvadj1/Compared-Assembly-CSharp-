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
	// Token: 0x02000254 RID: 596
	[DebuggerNonUserCode]
	public sealed class Vector : GeneratedMessage<Vector, Vector.Builder>
	{
		// Token: 0x06001498 RID: 5272 RVA: 0x000469A8 File Offset: 0x00044BA8
		private Vector()
		{
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000469B8 File Offset: 0x00044BB8
		static Vector()
		{
			object.ReferenceEquals(Common.Descriptor, null);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00046A20 File Offset: 0x00044C20
		public static RustProto.Helpers.Recycler<Vector, Vector.Builder> Recycler()
		{
			return RustProto.Helpers.Recycler<Vector, Vector.Builder>.Manufacture();
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00046A28 File Offset: 0x00044C28
		public static Vector DefaultInstance
		{
			get
			{
				return Vector.defaultInstance;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00046A30 File Offset: 0x00044C30
		public override Vector DefaultInstanceForType
		{
			get
			{
				return Vector.DefaultInstance;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00046A38 File Offset: 0x00044C38
		protected override Vector ThisMessage
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00046A3C File Offset: 0x00044C3C
		public static MessageDescriptor Descriptor
		{
			get
			{
				return Common.internal__static_RustProto_Vector__Descriptor;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00046A44 File Offset: 0x00044C44
		protected override FieldAccessorTable<Vector, Vector.Builder> InternalFieldAccessors
		{
			get
			{
				return Common.internal__static_RustProto_Vector__FieldAccessorTable;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00046A4C File Offset: 0x00044C4C
		public bool HasX
		{
			get
			{
				return this.hasX;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00046A54 File Offset: 0x00044C54
		public float X
		{
			get
			{
				return this.x_;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00046A5C File Offset: 0x00044C5C
		public bool HasY
		{
			get
			{
				return this.hasY;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00046A64 File Offset: 0x00044C64
		public float Y
		{
			get
			{
				return this.y_;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x00046A6C File Offset: 0x00044C6C
		public bool HasZ
		{
			get
			{
				return this.hasZ;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x00046A74 File Offset: 0x00044C74
		public float Z
		{
			get
			{
				return this.z_;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x00046A7C File Offset: 0x00044C7C
		public override bool IsInitialized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00046A80 File Offset: 0x00044C80
		public override void WriteTo(ICodedOutputStream output)
		{
			int serializedSize = this.SerializedSize;
			string[] vectorFieldNames = Vector._vectorFieldNames;
			if (this.hasX)
			{
				output.WriteFloat(1, vectorFieldNames[0], this.X);
			}
			if (this.hasY)
			{
				output.WriteFloat(2, vectorFieldNames[1], this.Y);
			}
			if (this.hasZ)
			{
				output.WriteFloat(3, vectorFieldNames[2], this.Z);
			}
			this.UnknownFields.WriteTo(output);
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x00046AF8 File Offset: 0x00044CF8
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
				num += this.UnknownFields.SerializedSize;
				this.memoizedSerializedSize = num;
				return num;
			}
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00046B7C File Offset: 0x00044D7C
		public static Vector ParseFrom(ByteString data)
		{
			return Vector.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00046B90 File Offset: 0x00044D90
		public static Vector ParseFrom(ByteString data, ExtensionRegistry extensionRegistry)
		{
			return Vector.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00046BA4 File Offset: 0x00044DA4
		public static Vector ParseFrom(byte[] data)
		{
			return Vector.CreateBuilder().MergeFrom(data).BuildParsed();
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00046BB8 File Offset: 0x00044DB8
		public static Vector ParseFrom(byte[] data, ExtensionRegistry extensionRegistry)
		{
			return Vector.CreateBuilder().MergeFrom(data, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x00046BCC File Offset: 0x00044DCC
		public static Vector ParseFrom(Stream input)
		{
			return Vector.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00046BE0 File Offset: 0x00044DE0
		public static Vector ParseFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Vector.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00046BF4 File Offset: 0x00044DF4
		public static Vector ParseDelimitedFrom(Stream input)
		{
			return Vector.CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00046C08 File Offset: 0x00044E08
		public static Vector ParseDelimitedFrom(Stream input, ExtensionRegistry extensionRegistry)
		{
			return Vector.CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00046C1C File Offset: 0x00044E1C
		public static Vector ParseFrom(ICodedInputStream input)
		{
			return Vector.CreateBuilder().MergeFrom(input).BuildParsed();
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00046C30 File Offset: 0x00044E30
		public static Vector ParseFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
		{
			return Vector.CreateBuilder().MergeFrom(input, extensionRegistry).BuildParsed();
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x00046C44 File Offset: 0x00044E44
		private Vector MakeReadOnly()
		{
			return this;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x00046C48 File Offset: 0x00044E48
		public static Vector.Builder CreateBuilder()
		{
			return new Vector.Builder();
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00046C50 File Offset: 0x00044E50
		public override Vector.Builder ToBuilder()
		{
			return Vector.CreateBuilder(this);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00046C58 File Offset: 0x00044E58
		public override Vector.Builder CreateBuilderForType()
		{
			return new Vector.Builder();
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x00046C60 File Offset: 0x00044E60
		public static Vector.Builder CreateBuilder(Vector prototype)
		{
			return new Vector.Builder(prototype);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00046C68 File Offset: 0x00044E68
		public static implicit operator Vector(Vector3 v)
		{
			Vector result;
			using (RustProto.Helpers.Recycler<Vector, Vector.Builder> recycler = Vector.Recycler())
			{
				Vector.Builder builder = recycler.OpenBuilder();
				builder.SetX(v.x);
				builder.SetY(v.y);
				builder.SetZ(v.z);
				result = builder.Build();
			}
			return result;
		}

		// Token: 0x04000AFA RID: 2810
		public const int XFieldNumber = 1;

		// Token: 0x04000AFB RID: 2811
		public const int YFieldNumber = 2;

		// Token: 0x04000AFC RID: 2812
		public const int ZFieldNumber = 3;

		// Token: 0x04000AFD RID: 2813
		private static readonly Vector defaultInstance = new Vector().MakeReadOnly();

		// Token: 0x04000AFE RID: 2814
		private static readonly string[] _vectorFieldNames = new string[]
		{
			"x",
			"y",
			"z"
		};

		// Token: 0x04000AFF RID: 2815
		private static readonly uint[] _vectorFieldTags = new uint[]
		{
			13u,
			21u,
			29u
		};

		// Token: 0x04000B00 RID: 2816
		private bool hasX;

		// Token: 0x04000B01 RID: 2817
		private float x_;

		// Token: 0x04000B02 RID: 2818
		private bool hasY;

		// Token: 0x04000B03 RID: 2819
		private float y_;

		// Token: 0x04000B04 RID: 2820
		private bool hasZ;

		// Token: 0x04000B05 RID: 2821
		private float z_;

		// Token: 0x04000B06 RID: 2822
		private int memoizedSerializedSize = -1;

		// Token: 0x02000255 RID: 597
		[DebuggerNonUserCode]
		public sealed class Builder : GeneratedBuilder<Vector, Vector.Builder>
		{
			// Token: 0x060014B9 RID: 5305 RVA: 0x00046CE8 File Offset: 0x00044EE8
			public Builder()
			{
				this.result = Vector.DefaultInstance;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060014BA RID: 5306 RVA: 0x00046D04 File Offset: 0x00044F04
			internal Builder(Vector cloneFrom)
			{
				this.result = cloneFrom;
				this.resultIsReadOnly = true;
			}

			// Token: 0x060014BB RID: 5307 RVA: 0x00046D1C File Offset: 0x00044F1C
			public void Set(Vector3 value)
			{
				this.SetX(value.x);
				this.SetY(value.y);
				this.SetZ(value.z);
			}

			// Token: 0x170005C3 RID: 1475
			// (get) Token: 0x060014BC RID: 5308 RVA: 0x00046D54 File Offset: 0x00044F54
			protected override Vector.Builder ThisBuilder
			{
				get
				{
					return this;
				}
			}

			// Token: 0x060014BD RID: 5309 RVA: 0x00046D58 File Offset: 0x00044F58
			private Vector PrepareBuilder()
			{
				if (this.resultIsReadOnly)
				{
					Vector other = this.result;
					this.result = new Vector();
					this.resultIsReadOnly = false;
					this.MergeFrom(other);
				}
				return this.result;
			}

			// Token: 0x170005C4 RID: 1476
			// (get) Token: 0x060014BE RID: 5310 RVA: 0x00046D98 File Offset: 0x00044F98
			public override bool IsInitialized
			{
				get
				{
					return this.result.IsInitialized;
				}
			}

			// Token: 0x170005C5 RID: 1477
			// (get) Token: 0x060014BF RID: 5311 RVA: 0x00046DA8 File Offset: 0x00044FA8
			protected override Vector MessageBeingBuilt
			{
				get
				{
					return this.PrepareBuilder();
				}
			}

			// Token: 0x060014C0 RID: 5312 RVA: 0x00046DB0 File Offset: 0x00044FB0
			public override Vector.Builder Clear()
			{
				this.result = Vector.DefaultInstance;
				this.resultIsReadOnly = true;
				return this;
			}

			// Token: 0x060014C1 RID: 5313 RVA: 0x00046DC8 File Offset: 0x00044FC8
			public override Vector.Builder Clone()
			{
				if (this.resultIsReadOnly)
				{
					return new Vector.Builder(this.result);
				}
				return new Vector.Builder().MergeFrom(this.result);
			}

			// Token: 0x170005C6 RID: 1478
			// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00046DF4 File Offset: 0x00044FF4
			public override MessageDescriptor DescriptorForType
			{
				get
				{
					return Vector.Descriptor;
				}
			}

			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00046DFC File Offset: 0x00044FFC
			public override Vector DefaultInstanceForType
			{
				get
				{
					return Vector.DefaultInstance;
				}
			}

			// Token: 0x060014C4 RID: 5316 RVA: 0x00046E04 File Offset: 0x00045004
			public override Vector BuildPartial()
			{
				if (this.resultIsReadOnly)
				{
					return this.result;
				}
				this.resultIsReadOnly = true;
				return this.result.MakeReadOnly();
			}

			// Token: 0x060014C5 RID: 5317 RVA: 0x00046E38 File Offset: 0x00045038
			public override Vector.Builder MergeFrom(IMessage other)
			{
				if (other is Vector)
				{
					return this.MergeFrom((Vector)other);
				}
				base.MergeFrom(other);
				return this;
			}

			// Token: 0x060014C6 RID: 5318 RVA: 0x00046E5C File Offset: 0x0004505C
			public override Vector.Builder MergeFrom(Vector other)
			{
				if (other == Vector.DefaultInstance)
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
				this.MergeUnknownFields(other.UnknownFields);
				return this;
			}

			// Token: 0x060014C7 RID: 5319 RVA: 0x00046ED0 File Offset: 0x000450D0
			public override Vector.Builder MergeFrom(ICodedInputStream input)
			{
				return this.MergeFrom(input, ExtensionRegistry.Empty);
			}

			// Token: 0x060014C8 RID: 5320 RVA: 0x00046EE0 File Offset: 0x000450E0
			public override Vector.Builder MergeFrom(ICodedInputStream input, ExtensionRegistry extensionRegistry)
			{
				this.PrepareBuilder();
				UnknownFieldSet.Builder builder = null;
				uint num;
				string text;
				while (input.ReadTag(ref num, ref text))
				{
					if (num == 0u && text != null)
					{
						int num2 = Array.BinarySearch<string>(Vector._vectorFieldNames, text, StringComparer.Ordinal);
						if (num2 < 0)
						{
							if (builder == null)
							{
								builder = UnknownFieldSet.CreateBuilder(this.UnknownFields);
							}
							this.ParseUnknownField(input, builder, extensionRegistry, num, text);
							continue;
						}
						num = Vector._vectorFieldTags[num2];
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

			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00047048 File Offset: 0x00045248
			public bool HasX
			{
				get
				{
					return this.result.hasX;
				}
			}

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x060014CA RID: 5322 RVA: 0x00047058 File Offset: 0x00045258
			// (set) Token: 0x060014CB RID: 5323 RVA: 0x00047068 File Offset: 0x00045268
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

			// Token: 0x060014CC RID: 5324 RVA: 0x00047074 File Offset: 0x00045274
			public Vector.Builder SetX(float value)
			{
				this.PrepareBuilder();
				this.result.hasX = true;
				this.result.x_ = value;
				return this;
			}

			// Token: 0x060014CD RID: 5325 RVA: 0x000470A4 File Offset: 0x000452A4
			public Vector.Builder ClearX()
			{
				this.PrepareBuilder();
				this.result.hasX = false;
				this.result.x_ = 0f;
				return this;
			}

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x060014CE RID: 5326 RVA: 0x000470D8 File Offset: 0x000452D8
			public bool HasY
			{
				get
				{
					return this.result.hasY;
				}
			}

			// Token: 0x170005CB RID: 1483
			// (get) Token: 0x060014CF RID: 5327 RVA: 0x000470E8 File Offset: 0x000452E8
			// (set) Token: 0x060014D0 RID: 5328 RVA: 0x000470F8 File Offset: 0x000452F8
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

			// Token: 0x060014D1 RID: 5329 RVA: 0x00047104 File Offset: 0x00045304
			public Vector.Builder SetY(float value)
			{
				this.PrepareBuilder();
				this.result.hasY = true;
				this.result.y_ = value;
				return this;
			}

			// Token: 0x060014D2 RID: 5330 RVA: 0x00047134 File Offset: 0x00045334
			public Vector.Builder ClearY()
			{
				this.PrepareBuilder();
				this.result.hasY = false;
				this.result.y_ = 0f;
				return this;
			}

			// Token: 0x170005CC RID: 1484
			// (get) Token: 0x060014D3 RID: 5331 RVA: 0x00047168 File Offset: 0x00045368
			public bool HasZ
			{
				get
				{
					return this.result.hasZ;
				}
			}

			// Token: 0x170005CD RID: 1485
			// (get) Token: 0x060014D4 RID: 5332 RVA: 0x00047178 File Offset: 0x00045378
			// (set) Token: 0x060014D5 RID: 5333 RVA: 0x00047188 File Offset: 0x00045388
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

			// Token: 0x060014D6 RID: 5334 RVA: 0x00047194 File Offset: 0x00045394
			public Vector.Builder SetZ(float value)
			{
				this.PrepareBuilder();
				this.result.hasZ = true;
				this.result.z_ = value;
				return this;
			}

			// Token: 0x060014D7 RID: 5335 RVA: 0x000471C4 File Offset: 0x000453C4
			public Vector.Builder ClearZ()
			{
				this.PrepareBuilder();
				this.result.hasZ = false;
				this.result.z_ = 0f;
				return this;
			}

			// Token: 0x04000B07 RID: 2823
			private bool resultIsReadOnly;

			// Token: 0x04000B08 RID: 2824
			private Vector result;
		}
	}
}
