using System;
using UnityEngine;

// Token: 0x02000415 RID: 1045
public class RPOSInvCellManager : MonoBehaviour
{
	// Token: 0x060026A1 RID: 9889 RVA: 0x0009664C File Offset: 0x0009484C
	private void Awake()
	{
		if (this.SpawnCells)
		{
			this.CreateCellsOnGameObject(null, base.gameObject);
		}
	}

	// Token: 0x060026A2 RID: 9890 RVA: 0x00096668 File Offset: 0x00094868
	public void SetInventory(Inventory newInv, bool spawnNewCells)
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
		foreach (RPOSInventoryCell rposinventoryCell in this._inventoryCells)
		{
			rposinventoryCell._displayInventory = newInv;
			rposinventoryCell._mySlot = (byte)(this.CellIndexStart + num);
			newInv.MarkSlotDirty((int)rposinventoryCell._mySlot);
			num++;
		}
	}

	// Token: 0x060026A3 RID: 9891 RVA: 0x0009673C File Offset: 0x0009493C
	public int GetNumCells()
	{
		if (this.SpawnCells || this.generatedCells)
		{
			return this.NumCellsHorizontal * this.NumCellsVertical;
		}
		return this._inventoryCells.Length;
	}

	// Token: 0x060026A4 RID: 9892 RVA: 0x00096778 File Offset: 0x00094978
	public static int GetIndex2D(int x, int y, int width)
	{
		return x + y * width;
	}

	// Token: 0x060026A5 RID: 9893 RVA: 0x00096780 File Offset: 0x00094980
	protected virtual void CreateCellsOnGameObject(Inventory inven, GameObject parent)
	{
		bool flag = inven;
		int newSize;
		int num;
		if (flag)
		{
			Inventory.Slot.Range range;
			inven.GetSlotsOfKind(Inventory.Slot.Kind.Default, out range);
			newSize = range.Count;
			num = range.End;
		}
		else
		{
			newSize = this.GetNumCells();
			num = int.MaxValue;
		}
		Array.Resize<RPOSInventoryCell>(ref this._inventoryCells, newSize);
		float x = this.CellPrefab.GetComponent<RPOSInventoryCell>()._background.transform.localScale.x;
		float y = this.CellPrefab.GetComponent<RPOSInventoryCell>()._background.transform.localScale.y;
		for (int i = 0; i < this.NumCellsVertical; i++)
		{
			for (int j = 0; j < this.NumCellsHorizontal; j++)
			{
				byte b = (byte)(this.CellIndexStart + RPOSInvCellManager.GetIndex2D(j, i, this.NumCellsHorizontal));
				if ((int)b >= num)
				{
					return;
				}
				GameObject gameObject = NGUITools.AddChild(parent, this.CellPrefab);
				RPOSInventoryCell component = gameObject.GetComponent<RPOSInventoryCell>();
				component._mySlot = b;
				component._displayInventory = inven;
				if (this.NumberedCells)
				{
					component._numberLabel.text = (RPOSInvCellManager.GetIndex2D(j, i, this.NumCellsHorizontal) + 1).ToString();
				}
				gameObject.transform.localPosition = new Vector3((float)this.CellOffsetX + ((float)j * x + (float)(j * this.CellSpacing)), -((float)this.CellOffsetY + ((float)i * y + (float)(i * this.CellSpacing))), -2f);
				this._inventoryCells[RPOS.GetIndex2D(j, i, this.NumCellsHorizontal)] = gameObject.GetComponent<RPOSInventoryCell>();
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

	// Token: 0x040012E5 RID: 4837
	public bool SpawnCells;

	// Token: 0x040012E6 RID: 4838
	private bool generatedCells;

	// Token: 0x040012E7 RID: 4839
	public int NumCellsHorizontal;

	// Token: 0x040012E8 RID: 4840
	public int NumCellsVertical;

	// Token: 0x040012E9 RID: 4841
	public int CellOffsetX;

	// Token: 0x040012EA RID: 4842
	public int CellOffsetY;

	// Token: 0x040012EB RID: 4843
	public int CellSize = 96;

	// Token: 0x040012EC RID: 4844
	public int CellSpacing = 10;

	// Token: 0x040012ED RID: 4845
	public int CellIndexStart;

	// Token: 0x040012EE RID: 4846
	public bool CenterFromCells;

	// Token: 0x040012EF RID: 4847
	public bool NumberedCells;

	// Token: 0x040012F0 RID: 4848
	public GameObject CellPrefab;

	// Token: 0x040012F1 RID: 4849
	public Inventory displayInventory;

	// Token: 0x040012F2 RID: 4850
	public RPOSInventoryCell[] _inventoryCells;
}
