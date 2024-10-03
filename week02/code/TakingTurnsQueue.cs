/// <summary>
/// This queue is circular. When people are added via AddPerson, they are added to the 
/// back of the queue (per FIFO rules). When GetNextPerson is called, the next person
/// in the queue is returned and then placed back at the back of the queue. Each person 
/// stays in the queue and is given turns. When a person is added to the queue, a turns 
/// parameter is provided to identify how many turns they will be given. If the turns is 0 or
/// less, they will stay in the queue forever. If a person is out of turns, they will 
/// not be added back into the queue.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add new people to the queue with a name and number of turns
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Get the next person in the queue and return them. The person should
    /// go to the back of the queue again unless the turns variable shows that they 
    /// have no more turns left. A turns value of 0 or less means the person has 
    /// an infinite number of turns. An exception is thrown if the queue is empty.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        var person = _people.Dequeue();

        // If turns <= 0, the person has infinite turns and will always be re-enqueued
        if (person.Turns <= 0)
        {
            _people.Enqueue(person);
        }
        // If they have more than 1 turn left, decrement their turn count and re-enqueue them
        else if (person.Turns > 1)
        {
            person.Turns--;
            _people.Enqueue(person);
        }
        // If they have exactly 1 turn left, they won't be re-enqueued

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}
