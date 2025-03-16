using System.Text;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            int result = solution.MaxOperations([3, 1, 3, 4, 3], 6);
        }
    }

    public class Solution
    {
        public string MergeAlternately(string word1, string word2)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < Math.Max(word1.Length, word2.Length); i++)
            {
                if (i >= word2.Length)
                {
                    builder.Append(word1[i]);
                    continue;
                }
                if (i >= word1.Length)
                {
                    builder.Append(word2[i]);
                    continue;
                }

                builder.Append(word1[i]);
                builder.Append(word2[i]);
            }

            return builder.ToString();
        }

        public string GcdOfStrings(string str1, string str2)
        {
            if (str1 + str2 != str2 + str1)
            {
                return "";
            }

            return str1.Substring(0, Gcd(str1.Length, str2.Length));
        }

        private int Gcd(int a, int b)
        {
            while (b > 0)
            {
                a %= b;
                (a, b) = (b, a);
            }

            return a;
        }


        public IList<bool> KidsWithCandies(int[] candies, int extraCandies)
        {
            List<bool> result = new List<bool>();
            int max = candies[0];

            for (int i = 1; i < candies.Length; i++)
            {
                if (candies[i] > max)
                {
                    max = candies[i];
                }
            }

            for (int i = 0; i < candies.Length; i++)
            {
                if (candies[i] + extraCandies >= max)
                {
                    result.Add(true);
                }
                else
                {
                    result.Add(false);
                }
            }

            return result;
        }

        public bool CanPlaceFlowers(int[] flowerbed, int n)
        {
            for (int i = 0; i < flowerbed.Length; i++)
            {
                if (n == 0)
                {
                    return true;
                }

                if (flowerbed[i] == 0 && (i == 0 || flowerbed[i - 1] == 0) && (i == flowerbed.Length - 1 || flowerbed[i + 1] == 0))
                {
                    flowerbed[i] = 1;
                    n--;
                }
            }
            return n <= 0 ? true : false;
        }

        public string ReverseVowels(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || s.Length == 1)
            {
                return s;
            }

            HashSet<char> vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
            StringBuilder builder = new StringBuilder();

            builder.Length = s.Length;

            int start = 0;
            int end = s.Length - 1;

            while (start <= end)
            {
                if (vowels.TryGetValue(s[start], out _) && vowels.TryGetValue(s[end], out _))
                {
                    builder[start] = s[end];
                    builder[end] = s[start];
                    start++;
                    end--;
                    continue;
                }

                if (vowels.TryGetValue(s[start], out _) == false)
                {
                    builder[start] = s[start];
                    start++;
                }

                if (vowels.TryGetValue(s[end], out _) == false)
                {
                    builder[end] = s[end];
                    end--;
                }
            }

            return builder.ToString();
        }

        public string ReverseWords(string s)
        {
            string[] words = s.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(words);
            return string.Join(" ", words);
        }

        public int[] ProductExceptSelf(int[] nums)
        {
            int[] prefixes = new int[nums.Length];
            int[] suffixes = new int[nums.Length];
            prefixes[0] = 1;
            suffixes[nums.Length - 1] = 1;

            for (int i = 1; i < nums.Length; i++)
            {
                prefixes[i] = prefixes[i - 1] * nums[i - 1];
            }

            for (int i = nums.Length - 2; i >= 0; i--)
            {
                suffixes[i] = suffixes[i + 1] * nums[i + 1];
            }

            int[] answer = new int[nums.Length];

            for (int i = 0; i < answer.Length; i++)
            {
                answer[i] = prefixes[i] * suffixes[i];
            }

            return answer;
        }

        public void MoveZeroes(int[] nums)
        {
            for (int i = 0, j = 1; j < nums.Length; i++, j++)
            {
                if (nums[i] == 0)
                {
                    if (nums[j] != 0)
                    {
                        (nums[i], nums[j]) = (nums[j], nums[i]);
                    }
                    else if (nums[j] == 0)
                    {
                        i--;
                    }
                }
            }
        }

        public bool IsSubsequence(string s, string t)
        {
            int sIndex = 0;

            for (int i = 0; i < t.Length; i++)
            {
                if (sIndex >= s.Length)
                {
                    return true;
                }

                if (t[i] == s[sIndex])
                {
                    sIndex++;
                }
            }

            return sIndex >= s.Length;
        }

        public int MaxArea(int[] height)
        {
            int left = 0;
            int right = height.Length - 1;
            int maxVolume = 0;

            while (left < right)
            {
                int currentVolume = Min(height[left], height[right]) * (right - left);
                if (currentVolume > maxVolume)
                {
                    maxVolume = currentVolume;
                }

                if (height[left] == Min(height[left], height[right]))
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }

            return maxVolume;
        }

        private int Min(int a, int b)
        {
            if (a < b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        public int MaxOperations(int[] nums, int k)
        {
            Array.Sort(nums);

            int operations = 0;
            int left = 0;
            int right = nums.Length - 1;

            while (left < right)
            {
                int sum = nums[left] + nums[right];

                if (sum == k)
                {
                    operations++;
                    left++;
                    right--;
                }
                else if (sum > k)
                {
                    right--;
                }
                else if (sum < k)
                {
                    left++;
                }
            }

            return operations;

            //быстрее
            // Dictionary<int, int> processed = new Dictionary<int, int>();
            // int operations = 0;

            // for (int i = 0; i < nums.Length; i++) {
            //     int toFind = k - nums[i];

            //     if (processed.ContainsKey(toFind)) {
            //         operations++;

            //         if (processed[toFind] == 1) {
            //             processed.Remove(toFind);
            //         }
            //         else {
            //             processed[toFind]--;
            //         }
            //     }
            //     else {
            //         if (processed.ContainsKey(nums[i])) {
            //             processed[nums[i]]++;
            //         }
            //         else {
            //             processed.Add(nums[i], 1);
            //         }
            //     }
            // }

            // return operations;
        }

        public double FindMaxAverage(int[] nums, int k)
        {
            int maxSum = 0;

            for (int i = 0; i < k; i++)
            {
                maxSum += nums[i];
            }

            int previousSum = maxSum;
            int leftMaxSum = 0;
            int rightMaxSum = k - 1;

            for (int i = leftMaxSum + 1, j = rightMaxSum + 1; j < nums.Length; i++, j++)
            {
                int currentSum = previousSum - nums[i - 1] + nums[j];
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    leftMaxSum = i;
                    rightMaxSum = j;
                }
                previousSum = currentSum;
            }

            return (double)maxSum / k;
        }

        public int MaxVowels(string s, int k)
        {
            HashSet<char> vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'};
            int countVowels = 0;

            for (int i = 0; i < k; i++) {
                if (vowels.TryGetValue(s[i], out _)) {
                    countVowels++;
                }
            }

            int maxCountVowels = countVowels;

            for (int i = 1, j = k; j < s.Length; i++, j++) {
                if (vowels.TryGetValue(s[i - 1], out _)) {
                    countVowels--;
                }
                if (vowels.TryGetValue(s[j], out _)) {
                    countVowels++;
                }

                if (countVowels > maxCountVowels) {
                    maxCountVowels = countVowels;
                }
                
                if (maxCountVowels == k) {
                    return maxCountVowels;
                }
            }

            return maxCountVowels;
        }
    }
}