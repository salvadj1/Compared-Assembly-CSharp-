using System;

namespace JSON
{
	// Token: 0x02000854 RID: 2132
	public class Value
	{
		// Token: 0x06004AF5 RID: 19189 RVA: 0x00148034 File Offset: 0x00146234
		public Value(ValueType type)
		{
			this.Type = type;
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x00148044 File Offset: 0x00146244
		public Value(string str)
		{
			this.Type = ValueType.String;
			this.Str = str;
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x0014805C File Offset: 0x0014625C
		public Value(double number)
		{
			this.Type = ValueType.Number;
			this.Number = number;
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x00148074 File Offset: 0x00146274
		public Value(Object obj)
		{
			if (obj == null)
			{
				this.Type = ValueType.Null;
			}
			else
			{
				this.Type = ValueType.Object;
				this.Obj = obj;
			}
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x001480A8 File Offset: 0x001462A8
		public Value(Array array)
		{
			this.Type = ValueType.Array;
			this.Array = array;
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x001480C0 File Offset: 0x001462C0
		public Value(bool boolean)
		{
			this.Type = ValueType.Boolean;
			this.Boolean = boolean;
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x001480D8 File Offset: 0x001462D8
		public Value(Value value)
		{
			this.Type = value.Type;
			switch (this.Type)
			{
			case ValueType.String:
				this.Str = value.Str;
				break;
			case ValueType.Number:
				this.Number = value.Number;
				break;
			case ValueType.Object:
				if (value.Obj != null)
				{
					this.Obj = new Object(value.Obj);
				}
				break;
			case ValueType.Array:
				this.Array = new Array(value.Array);
				break;
			case ValueType.Boolean:
				this.Boolean = value.Boolean;
				break;
			}
		}

		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x06004AFC RID: 19196 RVA: 0x00148188 File Offset: 0x00146388
		// (set) Token: 0x06004AFD RID: 19197 RVA: 0x00148190 File Offset: 0x00146390
		public ValueType Type { get; private set; }

		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x06004AFE RID: 19198 RVA: 0x0014819C File Offset: 0x0014639C
		// (set) Token: 0x06004AFF RID: 19199 RVA: 0x001481A4 File Offset: 0x001463A4
		public string Str { get; set; }

		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06004B00 RID: 19200 RVA: 0x001481B0 File Offset: 0x001463B0
		// (set) Token: 0x06004B01 RID: 19201 RVA: 0x001481B8 File Offset: 0x001463B8
		public double Number { get; set; }

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06004B02 RID: 19202 RVA: 0x001481C4 File Offset: 0x001463C4
		// (set) Token: 0x06004B03 RID: 19203 RVA: 0x001481CC File Offset: 0x001463CC
		public Object Obj { get; set; }

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06004B04 RID: 19204 RVA: 0x001481D8 File Offset: 0x001463D8
		// (set) Token: 0x06004B05 RID: 19205 RVA: 0x001481E0 File Offset: 0x001463E0
		public Array Array { get; set; }

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06004B06 RID: 19206 RVA: 0x001481EC File Offset: 0x001463EC
		// (set) Token: 0x06004B07 RID: 19207 RVA: 0x001481F4 File Offset: 0x001463F4
		public bool Boolean { get; set; }

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06004B08 RID: 19208 RVA: 0x00148200 File Offset: 0x00146400
		// (set) Token: 0x06004B09 RID: 19209 RVA: 0x00148208 File Offset: 0x00146408
		public Value Parent { get; set; }

		// Token: 0x06004B0A RID: 19210 RVA: 0x00148214 File Offset: 0x00146414
		public override string ToString()
		{
			switch (this.Type)
			{
			case ValueType.String:
				return "\"" + this.Str + "\"";
			case ValueType.Number:
				return this.Number.ToString();
			case ValueType.Object:
				return this.Obj.ToString();
			case ValueType.Array:
				return this.Array.ToString();
			case ValueType.Boolean:
				return (!this.Boolean) ? "false" : "true";
			case ValueType.Null:
				return "null";
			default:
				return "null";
			}
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x001482B0 File Offset: 0x001464B0
		public static implicit operator Value(string str)
		{
			return new Value(str);
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x001482B8 File Offset: 0x001464B8
		public static implicit operator Value(double number)
		{
			return new Value(number);
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x001482C0 File Offset: 0x001464C0
		public static implicit operator Value(Object obj)
		{
			return new Value(obj);
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x001482C8 File Offset: 0x001464C8
		public static implicit operator Value(Array array)
		{
			return new Value(array);
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x001482D0 File Offset: 0x001464D0
		public static implicit operator Value(bool boolean)
		{
			return new Value(boolean);
		}
	}
}
