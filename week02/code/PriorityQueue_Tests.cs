using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Test enqueueing and dequeuing based on priority
    // Expected Result: Items should be dequeued based on their priority (higher first)
    // Defect(s) Found: <Document any defects found during the test>
    public void TestPriorityQueue_Dequeue_HighestPriorityFirst()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Task A", 2);
        priorityQueue.Enqueue("Task B", 5);
        priorityQueue.Enqueue("Task C", 3);

        // Test dequeue order: highest priority first (Task B)
        Assert.AreEqual("Task B", priorityQueue.Dequeue());
        Assert.AreEqual("Task C", priorityQueue.Dequeue());
        Assert.AreEqual("Task A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test enqueueing items with the same priority and ensuring FIFO
    // Expected Result: Items with the same priority should follow FIFO order
    // Defect(s) Found: <Document any defects found during the test>
    public void TestPriorityQueue_FifoForSamePriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Task A", 3);
        priorityQueue.Enqueue("Task B", 3);
        priorityQueue.Enqueue("Task C", 1);

        // Test FIFO for same priority (Task A before Task B)
        Assert.AreEqual("Task A", priorityQueue.Dequeue());
        Assert.AreEqual("Task B", priorityQueue.Dequeue());
        Assert.AreEqual("Task C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test dequeue on an empty queue
    // Expected Result: An exception should be thrown when attempting to dequeue from an empty queue
    // Defect(s) Found: <Document any defects found during the test>
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        // Test for exception when queue is empty
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }
}