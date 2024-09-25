public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Step 1: Create a new array to hold the multiples
        double[] multiples = new double[length];
        
        // Step 2: Use a loop to fill the array with multiples of the number
        for (int i = 0; i < length; i++)
        {
            // Step 3: Each item in the array is the number multiplied by (i + 1)
            multiples[i] = number * (i + 1);
        }
        // Step 4: Return the array with all the multiples
        return multiples; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
{
    // Step 1: Adjust the amount to prevent rotating more than needed
    // If 'amount' is larger than the list size, using % makes sure we only rotate the necessary times
    amount = amount % data.Count;

    // Step 2: Get the last 'amount' elements from the list (the ones that will be moved to the front)
    List<int> toMoveToFront = data.GetRange(data.Count - amount, amount);

    // Step 3: Remove the elements that will be moved (from the end of the list)
    data.RemoveRange(data.Count - amount, amount);

    // Step 4: Insert the removed elements at the front of the list
    data.InsertRange(0, toMoveToFront);
}
    
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
    
}