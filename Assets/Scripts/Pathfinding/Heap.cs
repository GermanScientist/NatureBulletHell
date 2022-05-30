﻿using UnityEngine;
using System.Collections;
using System;

public class Heap<Item> where Item : IHeapItem<Item> {

	private Item[] items;
	private int currentItemCount;

	public int Count { get { return currentItemCount; } }

	public Heap(int _maxHeapSize) {
		items = new Item[_maxHeapSize];
	}
	
	public void Add(Item _item) {
		_item.HeapIndex = currentItemCount;
		items[currentItemCount] = _item;
		SortUp(_item);
		currentItemCount++;
	}

	public Item RemoveFirst() {
		Item firstItem = items[0];
		currentItemCount--;
		items[0] = items[currentItemCount];
		items[0].HeapIndex = 0;
		SortDown(items[0]);
		return firstItem;
	}

	public void UpdateItem(Item _item) {
		SortUp(_item);
	}

	public bool Contains(Item _item) {
		return Equals(items[_item.HeapIndex], _item);
	}

	private void SortDown(Item _item) {
		while (true) {
			int childIndexLeft = _item.HeapIndex * 2 + 1;
			int childIndexRight = _item.HeapIndex * 2 + 2;
			int swapIndex = 0;

			if (childIndexLeft < currentItemCount) {
				swapIndex = childIndexLeft;

				if (childIndexRight < currentItemCount) {
					if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
						swapIndex = childIndexRight;
				}

				if (_item.CompareTo(items[swapIndex]) < 0) 
					Swap(_item, items[swapIndex]);
				else return;
			}
			else return;
		}
	}
	
	private void SortUp(Item _item) {
		int parentIndex = (_item.HeapIndex-1)/2;
		
		while (true) {
			Item parentItem = items[parentIndex];
			if (_item.CompareTo(parentItem) > 0)
				Swap(_item, parentItem);
			else break;

			parentIndex = (_item.HeapIndex-1)/2;
		}
	}
	
	private void Swap(Item _itemA, Item _itemB) {
		items[_itemA.HeapIndex] = _itemB;
		items[_itemB.HeapIndex] = _itemA;
		int itemAIndex = _itemA.HeapIndex;
		_itemA.HeapIndex = _itemB.HeapIndex;
		_itemB.HeapIndex = itemAIndex;
	}
}

public interface IHeapItem<T> : IComparable<T> { 
	int HeapIndex { get; set; }
}
