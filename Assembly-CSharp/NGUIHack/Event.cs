using System;
using UnityEngine;

namespace NGUIHack
{
	// Token: 0x0200080D RID: 2061
	public class Event : IDisposable
	{
		// Token: 0x060049D9 RID: 18905 RVA: 0x0013B25C File Offset: 0x0013945C
		internal Event(Event @event)
		{
			this.@event = @event;
			this.originalType = @event.type;
			this.originalRawType = @event.rawType;
			this.overrideType = 12;
			this.screenPosition = Input.mousePosition;
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x0013B2A8 File Offset: 0x001394A8
		internal Event(Event @event, EventType overrideType) : this(@event)
		{
			this.overrideType = overrideType;
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x060049DB RID: 18907 RVA: 0x0013B2B8 File Offset: 0x001394B8
		public EventType type
		{
			get
			{
				return (this.overrideType != 12) ? this.overrideType : this.@event.type;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x060049DC RID: 18908 RVA: 0x0013B2E0 File Offset: 0x001394E0
		public EventType rawType
		{
			get
			{
				return (this.overrideType != 12) ? this.overrideType : this.@event.rawType;
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x060049DD RID: 18909 RVA: 0x0013B308 File Offset: 0x00139508
		public int button
		{
			get
			{
				return this.@event.button;
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x060049DE RID: 18910 RVA: 0x0013B318 File Offset: 0x00139518
		public Vector2 mousePosition
		{
			get
			{
				return this.screenPosition;
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x060049DF RID: 18911 RVA: 0x0013B320 File Offset: 0x00139520
		public KeyCode keyCode
		{
			get
			{
				return this.@event.keyCode;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x0013B330 File Offset: 0x00139530
		public char character
		{
			get
			{
				return this.@event.character;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x060049E1 RID: 18913 RVA: 0x0013B340 File Offset: 0x00139540
		public EventModifiers modifiers
		{
			get
			{
				return this.@event.modifiers;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x060049E2 RID: 18914 RVA: 0x0013B350 File Offset: 0x00139550
		public bool shift
		{
			get
			{
				return this.@event.shift;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x060049E3 RID: 18915 RVA: 0x0013B360 File Offset: 0x00139560
		public bool alt
		{
			get
			{
				return this.@event.alt;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x0013B370 File Offset: 0x00139570
		public bool control
		{
			get
			{
				return this.@event.control;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x060049E5 RID: 18917 RVA: 0x0013B380 File Offset: 0x00139580
		public bool capsLock
		{
			get
			{
				return this.@event.capsLock;
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x060049E6 RID: 18918 RVA: 0x0013B390 File Offset: 0x00139590
		public Vector2 delta
		{
			get
			{
				return this.@event.delta;
			}
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x0013B3A0 File Offset: 0x001395A0
		public void Dispose()
		{
			if (this.overrideType != 12 && this.@event.type == this.overrideType)
			{
				this.@event.type = this.originalType;
			}
		}

		// Token: 0x060049E8 RID: 18920 RVA: 0x0013B3E4 File Offset: 0x001395E4
		public void Use()
		{
			if (this.overrideType == 12)
			{
				this.@event.Use();
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x060049E9 RID: 18921 RVA: 0x0013B400 File Offset: 0x00139600
		internal Event real
		{
			get
			{
				return this.@event;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x060049EA RID: 18922 RVA: 0x0013B408 File Offset: 0x00139608
		public EventType unityOriginalRawType
		{
			get
			{
				return this.originalRawType;
			}
		}

		// Token: 0x040029D0 RID: 10704
		public static int pressed;

		// Token: 0x040029D1 RID: 10705
		public static int unpressed;

		// Token: 0x040029D2 RID: 10706
		public static int held;

		// Token: 0x040029D3 RID: 10707
		private readonly Event @event;

		// Token: 0x040029D4 RID: 10708
		private readonly EventType originalType;

		// Token: 0x040029D5 RID: 10709
		private readonly EventType originalRawType;

		// Token: 0x040029D6 RID: 10710
		private readonly EventType overrideType;

		// Token: 0x040029D7 RID: 10711
		private readonly Vector2 screenPosition;
	}
}
