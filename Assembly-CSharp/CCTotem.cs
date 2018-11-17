using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;

// Token: 0x0200028B RID: 651
public abstract class CCTotem : MonoBehaviour
{
	// Token: 0x06001783 RID: 6019 RVA: 0x0005B360 File Offset: 0x00059560
	internal CCTotem()
	{
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x0005B368 File Offset: 0x00059568
	private static string VS(Vector3 v)
	{
		return string.Format("[{0},{1},{2}]", v.x, v.y, v.z);
	}

	// Token: 0x170006D2 RID: 1746
	// (get) Token: 0x06001785 RID: 6021 RVA: 0x0005B3A4 File Offset: 0x000595A4
	protected internal CCTotem.TotemicObject totemicObject
	{
		get
		{
			return this._Object;
		}
	}

	// Token: 0x170006D3 RID: 1747
	// (get) Token: 0x06001786 RID: 6022
	internal abstract CCTotem.TotemicObject _Object { get; }

	// Token: 0x06001787 RID: 6023 RVA: 0x0005B3AC File Offset: 0x000595AC
	private static void DestroyCCDesc(CCTotemPole ScriptOwner, ref CCDesc CCDesc)
	{
		if (ScriptOwner)
		{
			ScriptOwner.DestroyCCDesc(ref CCDesc);
		}
		else
		{
			CCDesc ccdesc = CCDesc;
			CCDesc = null;
			if (ccdesc)
			{
				Object.Destroy(ccdesc.gameObject);
			}
		}
	}

	// Token: 0x04000C4D RID: 3149
	protected internal const int kMaxTotemicFiguresPerTotemPole = 8;

	// Token: 0x04000C4E RID: 3150
	protected internal const CollisionFlags kCF_Sides = 1;

	// Token: 0x04000C4F RID: 3151
	protected internal const CollisionFlags kCF_Above = 2;

	// Token: 0x04000C50 RID: 3152
	protected internal const CollisionFlags kCF_Below = 4;

	// Token: 0x04000C51 RID: 3153
	protected internal const CollisionFlags kCF_None = 0;

	// Token: 0x0200028C RID: 652
	protected internal struct Configuration
	{
		// Token: 0x06001788 RID: 6024 RVA: 0x0005B3EC File Offset: 0x000595EC
		public Configuration(ref CCTotem.Initialization totem)
		{
			if (!totem.figurePrefab)
			{
				throw new ArgumentException("figurePrefab was missing", "totem");
			}
			this.totem = totem;
			this.totemMinHeight = totem.minHeight;
			this.totemMaxHeight = totem.maxHeight;
			this.totemBottomBufferUnits = totem.bottomBufferUnits;
			if (this.totemMinHeight >= this.totemMaxHeight)
			{
				throw new ArgumentException("maxHeight is less than or equal to minHeight", "totem");
			}
			if (Mathf.Approximately(this.totemBottomBufferUnits, 0f))
			{
				this.totemBottomBufferUnits = 0f;
			}
			else if (this.totemBottomBufferUnits < 0f)
			{
				throw new ArgumentException("bottomBufferPercent must not be less than zero", "totem");
			}
			CCDesc figurePrefab = totem.figurePrefab;
			this.figureSkinWidth = figurePrefab.skinWidth;
			this.figure2SkinWidth = this.figureSkinWidth + this.figureSkinWidth;
			this.figureRadius = figurePrefab.radius;
			this.figureSkinnedRadius = this.figureRadius + this.figureSkinWidth;
			this.figureDiameter = this.figureRadius + this.figureRadius;
			this.figureSkinnedDiameter = this.figureSkinnedRadius + this.figureSkinnedRadius;
			this.figureHeight = figurePrefab.height;
			if (this.figureHeight <= this.figureDiameter)
			{
				throw new ArgumentException("The CCDesc(CharacterController) Prefab is a sphere, not a capsule. Thus cannot be expanded on the totem pole", "totem");
			}
			this.figureSkinnedHeight = this.figureHeight + this.figure2SkinWidth;
			if (this.figureSkinnedHeight > this.totemMinHeight && !Mathf.Approximately(this.totemMinHeight, this.figureSkinnedHeight))
			{
				throw new ArgumentException("minHeight is too small. It must be at least the size of the CCDesc(CharacterController) prefab's [height+(skinWidth*2)]", "totem");
			}
			this.figureSlideHeight = this.figureSkinnedHeight - this.figureSkinnedDiameter;
			if (this.figureSlideHeight <= 0f)
			{
				throw new ArgumentException("The CCDesc(CharacterController) Prefab has limited height availability. Thus cannot be expanded on the totem pole", "totem");
			}
			this.figureFixedHeight = this.figureSkinnedHeight - this.figureSlideHeight;
			this.poleTopBufferAmount = this.figureSkinnedRadius;
			this.poleBottomBufferUnitSize = this.figureSlideHeight * 0.5f;
			this.poleBottomBufferAmount = this.poleBottomBufferUnitSize * this.totemBottomBufferUnits;
			if (this.poleBottomBufferAmount > this.figureSlideHeight)
			{
				if (!Mathf.Approximately(this.poleBottomBufferAmount, this.figureSlideHeight))
				{
					throw new ArgumentException("The bottomBuffer was too large and landed outside of sliding height area of the capsule", "totem");
				}
				this.poleBottomBufferAmount = this.figureSlideHeight;
				this.totemBottomBufferUnits = this.figureSlideHeight / this.poleBottomBufferUnitSize;
			}
			this.poleBottomBufferHeight = this.figureSkinnedRadius + this.poleBottomBufferAmount;
			this.poleMostContractedHeightPossible = this.figureSkinnedHeight + this.poleBottomBufferAmount;
			if (this.poleMostContractedHeightPossible > this.totemMinHeight)
			{
				if (!Mathf.Approximately(this.poleMostContractedHeightPossible, this.totemMinHeight))
				{
					throw new ArgumentException("bottomBufferPercent value is too high with the current setup, results in contracted height greater than totem.minHeight.", "totem");
				}
				this.totemMinHeight = this.poleMostContractedHeightPossible;
			}
			this.poleContractedHeight = Mathf.Max(this.poleMostContractedHeightPossible, this.totemMinHeight);
			this.poleContractedHeightFromMostContractedHeightPossible = this.poleContractedHeight - this.poleMostContractedHeightPossible;
			this.poleExpandedHeight = Mathf.Max(this.poleContractedHeight, this.totemMaxHeight);
			this.poleExpandedHeightFromMostContractedHeightPossible = this.poleExpandedHeight - this.poleMostContractedHeightPossible;
			if (Mathf.Approximately(this.poleContractedHeightFromMostContractedHeightPossible, this.poleExpandedHeightFromMostContractedHeightPossible))
			{
				throw new ArgumentException("minHeight and maxHeight were too close to eachother to provide reliable contraction/expansion calculations.", "totem");
			}
			if (this.poleContractedHeightFromMostContractedHeightPossible < 0f || this.poleExpandedHeightFromMostContractedHeightPossible < this.poleContractedHeightFromMostContractedHeightPossible)
			{
				throw new ArgumentException("Calculation error with current configuration.", "totem");
			}
			this.poleFixedLength = this.poleBottomBufferHeight + this.poleTopBufferAmount;
			this.poleExpansionLength = this.poleExpandedHeight - this.poleFixedLength;
			this.numSlidingTotemicFigures = Mathf.CeilToInt(this.poleExpansionLength / this.figureSlideHeight);
			if (this.numSlidingTotemicFigures < 1)
			{
				throw new ArgumentException("The current configuration of the CCTotem resulted in no need for more than one CCDesc(CharacterController), thus rejecting usage..", "totem");
			}
			this.poleMostExpandedHeightPossible = this.poleFixedLength + (float)this.numSlidingTotemicFigures * this.figureSlideHeight;
			this.numRequiredTotemicFigures = 1 + this.numSlidingTotemicFigures;
			if (this.numRequiredTotemicFigures > 8)
			{
				throw new ArgumentOutOfRangeException("totem", this.numRequiredTotemicFigures, "The current configuration of the CCTotem resulted in more than the max number of TotemicFigure's allowed :" + 8);
			}
			Vector3 center = figurePrefab.center;
			this.figureOriginOffsetCenter = new Vector3(0f - center.x, 0f - center.y, 0f - center.z);
			this.figureOriginOffsetBottom = new Vector3(this.figureOriginOffsetCenter.x, 0f - (center.y - this.figureSkinnedHeight / 2f), this.figureOriginOffsetCenter.z);
			this.figureOriginOffsetTop = new Vector3(this.figureOriginOffsetCenter.x, 0f - (center.y + this.figureSkinnedHeight / 2f), this.figureOriginOffsetCenter.z);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0005B8D8 File Offset: 0x00059AD8
		public override string ToString()
		{
			return CCTotem.ToStringHelper<CCTotem.Configuration>.GetString(this);
		}

		// Token: 0x04000C52 RID: 3154
		public readonly CCTotem.Initialization totem;

		// Token: 0x04000C53 RID: 3155
		public readonly float totemMinHeight;

		// Token: 0x04000C54 RID: 3156
		public readonly float totemMaxHeight;

		// Token: 0x04000C55 RID: 3157
		public readonly float totemBottomBufferUnits;

		// Token: 0x04000C56 RID: 3158
		public readonly float figureSkinWidth;

		// Token: 0x04000C57 RID: 3159
		public readonly float figure2SkinWidth;

		// Token: 0x04000C58 RID: 3160
		public readonly float figureRadius;

		// Token: 0x04000C59 RID: 3161
		public readonly float figureSkinnedRadius;

		// Token: 0x04000C5A RID: 3162
		public readonly float figureDiameter;

		// Token: 0x04000C5B RID: 3163
		public readonly float figureSkinnedDiameter;

		// Token: 0x04000C5C RID: 3164
		public readonly float figureHeight;

		// Token: 0x04000C5D RID: 3165
		public readonly float figureSkinnedHeight;

		// Token: 0x04000C5E RID: 3166
		public readonly float figureSlideHeight;

		// Token: 0x04000C5F RID: 3167
		public readonly float figureFixedHeight;

		// Token: 0x04000C60 RID: 3168
		public readonly float poleTopBufferAmount;

		// Token: 0x04000C61 RID: 3169
		public readonly float poleBottomBufferAmount;

		// Token: 0x04000C62 RID: 3170
		public readonly float poleBottomBufferHeight;

		// Token: 0x04000C63 RID: 3171
		public readonly float poleBottomBufferUnitSize;

		// Token: 0x04000C64 RID: 3172
		public readonly float poleMostContractedHeightPossible;

		// Token: 0x04000C65 RID: 3173
		public readonly float poleMostExpandedHeightPossible;

		// Token: 0x04000C66 RID: 3174
		public readonly float poleContractedHeight;

		// Token: 0x04000C67 RID: 3175
		public readonly float poleContractedHeightFromMostContractedHeightPossible;

		// Token: 0x04000C68 RID: 3176
		public readonly float poleExpandedHeight;

		// Token: 0x04000C69 RID: 3177
		public readonly float poleExpandedHeightFromMostContractedHeightPossible;

		// Token: 0x04000C6A RID: 3178
		public readonly float poleFixedLength;

		// Token: 0x04000C6B RID: 3179
		public readonly float poleExpansionLength;

		// Token: 0x04000C6C RID: 3180
		public readonly int numRequiredTotemicFigures;

		// Token: 0x04000C6D RID: 3181
		public readonly int numSlidingTotemicFigures;

		// Token: 0x04000C6E RID: 3182
		public readonly Vector3 figureOriginOffsetBottom;

		// Token: 0x04000C6F RID: 3183
		public readonly Vector3 figureOriginOffsetTop;

		// Token: 0x04000C70 RID: 3184
		public readonly Vector3 figureOriginOffsetCenter;
	}

	// Token: 0x0200028D RID: 653
	protected internal struct Contraction
	{
		// Token: 0x0600178A RID: 6026 RVA: 0x0005B8EC File Offset: 0x00059AEC
		private Contraction(float Contracted, float Expanded, float Range, float InverseRange)
		{
			this.Contracted = Contracted;
			this.Expanded = Expanded;
			this.Range = Range;
			this.InverseRange = InverseRange;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0005B90C File Offset: 0x00059B0C
		public CCTotem.Expansion ExpansionForValue(float Value)
		{
			CCTotem.Expansion result;
			if (Value <= this.Contracted)
			{
				result = new CCTotem.Expansion(this.Contracted, 0f, 0f);
			}
			else if (Value >= this.Expanded)
			{
				result = new CCTotem.Expansion(this.Expanded, 1f, this.Range);
			}
			else
			{
				float num = Value - this.Contracted;
				float fractionExpanded = num / this.Range;
				result = new CCTotem.Expansion(Value, fractionExpanded, num);
			}
			return result;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0005B988 File Offset: 0x00059B88
		public CCTotem.Expansion ExpansionForFraction(float FractionExpanded)
		{
			CCTotem.Expansion result;
			if (FractionExpanded <= 0f)
			{
				result = new CCTotem.Expansion(this.Contracted, 0f, 0f);
			}
			else if (FractionExpanded >= 1f)
			{
				result = new CCTotem.Expansion(this.Expanded, 1f, this.Range);
			}
			else
			{
				float num = FractionExpanded * this.Range;
				float value = this.Contracted + num;
				result = new CCTotem.Expansion(value, FractionExpanded, num);
			}
			return result;
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0005BA04 File Offset: 0x00059C04
		public CCTotem.Expansion ExpansionForAmount(float Amount)
		{
			CCTotem.Expansion result;
			if (Amount <= 0f)
			{
				result = new CCTotem.Expansion(this.Contracted, 0f, 0f);
			}
			else if (Amount >= this.Range)
			{
				result = new CCTotem.Expansion(this.Expanded, 1f, this.Range);
			}
			else
			{
				float fractionExpanded = Amount / this.Range;
				float value = this.Contracted + Amount;
				result = new CCTotem.Expansion(value, fractionExpanded, Amount);
			}
			return result;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0005BA80 File Offset: 0x00059C80
		public override string ToString()
		{
			return string.Format("{{Contracted={0},Expanded={1},Range={2},InverseRange={3}}}", new object[]
			{
				this.Contracted,
				this.Expanded,
				this.Range,
				this.InverseRange
			});
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0005BAD8 File Offset: 0x00059CD8
		public static CCTotem.Contraction Define(float Contracted, float Expanded)
		{
			if (Mathf.Approximately(Contracted, Expanded))
			{
				throw new ArgumentOutOfRangeException("Contracted", "approximately equal to Expanded");
			}
			float num = Expanded - Contracted;
			return new CCTotem.Contraction(Contracted, Expanded, num, (float)(1.0 / (double)num));
		}

		// Token: 0x04000C71 RID: 3185
		public readonly float Contracted;

		// Token: 0x04000C72 RID: 3186
		public readonly float Expanded;

		// Token: 0x04000C73 RID: 3187
		public readonly float Range;

		// Token: 0x04000C74 RID: 3188
		public readonly float InverseRange;
	}

	// Token: 0x0200028E RID: 654
	protected internal struct Direction
	{
		// Token: 0x06001790 RID: 6032 RVA: 0x0005BB1C File Offset: 0x00059D1C
		public Direction(CCTotem.TotemicFigure TotemicFigure)
		{
			if (object.ReferenceEquals(TotemicFigure, null))
			{
				throw new ArgumentNullException("TotemicFigure");
			}
			this.TotemicFigure = TotemicFigure;
			this.Exists = true;
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0005BB44 File Offset: 0x00059D44
		public static CCTotem.Direction None
		{
			get
			{
				return default(CCTotem.Direction);
			}
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0005BB5C File Offset: 0x00059D5C
		public override string ToString()
		{
			return (!this.Exists) ? "{Does Not Exist}" : string.Format("{{TotemicFigure={0}}}", this.TotemicFigure);
		}

		// Token: 0x04000C75 RID: 3189
		public readonly bool Exists;

		// Token: 0x04000C76 RID: 3190
		public readonly CCTotem.TotemicFigure TotemicFigure;
	}

	// Token: 0x0200028F RID: 655
	protected internal struct Ends<T>
	{
		// Token: 0x06001793 RID: 6035 RVA: 0x0005BB84 File Offset: 0x00059D84
		public Ends(T Bottom, T Top)
		{
			this.Bottom = Bottom;
			this.Top = Top;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0005BB94 File Offset: 0x00059D94
		public override string ToString()
		{
			return string.Format("{{Bottom={0},Top={1}}}", this.Bottom, this.Top);
		}

		// Token: 0x04000C77 RID: 3191
		public T Bottom;

		// Token: 0x04000C78 RID: 3192
		public T Top;
	}

	// Token: 0x02000290 RID: 656
	protected internal struct Expansion
	{
		// Token: 0x06001795 RID: 6037 RVA: 0x0005BBC4 File Offset: 0x00059DC4
		internal Expansion(float Value, float FractionExpanded, float Amount)
		{
			this.Value = Value;
			this.FractionExpanded = FractionExpanded;
			this.Amount = Amount;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0005BBDC File Offset: 0x00059DDC
		public override string ToString()
		{
			return string.Format("{{Value={0},FractionExpanded={1},Amount={2}}}", this.Value, this.FractionExpanded, this.Amount);
		}

		// Token: 0x04000C79 RID: 3193
		public readonly float Value;

		// Token: 0x04000C7A RID: 3194
		public readonly float FractionExpanded;

		// Token: 0x04000C7B RID: 3195
		public readonly float Amount;
	}

	// Token: 0x02000291 RID: 657
	protected internal struct Initialization
	{
		// Token: 0x06001797 RID: 6039 RVA: 0x0005BC0C File Offset: 0x00059E0C
		public Initialization(CCTotemPole totemPole, CCDesc figurePrefab, float minHeight, float maxHeight, float initialHeight, float bottomBufferUnits)
		{
			this.totemPole = totemPole;
			this.figurePrefab = figurePrefab;
			this.minHeight = minHeight;
			this.maxHeight = maxHeight;
			this.initialHeight = initialHeight;
			this.bottomBufferUnits = bottomBufferUnits;
			this.nonDefault = true;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0005BC50 File Offset: 0x00059E50
		public override string ToString()
		{
			return CCTotem.ToStringHelper<CCTotem.Initialization>.GetString(this);
		}

		// Token: 0x04000C7C RID: 3196
		public readonly CCTotemPole totemPole;

		// Token: 0x04000C7D RID: 3197
		public readonly CCDesc figurePrefab;

		// Token: 0x04000C7E RID: 3198
		public readonly float minHeight;

		// Token: 0x04000C7F RID: 3199
		public readonly float maxHeight;

		// Token: 0x04000C80 RID: 3200
		public readonly float initialHeight;

		// Token: 0x04000C81 RID: 3201
		public readonly float bottomBufferUnits;

		// Token: 0x04000C82 RID: 3202
		public readonly bool nonDefault;
	}

	// Token: 0x02000292 RID: 658
	public struct PositionPlacement
	{
		// Token: 0x06001799 RID: 6041 RVA: 0x0005BC64 File Offset: 0x00059E64
		public PositionPlacement(Vector3 Bottom, Vector3 Top, Vector3 ColliderPosition, float OriginalHeight)
		{
			this.bottom = Bottom;
			this.top = Top;
			this.colliderCenter = ColliderPosition;
			this.height = Top.y - Bottom.y;
			this.originalHeight = OriginalHeight;
			this.originalTop.x = Bottom.x;
			this.originalTop.y = Bottom.y + OriginalHeight;
			this.originalTop.z = Bottom.z;
		}

		// Token: 0x04000C83 RID: 3203
		public Vector3 bottom;

		// Token: 0x04000C84 RID: 3204
		public Vector3 top;

		// Token: 0x04000C85 RID: 3205
		public Vector3 colliderCenter;

		// Token: 0x04000C86 RID: 3206
		public float height;

		// Token: 0x04000C87 RID: 3207
		public float originalHeight;

		// Token: 0x04000C88 RID: 3208
		public Vector3 originalTop;
	}

	// Token: 0x02000293 RID: 659
	protected internal struct Route
	{
		// Token: 0x0600179A RID: 6042 RVA: 0x0005BCDC File Offset: 0x00059EDC
		public Route(CCTotem.Direction Up, CCTotem.Direction At, CCTotem.Direction Down)
		{
			this.Up = Up;
			this.At = At;
			this.Down = Down;
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0005BCF4 File Offset: 0x00059EF4
		public unsafe bool GetUp(out CCTotem.Route route)
		{
			if (this.Up.Exists)
			{
				route = *this.Up.TotemicFigure;
				return true;
			}
			route = new CCTotem.Route(CCTotem.Direction.None, CCTotem.Direction.None, this.At);
			return false;
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0005BD48 File Offset: 0x00059F48
		public unsafe bool GetDown(out CCTotem.Route route)
		{
			if (this.Down.Exists)
			{
				route = *this.Down.TotemicFigure;
				return true;
			}
			route = new CCTotem.Route(this.At, CCTotem.Direction.None, CCTotem.Direction.None);
			return false;
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x0005BD9C File Offset: 0x00059F9C
		public IEnumerable<CCTotem.TotemicFigure> EnumerateUpInclusive
		{
			get
			{
				CCTotem.Route it = ref this;
				while (it.At.Exists)
				{
					yield return it.At.TotemicFigure;
					it.GetUp(out it);
				}
				yield break;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x0005BDC4 File Offset: 0x00059FC4
		public IEnumerable<CCTotem.TotemicFigure> EnumerateUp
		{
			get
			{
				CCTotem.Route it;
				if (this.GetUp(out it))
				{
					while (it.At.Exists)
					{
						yield return it.At.TotemicFigure;
						it.GetUp(out it);
					}
				}
				yield break;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x0005BDEC File Offset: 0x00059FEC
		public IEnumerable<CCTotem.TotemicFigure> EnumerateDownInclusive
		{
			get
			{
				CCTotem.Route it = ref this;
				while (it.At.Exists)
				{
					yield return it.At.TotemicFigure;
					it.GetDown(out it);
				}
				yield break;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0005BE14 File Offset: 0x0005A014
		public IEnumerable<CCTotem.TotemicFigure> EnumerateDown
		{
			get
			{
				CCTotem.Route it;
				if (this.GetUp(out it))
				{
					while (it.At.Exists)
					{
						yield return it.At.TotemicFigure;
						it.GetDown(out it);
					}
				}
				yield break;
			}
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0005BE3C File Offset: 0x0005A03C
		public override string ToString()
		{
			return string.Format("{{Up={0},At={1},Down={2}}}", this.Up, this.At, this.Down);
		}

		// Token: 0x04000C89 RID: 3209
		public readonly CCTotem.Direction Up;

		// Token: 0x04000C8A RID: 3210
		public readonly CCTotem.Direction At;

		// Token: 0x04000C8B RID: 3211
		public readonly CCTotem.Direction Down;
	}

	// Token: 0x02000294 RID: 660
	public abstract class TotemicObject
	{
		// Token: 0x060017A2 RID: 6050 RVA: 0x0005BE6C File Offset: 0x0005A06C
		internal TotemicObject()
		{
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x0005BE74 File Offset: 0x0005A074
		protected internal CCTotem Script
		{
			get
			{
				return this._Script;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060017A4 RID: 6052
		internal abstract CCTotem _Script { get; }
	}

	// Token: 0x02000295 RID: 661
	public abstract class TotemicObject<CCTotemScript> : CCTotem.TotemicObject where CCTotemScript : CCTotem, new()
	{
		// Token: 0x060017A5 RID: 6053 RVA: 0x0005BE7C File Offset: 0x0005A07C
		internal TotemicObject()
		{
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x0005BE84 File Offset: 0x0005A084
		internal sealed override CCTotem _Script
		{
			get
			{
				return this.Script;
			}
		}

		// Token: 0x04000C8C RID: 3212
		protected internal new CCTotemScript Script;
	}

	// Token: 0x02000296 RID: 662
	public abstract class TotemicObject<CCTotemScript, TTotemicObject> : CCTotem.TotemicObject<CCTotemScript> where CCTotemScript : CCTotem<TTotemicObject, CCTotemScript>, new() where TTotemicObject : CCTotem.TotemicObject<CCTotemScript, TTotemicObject>, new()
	{
		// Token: 0x060017A7 RID: 6055 RVA: 0x0005BE94 File Offset: 0x0005A094
		internal TotemicObject()
		{
		}

		// Token: 0x060017A8 RID: 6056
		internal abstract void OnScriptDestroy(CCTotemScript Script);

		// Token: 0x060017A9 RID: 6057
		internal abstract void AssignedToScript(CCTotemScript Script);
	}

	// Token: 0x02000297 RID: 663
	private static class ToStringHelper<T> where T : struct
	{
		// Token: 0x060017AA RID: 6058 RVA: 0x0005BE9C File Offset: 0x0005A09C
		static ToStringHelper()
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				stringWriter.Write("{{");
				bool flag = true;
				for (int i = 0; i < CCTotem.ToStringHelper<T>.allFields.Length; i++)
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringWriter.Write(", ");
					}
					stringWriter.Write("{0}={{{1}}}", CCTotem.ToStringHelper<T>.allFields[i].Name, i);
				}
				stringWriter.Write("}}");
			}
			CCTotem.ToStringHelper<T>.formatterString = stringBuilder.ToString();
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0005BF7C File Offset: 0x0005A17C
		public static string GetString(object boxed)
		{
			string result;
			try
			{
				for (int i = 0; i < CCTotem.ToStringHelper<T>.allFields.Length; i++)
				{
					CCTotem.ToStringHelper<T>.valueHolders[i] = CCTotem.ToStringHelper<T>.allFields[i].GetValue(boxed);
				}
				result = string.Format(CCTotem.ToStringHelper<T>.formatterString, CCTotem.ToStringHelper<T>.valueHolders);
			}
			finally
			{
				for (int j = 0; j < CCTotem.ToStringHelper<T>.allFields.Length; j++)
				{
					CCTotem.ToStringHelper<T>.valueHolders[j] = null;
				}
			}
			return result;
		}

		// Token: 0x04000C8D RID: 3213
		private static readonly FieldInfo[] allFields = typeof(T).GetFields();

		// Token: 0x04000C8E RID: 3214
		private static readonly object[] valueHolders = new object[CCTotem.ToStringHelper<T>.allFields.Length];

		// Token: 0x04000C8F RID: 3215
		private static readonly string formatterString;
	}

	// Token: 0x02000298 RID: 664
	public struct MoveInfo
	{
		// Token: 0x060017AC RID: 6060 RVA: 0x0005C010 File Offset: 0x0005A210
		public MoveInfo(CollisionFlags CollisionFlags, CollisionFlags WorkingCollisionFlags, float WantedHeight, Vector3 BottomMovement, Vector3 TopMovement, CCTotem.PositionPlacement PositionPlacement)
		{
			this.CollisionFlags = CollisionFlags;
			this.WorkingCollisionFlags = WorkingCollisionFlags;
			this.WantedHeight = WantedHeight;
			this.BottomMovement = BottomMovement;
			this.TopMovement = TopMovement;
			this.PositionPlacement = PositionPlacement;
		}

		// Token: 0x04000C90 RID: 3216
		public readonly CollisionFlags CollisionFlags;

		// Token: 0x04000C91 RID: 3217
		public readonly CollisionFlags WorkingCollisionFlags;

		// Token: 0x04000C92 RID: 3218
		public readonly float WantedHeight;

		// Token: 0x04000C93 RID: 3219
		public readonly Vector3 BottomMovement;

		// Token: 0x04000C94 RID: 3220
		public readonly Vector3 TopMovement;

		// Token: 0x04000C95 RID: 3221
		public readonly CCTotem.PositionPlacement PositionPlacement;
	}

	// Token: 0x02000299 RID: 665
	public sealed class TotemPole : CCTotem.TotemicObject<CCTotemPole, CCTotem.TotemPole>
	{
		// Token: 0x060017AD RID: 6061 RVA: 0x0005C040 File Offset: 0x0005A240
		[Obsolete("Infrastructure", true)]
		public TotemPole()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0005C050 File Offset: 0x0005A250
		internal TotemPole(ref CCTotem.Configuration TotemConfiguration)
		{
			this.Configuration = TotemConfiguration;
			this.TotemicFigures = new CCTotem.TotemicFigure[8];
			this.TotemicFigureEnds = CCTotem.TotemicFigure.CreateAllTotemicFigures(this);
			this.Contraction = CCTotem.Contraction.Define(this.Configuration.poleContractedHeight, this.Configuration.poleExpandedHeight);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0005C0A8 File Offset: 0x0005A2A8
		internal override void AssignedToScript(CCTotemPole Script)
		{
			this.Script = Script;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0005C0B4 File Offset: 0x0005A2B4
		internal override void OnScriptDestroy(CCTotemPole Script)
		{
			if (object.ReferenceEquals(this.Script, Script))
			{
				this.DeleteAllFiguresAndClearScript();
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0005C0D0 File Offset: 0x0005A2D0
		private void DeleteAllFiguresAndClearScript()
		{
			CCTotemPole script = this.Script;
			this.Script = null;
			for (int i = this.Configuration.numRequiredTotemicFigures - 1; i >= 0; i--)
			{
				CCTotem.TotemicFigure totemicFigure = this.TotemicFigures[i];
				if (!object.ReferenceEquals(totemicFigure, null))
				{
					if (totemicFigure.TotemPole == this)
					{
						this.TotemicFigures[i].Delete(script);
					}
					else
					{
						this.TotemicFigures[i] = null;
					}
				}
			}
			CCTotem.DestroyCCDesc(script, ref this.CCDesc);
			if (script && object.ReferenceEquals(script.totemicObject, this))
			{
				script.totemicObject = null;
			}
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0005C178 File Offset: 0x0005A378
		private CCDesc InstantiateCCDesc(Vector3 worldBottom, string name)
		{
			CCDesc ccdesc = (CCDesc)Object.Instantiate(this.Configuration.totem.figurePrefab, worldBottom, Quaternion.identity);
			if (!string.IsNullOrEmpty(name))
			{
				ccdesc.name = name;
			}
			ccdesc.gameObject.hideFlags = 8;
			ccdesc.detectCollisions = false;
			return ccdesc;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0005C1D4 File Offset: 0x0005A3D4
		private CCTotemicFigure InstantiateTotemicFigure(Vector3 worldBottom, CCTotem.TotemicFigure target)
		{
			worldBottom.y += target.TotemContractionBottom.ExpansionForFraction(this.Expansion.FractionExpanded).Value;
			target.CCDesc = this.InstantiateCCDesc(worldBottom, string.Format("__TotemicFigure{0}", target.BottomUpIndex));
			CCTotemicFigure cctotemicFigure = target.CCDesc.gameObject.AddComponent<CCTotemicFigure>();
			cctotemicFigure.AssignTotemicObject(target);
			if (this.Script)
			{
				this.Script.ExecuteBinding(target.CCDesc, true);
			}
			return cctotemicFigure;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0005C270 File Offset: 0x0005A470
		public void Create()
		{
			float initialHeight = this.Configuration.totem.initialHeight;
			this.Expansion = this.Contraction.ExpansionForValue(initialHeight);
			Vector3 worldBottom = this.Script.transform.position + this.Configuration.figureOriginOffsetBottom;
			this.CCDesc = this.InstantiateCCDesc(worldBottom, "__TotemPole");
			if (this.Script)
			{
				this.Script.ExecuteBinding(this.CCDesc, true);
			}
			for (int i = 0; i < this.Configuration.numRequiredTotemicFigures; i++)
			{
				this.InstantiateTotemicFigure(worldBottom, this.TotemicFigures[i]);
			}
			this.Point.Bottom = this.TotemicFigures[0].BottomOrigin;
			this.Point.Top = this.TotemicFigureEnds.Top.TopOrigin;
			this.CCDesc.ModifyHeight(this.Point.Top.y - this.Point.Bottom.y, false);
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0005C39C File Offset: 0x0005A59C
		private CCDesc CCDescOrPrefab
		{
			get
			{
				return (!this.CCDesc) ? this.Configuration.totem.figurePrefab : this.CCDesc;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0005C3DC File Offset: 0x0005A5DC
		public bool isGrounded
		{
			get
			{
				return this.grounded;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0005C3E4 File Offset: 0x0005A5E4
		public Vector3 velocity
		{
			get
			{
				return (!this.CCDesc) ? Vector3.zero : this.CCDesc.velocity;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0005C40C File Offset: 0x0005A60C
		public CollisionFlags collisionFlags
		{
			get
			{
				return (!this.CCDesc) ? 0 : this.CCDesc.collisionFlags;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x0005C430 File Offset: 0x0005A630
		public float stepOffset
		{
			get
			{
				return this.CCDescOrPrefab.stepOffset;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x0005C440 File Offset: 0x0005A640
		public float slopeLimit
		{
			get
			{
				return this.CCDescOrPrefab.slopeLimit;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x0005C450 File Offset: 0x0005A650
		public float height
		{
			get
			{
				return this.CCDescOrPrefab.height;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0005C460 File Offset: 0x0005A660
		public float radius
		{
			get
			{
				return this.CCDescOrPrefab.radius;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x0005C470 File Offset: 0x0005A670
		public Vector3 center
		{
			get
			{
				return this.CCDescOrPrefab.center;
			}
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0005C480 File Offset: 0x0005A680
		public CCTotem.MoveInfo Move(Vector3 motion, float height)
		{
			CCTotem.Expansion expansion = this.Contraction.ExpansionForValue(height);
			height = expansion.Value;
			CollisionFlags collisionFlags = this.TotemicFigureEnds.Bottom.MoveSweep(motion) & this.TotemicFigureEnds.Bottom.CollisionFlagsMask;
			this.grounded = this.TotemicFigureEnds.Bottom.CCDesc.isGrounded;
			int num = 0;
			for (int i = this.Configuration.numRequiredTotemicFigures - 1; i >= 1; i--)
			{
				Vector3 sweepMovement = this.TotemicFigures[num].SweepMovement;
				collisionFlags |= (this.TotemicFigures[i].MoveSweep(sweepMovement) & this.TotemicFigures[i].CollisionFlagsMask);
				num = i;
			}
			if (this.TotemicFigures[num].SweepMovement != this.TotemicFigures[0].SweepMovement)
			{
				Vector3 sweepMovement2 = this.TotemicFigures[num].SweepMovement;
				for (int j = 0; j < this.Configuration.numRequiredTotemicFigures; j++)
				{
					Vector3 motion2 = sweepMovement2 - this.TotemicFigures[j].SweepMovement;
					collisionFlags |= (this.TotemicFigures[j].MoveSweep(motion2) & this.TotemicFigures[j].CollisionFlagsMask);
				}
			}
			this.Point.Bottom = this.TotemicFigures[0].BottomOrigin;
			this.Point.Top = this.TotemicFigureEnds.Top.TopOrigin;
			this.Expansion = this.Contraction.ExpansionForValue(this.Point.Top.y - this.Point.Bottom.y);
			if (this.Expansion.Value != expansion.Value)
			{
				Vector3 targetTop = this.Point.Bottom + new Vector3(0f, expansion.Value, 0f);
				collisionFlags |= (this.TotemicFigureEnds.Top.MoveWorldTopTo(targetTop) & this.TotemicFigureEnds.Top.CollisionFlagsMask);
				Vector3 topOrigin = this.TotemicFigureEnds.Top.TopOrigin;
				expansion = this.Contraction.ExpansionForValue(topOrigin.y - this.Point.Bottom.y);
				for (int k = this.Configuration.numRequiredTotemicFigures - 2; k > 0; k--)
				{
					CCTotem.TotemicFigure totemicFigure = this.TotemicFigures[k];
					Vector3 bottom = this.Point.Bottom;
					bottom.y += totemicFigure.TotemContractionBottom.ExpansionForFraction(expansion.FractionExpanded).Value;
					collisionFlags |= (totemicFigure.MoveWorldBottomTo(bottom) & totemicFigure.CollisionFlagsMask);
				}
				this.Point.Top = this.TotemicFigureEnds.Top.TopOrigin;
				this.Expansion = expansion;
			}
			float effectiveSkinnedHeight = this.CCDesc.effectiveSkinnedHeight;
			Vector3 worldSkinnedBottom = this.CCDesc.worldSkinnedBottom;
			Vector3 worldSkinnedTop = this.CCDesc.worldSkinnedTop;
			Vector3 vector = this.TotemicFigures[0].BottomOrigin - worldSkinnedBottom;
			CCDesc.HeightModification heightModification = this.CCDesc.ModifyHeight(this.Expansion.Value, false);
			CollisionFlags collisionFlags2 = this.CCDesc.Move(vector);
			Vector3 worldSkinnedBottom2 = this.CCDesc.worldSkinnedBottom;
			Vector3 vector2 = worldSkinnedBottom2 - worldSkinnedBottom;
			if (vector != vector2)
			{
				Vector3 motion3 = vector2 - vector;
				for (int l = 0; l < this.Configuration.numRequiredTotemicFigures; l++)
				{
					collisionFlags |= (this.TotemicFigures[l].MoveSweep(motion3) & this.TotemicFigures[l].CollisionFlagsMask);
				}
				this.Point.Bottom = this.TotemicFigures[0].BottomOrigin;
				this.Point.Top = this.TotemicFigureEnds.Top.TopOrigin;
				this.Expansion = this.Contraction.ExpansionForValue(this.Point.Top.y - this.Point.Bottom.y);
				CCDesc.HeightModification heightModification2 = this.CCDesc.ModifyHeight(this.Expansion.Value, false);
				worldSkinnedBottom2 = this.CCDesc.worldSkinnedBottom;
				vector2 = worldSkinnedBottom2 - worldSkinnedBottom;
			}
			Vector3 worldSkinnedTop2 = this.CCDesc.worldSkinnedTop;
			Vector3 topMovement = worldSkinnedTop2 - worldSkinnedTop;
			CCTotem.PositionPlacement positionPlacement = new CCTotem.PositionPlacement(worldSkinnedBottom2, worldSkinnedTop2, this.CCDesc.transform.position, this.Configuration.poleExpandedHeight);
			return new CCTotem.MoveInfo(collisionFlags2, collisionFlags, height, vector2, topMovement, positionPlacement);
		}

		// Token: 0x04000C96 RID: 3222
		private const int kCrouch_NotModified = 0;

		// Token: 0x04000C97 RID: 3223
		private const int kCrouch_MovingDown = -1;

		// Token: 0x04000C98 RID: 3224
		private const int kCrouch_MovingUp = 1;

		// Token: 0x04000C99 RID: 3225
		internal readonly CCTotem.Configuration Configuration;

		// Token: 0x04000C9A RID: 3226
		internal readonly CCTotem.TotemicFigure[] TotemicFigures;

		// Token: 0x04000C9B RID: 3227
		internal readonly CCTotem.Ends<CCTotem.TotemicFigure> TotemicFigureEnds;

		// Token: 0x04000C9C RID: 3228
		internal readonly CCTotem.Contraction Contraction;

		// Token: 0x04000C9D RID: 3229
		internal CCTotem.Ends<Vector3> Point;

		// Token: 0x04000C9E RID: 3230
		internal CCTotem.Expansion Expansion;

		// Token: 0x04000C9F RID: 3231
		internal CCDesc CCDesc;

		// Token: 0x04000CA0 RID: 3232
		private bool grounded;
	}

	// Token: 0x0200029A RID: 666
	public sealed class TotemicFigure : CCTotem.TotemicObject<CCTotemicFigure, CCTotem.TotemicFigure>
	{
		// Token: 0x060017BF RID: 6079 RVA: 0x0005C960 File Offset: 0x0005AB60
		[Obsolete("Infrastructure", true)]
		public TotemicFigure()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0005C970 File Offset: 0x0005AB70
		private TotemicFigure(CCTotem.TotemPole TotemPole, int BottomUpIndex)
		{
			this.TotemPole = TotemPole;
			this.BottomUpIndex = BottomUpIndex;
			this.TopDownIndex = this.TotemPole.Configuration.numRequiredTotemicFigures - (this.BottomUpIndex + 1);
			this.CollisionFlagsMask = 1;
			if (this.BottomUpIndex == 0)
			{
				this.CollisionFlagsMask |= 4;
			}
			if (this.TopDownIndex == 0)
			{
				this.CollisionFlagsMask |= 2;
			}
			this.TotemPole.TotemicFigures[this.BottomUpIndex] = this;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0005C9FC File Offset: 0x0005ABFC
		private TotemicFigure(CCTotem.Direction Down) : this(Down.TotemicFigure.TotemPole, Down.TotemicFigure.BottomUpIndex + 1)
		{
			float num = (float)this.BottomUpIndex / (float)this.TotemPole.Configuration.numSlidingTotemicFigures;
			float num2 = (this.TotemPole.Configuration.numSlidingTotemicFigures != 1) ? ((float)(this.BottomUpIndex - 1) / (float)(this.TotemPole.Configuration.numSlidingTotemicFigures - 1)) : num;
			float num3 = Mathf.Lerp(this.TotemPole.Configuration.poleBottomBufferAmount, this.TotemPole.Configuration.poleContractedHeight - this.TotemPole.Configuration.figureSkinnedHeight, num2);
			float num4 = Mathf.Lerp(this.TotemPole.Configuration.poleBottomBufferAmount, this.TotemPole.Configuration.poleExpandedHeight - this.TotemPole.Configuration.figureSkinnedHeight, num);
			this.TotemContractionBottom = CCTotem.Contraction.Define(num3, num4);
			this.TotemContractionTop = CCTotem.Contraction.Define(num3 + this.TotemPole.Configuration.figureSkinnedHeight, num4 + this.TotemPole.Configuration.figureSkinnedHeight);
			CCTotem.Direction direction = new CCTotem.Direction(this);
			CCTotem.Direction none;
			if (this.BottomUpIndex < this.TotemPole.Configuration.numRequiredTotemicFigures - 1)
			{
				none = new CCTotem.Direction(new CCTotem.TotemicFigure(direction));
			}
			else
			{
				none = CCTotem.Direction.None;
			}
			this.TotemicRoute = new CCTotem.Route(Down, direction, none);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0005CB78 File Offset: 0x0005AD78
		private TotemicFigure(CCTotem.TotemPole TotemPole) : this(TotemPole, 0)
		{
			CCTotem.Direction direction = new CCTotem.Direction(this);
			this.TotemicRoute = new CCTotem.Route(CCTotem.Direction.None, direction, new CCTotem.Direction(new CCTotem.TotemicFigure(direction)));
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x0005CBB4 File Offset: 0x0005ADB4
		internal static CCTotem.Ends<CCTotem.TotemicFigure> CreateAllTotemicFigures(CCTotem.TotemPole TotemPole)
		{
			if (!object.ReferenceEquals(TotemPole.TotemicFigures[0], null))
			{
				throw new ArgumentException("The totem pole already has totemic figures", "TotemPole");
			}
			CCTotem.TotemicFigure bottom = new CCTotem.TotemicFigure(TotemPole);
			CCTotem.TotemicFigure top = TotemPole.TotemicFigures[TotemPole.Configuration.numRequiredTotemicFigures - 1];
			return new CCTotem.Ends<CCTotem.TotemicFigure>(bottom, top);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0005CC0C File Offset: 0x0005AE0C
		internal override void OnScriptDestroy(CCTotemicFigure Script)
		{
			if (object.ReferenceEquals(this.Script, Script))
			{
				this.Script = null;
				if (object.ReferenceEquals(Script.totemicObject, this))
				{
					Script.totemicObject = null;
				}
			}
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0005CC4C File Offset: 0x0005AE4C
		internal override void AssignedToScript(CCTotemicFigure Script)
		{
			this.Script = Script;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0005CC58 File Offset: 0x0005AE58
		internal void Delete(CCTotemPole OwnerScript)
		{
			CCTotemicFigure script = this.Script;
			this.Script = null;
			if (script && object.ReferenceEquals(script.totemicObject, this))
			{
				script.totemicObject = null;
			}
			CCTotem.DestroyCCDesc(OwnerScript, ref this.CCDesc);
			if (script)
			{
				Object.Destroy(script.gameObject);
			}
			if (object.ReferenceEquals(this.TotemPole.TotemicFigures[this.BottomUpIndex], this))
			{
				this.TotemPole.TotemicFigures[this.BottomUpIndex] = null;
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0005CCE8 File Offset: 0x0005AEE8
		public override string ToString()
		{
			return string.Format("{{Index={0},ContractionBottom={1},ContractionTop={2},Script={3}}}", new object[]
			{
				this.BottomUpIndex,
				this.TotemContractionBottom,
				this.TotemContractionTop,
				this.Script
			});
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0005CD38 File Offset: 0x0005AF38
		public Vector3 BottomOrigin
		{
			get
			{
				return this.CCDesc.worldSkinnedBottom;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0005CD48 File Offset: 0x0005AF48
		public Vector3 CenterOrigin
		{
			get
			{
				return this.CCDesc.worldCenter;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0005CD58 File Offset: 0x0005AF58
		public Vector3 TopOrigin
		{
			get
			{
				return this.CCDesc.worldSkinnedTop;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0005CD68 File Offset: 0x0005AF68
		public Vector3 SlideBottomOrigin
		{
			get
			{
				return this.CCDesc.OffsetToWorld(this.CCDesc.center - new Vector3(0f, this.CCDesc.effectiveSkinnedHeight * 0.5f - this.CCDesc.skinnedRadius, 0f));
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x0005CDBC File Offset: 0x0005AFBC
		public Vector3 SlideTopOrigin
		{
			get
			{
				return this.CCDesc.OffsetToWorld(this.CCDesc.center + new Vector3(0f, this.CCDesc.effectiveSkinnedHeight * 0.5f - this.CCDesc.skinnedRadius, 0f));
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0005CE10 File Offset: 0x0005B010
		public CollisionFlags MoveWorldBottomTo(Vector3 targetBottom)
		{
			return this.MoveWorld(targetBottom - this.BottomOrigin);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0005CE24 File Offset: 0x0005B024
		public CollisionFlags MoveWorldTopTo(Vector3 targetTop)
		{
			return this.MoveWorld(targetTop - this.TopOrigin);
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0005CE38 File Offset: 0x0005B038
		public CollisionFlags MoveWorld(Vector3 motion)
		{
			return this.CCDesc.Move(motion);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0005CE48 File Offset: 0x0005B048
		public CollisionFlags MoveSweep(Vector3 motion)
		{
			this.PreSweepBottom = this.BottomOrigin;
			CollisionFlags result = this.MoveWorld(motion);
			this.PostSweepBottom = this.BottomOrigin;
			this.SweepMovement = this.PostSweepBottom - this.PreSweepBottom;
			return result;
		}

		// Token: 0x04000CA1 RID: 3233
		public CCDesc CCDesc;

		// Token: 0x04000CA2 RID: 3234
		internal readonly CCTotem.TotemPole TotemPole;

		// Token: 0x04000CA3 RID: 3235
		internal readonly int BottomUpIndex;

		// Token: 0x04000CA4 RID: 3236
		internal readonly int TopDownIndex;

		// Token: 0x04000CA5 RID: 3237
		internal readonly CollisionFlags CollisionFlagsMask;

		// Token: 0x04000CA6 RID: 3238
		internal readonly CCTotem.Route TotemicRoute;

		// Token: 0x04000CA7 RID: 3239
		internal readonly CCTotem.Contraction TotemContractionTop;

		// Token: 0x04000CA8 RID: 3240
		internal readonly CCTotem.Contraction TotemContractionBottom;

		// Token: 0x04000CA9 RID: 3241
		public Vector3 PreSweepBottom;

		// Token: 0x04000CAA RID: 3242
		public Vector3 PostSweepBottom;

		// Token: 0x04000CAB RID: 3243
		public Vector3 SweepMovement;
	}

	// Token: 0x0200086A RID: 2154
	// (Invoke) Token: 0x06004B80 RID: 19328
	public delegate void PositionBinder(ref CCTotem.PositionPlacement binding, object Tag);

	// Token: 0x0200086B RID: 2155
	// (Invoke) Token: 0x06004B84 RID: 19332
	public delegate void ConfigurationBinder(bool Bind, CCDesc CCDesc, object Tag);
}
