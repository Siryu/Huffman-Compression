using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman-Compression.Models
{
    public class HuffmanTree
    {
        public List<HuffmanNode> nodes { get; set; }
        public char[] decompressInput { get; set; }

        public HuffmanTree()
        {
            nodes = new List<HuffmanNode>();
        }

        public HuffmanTree(string input) : this()
        {
            CreateTree(input);
        }

        public void CreateTree(string input)
        {
            Dictionary<string, int> letterCount = new Dictionary<string, int>();

            foreach (char item in input)
            {
                if (letterCount.ContainsKey(item + ""))
                {
                    letterCount[item + ""] = letterCount[item + ""] += 1;
                }
                else
                {
                    letterCount.Add(item + "", 1);
                }
            }

            AddIndividualPiecesAsNodes(letterCount);
            CombineNodeRelations();
        }

        public string Compress(string input)
        {
            string compressed = "";

            foreach (char c in input)
            {
                compressed += string.Concat(Find(nodes.FirstOrDefault(), c).Skip(1));
            }
            return compressed;
        }

        public string Decompress(string input)
        {
            decompressInput = input.ToArray();
            string answer = "";
            while (decompressInput.Length > 0)
            {
                 answer += DecompressString(nodes[0]);
            }
            return answer;
        }

        private string DecompressString(HuffmanNode currentNode, string answer = "")
        {
            if (currentNode.Value.Length == 1)
            {
                return currentNode.Value;
            }
            if (decompressInput[0] == '0')
            {
                decompressInput = decompressInput.Skip(1).ToArray();
                answer += DecompressString(currentNode.LeftNode);
            }
            if (decompressInput.Length > 0 && decompressInput[0] == '1' && string.IsNullOrEmpty(answer))
            {
                decompressInput = decompressInput.Skip(1).ToArray();
                answer += DecompressString(currentNode.RightNode);
            }

            return answer;
        }

        private string Find(HuffmanNode head, char c)
        {
            if (head.Value.Equals(c + ""))
            {
                return head.BitValue + "";
            }

            string bitValue = "";
            string found = "";
            if(head.LeftNode != null)
            {
                found = Find(head.LeftNode, c);
            }
            if(head.RightNode != null && string.IsNullOrEmpty(found))
            {
                found = Find(head.RightNode, c);    
            }

            if (!string.IsNullOrEmpty(found))
                bitValue = head.BitValue + "" + found;
            return bitValue;
        }

        private void CombineNodeRelations()
        {
            if (nodes.Count > 1)
            {
                nodes = nodes.OrderBy(x => x.ItemCount).ToList();
                HuffmanNode h1 = nodes[0];
                h1.BitValue = 1;
                HuffmanNode h2 = nodes[1];
                h2.BitValue = 0;
                HuffmanNode newNode = new HuffmanNode(h1, h2);
                nodes.Remove(h1);
                nodes.Remove(h2);
                nodes.Add(newNode);
                CombineNodeRelations();
            }
        }

        private void AddIndividualPiecesAsNodes(Dictionary<string, int> letterCount)
        {
            foreach (var item in letterCount)
            {
                HuffmanNode hn = new HuffmanNode();
                hn.Value = item.Key;
                hn.ItemCount = item.Value;
                this.nodes.Add(hn);
            }
        }
    }
}
