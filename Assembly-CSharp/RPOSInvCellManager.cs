using System;
using UnityEngine;

// Token: 0x020004CA RID: 1226
public class RPOSInvCellManager : MonoBehaviour
{
	// Token: 0x06002A2B RID: 10795 RVA: 0x0009C510 File Offset: 0x0009A710
	private void Awake()
	{
		if (this.SpawnCells)
		{
			this.CreateCellsOnGameObject(null, base.gameObject);
		}
	}

	// Token: 0x06002A2C RID: 10796 RVA: 0x0009C52C File Offset: 0x0009A72C
	public void SetInventory(global::Inventory newInv, bool spawnNewCells)
	{
		this.displayInventory = newInv;
		if (spawnNewCells && this.SpawnCells)
		{
			this.generatedCells = true;
			for (int i = 0; i < this._inventoryCells.Length; i++)
			{
				Object.Destroy(this._inventoryCells[i].gameObject);
				this._inventoryCells[i] = null;
			}
			this.NumCellsVertical = Mathf.CeilToInt((float)newInv.slotCount / 3f);
			this.CreateCellsOnGameObject(newInv, base.gameObject);
		}
		int num = 0;
		foreach (global::RPOSInventoryCell rposinventoryCell in this._inventoryCells)
		{
			rposinventoryCell._displayInventory = newInv;
			rposinventoryCell._mySlot = (byte)(this.CellIndexStart + num);
			newInv.MarkSlotDirty((int)rposinventoryCell._mySlot);
			num++;
		}
	}

	// Token: 0x06002A2D RID: 10797 RVA: 0x0009C600 File Offset: 0x0009A800
	public int GetNumCells()
	{
		if (this.SpawnCells || this.generatedCells)
		{
			return this.NumCellsHorizontal * this.NumCellsVertical;
		}
		return this._inventoryCells.Length;
	}

	// Token: 0x06002A2E RID: 10798 RVA: 0x0009C63C File Offset: 0x0009A83C
	public static int GetIndex2D(int x, int y, int width)
	{
		return x + y * width;
	}

	// Token: 0x06002A2F RID: 10799 RVA: 0x0009C644 File Offset: 0x0009A844
	protected virtual void CreateCellsOnGameObject(global::Inventory inven, GameObject parent)
	{
		bool flag = inven;
		int newSize;
		int num;
		if (flag)
		{
			global::Inventory.Slot.Range range;
			inven.GetSlotsOfKind(global::Inventory.Slot.Kind.Default, out range);
			newSize = range.Count;
			num = range.End;
		}
		else
		{
			newSize = this.GetNumCells();
			num = int.MaxValue;
		}
		Array.Resize<global::RPOSInventoryCell>(ref this._inventoryCells, newSize);
		float x = this.CellPrefab.GetComponent<global::RPOSInventoryCell>()._background.transform.localScale.x;
		float y = this.CellPrefab.GetComponent<global::RPOSInventoryCell>()._background.transform.localScale.y;
		for (int i = 0; i < this.NumCellsVertical; i++)
		{
			for (int j = 0; j < this.NumCellsHorizontal; j++)
			{
				byte b = (byte)(this.CellIndexStart + global::RPOSInvCellManager.GetIndex2D(j, i, this.NumCellsHorizontal));
				if ((int)b >= num)
				{
					return;
				}
				GameObject gameObject = global::NGUITools.AddChild(parent, this.CellPrefab);
				global::RPOSInventoryCell component = gameObject.GetComponent<global::RPOSInventoryCell>();
				component._mySlot = b;
				component._displayInventory = inven;
				if (this.NumberedCells)
				{
					component._numberLabel.text = (global::RPOSInvCellManager.GetIndex2D(j, i, this.NumCellsHorizontal) + 1).ToString();
				}
				gameObject.transform.localPosition = new Vector3((float)this.CellOffsetX + ((float)j * x + (float)(j * this.CellSpacing)), -((float)this.CellOffsetY + ((float)i * y + (float)(i * this.CellSpacing))), -2f);
				this._inventoryCells[global::RPOS.GetIndex2D(j, i, this.NumCellsHorizontal)] = gameObject.GetComponent<global::RPOSInventoryCell>();
			}
		}
		if (this.CenterFromCells)
		{
			if (this.NumCellsHorizontal > 1)
			{
				base.transform.localPosition = new Vector3((float)(this.CellOffsetX + (this.NumCellsHorizontal * this.CellSize + (this.NumCellsHorizontal - 1) * this.CellSpacing)) * -0.5f, (float)this.CellSize, 0f);
			}
			else if (this.NumCellsVertical > 1)
			{
				base.transform.localPosition = new Vector3((float)(-(float)this.CellSize), (float)(this.CellOffsetY + this.NumCellsVertical * this.CellSize) * 0.5f, 0f);
			}
		}
	}

	// Token: 0x04001465 RID: 5221
	public bool SpawnCells;

	// Token: 0x04001466 RID: 5222
	private bool generatedCells;

	// Token: 0x04001467 RID: 5223
	public int NumCellsHorizontal;

	// Token: 0x04001468 RID: 5224
	public int NumCellsVertical;

	// Token: 0x04001469 RID: 5225
	public int CellOffsetX;

	// Token: 0x0400146A RID: 5226
	public int CellOffsetY;

	// Token: 0x0400146B RID: 5227
	public int CellSize = 96;

	// Token: 0x0400146C RID: 5228
	public int CellSpacing = 10;

	// Token: 0x0400146D RID: 5229
	public int CellIndexStart;

	// Token: 0x0400146E RID: 5230
	public bool CenterFromCells;

	// Token: 0x0400146F RID: 5231
	public bool NumberedCells;

	// Token: 0x04001470 RID: 5232
	public GameObject CellPrefab;

	// Token: 0x04001471 RID: 5233
	public global::Inventory displayInventory;

	// Token: 0x04001472 RID: 5234
	public global::RPOSInventoryCell[] _inventoryCells;
}
