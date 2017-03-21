using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman-Compression.Models
{
    public class HuffmanNode
    {
        public string Value { get; set; }
        public int ItemCount { get; set; }
        public int BitValue { get; set; }
        public HuffmanNode LeftNode { get; set; }
        public HuffmanNode RightNode { get; set; }
    
        public HuffmanNode() { }

        public HuffmanNode (HuffmanNode h1, HuffmanNode h2)
        {
            this.Value = h2.Value + h1.Value;
            this.ItemCount = h1.ItemCount + h2.ItemCount;
            this.LeftNode = h2;
            this.RightNode = h1;
        }

        public override bool Equals(object obj)
        {
            HuffmanNode NObject = (HuffmanNode)obj;
            return this.Value.Equals(NObject.Value);
        }
    }
}
