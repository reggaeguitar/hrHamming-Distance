﻿namespace hrHamming_Distance
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main()
        { // a = false, b = true
            var len = Int32.Parse(Console.ReadLine());
            var strArr = Console.ReadLine().ToCharArray();
            var bitArr = new BitArray(len);
            var scratchArr = new BitArray(len);
            var midArr = new BitArray(len);
            for (int index = 0; index < strArr.Length; index++)
            {
                var ch = strArr[index];
                if (ch == 'b')
                {
                    bitArr[index] = true;
                }
            }
            var numQueries = Int32.Parse(Console.ReadLine());
            while (numQueries-- > 0)
            {
                var args = Console.ReadLine().Split(' ').ToArray();
                switch (args[0][0])
                {
                    case 'C':
                        var l = Int32.Parse(args[1]) - 1;
                        var r = Int32.Parse(args[2]) - 1;
                        var value = args[3][0] == 'b';
                        for (int i = l; i <= r; ++i)
                        {
                            bitArr[i] = value;
                        }
                        break;
                    case 'S':
                        var l1 = Int32.Parse(args[1]) - 1;
                        var r1 = Int32.Parse(args[2]) - 1;
                        var sLen1 = (r1 - l1) + 1;
                        var l2 = Int32.Parse(args[3]) - 1;
                        var r2 = Int32.Parse(args[4]) - 1;
                        var sLen2 = (r2 - l2) + 1;
                        var dif = l2 - r1 - 1;
                        // save first substring in scratchArr starting at index 0
                        for (int i = 0; i < sLen1; i++)
                        {
                            scratchArr[i] = bitArr[l1 + i];
                        }
                        // save the middle part in midArr starting at index 0
                        for (int i = r1 + 1; i < l2; i++)
                        {
                            midArr[i - r1 - 1] = bitArr[i];
                        }
                        // overwrite first substring with second substring
                        for (int i = 0; i < sLen2; i++)
                        {
                            bitArr[l1 + i] = bitArr[l2 + i];
                        }
                        // overwrite the mid substring after the first substring
                        for (int i = l1 + sLen2; i < dif + l1 + sLen2; i++)
                        {
                            bitArr[i] = midArr[i - l1 - sLen2];
                        }
                        // overwrite second substring with first substring saved in scratchArr starting at index 0
                        for (int i = 0; i < sLen1; i++)
                        {
                            bitArr[l1 + sLen2 + i + dif] = scratchArr[i];
                        }
                        break;
                    case 'R':
                        var lr = Int32.Parse(args[1]) - 1;
                        var rr = Int32.Parse(args[2]) - 1;
                        var stack = new Stack<bool>((rr - lr) + 1);
                        var lrCopy = lr;
                        for (; lrCopy <= rr; ++lrCopy)
                        {
                            stack.Push(bitArr[lrCopy]);
                        }
                        for (; lr <= rr; ++lr)
                        {
                            bitArr[lr] = stack.Pop();
                        }
                        break;
                    case 'W':
                        var lw = Int32.Parse(args[1]) - 1;
                        var rw = Int32.Parse(args[2]) - 1;
                        for (; lw <= rw; ++lw)
                        {
                            Console.Write(bitArr[lw] ? 'b' : 'a');
                        }
                        Console.WriteLine();
                        break;
                    case 'H':
                        int dist = 0;
                        var oneStart = Int32.Parse(args[1]) - 1;
                        var twoStart = Int32.Parse(args[2]) - 1;
                        if (oneStart == twoStart)
                        {
                            Console.WriteLine(0);
                            break;
                        }
                        var lenH = Int32.Parse(args[3]);
                        for (int i = 0; i < lenH; i++)
                        {
                            if (bitArr[oneStart + i] != bitArr[twoStart + i])
                            {
                                ++dist;
                            }
                        }
                        Console.WriteLine(dist);
                        break;
                }
            }
        }
    }
}
