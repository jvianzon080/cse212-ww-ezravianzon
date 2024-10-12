using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var result = new List<string>();
        var wordSet = new HashSet<string>();

        foreach (var word in words)
        {
            string reversedWord = new string(new char[] { word[1], word[0] });

            // Check if the reversed word is already in the set
            if (wordSet.Contains(reversedWord))
            {
                result.Add($"{word} & {reversedWord}");
            }
            else
            {
                wordSet.Add(word); // Add the word to the set
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file. The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree. The degree information is in
    /// the 4th column of the file. There is no header row in the
    /// file.
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            string degree = fields[3].Trim(); // Get the degree from the 4th column

            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++; // Increment the count for this degree
            }
            else
            {
                degrees[degree] = 1; // Add the degree with an initial count of 1
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams. An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word. A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// IsAnagram("CAT", "ACT") would return true
    /// IsAnagram("DOG", "GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces. You should also ignore cases. For 
    /// example, 'Ab' and 'Ba' should be considered anagrams.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        var letterCounts = new Dictionary<char, int>();

        // Normalize words by removing spaces and converting to lowercase
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        // Count the frequency of each letter in word1
        foreach (char letter in word1)
        {
            if (letterCounts.ContainsKey(letter))
            {
                letterCounts[letter]++;
            }
            else
            {
                letterCounts[letter] = 1;
            }
        }

        // Decrease the count based on letters in word2
        foreach (char letter in word2)
        {
            if (!letterCounts.ContainsKey(letter))
            {
                return false; // Letter not found, so not an anagram
            }

            letterCounts[letter]--;
            if (letterCounts[letter] < 0)
            {
                return false; // More occurrences of this letter in word2
            }
        }

        // Check if all letter counts are zero
        return letterCounts.Values.All(count => count == 0);
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Check if featureCollection is not null and extract earthquake information
        if (featureCollection != null && featureCollection.Features != null)
        {
            var earthquakeSummaries = featureCollection.Features
                .Where(feature => feature.Properties != null && feature.Properties.Mag.HasValue)
                .Select(feature => $"{feature.Properties.Place} - Mag {feature.Properties.Mag.Value}")
                .ToArray();

            return earthquakeSummaries;
        }

        return Array.Empty<string>();
    }
}
