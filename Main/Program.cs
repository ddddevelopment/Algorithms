using System.Collections;
using System.Text;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
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
            HashSet<char> vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
            int countVowels = 0;

            for (int i = 0; i < k; i++)
            {
                if (vowels.TryGetValue(s[i], out _))
                {
                    countVowels++;
                }
            }

            int maxCountVowels = countVowels;

            for (int i = 1, j = k; j < s.Length; i++, j++)
            {
                if (vowels.TryGetValue(s[i - 1], out _))
                {
                    countVowels--;
                }
                if (vowels.TryGetValue(s[j], out _))
                {
                    countVowels++;
                }

                if (countVowels > maxCountVowels)
                {
                    maxCountVowels = countVowels;
                }

                if (maxCountVowels == k)
                {
                    return maxCountVowels;
                }
            }

            return maxCountVowels;
        }

        public int LargestAltitude(int[] gain)
        {
            int[] heights = new int[gain.Length + 1];
            heights[0] = 0;
            int max = 0;

            for (int i = 1; i < heights.Length; i++)
            {
                heights[i] = heights[i - 1] + gain[i - 1];
                if (heights[i] > max)
                {
                    max = heights[i];
                }
            }

            return max;
        }

        public int PivotIndex(int[] nums)
        {
            // int n = nums.Length;

            // int[] leftSum = new int[n];
            // leftSum[0] = nums[0];

            // int[] rightSum = new int[n];    
            // rightSum[n - 1] = nums[n - 1];    

            // for (int i = 1, j = n - 2; i < n && j >= 0; i++, j--) {
            //     leftSum[i] = leftSum[i - 1] + nums[i];
            //     rightSum[j] = rightSum[j + 1] + nums[j];
            // }

            // for (int i = 0; i < n; i++) {
            //     if (leftSum[i] == rightSum[i]) {
            //         return i;
            //     }
            // }

            // return -1;

            int totalSum = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                totalSum += nums[i];
            }

            int leftSum = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (leftSum == totalSum - leftSum - nums[i])
                {
                    return i;
                }
                leftSum += nums[i];
            }

            return -1;
        }

        public IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
        {
            HashSet<int> nums1Set = new HashSet<int>();

            foreach (var num in nums1)
            {
                nums1Set.Add(num);
            }

            HashSet<int> nums2Set = new HashSet<int>();

            foreach (var num in nums2)
            {
                nums2Set.Add(num);
            }

            nums1Set.ExceptWith(nums2);
            nums2Set.ExceptWith(nums1);

            return new List<int>[2] { nums1Set.ToList(), nums2Set.ToList() };
        }

        public bool UniqueOccurrences(int[] arr)
        {
            Dictionary<int, int> repeatItems = new Dictionary<int, int>();

            for (int i = 0; i < arr.Length; i++)
            {
                if (repeatItems.ContainsKey(arr[i]) == false)
                {
                    repeatItems.Add(arr[i], 1);
                }
                else
                {
                    repeatItems[arr[i]]++;
                }
            }

            HashSet<int> repeats = new HashSet<int>();

            foreach (var item in repeatItems)
            {
                repeats.Add(item.Value);
            }

            return repeats.Count == repeatItems.Count();
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public ListNode ReverseList(ListNode head)
        {
            if (head == null)
            {
                return null;
            }

            ListNode previous = null;
            ListNode current = head;
            ListNode next = head.next;

            while (current != null)
            {
                current.next = previous;
                previous = current;
                current = next;

                if (next != null)
                {
                    next = next.next;
                }
            }

            return previous;
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        public bool LeafSimilar(TreeNode root1, TreeNode root2)
        {
            if (root1 == null || root2 == null)
            {
                return false;
            }

            List<int> leaf1 = new List<int>();
            List<int> leaf2 = new List<int>();
            FindLeave(root1, leaf1);
            FindLeave(root2, leaf2);

            return leaf1.SequenceEqual(leaf2);
        }

        private void FindLeave(TreeNode root, List<int> leafValueSequence)
        {
            if (root.left == null && root.right == null)
            {
                leafValueSequence.Add(root.val);
            }
            else
            {
                if (root.left != null)
                {
                    FindLeave(root.left, leafValueSequence);
                }

                if (root.right != null)
                {
                    FindLeave(root.right, leafValueSequence);
                }
            }
        }

        public int GoodNodes(TreeNode root)
        {
            int count = 1;
            CheckGood(root.val, root.left, ref count);
            CheckGood(root.val, root.right, ref count);

            return count;
        }

        private void CheckGood(int maxVal, TreeNode root, ref int count)
        {
            if (root == null)
            {
                return;
            }

            if (root.val >= maxVal)
            {
                maxVal = root.val;
                count++;
            }

            CheckGood(maxVal, root.left, ref count);
            CheckGood(maxVal, root.right, ref count);
        }

        public TreeNode SearchBST(TreeNode root, int val)
        {
            if (root == null || root.val == val)
            {
                return root;
            }

            else
            {
                if (root.val > val)
                {
                    return SearchBST(root.left, val);
                }
                else
                {
                    return SearchBST(root.right, val);
                }
            }
        }

        public TreeNode DeleteNode(TreeNode root, int key)
        {
            TreeNode deletedRoot = DeleteNodeHelper(ref root, key);

            if (deletedRoot == null)
            {
                return root;
            }

            return root;
        }

        private TreeNode DeleteNodeHelper(ref TreeNode root, int key)
        {
            if (root == null)
            {
                return null;
            }

            if (key == root.val)
            {
                if (root.left == null && root.right == null)
                {
                    root = null;
                }

                else
                {
                    if (root.left == null)
                    {
                        root = root.right;
                    }
                    else if (root.right == null)
                    {
                        root = root.left;
                    }
                    else
                    {
                        TreeNode left = root.left;
                        root = root.right;
                        InsertNode(ref root, left);
                    }
                }

                return root;
            }

            else
            {
                if (key < root.val)
                {
                    return DeleteNodeHelper(ref root.left, key);
                }
                else
                {
                    return DeleteNodeHelper(ref root.right, key);
                }
            }
        }

        private void InsertNode(ref TreeNode root, TreeNode insert)
        {
            if (root == null)
            {
                root = insert;
                return;
            }

            else
            {
                if (insert.val < root.val)
                {
                    InsertNode(ref root.left, insert);
                }
                else
                {
                    InsertNode(ref root.right, insert);
                }
            }
        }

        public int Tribonacci(int n)
        {
            if (n == 1)
            {
                return 1;
            }

            int nDiv3 = 0;
            int nDiv2 = 0;
            int nDiv1 = 1;

            int currentNum = 0;
            for (int i = 2; i <= n; i++)
            {
                currentNum = nDiv3 + nDiv2 + nDiv1;
                nDiv3 = nDiv2;
                nDiv2 = nDiv1;
                nDiv1 = currentNum;
            }

            return currentNum;
        }

        public int MinCostClimbingStairs(int[] cost)
        {
            return Math.Min(MinCostHelper(cost, cost.Length - 1), MinCostHelper(cost, cost.Length - 2));
        }

        private int MinCostHelper(int[] cost, int index)
        {
            if (index == 0)
            {
                return cost[0];
            }
            if (index == 1)
            {
                return cost[1];
            }

            int minCost = cost[index] + Math.Min(MinCostHelper(cost, index - 1), MinCostHelper(cost, index - 2));

            return minCost;
        }

        public int SingleNumber(int[] nums)
        {
            int result = 0;

            for (int i = 0; i < nums.Length; i++) {
                result ^= nums[i];
            }

            return result;
        }
    }
}
