using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo
{
    internal class Program
    {
        static string Decode(string str, int times)
        {
            var sb = new StringBuilder();
            for(var i = 0; i<str.Length; i++)
            {
                if ((str[i] > '0' && str[i] <= '9') && i + 1 < str.Length && str[i + 1] == '[')
                {
                    var newTimes = str[i] - '0';
                    var stack = new Stack<int>();
                    var newStart = -1;
                    var newEnd = -1;
                    for(var j = i + 1; j < str.Length; j++)
                    {
                        var popedIndex = -1;
                        if (str[j] == '[')
                        {
                            stack.Push(j);
                        }
                        else if (str[j] ==  ']')
                        {
                            popedIndex = stack.Pop();
                        }
                        if(popedIndex != -1 && stack.Count == 0)
                        {
                            newStart = popedIndex + 1;
                            newEnd = j - 1;
                            break;
                        }
                    }
                    if(newTimes > 0 && newStart != -1 && newEnd != -1)
                    {
                        var decoded = Decode(str.Substring(newStart, newEnd - newStart + 1), newTimes);
                        for(var k = 0; k < newTimes; k++)
                        {
                            sb.Append(decoded);
                        }
                        i += 2 + newEnd - newStart + 1;
                    }
                    else
                    {
                        sb.Append(str[i]);
                    }
                }
                else
                {
                    sb.Append(str[i]);
                }
            }
            return sb.ToString();
        }

        static void Main(string[] args)
        {
            var str = "a2[b]3[c]";
            var decoded = Decode(str, 1);
            Console.WriteLine(decoded);

            str = "a2[b3[c]]";
            decoded = Decode(str, 1);
            Console.WriteLine(decoded);

            str = "a[b3[c]]";
            decoded = Decode(str, 1);
            Console.WriteLine(decoded);
        }
    }
}
