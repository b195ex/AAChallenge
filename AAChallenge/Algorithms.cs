using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAChallenge
{
    class Algorithms
    {
        public static void SortArray(AAItem item, bool desc)
        {
            if (desc)
                item.words = item.words.OrderByDescending(w => w).ToArray();
            else
                item.words = item.words.OrderBy(w => w).ToArray();
        }
        public static void ShiftVowels(AAItem item)
        {
            for (int j = 0; j < item.words.Length; j++)
            {
                for (int i = 0; i < item.words[j].Length; i++)
                {
                    if (isVowel(item.words[j][i]))
                    {
                        item.words[j] = ShiftChar(item.words[j], i);
                        i++;
                    }
                }
            } 
        }
        public static string AsciiConcat(AAItem item)
        {
            StringBuilder sb = new StringBuilder(item.words[0]);
            sb.Append((int)item.words[item.words.Length - 1][0]);
            for (int i = 1; i < item.words.Length; i++)
            {
                sb.Append(item.words[i]);
                sb.Append((int)item.words[i - 1][0]);
            }
            return sb.ToString();
        }
        public static string AsterixConcat(AAItem item)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var word in item.words)
            {
                sb.Append(word).Append('*');
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
        public static void crazy(AAItem item)
        {
            List<string> ItemsList = item.words.ToList();
            List<string> dict = new List<string>(new string[]{ "drool", "cats", "clean", "code", "dogs", "materials", "needed", "this", "is", "hard", "what", "are", "you", "smoking", "shot", "gun", "down", "river", "super", "man", "rule", "acklen", "developers", "are", "amazing" });
            List<string> rep = new List<string>();
            string temp;
            bool flag;
            for (int i = 0; i < ItemsList.Count; i++)
            {
                flag = true;
                temp = string.Copy(ItemsList[i]);
                while (!string.IsNullOrWhiteSpace(temp))
                {
                    foreach (var def in dict)
                    {
                        if (temp.ToLower().StartsWith(def))
                        {
                            flag = false;
                            rep.Add(temp.Substring(0, def.Length));
                            temp=temp.Remove(0, def.Length);
                            break;
                        }
                    }
                    if (flag) break;
                }
                if (rep.Count > 0)
                {
                    ItemsList.RemoveAt(i);
                    ItemsList.InsertRange(i, rep);
                    rep.Clear();
                }
            }
            item.words = ItemsList.ToArray();
        }
        public static void AltCase(AAItem item)
        {
            bool upper = char.IsUpper(item.words[0][0]);
            for (int i = 0; i < item.words.Length; i++)
            {
                char[] temp = item.words[i].ToCharArray();
                for (int j = 0; j < item.words[i].Length; j++)
                {
                    if (!isVowel(temp[j]))
                    {
                        if (upper)
                            temp[j] = char.ToUpper(temp[j]);
                        else
                            temp[j] = char.ToLower(temp[j]);
                        upper = !upper;
                    }
                }
                item.words[i] = new string(temp);
            }
        }
        public static void Fib(AAItem item)
        {
            long a, b = 1, c;
            a = c = 0;
            while (c <= item.startingFibonacciNumber)
            {
                c = a + b;
                a = b;
                b = c;
                //a es el inicial, b es el siguiente, c es el siguiente
            }
            List<char> temp;
            string num;
            for (int i = 0; i < item.words.Length; i++)
            {
                temp = item.words[i].ToList();
                for (int j = 0; j < temp.Count; j++)
                {
                    if (isVowel(temp[j]))
                    {
                        temp.RemoveAt(j);
                        num = a.ToString();
                        temp.InsertRange(j, num);
                        c = a + b;
                        a = b;
                        b = c;
                        j += num.Length - 1;
                    }
                }
                item.words[i] = new string(temp.ToArray());
            }
        }
        static bool isVowel(char letra)
        {
            return new[] { 'a', 'e', 'i', 'o', 'u', 'y' }.Contains(char.ToLower(letra));
        }
        static string ShiftChar(string word, int pos)
        {
            int count = word.Length;
            var array = word.ToList();
            char temp = array[pos];
            array.RemoveAt(pos);
            array.Insert((pos + 1)%count, temp);
            return new string(array.ToArray());
        }
    }
}
