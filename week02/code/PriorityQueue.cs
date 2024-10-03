public class PriorityQueue
{
    private List<PriorityItem> _queue = new();

    /// <summary>
    /// Add a new value to the queue with an associated priority.
    /// The node is always added to the back of the queue regardless of the priority.
    /// </summary>
    /// <param name="value">The value</param>
    /// <param name="priority">The priority</param>
    public void Enqueue(string value, int priority)
    {
        var newNode = new PriorityItem(value, priority);
        _queue.Add(newNode); // Always add to the back of the queue
    }

    /// <summary>
    /// Dequeue the item with the highest priority. If multiple items have the highest priority, 
    /// the first one (FIFO) is dequeued. An exception is thrown if the queue is empty.
    /// </summary>
    /// <returns>The value of the dequeued item</returns>
    public string Dequeue()
    {
        if (_queue.Count == 0) // Verify the queue is not empty
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        // Find the index of the highest priority item
        int highPriorityIndex = 0;
        for (int index = 1; index < _queue.Count; index++)
        {
            // Compare the current item with the current highest priority item
            if (_queue[index].Priority > _queue[highPriorityIndex].Priority)
            {
                highPriorityIndex = index;
            }
            else if (_queue[index].Priority == _queue[highPriorityIndex].Priority)
            {
                // If priority is the same, we keep the earlier item (FIFO), so no change
                // to the highPriorityIndex.
            }
        }

        // Remove and return the item with the highest priority
        var highestPriorityItem = _queue[highPriorityIndex];
        _queue.RemoveAt(highPriorityIndex);
        return highestPriorityItem.Value;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

internal class PriorityItem
{
    internal string Value { get; set; }
    internal int Priority { get; set; }

    internal PriorityItem(string value, int priority)
    {
        Value = value;
        Priority = priority;
    }

    public override string ToString()
    {
        return $"{Value} (Pri:{Priority})";
    }
}