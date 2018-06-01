using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T>
{
    SortedList<Pair<int>, T> _list;
    int count;

    public PriorityQueue()
    {
        _list = new SortedList<Pair<int>, T>(new PairComparer<int>());
    }
    public void Enqueue(T item, int priority)
    {
        _list.Add(new Pair<int>(priority, count), item);
        count++;
    }

    public T Dequeue()
    {
        T item = _list[_list.Keys[0]];
        _list.RemoveAt(0);
        return item;
    }
}